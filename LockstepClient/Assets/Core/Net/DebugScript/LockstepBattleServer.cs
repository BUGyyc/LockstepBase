using LiteNetLib;
using LiteNetLib.Utils;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class LockstepBattleServer : MonoBehaviour, INetEventListener, INetLogger
{
    private NetManager _netServer;
    private NetPeer _ourPeer;
    private NetDataWriter _dataWriter;

    //[SerializeField] public GameObject _serverBall;


    public void StartServer(int port = 5000)
    {
        NetDebug.Logger = this;
        _dataWriter = new NetDataWriter();
        _netServer = new NetManager(this);
        //指定端口号
        _netServer.Start(port);
        //_netServer.BroadcastReceiveEnabled = true;
        _netServer.UpdateTime = 15;
    }

    void Update()
    {
        _netServer.PollEvents();
    }

    void FixedUpdate()
    {
        if (_ourPeer != null)
        {
            //_serverBall.transform.Translate(1f * Time.fixedDeltaTime, 0f, 0f);
            //_dataWriter.Reset();
            //_dataWriter.Put(_serverBall.transform.position.x);
            //_ourPeer.Send(_dataWriter, DeliveryMethod.Sequenced);
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

        //通知客户端初始化游戏世界
        NetDataWriter data = new NetDataWriter();
        data.Put(NetID.InitStart);
        _netServer.SendUnconnectedMessage(data, peer.EndPoint);

        //Serializer serializer = new Serializer();
        //int seed = new Random().Next(int.MinValue, int.MaxValue);
        //this.Starting?.Invoke(this, new StartedEventArgs(20, _actorIds.Values.ToArray()));
        //foreach (KeyValuePair<int, byte> actorId in _actorIds)
        //{
        //    serializer.Reset();
        //    serializer.Put((byte)0);
        //    Init init = new Init();
        //    init.Seed = seed;
        //    init.ActorID = actorId.Value;
        //    init.AllActors = _actorIds.Values.ToArray();
        //    init.SimulationSpeed = 20;
        //    init.Serialize(serializer);
        //    _server.Send(actorId.Key, Compressor.Compress(serializer));
        //}
        //this.Started?.Invoke(this, new StartedEventArgs(20, _actorIds.Values.ToArray()));

    }

    public void OnNetworkError(IPEndPoint endPoint, SocketError socketErrorCode)
    {
        Debug.Log("[SERVER] error " + socketErrorCode);
    }

    public void OnNetworkReceiveUnconnected(IPEndPoint remoteEndPoint, NetPacketReader reader,
        UnconnectedMessageType messageType)
    {
        //if (messageType == UnconnectedMessageType.Broadcast)
        {
            var NetId = reader.GetInt();
            switch (NetId)
            {
                case NetID.ClientSend2ServerStringMsg:
                    var msg = reader.GetString();
                    Debug.Log($"[SERVER] 服务器接收到客户端数据     端口：{remoteEndPoint}   协议ID: {NetId}   msg :{msg} ");
                    break;
                case NetID.ClientConnectReq:

                    Debug.Log($"[SERVER] 服务器接收链接请求    端口：{remoteEndPoint}   协议ID: {NetId}   ");
                    //服务器同意链接
                    NetDataWriter resp = new NetDataWriter();
                    resp.Put(NetID.ServerArgeeConnectClient);
                    resp.Put(1);
                    //发送无链接下的包出去
                    _netServer.SendUnconnectedMessage(resp, remoteEndPoint);
                    break;


                case NetID.ServerBroadcast:

                    break;
            }


        }
    }

    public void OnNetworkLatencyUpdate(NetPeer peer, int latency)
    {
    }

    public void OnConnectionRequest(ConnectionRequest request)
    {
        request.AcceptIfKey("SomeConnectionKey");
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



        var eid = reader.GetInt();

        Debug.Log($"[SERVER]  服务器收到    peer.EndPoint {peer.EndPoint}   eid {eid} ");
    }
}
