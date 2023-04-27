/*
 * @Author: delevin.ying
 * @Date: 2023-04-27 17:53:52
 * @Last Modified by: delevin.ying
 * @Last Modified time: 2023-04-27 17:55:11
 */
using UnityEditor;
using UnityEngine;
using System.IO;

public static class BundleMaster
{
    public static string SetNextVersion(int version)
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

    public static int VersionParse(string version)
    {
        if (string.IsNullOrEmpty(version))
        {
            version = "1";
        }
        version = version.Replace(".", "");
        return int.Parse(version);
    }

    public static void BuildApplication()
    {
        // string path = "Assets/Resources/BuildApplicationConfig.asset";

        // BuildApplicationConfig config =
        //     AssetDatabase.LoadAssetAtPath(path, typeof(UnityEngine.Object))
        //     as BuildApplicationConfig;
        // PlayerSettings.companyName = config.companyName;
        // PlayerSettings.productName = config.appName;

        //int versionCode = VersionParse(config.version);

        string appName = "AutoLegend";
        string versionCode = "0.0.1";

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

        string appOutPutPath =
            floderPath + string.Format($"{appName}_v{versionCode}_c{channel}.apk");

        buildPlayerOptions = new BuildPlayerOptions()
        {
            scenes = new string[] { "Assets/Scenes/Test/ABLaunch.unity" },
            locationPathName = appOutPutPath,
            options = buildOp,
            target = BuildTarget.Android,
            targetGroup = BuildTargetGroup.Android
        };

        var buildReport = BuildPipeline.BuildPlayer(buildPlayerOptions);

        if (buildReport.summary.result == UnityEditor.Build.Reporting.BuildResult.Succeeded)
        {
            LogMaster.L($"构建成功 path {appOutPutPath}");

            //config.version = SetNextVersion(versionCode);

            // AssetDatabase.SaveAssetIfDirty(config);
        }
        else
        {
            LogMaster.E($"构建失败  path {appOutPutPath}");
        }
    }
}
