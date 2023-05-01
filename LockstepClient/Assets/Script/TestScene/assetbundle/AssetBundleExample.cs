﻿using UnityEngine;
using System;
using System.IO;
using ET;
using BM;
using System.Collections.Generic;
// using ABCore;
//namespace ET.Client
//{
public class AssetBundleExample : MonoBehaviour
{
    void Start()
    {
        LogMaster.L("Hello Start");

        Download().Coroutine();
    }

    private async ETTask Download()
    {
        //点名要分包文件
        Dictionary<string, bool> updatePackageBundle = new Dictionary<string, bool>()
        {
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
            return;
        }
        InitializePackage().Coroutine();

        LogMaster.L("需要更新包体大小 " + updateInfo.NeedUpdateSize);

        updateInfo.DownLoadFinishCallback += () =>
        {
            LogMaster.L("加载完成");
        };

        updateInfo.ProgressCallback += (p) =>
        {
            LogMaster.L($"加载中 {p / 100f}");
        };

        updateInfo.ErrorCancelCallback += () =>
        {
            LogMaster.E("加载失败");
        };

        AssetComponent.DownLoadUpdate(updateInfo).Coroutine();


    }

    private async ETTask InitializePackage()
    {
        //await AssetComponent.Initialize(AssetComponentConfig.DefaultBundlePackageName);
        await AssetComponent.Initialize("SubBundle");
    }

    private void Update()
    {
        //LogMaster.E("AB  Update");
    }

    // private async ETVoid Func()
    // {
    //     //Debug.Log("3");

    //     await AssetBundleHelper.instance.AsyncLoadAssetBundle<AssetBundle>("hero");

    //     await AssetBundleHelper.instance.AsyncLoadAssetBundle<AssetBundle>("bullet");


    //     await AssetBundleHelper.instance.AsyncLoadAssetBundle<AssetBundle>("canvas");


    //     await AssetBundleHelper.instance.AsyncLoadAssetBundle<AssetBundle>("enemy");

    //     await AssetBundleHelper.instance.AsyncLoadAssetBundle<AssetBundle>("clazyrunnner");


    //     await AssetBundleHelper.instance.AsyncLoadAssetBundle<AssetBundle>("player");
    // }




}
//}