using Entitas;
using System.Collections.Generic;
using UnityEngine;
public class ModelComponent : IComponent
{
    public uint modelId;
    public bool hasLoad;
}


/// <summary>
/// 临时测试用，后续再导表配置
/// </summary>
public static class ModelConfig
{
    private static Dictionary<uint, string> modelPathDic;

    static ModelConfig()
    {
        modelPathDic = new Dictionary<uint, string>()
        {
            {1,"Prefabs/ClazyRunner" }

        };
    }

    public static string FindModelPath(uint id)
    {
        string path = "";
        modelPathDic.TryGetValue(id, out path);

        return path;
    }

    public static GameObject FindModel(uint id)
    {
        string path = FindModelPath(id);

        if (string.IsNullOrEmpty(path)) return null;
        var obj = Resources.Load<GameObject>(path);
        if (obj == null) Debug.LogError("obj == null " + path);
        else Debug.Log("obj " + obj.name);
        return obj;
    }

}