using UnityEngine;

public class LogTool
{
    public static LogTool Instance
    {
        get
        {
            if (_instance == null) _instance = new LogTool();

            return _instance;
        }
    }
    private static LogTool _instance;

    public void Msg(params string[] strs)
    {
        //Debug.LogFormat($"<color=red> {0}  </color>", strs);
    }

}