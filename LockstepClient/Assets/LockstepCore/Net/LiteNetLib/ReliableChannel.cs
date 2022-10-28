﻿using System;

namespace LiteNetLib
{
    internal sealed class ReliableChannel : BaseChannel
    {
        /// <summary>
        /// 等待发送中的包，这里应该是保证顺序的一种方式
        /// </summary>
        private struct PendingPacket
        {
            private NetPacket _packet;
            private long _timeStamp;
            private bool _isSent;

            public override string ToString()
            {
                return _packet == null ? "Empty" : _packet.Sequence.ToString();
            }

            public void Init(NetPacket packet)
            {
                _packet = packet;
                _isSent = false;
            }

            /// <summary>
            /// 判断包是否已经发送并在规定时间内收到了确认包
            /// </summary>
            /// <param name="currentTime"></param>
            /// <param name="peer"></param>
            /// <returns></returns>
            //Returns true if there is a pending packet inside
            public bool TrySend(long currentTime, NetPeer peer)
            {
                if (_packet == null)
                    return false;

                if (_isSent) //check send time
                {
                    double resendDelay = peer.ResendDelay * TimeSpan.TicksPerMillisecond;
                    double packetHoldTime = currentTime - _timeStamp;
                    //这里就是做时间比较，和TCP下的计时器是一样的概念，为了辨别超时的情况
                    if (packetHoldTime < resendDelay)
                        return true;
                    NetDebug.Write("[RC]Resend: {0} > {1}", (int)packetHoldTime, resendDelay);
                }
                _timeStamp = currentTime;
                _isSent = true;
                //发送包
                peer.SendUserData(_packet);
                return true;
            }

