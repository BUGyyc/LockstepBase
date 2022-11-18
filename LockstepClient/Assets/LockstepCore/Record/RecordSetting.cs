/*
 * @Author: delevin.ying 
 * @Date: 2022-11-18 18:01:30 
 * @Last Modified by: delevin.ying
 * @Last Modified time: 2022-11-18 18:21:33
 */


public static class RecordSetting
{
    /// <summary>
    /// 是否录下客户端的镜头，一般不录，因为每帧的镜头操作太多了，只是一种观察视角的记录，也许有特殊需求就需要做
    /// </summary>
    public const bool RecordCamera = false;


    /// <summary>
    /// 是否持久化到客户端
    /// </summary>
    public const bool RecordToClient = false;

    
    /// <summary>
    /// 是否持久化到服务器
    /// </summary>
    public const bool RecordToServer = false;
}