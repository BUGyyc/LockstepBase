/*
 * @Author: delevin.ying 
 * @Date: 2023-04-24 10:17:41 
 * @Last Modified by: delevin.ying
 * @Last Modified time: 2023-04-25 17:54:26
 */


using UnityEngine;
using ET.Client;
using System;
using System.IO;
using ET;
using System.Collections.Generic;
using System.Linq;

namespace ABCore
{
    public partial class AssetBundleHelper : Singleton<AssetBundleHelper>, IAssetBundle
    {

        private ABLoadType loadType = ABLoadType.FromLocalAssetBundle;

        public AssetBundleManifest AssetBundleManifestCoreData;

        public Dictionary<string, AssetBundle> ABDic;

        public Dictionary<string, string[]> ABDependencies;

        private GameObject ABHookObj;

        private ABHookScript abHook;

        public ABHookScript ABHook
        {
            get
            {
                if (abHook == null)
                {
                    if (ABHookObj == null)
                    {
                        ABHookObj = new GameObject();
                        ABHookObj.name = "ABHookObj";
                        GameObject.DontDestroyOnLoad(ABHookObj);
                        abHook = ABHookObj.AddComponent<ABHookScript>();
                    }
                    else if (ABHookObj.GetComponent<ABHookScript>() == null)
                    {
                        abHook = ABHookObj.AddComponent<ABHookScript>();
                    }
                    else
                    {
                        abHook = ABHookObj.GetComponent<ABHookScript>();
                    }
                }
                return abHook;
            }
        }


        public readonly Dictionary<AssetFileType, string> FilePathDefine = new Dictionary<AssetFileType, string>()
        {
            {AssetFileType.Prefab,"prefabs/"},
            {AssetFileType.Audio,"audio/"},
            {AssetFileType.Texture,"texture/"},
            {AssetFileType.Mat,"mat/"},
            {AssetFileType.Mesh,"mesh/"},
        };

        public override void Init()
        {
            base.Init();

            ABDic = new Dictionary<string, AssetBundle>();
            ABDependencies = new Dictionary<string, string[]>();

            AssetBundle ab = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/OutPutAB/windows/windows");

            AssetBundleManifestCoreData = ab.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        }

        public override void Dispose()
        {
            // throw new NotImplementedException();
        }

        public void LoadFile(string fileName, AssetFileType type, Action<GameObject> call)
        {

            string filePath = LoadFilePath(fileName, type);
            switch (loadType)
            {
                case ABLoadType.FromResource:
                    LoadFileFromResource(filePath, call);
                    break;
                case ABLoadType.FromLocalAssetBundle:
                    LoadFileFromAssetBundle(filePath, type, call);
                    break;
                case ABLoadType.FromSimulationAssetBundle: break;
                case ABLoadType.Release: break;

                default: break;
            }

            //if (loadType == ABLoadType.FRom)
        }

        private string LoadFilePath(string fileName, AssetFileType type)
        {
            switch (loadType)
            {
                case ABLoadType.FromResource:
                    switch (type)
                    {
                        case AssetFileType.Prefab:
                            return "prefabs/" + fileName;
                        case AssetFileType.Mat:
                            return "mat/" + fileName;
                        //case AssetFileType.Mesh
                        default:
                            return string.Empty;
                            //break;
                    }
                //    break;
                //case ABLoadType.FromLocalAssetBundle:
                //    LoadFile(fileName, type, call);
                //    break;
                //case ABLoadType.FromSimulationAssetBundle: break;
                //case ABLoadType.Release: break;

                default: return fileName;
            }
        }


        private void LoadFileFromResource(string fileName, Action<GameObject> call)
        {
            var obj = Resources.Load<GameObject>(fileName);
            call?.Invoke(obj);
        }



        private void LoadFileFromAssetBundle(string fileName, AssetFileType type, Action<GameObject> call)
        {




            if (FilePathDefine.TryGetValue(type, out string path))
            {
                string abPath = path + fileName + ".data.ab";
                LoadFileFromAssetBundle(abPath, fileName, call);
            }
            else
            {
                Debug.LogError("[AssetBundle]  路径未定义");
            }
        }




        private void LoadFileFromAssetBundle(string abName, string fileName, Action<GameObject> call = null)
        {
            if (ABDic.TryGetValue(abName, out AssetBundle ab))
            {
                GameObject obj = ab.LoadAsset<GameObject>(fileName);
                call?.Invoke(obj);
            }
            else
            {
                AssetBundle loadAb = LoadAssetBundle(abName);
                GameObject obj = loadAb.LoadAsset<GameObject>(fileName);
                call?.Invoke(obj);
            }
        }

