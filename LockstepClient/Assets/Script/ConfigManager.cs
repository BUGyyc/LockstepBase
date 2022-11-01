using Protocol;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ConfigManager
{
    public static ConfigManager Instance;
    public const string DATA_PATH = "ConfigData";

    private Dictionary<Type, object> _configsDic;
    private static Dictionary<string, MoveControllerCfg> mCtrlDic = new Dictionary<string, MoveControllerCfg>();
    public void Init()
    {
        _configsDic = new Dictionary<Type, object>();
    }


    private void LoadData(Type type, string bytesName = default)
    {
        if (string.IsNullOrEmpty(bytesName))
        {
            bytesName = type.Name.Replace("_ARRAY", "");
            bytesName = bytesName.ToLower();
        }

        Debug.Log("ConfigManager LoadData:" + bytesName);

        string path = string.Format("{0}/{1}/{2}.bytes", UnityEngine.Application.streamingAssetsPath, DATA_PATH, bytesName);

        byte[] rawData = FileUtil.LoadFile(path);

        var method = type.GetMethod("ParseFrom", new Type[] { typeof(byte[]) });

        if (method != null)
        {
            _configsDic[type] = method.Invoke(null, new object[] { rawData });
        }
        else
        {
            UnityEngine.Debug.LogError("类型错误 " + type.Name);
        }
    }






    public static MoveControllerCfg GetMoveController(string name)
    {
        if (!mCtrlDic.ContainsKey(name))
        {
            string path = string.Format("{0}/{1}/{2}.bytes", UnityEngine.Application.streamingAssetsPath, "MoveCtrl", name);

            var bytes = FileUtil.LoadFile(path);

            mCtrlDic.Add(name, MoveControllerCfg.ParseFrom(bytes));
        }

        return mCtrlDic[name];
    }
}
