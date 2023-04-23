

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AssetBuilderCore
{

    public class PathTools
    {
        /* 路径常量 */
        public const string AB_RESOURCES = "Resources";  // 打包AB包根路径

        /* 路径方法 */

        /// <summary>
        /// 得到 AB 资源的输入目录
        /// </summary>
        /// <returns></returns>
        public static string GetABResourcesPath()
        {
            return Application.dataPath + "/" + AB_RESOURCES;
        }

        /// <summary>
        /// 获得 AB 包输出路径
        ///     1\ 平台(PC/移动端等)路径
        ///     2\ 平台名称
        /// </summary>
        /// <returns></returns>
        public static string GetABOutPath()
        {
            return GetPlatformPath() + "/" + GetPlatformName();
        }

        /// <summary>
        /// 获得平台路径
        /// </summary>
        /// <returns></returns>
        private static string GetPlatformPath()
        {

            string strReturenPlatformPath = string.Empty;

            switch (Application.platform)
            {

                case RuntimePlatform.WindowsPlayer:
                case RuntimePlatform.WindowsEditor:
                    strReturenPlatformPath = Application.streamingAssetsPath;

                    break;
                case RuntimePlatform.IPhonePlayer:
                case RuntimePlatform.Android:
                    strReturenPlatformPath = Application.persistentDataPath;

                    break;

                default:
                    break;
            }

            return strReturenPlatformPath;
        }

        /// <summary>
        /// 获得平台名称
        /// </summary>
        /// <returns></returns>
        public static string GetPlatformName()
        {
            string strReturenPlatformName = string.Empty;

            switch (Application.platform)
            {

                case RuntimePlatform.WindowsPlayer:
                case RuntimePlatform.WindowsEditor:
                    strReturenPlatformName = "Windows";

                    break;
                case RuntimePlatform.IPhonePlayer:
                    strReturenPlatformName = "IPhone";

                    break;
                case RuntimePlatform.Android:
                    strReturenPlatformName = "Android";

                    break;

                default:
                    break;
            }

            return strReturenPlatformName;
        }

        /// <summary>
        /// 返回 WWW 下载 AB 包加载路径
        /// </summary>
        /// <returns></returns>
        public static string GetWWWAssetBundlePath()
        {
            string strReturnWWWPath = string.Empty;

            switch (Application.platform)
            {

                case RuntimePlatform.WindowsPlayer:
                case RuntimePlatform.WindowsEditor:
                    strReturnWWWPath = "file://" + GetABOutPath();

                    break;
                case RuntimePlatform.IPhonePlayer:
                    strReturnWWWPath = GetABOutPath() + "/Raw/";

                    break;

                case RuntimePlatform.Android:
                    strReturnWWWPath = "jar:file://" + GetABOutPath();

                    break;

                default:
                    break;
            }

            return strReturnWWWPath;
        }

    }//Class_End
}