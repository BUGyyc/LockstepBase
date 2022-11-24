using UnityEngine;
using Lockstep.Core.State.Game;
using Lockstep.Game.Commands;
public class GameWorldManager
{
    private static GameWorldManager _instance;
    public static GameWorldManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameWorldManager();
            }
            return _instance;
        }
    }

    public void Awake()
    {

    }

    public void Init(byte[] actors)
    {
        InitConfig();

        CreateNPC();

        CreateCharacter(actors);
    }

    private void InitConfig()
    {
        ConfigManager.Instance = new ConfigManager();
        ConfigManager.Instance.Init();
    }

    void CreateNPC()
    {

        //生成 Entity , 后续应该改成对应 EntityType 生成 Entity ,走工厂模式

        //for (int i = 0; i < 5; i++)
        //{
        //    for (int j = 0; j < 5; j++)
        //    {
        //        ActionWorld.Instance.Execute(new SpawnCommand
        //        {
        //            EntityConfigId = 3,
        //            Position = new Lockstep.LVector3(true, (-12 + i) * 2000, 0, (-12 + j) * 2000)
        //        });
        //    }
        //}
    }

    void CreateCharacter(byte[] actors)
    {

        foreach (var actor in actors)
        {
            EntityUtil.CreateSimpleHero(actor);
        }
        Debug.LogFormat("创建玩家");

        for (var i = 0; i < 20; i++)
        {
            EntityUtil.CreateAI(i);
        }
    }

    private void CreateLocalPlayer()
    {

    }

    public void OnFixedUpdate()
    {

    }

    public void Update()
    {
#if UNITY_EDITOR

#endif
    }
}