            public bool Clear(NetPeer peer)
            {
                if (_packet != null)
                {
                    peer.RecycleAndDeliver(_packet);
                    _packet = null;
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// 确认包
        /// </summary>
        private readonly NetPacket _outgoingAcks;            //for send acks
        /// <summary>
        /// 无被确认的复制包，应该是为了超时的再次发送
        /// </summary>
        private readonly PendingPacket[] _pendingPackets;    //for unacked packets and duplicates

        /// <summary>
        /// 按顺序的接收包
        /// </summary>
        private readonly NetPacket[] _receivedPackets;       //for order

        /// <summary>
        /// ??提前接收
        /// </summary>
        private readonly bool[] _earlyReceived;              //for unordered

        private int _localSeqence;
        private int _remoteSequence;
        private int _localWindowStart;
        private int _remoteWindowStart;

        private bool _mustSendAcks;

        private readonly DeliveryMethod _deliveryMethod;
        private readonly bool _ordered;
        private readonly int _windowSize;
        private const int BitsInByte = 8;
        private readonly byte _id;

        public ReliableChannel(NetPeer peer, bool ordered, byte id) : base(peer)
        {
            _id = id;
            _windowSize = NetConstants.DefaultWindowSize;
            _ordered = ordered;
            _pendingPackets = new PendingPacket[_windowSize];
            for (int i = 0; i < _pendingPackets.Length; i++)
                _pendingPackets[i] = new PendingPacket();

            if (_ordered)
            {
                _deliveryMethod = DeliveryMethod.ReliableOrdered;
                _receivedPackets = new NetPacket[_windowSize];
            }
            else
            {
                _deliveryMethod = DeliveryMethod.ReliableUnordered;
                _earlyReceived = new bool[_windowSize];
            }

            _localWindowStart = 0;
            _localSeqence = 0;
            _remoteSequence = 0;
            _remoteWindowStart = 0;
            _outgoingAcks = new NetPacket(PacketProperty.Ack, (_windowSize - 1) / BitsInByte + 2) { ChannelId = id };
        }

        /// <summary>
        /// 接收方发送确认包，回应被接收的数据包
        /// </summary>
        /// <param name="packet"></param>
        //ProcessAck in packet
        private void ProcessAck(NetPacket packet)
        {
            if (packet.Size != _outgoingAcks.Size)
            {
                NetDebug.Write("[PA]Invalid acks packet size");
                return;
            }

            //接收的起始位置
            ushort ackWindowStart = packet.Sequence;
            int windowRel = NetUtils.RelativeSequenceNumber(_localWindowStart, ackWindowStart);
            if (ackWindowStart >= NetConstants.MaxSequence || windowRel < 0)
            {
                NetDebug.Write("[PA]Bad window start");
                return;
            }

            //check relevance
            if (windowRel >= _windowSize)
            {
                NetDebug.Write("[PA]Old acks");
                return;
            }

            byte[] acksData = packet.RawData;
            //多线程下加锁
            lock (_pendingPackets)
            {
                //按顺序回应
                for (int pendingSeq = _localWindowStart;
                    pendingSeq != _localSeqence;
                    pendingSeq = (pendingSeq + 1) % NetConstants.MaxSequence)
                {
                    int rel = NetUtils.RelativeSequenceNumber(pendingSeq, ackWindowStart);
                    if (rel >= _windowSize)
                    {
                        NetDebug.Write("[PA]REL: " + rel);
                        break;
                    }

                    int pendingIdx = pendingSeq % _windowSize;
                    int currentByte = NetConstants.ChanneledHeaderSize + pendingIdx / BitsInByte;
                    int currentBit = pendingIdx % BitsInByte;
                    if ((acksData[currentByte] & (1 << currentBit)) == 0)
                    {
                        if (Peer.NetManager.EnableStatistics)
                        {
                            //统计丢包？？
                            Peer.Statistics.IncrementPacketLoss();
                            Peer.NetManager.Statistics.IncrementPacketLoss();
                        }

                        //Skip false ack
                        NetDebug.Write("[PA]False ack: {0}", pendingSeq);
                        continue;
                    }

                    if (pendingSeq == _localWindowStart)
                    {
                        //滑动窗口 Move window
                        _localWindowStart = (_localWindowStart + 1) % NetConstants.MaxSequence;
                    }

                    //clear packet
                    if (_pendingPackets[pendingIdx].Clear(Peer))
                        NetDebug.Write("[PA]Removing reliableInOrder ack: {0} - true", pendingSeq);
                }
            }
        }

        protected override bool SendNextPackets()
        {
            if (_mustSendAcks)
            {
                _mustSendAcks = false;
                NetDebug.Write("[RR]SendAcks");
                lock (_outgoingAcks)
                    Peer.SendUserData(_outgoingAcks);
            }

            long currentTime = DateTime.UtcNow.Ticks;
            bool hasPendingPackets = false;

            //多线程下得加锁
            lock (_pendingPackets)
            {
                //get packets from queue
                while (!OutgoingQueue.IsEmpty)
                {
                    //已经确认的可靠包数量
                    int relate = NetUtils.RelativeSequenceNumber(_localSeqence, _localWindowStart);
                    //大于等于滑动窗口，那么窗口能已经确认完成
                    if (relate >= _windowSize)
                        break;
                    //无法取得窗口首项
                    if (!OutgoingQueue.TryDequeue(out var netPacket))
                        break;

                    netPacket.Sequence = (ushort)_localSeqence;
                    netPacket.ChannelId = _id;
                    //把这一项包放进待发送的数组内
                    _pendingPackets[_localSeqence % _windowSize].Init(netPacket);
                    //这里自增一下
                    _localSeqence = (_localSeqence + 1) % NetConstants.MaxSequence;
                }

                //发送本地预备好的包 send
                for (int pendingSeq = _localWindowStart; pendingSeq != _localSeqence; pendingSeq = (pendingSeq + 1) % NetConstants.MaxSequence)
                {
                    //发送包、判断发送的包是否被回应接收确认
                    if (_pendingPackets[pendingSeq % _windowSize].TrySend(currentTime, Peer))
                        hasPendingPackets = true;
                }
            }

            return hasPendingPackets || _mustSendAcks || !OutgoingQueue.IsEmpty;
        }

        //Process incoming packet
        public override bool ProcessPacket(NetPacket packet)
        {
            if (packet.Property == PacketProperty.Ack)
            {
                ProcessAck(packet);
                return false;
            }
            int seq = packet.Sequence;
            if (seq >= NetConstants.MaxSequence)
            {
                NetDebug.Write("[RR]Bad sequence");
                return false;
            }

            int relate = NetUtils.RelativeSequenceNumber(seq, _remoteWindowStart);
            int relateSeq = NetUtils.RelativeSequenceNumber(seq, _remoteSequence);

            if (relateSeq > _windowSize)
            {
                NetDebug.Write("[RR]Bad sequence");
                return false;
            }

            //Drop bad packets
            if (relate < 0)
            {
                //Too old packet doesn't ack
                NetDebug.Write("[RR]ReliableInOrder too old");
                return false;
            }
            if (relate >= _windowSize * 2)
            {
                //Some very new packet
                NetDebug.Write("[RR]ReliableInOrder too new");
                return false;
            }


            //滑动窗口确认包
            int ackIdx;
            int ackByte;
            int ackBit;
            lock (_outgoingAcks)
            {
                if (relate >= _windowSize)
                {
                    //调整滑动窗口的位置
                    int newWindowStart = (_remoteWindowStart + relate - _windowSize + 1) % NetConstants.MaxSequence;
                    _outgoingAcks.Sequence = (ushort)newWindowStart;

                    //清理旧数据
                    while (_remoteWindowStart != newWindowStart)
                    {
                        ackIdx = _remoteWindowStart % _windowSize;
                        ackByte = NetConstants.ChanneledHeaderSize + ackIdx / BitsInByte;
                        ackBit = ackIdx % BitsInByte;
                        _outgoingAcks.RawData[ackByte] &= (byte)~(1 << ackBit);
                        _remoteWindowStart = (_remoteWindowStart + 1) % NetConstants.MaxSequence;
                    }
                }

                //Final stage - process valid packet
                //trigger acks send
                _mustSendAcks = true;

                ackIdx = seq % _windowSize;
                ackByte = NetConstants.ChanneledHeaderSize + ackIdx / BitsInByte;
                ackBit = ackIdx % BitsInByte;
                if ((_outgoingAcks.RawData[ackByte] & (1 << ackBit)) != 0)
                {
                    //未确认的数据，放入发送队列中，进行确认接收
                    NetDebug.Write("[RR]ReliableInOrder duplicate");
                    //because _mustSendAcks == true
                    AddToPeerChannelSendQueue();
                    return false;
                }

                //写入最新状态数据
                _outgoingAcks.RawData[ackByte] |= (byte)(1 << ackBit);
            }

            AddToPeerChannelSendQueue();

            //detailed check
            if (seq == _remoteSequence)
            {
                NetDebug.Write("[RR]ReliableInOrder packet succes");
                Peer.AddReliablePacket(_deliveryMethod, packet);
                _remoteSequence = (_remoteSequence + 1) % NetConstants.MaxSequence;

                if (_ordered)
                {
                    NetPacket p;
                    while ((p = _receivedPackets[_remoteSequence % _windowSize]) != null)
                    {
                        //process holden packet
                        _receivedPackets[_remoteSequence % _windowSize] = null;
                        Peer.AddReliablePacket(_deliveryMethod, p);
                        _remoteSequence = (_remoteSequence + 1) % NetConstants.MaxSequence;
                    }
                }
                else
                {
                    while (_earlyReceived[_remoteSequence % _windowSize])
                    {
                        //process early packet
                        _earlyReceived[_remoteSequence % _windowSize] = false;
                        _remoteSequence = (_remoteSequence + 1) % NetConstants.MaxSequence;
                    }
                }
                return true;
            }

            //holden packet
            if (_ordered)
            {
                _receivedPackets[ackIdx] = packet;
            }
            else
            {
                _earlyReceived[ackIdx] = true;
                Peer.AddReliablePacket(_deliveryMethod, packet);
            }
            return true;
        }
    }
}
