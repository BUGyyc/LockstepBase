using System.Collections;
using UnityEngine;

using ET;

namespace ABCore
{
    public partial class AssetBundleHelper
    {

        public async ETTask<T> LoadAsync<T>(string abPath, string fileName = null) where T : UnityEngine.Object
        {
            if (ABDic.TryGetValue(abPath, out AssetBundle ab))
            {
                var obj = ab.LoadAsset<T>(fileName);
                return obj;
            }
            else
            {
                await AsyncLoadAssetBundle<AssetBundle>(abPath);
            }
            return null;
        }

        public async ETTask<UnityEngine.AssetBundle> AsyncLoadAssetBundle<AssetBundle>(string abName)
        {
            string[] deps = GetDependencies(abName);

            foreach (string dependency in deps)
            {
                Debug.Log($"[AssetBundle] 需要依赖包 , 开始加载 {dependency} ...");
                AsyncLoadAssetBundle<AssetBundle>(dependency);
            }

            if (!ABDic.TryGetValue(abName, out UnityEngine.AssetBundle ab))
            {
                return ab;
            }
            else
            {
                var loadAB = UnityEngine.AssetBundle.LoadFromFile(abName);
                ABDic.Add(abName, loadAB);

                return loadAB;
            }

            //return null;
        }

        //public
    }
}