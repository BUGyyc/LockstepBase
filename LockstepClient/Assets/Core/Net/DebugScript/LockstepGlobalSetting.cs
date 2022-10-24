

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

public static class GameSetting
{
    public static int ServerPort = 9050;

    public static string ServerIp = "";
}



public static class GameSceneSetting
{
    public static string RTS_SCENE = "RTS";
    public static string TPS_SCENE = "TPS";

    public static string BattleTestScene = TPS_SCENE;
}