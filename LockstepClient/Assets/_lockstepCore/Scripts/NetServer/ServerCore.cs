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
    LiteNetLibServer server;
    // Start is called before the first frame update
    void Start()
    {
        int roomSize = 2;
        //int waitInSeconds = 5;

        //var sw = new Stopwatch();
        //sw.Start();
        //Console.Write($"Enter room size (defaults to {roomSize} after {waitInSeconds} seconds): ");
        //while (!Console.KeyAvailable && sw.Elapsed.Seconds < waitInSeconds)
        //{
        //    Thread.Sleep(1);
        //}
        //sw.Stop();

        //if (Console.KeyAvailable)
        //{
        //    roomSize = Console.ReadKey().KeyChar - 48;
        //}
        //Console.Write(Environment.NewLine);

        server = new LiteNetLibServer();

        var room = new Lockstep.Network.Server.Room(server, roomSize);

        room.Open(9050);


    }

    // Update is called once per frame
    void Update()
    {
        server.PollEvents();
    }
}
