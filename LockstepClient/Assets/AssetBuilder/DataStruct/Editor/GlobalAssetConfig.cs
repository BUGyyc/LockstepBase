/*
 * @Author: delevin.ying 
 * @Date: 2023-04-27 11:53:45 
 * @Last Modified by: delevin.ying
 * @Last Modified time: 2023-04-27 14:13:24
 */
using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

[CreateAssetMenu(menuName = "BuildAPKConfig/Create GlobalAssetConfig")]
public class GlobalAssetConfig : ScriptableObject
{
    public string path = "";

    [Header("初始场景，打进包体内（其余场景，会被打进子包内，这样可以保证首包特别小）")]
    public List<SceneAsset> OriginSceneList;


    public List<SubAssetBundleConfig> subABCfgList;
}