        private AssetBundle LoadAssetBundle(string abName)
        {
            string fullPath = Application.streamingAssetsPath + "/OutPutAB/windows/" + abName;

            string[] deps = GetDependencies(abName);

            foreach (string dependency in deps)
            {
                Debug.Log($"[AssetBundle] 需要依赖包 , 开始加载 {dependency} ...");
                LoadAssetBundle(dependency);
            }


            var loadAb = AssetBundle.LoadFromFile(fullPath);

            Debug.Log($"[AssetBundle]  {fullPath} 加载完成");

            return loadAb;
        }


        //public Coroutine AsyncLoadAssetBundle(string path, Action<AssetBundle> call)
        //{
        //    // var ab = AssetBundle.LoadFromFileAsync(path);


        //    return null;
        //}


        public void UnLoadAll()
        {
            if (ABDic == null) return;
            if (ABDic.Count == 0) return;

            var keys = ABDic.Keys;

            for (var i = 0; i < keys.Count; i++)
            {
                // var ab = CacheABDic[keys[i]];
                // AssetBundle.Destroy();
            }

        }

        private string[] GetDependencies(string abName)
        {
            string[] dependencies = Array.Empty<string>();
            if (ResourcesMgr.Instance.DependenciesCache.TryGetValue(abName, out dependencies))
            {
                if (dependencies.Length > 0)
                {
                    Debug.Log($" {abName} 发现依赖包  " + dependencies);
                }

                return dependencies;
            }

            dependencies = AssetBundleManifestCoreData.GetAllDependencies(abName);

            if (dependencies.Length > 0)
            {
                Debug.Log($" {abName} 发现依赖包  " + dependencies);
            }

            ResourcesMgr.Instance.DependenciesCache.Add(abName, dependencies);

            return dependencies;
        }

        //TODO: 协程加载

        // private IEnumerator LoadAssetAsync(string path)
        // {
        //     // yield return 
        // }





        //TODO: ET Task 多线程加载


        // public async ETTask AsyncAssetBundle(string abName)
        // {
        //     string fullPath = Application.streamingAssetsPath + "/OutPutAB/windows/" + abName;

        //     string[] deps = GetDependencies(abName);

        //     foreach (string dependency in deps)
        //     {
        //         Debug.Log($"[AssetBundle] 需要依赖包 , 开始加载 {dependency} ...");


        //         LoadAssetBundle(dependency);
        //     }


        //     var loadAb = AssetBundle.LoadFromFile(fullPath);

        //     Debug.Log($"[AssetBundle]  {fullPath} 加载完成");

        //     // return loadAb;
        // }


        // public static string BundleNameToLower(this string value)
        // {
        //     string result;
        //     if (ResourcesMgr.Instance.BundleNameToLowerDict.TryGetValue(value, out result))
        //     {
        //         return result;
        //     }

        //     result = value.ToLower();
        //     ResourcesMgr.Instance.BundleNameToLowerDict[value] = result;
        //     return result;
        // }


        // private static string[] GetSortedDependencies(string assetBundleName)
        // {
        //     var info = new Dictionary<string, int>();
        //     var parents = new List<string>();
        //     CollectDependencies(parents, assetBundleName, info);
        //     string[] ss = info.OrderBy(x => x.Value).Select(x => x.Key).ToArray();
        //     return ss;
        // }

        /// <summary>
        /// 异步加载assetbundle, 加载ab包分两部分，第一部分是从硬盘加载，第二部分加载all assets。两者不能同时并发
        /// </summary>
        // public static async ETTask LoadBundleAsync(string assetBundleName)
        // {
        // assetBundleName = assetBundleName.BundleNameToLower();

        // string[] dependencies = GetSortedDependencies(assetBundleName);


        // using (ListComponent<ABInfo> abInfos = ListComponent<ABInfo>.Create())
        // {
        //     async ETTask LoadDependency(string dependency, List<ABInfo> abInfosList)
        //     {
        //         using CoroutineLock coroutineLock = await CoroutineLockComponent.Instance.Wait(CoroutineLockType.Resources, dependency.GetHashCode());

        //         ABInfo abInfo = await self.LoadOneBundleAsync(dependency);
        //         if (abInfo == null || abInfo.RefCount > 1)
        //         {
        //             return;
        //         }

        //         abInfosList.Add(abInfo);
        //     }

        //     // LoadFromFileAsync部分可以并发加载
        //     using (ListComponent<ETTask> tasks = ListComponent<ETTask>.Create())
        //     {
        //         foreach (string dependency in dependencies)
        //         {
        //             tasks.Add(LoadDependency(dependency, abInfos));
        //         }
        //         await ETTaskHelper.WaitAll(tasks);

