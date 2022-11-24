using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Lockstep.Game;
using System.IO;

public class RecordStart : MonoBehaviour
{
    public InputField fileIf;
    public Button startBtn;
    private const string basePath = "/Log/";
    // Start is called before the first frame update
    void Start()
    {
        startBtn.onClick.AddListener(OnBtnClick);
    }

    private void OnBtnClick()
    {
        if (fileIf == null) return;
        var fileName = fileIf.text;


        var path = Application.streamingAssetsPath;

        var fullPath = path + basePath + fileName + ".bin";

        FileStream fs = new FileStream(fullPath, FileMode.Open);

        var gameLog = GameLog.ReadFrom(fs);
        if (gameLog != null)
        {
            // var gameLog = GameLog.ReadFrom(stream);

            Debug.Log($"[GameLog]  {gameLog.LocalActorId}   {gameLog.InputLog.Count} ");
        }

        // Application.streamingAssetsPath
        // GameLog

        // var stream = FileUtil.LoadStreamByFile(Application.streamingAssetsPath + basePath + fileName + ".bin");
        // if (stream != null)
        // {
        //     var gameLog = GameLog.ReadFrom(stream);

        //     Debug.Log($"[GameLog]  {gameLog.LocalActorId}   {gameLog.InputLog.Count} ");
        // }

        // 
    }

    // Update is called once per frame
    void Update()
    {

    }
}
