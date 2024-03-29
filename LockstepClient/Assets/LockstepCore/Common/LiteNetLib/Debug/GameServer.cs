﻿using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using LiteNetLib;
using LiteNetLib.Utils;

public class GameServer : MonoBehaviour, INetEventListener, INetLogger
{
    private NetManager _netServer;
    private NetPeer _ourPeer;
    private NetDataWriter _dataWriter;

    [SerializeField] private GameObject _serverBall;

    void Start()
    {
        NetDebug.Logger = this;
        _dataWriter = new NetDataWriter();
        _netServer = new NetManager(this);
        //指定端口号
        _netServer.Start(5000);
        _netServer.BroadcastReceiveEnabled = true;
        _netServer.UpdateTime = 15;

    }

    void Update()
    {
        if (_netServer == null) return;
        _netServer.PollEvents();
    }

    void FixedUpdate()
    {
        if (_ourPeer != null)
        {
            _serverBall.transform.Translate(1f * Time.fixedDeltaTime, 0f, 0f);
            _dataWriter.Reset();
            _dataWriter.Put(_serverBall.transform.position.x);
            _ourPeer.Send(_dataWriter, DeliveryMethod.Sequenced);
        }
    }

    void OnDestroy()
    {
        NetDebug.Logger = null;
        if (_netServer != null)
            _netServer.Stop();
    }

    public void OnPeerConnected(NetPeer peer)
    {
        Debug.Log("[SERVER] We have new peer " + peer.EndPoint);
        _ourPeer = peer;
    }

    public void OnNetworkError(IPEndPoint endPoint, SocketError socketErrorCode)
    {
        Debug.Log("[SERVER] error " + socketErrorCode);
    }

    public void OnNetworkReceiveUnconnected(IPEndPoint remoteEndPoint, NetPacketReader reader,
        UnconnectedMessageType messageType)
    {
        if (messageType == UnconnectedMessageType.Broadcast)
        {
            //服务器在无链接的情况下，收到客户端广播，告知所有人，服务器的IP、端口
            Debug.Log($"[SERVER] Received discovery request. Send discovery response   客户端：{remoteEndPoint}");
            NetDataWriter resp = new NetDataWriter();
            resp.Put(1);
            //发送无链接下的包出去
            _netServer.SendUnconnectedMessage(resp, remoteEndPoint);
        }
    }

    public void OnNetworkLatencyUpdate(NetPeer peer, int latency)
    {
    }

    public void OnConnectionRequest(ConnectionRequest request)
    {
        request.AcceptIfKey("sample_app");
    }

    public void OnPeerDisconnected(NetPeer peer, DisconnectInfo disconnectInfo)
    {
        Debug.Log("[SERVER] peer disconnected " + peer.EndPoint + ", info: " + disconnectInfo.Reason);
        if (peer == _ourPeer)
            _ourPeer = null;
    }

    public void OnNetworkReceive(NetPeer peer, NetPacketReader reader, DeliveryMethod deliveryMethod)
    {
    }

    public void WriteNet(NetLogLevel level, string str, params object[] args)
    {
        Debug.LogFormat(str, args);
    }

    public void OnNetworkReceive(NetPeer peer, NetPacketReader reader, byte channelNumber, DeliveryMethod deliveryMethod)
    {
        //throw new NotImplementedException();
    }
}
