﻿/*
 * @Author: delevin.ying 
 * @Date: 2023-04-23 17:47:41 
 * @Last Modified by: delevin.ying
 * @Last Modified time: 2023-04-26 17:50:24
 */

using UnityEditor;
using UnityEngine;
using System.IO;

using AssetBuilder.Process;

namespace AssetBuilderCore
{
    public class AssetBuilderWindow : EditorWindow
    {


        [MenuItem("项目工具/打包")]
        private static void ShowWindow()
        {
            var window = GetWindow<AssetBuilderWindow>();
            window.titleContent = new GUIContent("打包总览");
            window.Show();
        }

        /**
         * 1.自动打标签
         * 2.一键生成AB包
         * 3.快速构建 APP
         * 4.首包策略、配置工具
         * 5.资源服务器download AB 包测试
         * 
         * 
         * 
         * **/
        private void OnGUI()
        {
            if (GUILayout.Button("AutoABLabel"))
            {
                AutoCreateABLabel auto = new AutoCreateABLabel();
                auto.ExecuteProcess();
            }

            if (GUILayout.Button("CreateAB"))
            {
                BuildABDataProcess build = new BuildABDataProcess();
                build.ExecuteProcess();
            }

            if (GUILayout.Button("构建APP"))
            {
                BuildApplication();
            }


        }

        /// <summary>
        /// 快速构建首包
        /// 1.通过BuildConfig设置，进行包体的构建
        /// 2.
        /// </summary>
        private void BuildFirstAPK()
        {

        }


        /// <summary>
        /// 一键生成APP
        /// </summary>
        private void BuildApplication()
        {
            string path = "Assets/Resources/BuildApplicationConfig.asset";

            BuildApplicationConfig config = AssetDatabase.LoadAssetAtPath(path, typeof(UnityEngine.Object)) as BuildApplicationConfig;
            PlayerSettings.companyName = config.companyName;
            PlayerSettings.productName = config.appName;

            int versionCode = VersionParse(config.version);

            if (EditorUserBuildSettings.activeBuildTarget.Equals(BuildTarget.Android) == false)
            {
                Debug.Log("未包含Android模块");
                return;
            }

            var buildOp = BuildOptions.CompressWithLz4;

            BuildPlayerOptions buildPlayerOptions;

            //int version = 10001;
            string channel = "tx";
            string floderPath = Application.dataPath.Replace("/Assets", "") + "/OutPutAPP/";

            if (Directory.Exists(floderPath) == false)
            {
                Directory.CreateDirectory(floderPath);
            }

            string appOutPutPath = floderPath + string.Format($"{config.appName}_v{config.version}_c{channel}.apk");

            buildPlayerOptions = new BuildPlayerOptions()
            {
                scenes = new string[]{
                    "Assets/Scenes/Test/ABLaunch.unity"
                },
                locationPathName = appOutPutPath,
                options = buildOp,
                target = BuildTarget.Android,
                targetGroup = BuildTargetGroup.Android
            };

            var buildReport = BuildPipeline.BuildPlayer(buildPlayerOptions);

            if (buildReport.summary.result == UnityEditor.Build.Reporting.BuildResult.Succeeded)
            {
                LogMaster.L($"构建成功 path {appOutPutPath}");

                config.version = SetNextVersion(versionCode);

                AssetDatabase.SaveAssetIfDirty(config);
            }
            else
            {
                LogMaster.E($"构建失败  path {appOutPutPath}");
            }
        }

        private int VersionParse(string version)
        {
            if (string.IsNullOrEmpty(version))
            {
                version = "1";
            }
            version = version.Replace(".", "");
            return int.Parse(version);
        }


        /// <summary>
        /// 版本号自动增加
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        private string SetNextVersion(int version)
        {
            version++;
            if (version >= 9999999)
            {
                LogMaster.E("版本超过限定------");
                return string.Format("0.0.1");
            }

            if (version < 100)
            {
                return string.Format($"0.0.{version}");
            }
            else if (version < 10000)
            {
                return string.Format($"0.{version / 100}.{version % 100}");
            }
            else
            {
                var high = version / 10000;
                var low = version % 10000;
                return string.Format($"{high}.{low / 100}.{low % 100}");
            }
        }





    }








}