        //         // ab包从硬盘加载完成，可以再并发加载all assets
        //         tasks.Clear();
        //         foreach (ABInfo abInfo in abInfos)
        //         {
        //             tasks.Add(self.LoadOneBundleAllAssets(abInfo));
        //         }
        //         await ETTaskHelper.WaitAll(tasks);
        //     }
        // }
        // }

        // private static async ETTask<ABInfo> LoadOneBundleAsync(this ResourcesComponent self, string assetBundleName)
        // {
        //     assetBundleName = assetBundleName.BundleNameToLower();
        //     ABInfo abInfo;
        //     if (self.bundles.TryGetValue(assetBundleName, out abInfo))
        //     {
        //         ++abInfo.RefCount;
        //         //Log.Debug($"---------------load one bundle {assetBundleName} refcount: {abInfo.RefCount}");
        //         return null;
        //     }
        //     string p = "";
        //     AssetBundle assetBundle = null;

        //     if (!Define.IsAsync)
        //     {
        //         if (Define.IsEditor)
        //         {
        //             string[] realPath = Define.GetAssetPathsFromAssetBundle(assetBundleName);
        //             foreach (string s in realPath)
        //             {
        //                 string assetName = Path.GetFileNameWithoutExtension(s);
        //                 UnityEngine.Object resource = Define.LoadAssetAtPath(s);
        //                 self.AddResource(assetBundleName, assetName, resource);
        //             }

        //             if (realPath.Length > 0)
        //             {
        //                 abInfo = self.AddChild<ABInfo, string, AssetBundle>(assetBundleName, null);
        //                 self.bundles[assetBundleName] = abInfo;
        //                 //Log.Debug($"---------------load one bundle {assetBundleName} refcount: {abInfo.RefCount}");
        //             }
        //             else
        //             {
        //                 Log.Error("Bundle not exist! BundleName: " + assetBundleName);
        //             }

        //             // 编辑器模式也不能同步加载
        //             await TimerComponent.Instance.WaitAsync(100);

        //             return abInfo;
        //         }
        //     }
        //     p = Path.Combine(PathHelper.AppHotfixResPath, assetBundleName);
        //     if (!File.Exists(p))
        //     {
        //         p = Path.Combine(PathHelper.AppResPath, assetBundleName);
        //     }
        //     Log.Debug("Async load bundle BundleName : " + p);

        //     // if (!File.Exists(p))
        //     // {
        //     //     Log.Error("Async load bundle not exist! BundleName : " + p);
        //     //     return null;
        //     // }
        //     AssetBundleCreateRequest assetBundleCreateRequest = AssetBundle.LoadFromFileAsync(p);
        //     await assetBundleCreateRequest;
        //     assetBundle = assetBundleCreateRequest.assetBundle;
        //     if (assetBundle == null)
        //     {
        //         // 获取资源的时候会抛异常，这个地方不直接抛异常，因为有些地方需要Load之后判断是否Load成功
        //         Log.Warning($"assets bundle not found: {assetBundleName}");
        //         return null;
        //     }
        //     abInfo = self.AddChild<ABInfo, string, AssetBundle>(assetBundleName, assetBundle);
        //     self.bundles[assetBundleName] = abInfo;
        //     return abInfo;
        //     //Log.Debug($"---------------load one bundle {assetBundleName} refcount: {abInfo.RefCount}");
        // }

        // // 加载ab包中的all assets
        // private static async ETTask LoadOneBundleAllAssets(this ResourcesComponent self, ABInfo abInfo)
        // {
        //     using CoroutineLock coroutineLock = await CoroutineLockComponent.Instance.Wait(CoroutineLockType.Resources, abInfo.Name.GetHashCode());

        //     if (abInfo.IsDisposed || abInfo.AlreadyLoadAssets)
        //     {
        //         return;
        //     }

        //     if (abInfo.AssetBundle != null && !abInfo.AssetBundle.isStreamedSceneAssetBundle)
        //     {
        //         // 异步load资源到内存cache住
        //         AssetBundleRequest request = abInfo.AssetBundle.LoadAllAssetsAsync();
        //         await request;
        //         UnityEngine.Object[] assets = request.allAssets;

        //         foreach (UnityEngine.Object asset in assets)
        //         {
        //             self.AddResource(abInfo.Name, asset.name, asset);
        //         }
        //     }

        //     abInfo.AlreadyLoadAssets = true;
        // }

    }
}