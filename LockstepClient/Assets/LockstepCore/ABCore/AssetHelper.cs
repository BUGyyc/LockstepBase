/*
 * @Author: delevin.ying 
 * @Date: 2023-04-24 10:17:41 
 * @Last Modified by: delevin.ying
 * @Last Modified time: 2023-04-24 10:44:43
 */


using UnityEngine;

namespace ABCore
{
    public class AssetHelper : Singleton<AssetHelper>
    {



        public bool LoadAsset<T>(string path, out T t)
        {
            t = default;
            return false;
        }


        public bool LoadFile(string filePath)
        {
            // var ab = AssetBundle.LoadFromFile(filePath);

            return false;
        }

        public bool AsyncLoadAsset(string path)
        {
            return false;
        }

        public bool AsyncLoadFile(string filePath)
        {
            return false;
        }

        public override void Dispose()
        {
            // throw new System.NotImplementedException();
        }
    }
}