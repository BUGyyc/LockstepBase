using UnityEngine;
using System;
using System.IO;
using ABCore;
namespace ET.Client
{
    public class AssetBundleExample : MonoBehaviour
    {
        void Start()
        {
            // AssetBundleHelper.instance.L


            AssetBundleHelper.instance.LoadFile("Bullet", AssetFileType.Prefab, (obj) =>
            {
                GameObject ins = GameObject.Instantiate(obj);
                obj.transform.position = Vector3.zero;
            });
        }


    }
}