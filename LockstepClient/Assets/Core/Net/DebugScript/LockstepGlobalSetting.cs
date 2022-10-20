

public static class NetID
{
    /// <summary>
    /// 客户端发起链接服务器
    /// </summary>
    public const int ClientConnectReq = 10001;

    /// <summary>
    /// 服务器响应，同意链接客户端
    /// </summary>
    public const int ServerArgeeConnectClient = 10002;


    /// <summary>
    /// 客户端向服务器发送协议
    /// </summary>
    public const int ClientSend2ServerStringMsg = 10003;


    /// <summary>
    /// 服务器广播协议
    /// </summary>
    public const int ServerBroadcast = 10004;
}
