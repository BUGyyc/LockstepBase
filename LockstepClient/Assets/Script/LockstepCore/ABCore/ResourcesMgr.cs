/*
 * @Author: delevin.ying 
 * @Date: 2023-04-24 14:33:53 
 * @Last Modified by: delevin.ying
 * @Last Modified time: 2023-04-24 15:38:41
 */

using System.Collections.Generic;
using UnityEngine;


public class ResourcesMgr
{
   

    private static ResourcesMgr _instance;

    public static ResourcesMgr Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new ResourcesMgr();
            }
            return _instance;
        }
    }

    public AssetBundleManifest AssetBundleManifestObject { get; set; }

    public Dictionary<int, string> IntToStringDict = new Dictionary<int, string>();

    public Dictionary<string, string> StringToABDict = new Dictionary<string, string>();

    public Dictionary<string, string> BundleNameToLowerDict = new Dictionary<string, string>() { { "StreamingAssets", "StreamingAssets" } };

    public readonly Dictionary<string, Dictionary<string, UnityEngine.Object>> resourceCache =
            new Dictionary<string, Dictionary<string, UnityEngine.Object>>();

    // public readonly Dictionary<string, ABInfo> bundles = new Dictionary<string, ABInfo>();

    // 缓存包依赖，不用每次计算
    public readonly Dictionary<string, string[]> DependenciesCache = new Dictionary<string, string[]>();
}