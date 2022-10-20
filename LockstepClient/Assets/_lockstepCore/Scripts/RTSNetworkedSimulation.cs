
using System;
using System.Collections;
using System.IO;
using Lockstep.Common.Logging;
using Lockstep.Core.Logic.Interfaces;
using Lockstep.Core.Logic.Serialization.Utils;
using Lockstep.Game;
using Lockstep.Network.Client;
using UnityEngine;
using Lockstep.Network.Messages;

public class RTSNetworkedSimulation : MonoBehaviour
{

    public static RTSNetworkedSimulation Instance;

    public string ServerIp = "127.0.0.1";
    public int ServerPort = 9050;

    //世界模拟器
    public Simulation Simulation;

    //为了拿到所有Entity
    public RTSEntityDatabase EntityDatabase;

    public bool Connected => _client.Connected;

    public byte[] AllActorIds { get; private set; }

    private NetworkCommandQueue _commandQueue;
    private readonly LiteNetLibClient _client = new LiteNetLibClient();

    private void Awake()
    {
        Instance = this;

        Log.OnMessage += (sender, args) => Debug.Log(args.Message);

        _commandQueue = new NetworkCommandQueue(_client)
        {
            LagCompensation = 10
        };
        _commandQueue.InitReceived += OnInitReceived;

        Simulation = new Simulation(Contexts.sharedInstance, _commandQueue, new UnityGameService(EntityDatabase));
    }

    public void OnInitReceived(object sender, Init msg)
    {
        //全部玩家数据
        AllActorIds = msg.AllActors;
        Debug.Log($"Starting simulation. Total actors: {msg.AllActors.Length}. Local ActorID: {msg.ActorID}");
        Simulation.Start(msg.SimulationSpeed, msg.ActorID, msg.AllActors);
    }


    public void DumpGameLog()
    {
        Simulation.DumpGameLog(new FileStream(@"C:\Log\" + Math.Abs(Contexts.sharedInstance.gameState.hashCode.value) + ".bin", FileMode.Create, FileAccess.Write));
    }

    public void Execute(Lockstep.Core.Logic.Interfaces.ICommand command)
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

        if (Input.GetKeyDown(KeyCode.P))
        {
            //AllActorIds = init.AllActors;
            Debug.Log($"Starting simulation. Total actors:-------------------------------------");
            Simulation.Start(30, 1, new byte[] { 1 });
        }
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
