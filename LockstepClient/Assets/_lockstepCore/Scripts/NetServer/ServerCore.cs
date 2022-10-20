using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using Lockstep.Network.Server;
using Server.LiteNetLib;
using System;
using System.Diagnostics;



public class ServerCore : MonoBehaviour
{
    [Header("房间玩家数量")]
    public int RoomPlayerNumber = 1;
    [Header("端口号")]
    public int Port = 9050;

    private string _ip;

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

    LiteNetLibServer server;
    // Start is called before the first frame update
    void Start()
    {
        //int roomSize = 2;

        server = new LiteNetLibServer();

        var room = new Lockstep.Network.Server.Room(server, RoomPlayerNumber);

        room.Open(Port);


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


        GUI.Label(new Rect(0, 90, 300, 100), $"ReadyPlayer:{RoomPlayerNumber} ");
    }
}
