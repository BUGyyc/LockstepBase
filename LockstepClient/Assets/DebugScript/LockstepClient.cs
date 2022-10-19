using System.Net;
using System.Net.Sockets;
using UnityEngine;
using LiteNetLib;
using LiteNetLib.Utils;

public class LockstepClient : MonoBehaviour, INetEventListener
{
    private NetManager _netClient;

    [SerializeField] public GameObject _clientBall;
    [SerializeField] public GameObject _clientBallInterpolated;

    private float _newBallPosX;
    private float _oldBallPosX;
    private float _lerpTime;

    private int _port = 5000;

    public void StartClient(int port)
    {
        _port = port;
        _netClient = new NetManager(this);
        _netClient.UnconnectedMessagesEnabled = true;
        _netClient.UpdateTime = 15;
        _netClient.Start();
    }

    void Update()
    {
        _netClient.PollEvents();

        var peer = _netClient.FirstPeer;
        if (peer != null && peer.ConnectionState == ConnectionState.Connected)
        {
            //Fixed delta set to 0.05
            var pos = _clientBallInterpolated.transform.position;
            pos.x = Mathf.Lerp(_oldBallPosX, _newBallPosX, _lerpTime);
            _clientBallInterpolated.transform.position = pos;

            //Basic lerp
            _lerpTime += Time.deltaTime / Time.fixedDeltaTime;
        }
        else
        {
            //ֻҪû�����ӳɹ��������һֱ�������ӵ�����
            Debug.Log($"�ͻ��˷�������  _port {_port} ");

            NetDataWriter writer = new NetDataWriter();

            writer.Put(NetID.ClientConnectReq);

            writer.Put(1);


            //����ָ���˶˿�
            _netClient.SendBroadcast(writer, _port);
        }

        ClientInput();


    }


    private void ClientInput()
    {
        var peer = _netClient.FirstPeer;
        if (peer == null || peer.ConnectionState != ConnectionState.Connected) return;

        if (Input.GetKeyDown(KeyCode.A))
        {
            NetDataWriter writer = new NetDataWriter();

            writer.Put(NetID.ClientSend2ServerStringMsg);

            writer.Put("Client Send Code:A");

            Debug.Log($"�ͻ�����������Code  A ");
            _netClient.SendBroadcast(writer, _port);
        }



    }

    void OnDestroy()
    {
        if (_netClient != null)
            _netClient.Stop();
    }

    public void OnPeerConnected(NetPeer peer)
    {
        Debug.Log("[CLIENT] We connected to " + peer.EndPoint);
    }

    public void OnNetworkError(IPEndPoint endPoint, SocketError socketErrorCode)
    {
        Debug.Log("[CLIENT] We received error " + socketErrorCode);
    }

    public void OnNetworkReceiveUnconnected(IPEndPoint remoteEndPoint, NetPacketReader reader, UnconnectedMessageType messageType)
    {
        if (messageType == UnconnectedMessageType.BasicMessage && _netClient.ConnectedPeersCount == 0)
        {
            var netId = reader.GetInt();

            //������֪ͨ�ͻ��ˣ�IP���˿�
            Debug.Log($"[CLIENT] �ͻ����յ��Է����� Connecting to: {remoteEndPoint}  Э��� {netId}  ");
            switch (netId)
            {
                case NetID.ServerArgeeConnectClient:
                    //������ͬ�����ӣ���ô�ͻ��˷�������
                    _netClient.Connect(remoteEndPoint, "sample_app");
                    break;
                default:
                    break;
            }

        }
    }

    public void OnNetworkLatencyUpdate(NetPeer peer, int latency)
    {

    }

    public void OnConnectionRequest(ConnectionRequest request)
    {

    }

    public void OnPeerDisconnected(NetPeer peer, DisconnectInfo disconnectInfo)
    {
        Debug.Log("[CLIENT] We disconnected because " + disconnectInfo.Reason);
    }


    /// <summary>
    /// ������շ��������ݣ����Ұ��ƶ�����д��
    /// </summary>
    /// <param name="peer"></param>
    /// <param name="reader"></param>
    /// <param name="channelNumber"></param>
    /// <param name="deliveryMethod"></param>
    public void OnNetworkReceive(NetPeer peer, NetPacketReader reader, byte channelNumber, DeliveryMethod deliveryMethod)
    {
        _newBallPosX = reader.GetFloat();

        var pos = _clientBall.transform.position;

        _oldBallPosX = pos.x;
        pos.x = _newBallPosX;

        _clientBall.transform.position = pos;

        _lerpTime = 0f;
    }
}
