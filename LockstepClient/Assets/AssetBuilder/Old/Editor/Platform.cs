/*
 * @Author: delevin.ying 
 * @Date: 2023-04-23 19:42:04 
 * @Last Modified by: delevin.ying
 * @Last Modified time: 2023-04-23 19:56:33
 */
using UnityEngine;
using UnityEditor;

namespace AssetBuilderCore
{

    public class Platform
    {
        public static string GetPlatformFolder(BuildTarget target)
        {
            switch (target)
            {
                case BuildTarget.Android:
                    return AssetBuilderSetting.Android;
                case BuildTarget.iOS:
                    return AssetBuilderSetting.IOS;
                case BuildTarget.StandaloneWindows:
                case BuildTarget.StandaloneWindows64:
                    return AssetBuilderSetting.Windows;
                default:
                    return null;
            }
        }
    }
}