using Server.LiteNetLib;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ServerCore : MonoBehaviour
{
    [Header("房间玩家数量")]
    public int RoomPlayerNumber = 1;
    [Header("端口号")]
    public int Port = 9050;

    private string _ip;
    LiteNetLibServer server;
    Lockstep.Network.Server.Room room;

    [HideInInspector]
    public string SetGet_str_ipAddress
    {
        set { _ip = value; }
        get
        {
            _ip = NetTool.IP(address.IPv4);
            return _ip;
        }
    }

    private void Awake()
    {
        GameObject.DontDestroyOnLoad(this);
    }


    // Start is called before the first frame update
    void Start()
    {
        Port = GameSetting.ServerPort;
        server = new LiteNetLibServer();

        room = new Lockstep.Network.Server.Room(server, RoomPlayerNumber);

        room.Open(Port);

        SceneManager.LoadScene("SampleScene");
    }

    // Update is called once per frame
    void Update()
    {
        server.PollEvents();
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 300, 100), $"ip:{SetGet_str_ipAddress} ");
        GUI.Label(new Rect(0, 30, 300, 100), $"port:{Port} ");
        GUI.Label(new Rect(0, 60, 300, 100), $"RoomPlayer:{RoomPlayerNumber} ");


        if (room != null) GUI.Label(new Rect(0, 90, 300, 100), $"ReadyPlayer:{room.OnLivePlayerCount()} ");
    }
}
