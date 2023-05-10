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
using Unity.VisualScripting;

public class ActionWorld : MonoBehaviour
{
    public static ActionWorld Instance;

    public string ServerIp = "127.0.0.1";
    public int ServerPort = 9050;

    //世界模拟器
    public Simulation Simulation;

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

        _commandQueue = new NetworkCommandQueue(_client) { LagCompensation = GlobalSetting.LagCompensation };
        _commandQueue.InitReceived += OnInitReceived;

        Simulation = new Simulation(
            Contexts.sharedInstance,
            _commandQueue,
            new UnityGameService(EntityDatabase)
        );

        GameWorldManager.Instance.Awake();

        if (RecordSetting.HasRecord)
        {
            // RecordManager.Instance = new RecordManager();
        }
    }

    /// <summary>
    /// 服务器下发到客户端，发起初始化
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="msg"></param>
    public void OnInitReceived(object sender, Init msg)
    {
        Debug.Log("接收到服务器，启动通知，客户端启动");
        //全部玩家数据
        AllActorIds = msg.AllActors;
        Debug.Log(
            $"Starting simulation. Total actors: {msg.AllActors.Length}. Local ActorID: {msg.ActorID}  msg.SimulationSpeed {msg.SimulationSpeed} "
        );
        Simulation.Start(30, msg.ActorID, msg.AllActors);

        LocalCharacterEntityId = msg.ActorID;

        GameWorldManager.Instance.Init(msg.AllActors);
    }

    public void DumpGameLog()
    {
        var path = Application.streamingAssetsPath;

        if (Directory.Exists(path + "/Log/") == false)
        {
            Directory.CreateDirectory(path + "/Log/");
        }

        var fullPath =
            path + "/Log/" + Math.Abs(Contexts.sharedInstance.gameState.hashCode.value) + ".bin";

        Simulation.DumpGameLog(new FileStream(fullPath, FileMode.Create, FileAccess.Write));

        //FileStream fs = new FileStream(fullPath, FileMode.Open);

        //var gameLog = GameLog.ReadFrom(fs);
        //if (gameLog != null)
        //{
        //    // var gameLog = GameLog.ReadFrom(stream);

        //    Debug.Log($"[GameLog]  {gameLog.LocalActorId}   {gameLog.InputLog.Count} ");

        //    var inputLog = gameLog.InputLog;

        //    foreach (var item in inputLog)
        //    {
        //        Debug.Log($"{item}");

        //    }
        //}
    }

    public void Execute(ICommand command)
    {
        

        Simulation.Execute(command);
    }

    private void Start()
    {
        _client.Start();
        if (RecordSetting.HasRecord == false)
        {
            StartCoroutine(AutoConnect());
        }
    }

    private void OnDestroy()
    {
        _client.Stop();
    }

    void Update()
    {
        _client.Update();

        //TODO：这里的Time 后续改成 LFloat
        Simulation.Update(Time.deltaTime * 1000);

        GameWorldManager.Instance.Update();

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.K))
        {
            DumpGameLog();
        }
#endif
    }

    private void FixedUpdate() { }

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
