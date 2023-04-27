using System.Collections;
using UnityEngine;

using ET;

namespace ABCore
{
    public partial class AssetBundleHelper
    {

        public async ETTask<T> AsyncLoad<T>(string abPath, string fileName = null) where T : UnityEngine.Object
        {
            if (ABDic.TryGetValue(abPath, out AssetBundle ab))
            {
                var obj = ab.LoadAsset<T>(fileName);
                return obj;
            }
            else
            {
                var loadAb = await AsyncLoadAssetBundle<AssetBundle>(abPath);
                if (loadAb != null)
                {
                    ABDic.Add(abPath, loadAb);
                    var obj = loadAb.LoadAsset<T>(fileName);
                    return obj;
                }
            }
            return null;
        }

        public async ETTask<UnityEngine.AssetBundle> AsyncLoadAssetBundle<AssetBundle>(string abName)
        {
            Debug.Log($"[AssetBundle] 包 {abName} ...");
            string[] deps = GetDependencies(abName);

            foreach (string dependency in deps)
            {
                Debug.Log($"[AssetBundle] 需要依赖包 , 开始加载 {dependency} ...");
                await AsyncLoadAssetBundle<AssetBundle>(dependency);
            }

            if (!ABDic.TryGetValue(abName, out UnityEngine.AssetBundle ab))
            {
                return ab;
            }
            else
            {
                Debug.Log($"[AssetBundle] 包开始加载 {abName} ...");
                var loadAB = UnityEngine.AssetBundle.LoadFromFile(abName);


                ABDic.Add(abName, loadAB);

                return loadAB;
            }

            //return null;
        }

        //public
    }
}