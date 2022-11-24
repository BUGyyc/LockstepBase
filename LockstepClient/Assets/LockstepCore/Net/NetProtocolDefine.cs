/*
 * @Author: delevin.ying 
 * @Date: 2022-11-18 17:20:08 
 * @Last Modified by: delevin.ying
 * @Last Modified time: 2022-11-18 17:59:38
 */


public static class NetProtocolDefine
{
    /// <summary>
    /// 游戏初始化指令
    /// </summary>
    public const byte Init = 0;
    // public const byte AAA = 1;
    /// <summary>
    /// 输入指令
    /// </summary>
    public const byte Input = 2;
    /// <summary>
    /// 验证是否同步
    /// </summary>
    public const byte CheckSync = 3;


    public const string ConnectKey = "LockstepGame";
}