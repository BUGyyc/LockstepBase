///*
// * @Author: delevin.ying 
// * @Date: 2023-04-27 11:53:49 
// * @Last Modified by: delevin.ying
// * @Last Modified time: 2023-04-27 14:11:43
// */
//using UnityEngine;
//using System.Collections.Generic;
//using UnityEditor;

//[CreateAssetMenu(menuName = "BuildAPKConfig/Create SubAssetBundleConfig")]
//public class SubAssetBundleConfig : ScriptableObject
//{
//    public string subABName;

//    public int versionCode;

//    public string hashKey;

//    public BuildAssetBundleOptions buildAssetBundleOptions = BuildAssetBundleOptions.UncompressedAssetBundle;


//    [Header("打包资源的所在路径")]
//    public List<string> assetPathList = new List<string>();


//    [Header("子包中包含的场景")]
//    public List<SceneAsset> sceneList = new List<SceneAsset>();
//}