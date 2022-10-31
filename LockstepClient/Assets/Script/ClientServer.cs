using Server.LiteNetLib;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClientServer : MonoBehaviour
{
    [Header("房间玩家数量")]
    //[HideInInspector]
    public uint RoomPlayerNumber = 2;
    [Header("端口号")]
    public uint Port = 9000;

    private string _ip;
    LiteNetLibServer server;
    Lockstep.Network.Server.Room room;

    [HideInInspector]
    public string SetGet_str_ipAddress
    {
        set { _ip = value; }
        get
        {
            _ip = LiteNetLib.NetUtils.GetLocalIp(LiteNetLib.LocalAddrType.IPv4);
            return _ip;
        }
    }

    private void Awake()
    {
        GameObject.DontDestroyOnLoad(this);
    }


    float stepTime = 0;
    bool hasLoad = false;

    // Start is called before the first frame update
    void Start()
    {
        NetSetting.ServerPort = Port;
        NetSetting.PlayerNumber = RoomPlayerNumber;
        //Port = NetSetting.ServerPort;
        //RoomPlayerNumber = NetSetting.PlayerNumber;
        server = new LiteNetLibServer();

        room = new Lockstep.Network.Server.Room(server, (int)RoomPlayerNumber);

        room.Open((int)Port);

        UnityEngine.Debug.Log($"房间开启 RoomPlayerNumber{RoomPlayerNumber}  port {Port}  ");


    }

    // Update is called once per frame
    void Update()
    {
        server.PollEvents();
        //stepTime++;
        if (/*stepTime >= 10 &&*/ hasLoad == false)
        {
            hasLoad = true;
            SceneManager.LoadScene(GameSceneSetting.BattleTestScene);
        }
    }

    private void OnGUI()
    {
        if (room == null) return;

        GUI.Label(new Rect(0, 0, 300, 100), $"ip:{SetGet_str_ipAddress} ");
        GUI.Label(new Rect(0, 30, 300, 100), $"port:{Port} ");
        GUI.Label(new Rect(0, 60, 300, 100), $"RoomPlayer:{room.OnLivePlayerCount()} / {RoomPlayerNumber} ");


        //GUI.Label(new Rect(0, 90, 300, 100), $"ReadyPlayer: ");


    }
}
