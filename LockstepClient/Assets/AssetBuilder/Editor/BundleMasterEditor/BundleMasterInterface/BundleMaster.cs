/*
 * @Author: delevin.ying
 * @Date: 2023-04-27 17:53:52
 * @Last Modified by: delevin.ying
 * @Last Modified time: 2023-04-27 17:55:11
 */
using UnityEditor;
using UnityEngine;
using System.IO;
using BM;

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

    public static string SetNextVersion(string str)
    {
        var codeStr = string.Copy(str);
        codeStr = codeStr.Replace(".", "");
        var version = int.Parse(codeStr);
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

    private static bool CheckBuildTargetSetting(AssetsOriginSetting originSetting)
    {
        var activeTarget = EditorUserBuildSettings.activeBuildTarget;
        switch (originSetting.buildTarget)
        {
            case BuildTarget.StandaloneWindows:
            case BuildTarget.StandaloneWindows64:
                if (activeTarget.Equals(BuildTarget.StandaloneWindows) == false && activeTarget.Equals(BuildTarget.StandaloneWindows64) == false)
                {
                    LogMaster.E("未包含Windows模块");
                    return false;
                }
                break;
            case BuildTarget.Android:
                if (activeTarget.Equals(BuildTarget.Android) == false)
                {
                    LogMaster.E("不包含Android");
                    return false;
                }
                break;
            case BuildTarget.iOS:
                if (activeTarget.Equals(BuildTarget.iOS) == false)
                {
                    LogMaster.E("不包含iOS");
                    return false;
                }
                break;
            default:
                LogMaster.E("不确定平台");
                return false;

        }
        return true;
    }

    private static string GetTargetFloder(AssetsOriginSetting originSetting, string appName, string versionCode, string channel)
    {
        string floderPath = Application.dataPath.Replace("/Assets", "") + "/OutPutAPP/";
        switch (originSetting.buildTarget)
        {
            case BuildTarget.StandaloneWindows:
            case BuildTarget.StandaloneWindows64:
                return floderPath + string.Format($"Windows/{appName}_V{versionCode}_C{channel}/");
            case BuildTarget.Android:
                return floderPath + "Android/";
            case BuildTarget.iOS:
                return floderPath + "IOS/";
            default:
                LogMaster.E("不确定平台");
                return string.Empty;
        }
    }

    private static string GetTargetFile(AssetsOriginSetting originSetting, string appName, string versionCode, string channel)
    {
        switch (originSetting.buildTarget)
        {
            case BuildTarget.StandaloneWindows:
            case BuildTarget.StandaloneWindows64:
                return string.Format($"{appName}_v{versionCode}_c{channel}.exe");
            case BuildTarget.Android:
                return string.Format($"{appName}_v{versionCode}_c{channel}.apk");
            default:
                LogMaster.E("不确定平台");
                return string.Empty;
        }
    }


    public static void BuildAPP()
    {
        AssetsOriginSetting originSetting =
           AssetDatabase.LoadAssetAtPath(BundleMasterWindow.AssetsOriginSettingPath, typeof(UnityEngine.Object))
           as AssetsOriginSetting;

        if (CheckBuildTargetSetting(originSetting) == false) return;

        //PlayerSettings.companyName = originSetting.name;
        PlayerSettings.productName = originSetting.BuildName;
        string appName = originSetting.BuildName;
        string versionCode = originSetting.VersionCode;

        if (string.IsNullOrEmpty(versionCode)) { versionCode = "0.0.1"; }

        var buildOp = BuildOptions.CompressWithLz4;

        BuildPlayerOptions buildPlayerOptions;

        string channel = "tx";
        string floderPath = GetTargetFloder(originSetting, appName, versionCode, channel);

        if (Directory.Exists(floderPath) == false)
        {
            Directory.CreateDirectory(floderPath);
        }

        string appOutPutPath =
            floderPath + GetTargetFile(originSetting, appName, versionCode, channel);

        buildPlayerOptions = new BuildPlayerOptions()
        {
            scenes = new string[] { "Assets/Scenes/Test/ABLaunch.unity" },
            locationPathName = appOutPutPath,
            options = buildOp,
            target = originSetting.buildTarget
            //targetGroup = BuildTargetGroup.Standalone
        };

        var buildReport = BuildPipeline.BuildPlayer(buildPlayerOptions);


        if (buildReport.summary.result == UnityEditor.Build.Reporting.BuildResult.Succeeded)
        {
            LogMaster.L($"构建成功 path {appOutPutPath}");
            versionCode = SetNextVersion(versionCode);
            originSetting.VersionCode = versionCode;
            AssetDatabase.SaveAssetIfDirty(originSetting);
        }
        else
        {
            LogMaster.E($"构建失败  path {appOutPutPath}");
        }
    }

    public static void BuildWindowsAPP()
    {
        if (EditorUserBuildSettings.activeBuildTarget.Equals(BuildTarget.StandaloneWindows) == false && EditorUserBuildSettings.activeBuildTarget.Equals(BuildTarget.StandaloneWindows64) == false)
        {
            LogMaster.E("未包含Windows模块");
            return;
        }

        AssetsOriginSetting originSetting =
            AssetDatabase.LoadAssetAtPath(BundleMasterWindow.AssetsOriginSettingPath, typeof(UnityEngine.Object))
            as AssetsOriginSetting;
        //PlayerSettings.companyName = originSetting.name;
        PlayerSettings.productName = originSetting.BuildName;
        string appName = originSetting.BuildName;
        string versionCode = originSetting.VersionCode;

        if (string.IsNullOrEmpty(versionCode)) { versionCode = "0.0.1"; }

        var buildOp = BuildOptions.CompressWithLz4;

        BuildPlayerOptions buildPlayerOptions;

        string channel = "tx";
        string floderPath = Application.dataPath.Replace("/Assets", "") + "/OutPutAPP/Windows/";

        if (Directory.Exists(floderPath) == false)
        {
            Directory.CreateDirectory(floderPath);
        }

        string appOutPutPath =
            floderPath + string.Format($"{appName}_v{versionCode}_c{channel}.exe");

        buildPlayerOptions = new BuildPlayerOptions()
        {
            scenes = new string[] { "Assets/Scenes/Test/ABLaunch.unity" },
            locationPathName = appOutPutPath,
            options = buildOp,
            target = BuildTarget.StandaloneWindows,
            targetGroup = BuildTargetGroup.Standalone
        };

        var buildReport = BuildPipeline.BuildPlayer(buildPlayerOptions);


        if (buildReport.summary.result == UnityEditor.Build.Reporting.BuildResult.Succeeded)
        {
            LogMaster.L($"构建成功 path {appOutPutPath}");
            versionCode = SetNextVersion(versionCode);
            originSetting.VersionCode = versionCode;
            AssetDatabase.SaveAssetIfDirty(originSetting);
        }
        else
        {
            LogMaster.E($"构建失败  path {appOutPutPath}");
        }
    }
}
