﻿
using Lockstep.Common.Logging;
using Lockstep.Core.Logic.Interfaces;
using Lockstep.Game;
using Lockstep.Network.Client;
using Lockstep.Network.Messages;
using System;
using System.Collections;
using System.IO;
using UnityEngine;
using Entitas;

public class ActionWorld : MonoBehaviour
{
    public static ActionWorld Instance;

    public string ServerIp = "127.0.0.1";
    public int ServerPort = 9050;

    //世界模拟器
    public Simulation Simulation;
    //[HideInInspector]
    //public FixMath.NET.Fix64Random FixRandom;

    //为了拿到所有Entity
    public RTSEntityDatabase EntityDatabase;

    public bool Connected => _client.Connected;

    public byte[] AllActorIds { get; private set; }

    [HideInInspector]
    public byte LocalCharacterEntityId;

    private NetworkCommandQueue _commandQueue;
    private readonly LiteNetLibClient _client = new LiteNetLibClient();

    private void Awake()
    {
        Instance = this;

        ServerIp = NetSetting.ServerIp;
        ServerPort = (int)NetSetting.ServerPort;

        Debug.Log($"客户端发起链接请求    服务端IP：{ServerIp}   Port {ServerPort}  ");


        Log.OnMessage += (sender, args) => Debug.Log(args.Message);

        _commandQueue = new NetworkCommandQueue(_client)
        {
            LagCompensation = 10
        };
        _commandQueue.InitReceived += OnInitReceived;

        Simulation = new Simulation(Contexts.sharedInstance, _commandQueue, new UnityGameService(EntityDatabase));

        GameWorldManager.Instance.Awake();
    }


    /// <summary>
    /// 服务器下发到客户端，发起初始化
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="msg"></param>
    public void OnInitReceived(object sender, Init msg)
    {
        //全部玩家数据
        AllActorIds = msg.AllActors;
        Debug.Log($"Starting simulation. Total actors: {msg.AllActors.Length}. Local ActorID: {msg.ActorID}  msg.SimulationSpeed {msg.SimulationSpeed} ");
        Simulation.Start(30, msg.ActorID, msg.AllActors);

        LocalCharacterEntityId = msg.ActorID;

        GameWorldManager.Instance.Init(msg.AllActors);
    }


    public void DumpGameLog()
    {
        Simulation.DumpGameLog(new FileStream(@"C:\Log\" + Math.Abs(Contexts.sharedInstance.gameState.hashCode.value) + ".bin", FileMode.Create, FileAccess.Write));
    }

    public void Execute(ICommand command)
    {
        Simulation.Execute(command);
    }

    private void Start()
    {
        _client.Start();
        StartCoroutine(AutoConnect());
    }

    private void OnDestroy()
    {
        _client.Stop();
    }

    void Update()
    {
        _client.Update();
        Simulation.Update(Time.deltaTime * 1000);

        GameWorldManager.Instance.Update();
    }

    private void FixedUpdate()
    {

    }

    public IEnumerator AutoConnect()
    {
        while (!Connected)
        {
            _client.Connect(ServerIp, ServerPort);
            yield return new WaitForSeconds(1);
        }

        yield return null;
    }
}
