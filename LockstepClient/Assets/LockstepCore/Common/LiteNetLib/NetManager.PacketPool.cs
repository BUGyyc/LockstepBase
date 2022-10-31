using System;
using System.Threading;

namespace LiteNetLib
{
    public partial class NetManager
    {
        private NetPacket _poolHead;
        private int _poolCount;
        private readonly object _poolLock = new object();

        /// <summary>
        /// Maximum packet pool size (increase if you have tons of packets sending)
        /// </summary>
        public int PacketPoolSize = 1000;

        public int PoolCount => _poolCount;

        private NetPacket PoolGetWithData(PacketProperty property, byte[] data, int start, int length)
        {
            int headerSize = NetPacket.GetHeaderSize(property);
            NetPacket packet = PoolGetPacket(length + headerSize);
            packet.Property = property;
            //copy原包，并且加了一些特别的数据
            Buffer.BlockCopy(data, start, packet.RawData, headerSize, length);
            return packet;
        }

        //Get packet with size
        private NetPacket PoolGetWithProperty(PacketProperty property, int size)
        {
            NetPacket packet = PoolGetPacket(size + NetPacket.GetHeaderSize(property));
            packet.Property = property;
            return packet;
        }

        private NetPacket PoolGetWithProperty(PacketProperty property)
        {
            NetPacket packet = PoolGetPacket(NetPacket.GetHeaderSize(property));
            packet.Property = property;
            return packet;
        }

        /// <summary>
        /// 从池子中取一个NetPacket，设定指定长度，用来装数据
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        internal NetPacket PoolGetPacket(int size)
        {
            NetPacket packet;
            lock (_poolLock)
            {
                packet = _poolHead;
                if (packet == null)
                    return new NetPacket(size);

                _poolHead = _poolHead.Next;
                _poolCount--;
            }

            packet.Size = size;
            if (packet.RawData.Length < size)
                packet.RawData = new byte[size];
            return packet;
        }

        internal void PoolRecycle(NetPacket packet)
        {
            if (packet.RawData.Length > NetConstants.MaxPacketSize || _poolCount >= PacketPoolSize)
            {
                //Don't pool big packets. Save memory
                return;
            }

            //Clean fragmented flag
            packet.RawData[0] = 0;
            lock (_poolLock)
            {
                packet.Next = _poolHead;
                _poolHead = packet;
                _poolCount++;
            }
        }
    }
}
