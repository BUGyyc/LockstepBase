using UnityEngine;
using System;
using System.IO;
// using ABCore;
//namespace ET.Client
//{
public class AssetBundleExample : MonoBehaviour
{
    void Start()
    {
        // AssetBundleHelper.instance.L


        LogMaster.L("Hello Start");

        //AssetBundleHelper.instance.LoadFile("Bullet", AssetFileType.Prefab, (obj) =>
        //{
        //    GameObject ins = GameObject.Instantiate(obj);
        //    obj.transform.position = Vector3.zero;
        //});



        // Singleton<UpdateABPipeline>.instance.Test();


        //Func().Coroutine();
    }

    private void Update()
    {
        LogMaster.E("AB  Update");
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