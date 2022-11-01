

public static class NetID
{
    /// <summary>
    /// �ͻ��˷������ӷ�����
    /// </summary>
    public const int ClientConnectReq = 10001;

    /// <summary>
    /// ��������Ӧ��ͬ�����ӿͻ���
    /// </summary>
    public const int ServerArgeeConnectClient = 10002;


    /// <summary>
    /// �ͻ��������������Э��
    /// </summary>
    public const int ClientSend2ServerStringMsg = 10003;


    /// <summary>
    /// �������㲥Э��
    /// </summary>
    public const int ServerBroadcast = 10004;




    public const byte InitStart = 0;

    public const byte KeyFrame = 2;

    public const byte Disconnect = 3;







}




public static class GameSceneSetting
{
    public const string RTS_SCENE = "RTS";
    public const string TPS_SCENE = "TPS";
    public const string ACTION_SCENE = "Action";

    public const string LaunchClient = "LaunchClient";
    public const string LaunchClientPath = "Assets/Scenes/Debug/0.launch/LaunchClient.unity";
    public const string LaunchServer = "LaunchServer";
    public const string LaunchServerPath = "Assets/Scenes/Debug/0.launch/LaunchServer.unity";



    public static string BattleTestScene = ACTION_SCENE;

}