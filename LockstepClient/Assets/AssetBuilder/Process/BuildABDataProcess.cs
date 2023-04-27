///*
// * @Author: delevin.ying 
// * @Date: 2023-04-23 20:14:02 
// * @Last Modified by: delevin.ying
// * @Last Modified time: 2023-04-24 10:46:28
// */

//using UnityEditor;
//using UnityEngine;
//using System.IO;
//using AssetBuilderCore;

//namespace AssetBuilder.Process
//{
//    public sealed class BuildABDataProcess : IABProcess
//    {

//        readonly string AssetBundlesOutputPath = Application.streamingAssetsPath + "/OutPutAB";
//        public void ExecuteProcess()
//        {
//            //OnBuildABDataProcess();
//        }

//        private void OnBuildABDataProcess()
//        {
//            ClearAssetBundlesName();

//            string sourcePath = Application.dataPath + "/Resources";

//            Pack(sourcePath);

//            string outputPath = Path.Combine(AssetBundlesOutputPath, Platform.GetPlatformFolder(EditorUserBuildSettings.activeBuildTarget));
//            if (!Directory.Exists(outputPath))
//            {
//                Directory.CreateDirectory(outputPath);
//            }

//            // 根据BuildSetting里面所激活的平台进行打包 设置过AssetBundleName的都会进行打包
//            BuildPipeline.BuildAssetBundles(outputPath, 0, EditorUserBuildSettings.activeBuildTarget);

//            AssetDatabase.Refresh();

//            Debug.Log("打包完成");
//        }

//        /// <summary>
//        /// 清除之前设置过的AssetBundleName，避免产生不必要的资源也打包
//        /// 之前说过，只要设置了AssetBundleName的，都会进行打包，不论在什么目录下
//        /// </summary>
//        static void ClearAssetBundlesName()
//        {
//            int length = AssetDatabase.GetAllAssetBundleNames().Length;
//            Debug.Log(length);
//            string[] oldAssetBundleNames = new string[length];
//            for (int i = 0; i < length; i++)
//            {
//                oldAssetBundleNames[i] = AssetDatabase.GetAllAssetBundleNames()[i];
//            }

//            for (int j = 0; j < oldAssetBundleNames.Length; j++)
//            {
//                AssetDatabase.RemoveAssetBundleName(oldAssetBundleNames[j], true);
//            }
//            length = AssetDatabase.GetAllAssetBundleNames().Length;
//            Debug.Log(length);
//        }

//        static void Pack(string source)
//        {
//            //Debug.Log("Pack source " + source);
//            DirectoryInfo folder = new DirectoryInfo(source);
//            FileSystemInfo[] files = folder.GetFileSystemInfos();
//            int length = files.Length;
//            for (int i = 0; i < length; i++)
//            {
//                if (files[i] is DirectoryInfo)
//                {
//                    Pack(files[i].FullName);
//                }
//                else
//                {
//                    if (!files[i].Name.EndsWith(".meta"))
//                    {
//                        fileWithDepends(files[i].FullName);
//                    }
//                }
//            }
//        }

//        // //设置要打包的文件
//        static void fileWithDepends(string source)
//        {
//            Debug.Log("file source " + source);
//            string _source = Replace(source);
//            string _assetPath = "Assets" + _source.Substring(Application.dataPath.Length);

//            Debug.Log(_assetPath);

//            //自动获取依赖项并给其资源设置AssetBundleName
//            string[] dps = AssetDatabase.GetDependencies(_assetPath);
//            foreach (var dp in dps)
//            {
//                Debug.Log("dp " + dp);

//                if (dp.EndsWith(".cs")) continue;

//                AssetImporter assetImporter = AssetImporter.GetAtPath(dp);
//                string pathTmp = dp.Substring("Assets".Length + 1);
//                string assetName = pathTmp.Substring(pathTmp.IndexOf("/") + 1);
//                assetName = assetName.Replace(Path.GetExtension(assetName), ".data");
//                Debug.Log(assetName);
//                assetImporter.assetBundleName = assetName;
//            }

//        }

//        static string Replace(string s)
//        {
//            return s.Replace("\\", "/");
//        }
//    }
//}