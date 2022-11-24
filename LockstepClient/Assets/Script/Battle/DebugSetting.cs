/*
 * @Author: delevin.ying 
 * @Date: 2022-11-18 14:08:46 
 * @Last Modified by: delevin.ying
 * @Last Modified time: 2022-11-18 14:19:23
 */

public static class DebugSetting
{
    /// <summary>
    /// 每隔 n 帧 输出一次 HashCode 的输出
    /// </summary>
    public const int HashCodePrintStepTick = 0;
}


public static class DebugLogSetting
{
    public const int LogState = Default | Lockstep;


    #region 特定环境下的Log 输出
    public const int Default = 1 << 1;
    public const int NET = 1 << 2;
    public const int UI = 1 << 3;
    public const int DEV = 1 << 4;
    public const int Asset = 1 << 5;
    public const int DEV_Battle = 1 << 6;
    public const int Lockstep = 1 << 7;
    #endregion
}


