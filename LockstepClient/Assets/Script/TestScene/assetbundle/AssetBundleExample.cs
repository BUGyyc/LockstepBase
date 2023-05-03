using UnityEngine;
using System;
using System.IO;
using ET;
using BM;
using System.Collections.Generic;
using UnityEngine.UI;
// using ABCore;
//namespace ET.Client
//{
public class AssetBundleExample : MonoBehaviour
{

    public Scrollbar loadPbr;

    private UpdateBundleDataInfo updateInfo;

    void Start()
    {
        LogMaster.L("Hello Start");

        AssetComponentConfig.HotfixPath = Application.dataPath + "/../HotfixBundles/";

        //声明一个默认包？？
        AssetComponentConfig.DefaultBundlePackageName = "AllBundle";

        Download().Coroutine();
    }

    private async ETTask Download()
    {
        //点名要分包文件
        Dictionary<string, bool> updatePackageBundle = new Dictionary<string, bool>()
        {
            {"AllBundle",false },
            {"SubBundle", false}
        };

        var updateInfo = await AssetComponent.CheckAllBundlePackageUpdate(updatePackageBundle);

        if (updateInfo == null)
        {
            LogMaster.L("info == null");
            return;
        }

        if (updateInfo.NeedUpdate == false)
        {
            LogMaster.L("不需要更新");
            InitializePackage().Coroutine();
            return;
        }


        Debug.LogError("需要更新, 大小: " + updateInfo.NeedUpdateSize);

        updateInfo.DownLoadFinishCallback += () =>
        {
            //LogMaster.L("加载完成");
            InitializePackage().Coroutine();
        };

        updateInfo.ProgressCallback += (p) =>
        {
            if (loadPbr != null) loadPbr.size = p / 100f;
            //LogMaster.L($"加载中 {p / 100f}");
        };

        updateInfo.ErrorCancelCallback += () =>
        {
            Debug.LogError("下载取消");
        };

        AssetComponent.DownLoadUpdate(updateInfo).Coroutine();


    }

    private async ETTask InitializePackage()
    {
        await AssetComponent.Initialize(AssetComponentConfig.DefaultBundlePackageName);
        await AssetComponent.Initialize("SubBundle");
    }

    /// <summary>
    /// 初始化 UI
    /// </summary>
    /// <returns></returns>
    private async ETTask InitUI()
    {
        //加载图集
        //await AssetComponent.LoadAsync(out LoadHandler atlasHandler,BPa);
    }

    private async ETTask LoadAtlas()
    {

    }

    void Update()
    {
        AssetComponent.Update();
    }

    void OnLowMemory()
    {
        AssetComponent.ForceUnLoadAll();
    }

    void OnDestroy()
    {
        updateInfo?.CancelUpdate();
        LMTD.ThreadFactory.Destroy();
    }

}
//}