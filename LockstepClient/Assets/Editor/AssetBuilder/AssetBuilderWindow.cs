/*
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



        private void BuildApplication()
        {
            string path = "Assets/Resources/BuildApplicationConfig.asset";

            BuildApplicationConfig config = AssetDatabase.LoadAssetAtPath(path, typeof(UnityEngine.Object)) as BuildApplicationConfig;
            PlayerSettings.companyName = config.companyName;
            PlayerSettings.productName = config.appName;

            int version = config.version;


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

            string appOutPutPath = floderPath + string.Format($"my_legend_v{version}_c{channel}.apk");

            buildPlayerOptions = new BuildPlayerOptions()
            {
                scenes = new string[]{
                    "Assets/Scenes/Test/ABLaunch.unity"
                },
                locationPathName = appOutPutPath,
                options = buildOp,
                target = BuildTarget.Android,
                //targetGroup = BuildTargetGroup.Android
            };

            var buildReport = BuildPipeline.BuildPlayer(buildPlayerOptions);

            if (buildReport.summary.result == UnityEditor.Build.Reporting.BuildResult.Succeeded)
            {
                LogMaster.L($"构建成功 path {appOutPutPath}");

                version = SetNextVersion(version);
            }
            else
            {
                //Debug.LogError($"构建失败 path {appOutPutPath}");

                LogMaster.E($"构建失败  path {appOutPutPath}");
            }
        }


        private int SetNextVersion(int version)
        {
            version++;
            if (version >= 9999999)
            {
                LogMaster.E("版本超过限定------");
                return 1;
            }

            return version;
        }





    }








}