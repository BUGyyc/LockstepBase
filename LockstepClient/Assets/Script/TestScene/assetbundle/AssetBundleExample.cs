using UnityEngine;
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
        Dictionary<string, bool> updatePackageBundle = new Dictionary<string, bool>()
        {
            {AssetComponentConfig.DefaultBundlePackageName, false},
            {"SubBundle", false},
            //{"Main", false},
            //{"APK", false},
        };
        var info = await AssetComponent.CheckAllBundlePackageUpdate(updatePackageBundle);

        if (info == null)
        {
            LogMaster.L("info == null");
            return;
        }

        if(info.NeedUpdate == false)
        {
            LogMaster.L("不需要更新");
            return;
        }

        LogMaster.L("需要更新包体大小 " + info.NeedUpdateSize);
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