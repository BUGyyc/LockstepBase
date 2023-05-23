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
using System.Collections.Generic;
using UnityEngine.U2D;
using UnityEditor.U2D;
//using UnityEngine.Windows;

public static class BundleMaster
{
    private const int MAX_SPRITE_SIZE = 1024;

    private static Dictionary<string, AtlasData> atlasDataDic = new Dictionary<string, AtlasData>();

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



    //const string SpritePath = "Assets/Art/UI/";
    //public static void BuildAtlas()
    //{
    //    string[] directories = System.IO.Directory.GetDirectories(SpritePath);

    //    //IDirectory tmpSourceDir;
    //    for (int i = 0; i < directories.Length; i++)
    //    {
    //        string dirName = directories[i].Substring(directories[i].LastIndexOf(@"\") + 1);
    //        BuildAtlasFromDirectory(directories[i], dirName);
    //    }

    //    AssetDatabase.Refresh();
    //}

    //private static void BuildAtlasFromDirectory(string dir, string dirName)
    //{
    //    Debug.Log(string.Format($" Building <color=#7BE578> {dirName} </color> atlas .."));

    //    string atlasName = dirName + "_atlas" + ".spriteatlas";

    //    SpriteAtlas atlas = new SpriteAtlas();
    //    atlas.SetPackingSettings(packSetting);
    //    atlas.SetTextureSettings(textureSetting);
    //    atlas.SetPlatformSettings(importerSetting);


    //    IFile[] tmpFile = directory.GetFiles();
    //    Sprite sprite;
    //    List<Sprite> spriteList = new List<Sprite>();
    //    string assetPath = string.Empty;
    //    for (int i = 0; i < tmpFile.Length; i++)
    //    {
    //        if (tmpFile[i].Extension.Contains(".meta"))
    //        {
    //            continue;
    //        }
    //        EditorUtility.DisplayProgressBar(string.Format($"Start Build {dirName} atlas .."), tmpFile[i].Name, i / tmpFile.Length);

    //        assetPath = "Assets/ArtAssets/ui/sprites/" + dirName + "/" + tmpFile[i].Name;
    //        sprite = AssetDatabase.LoadAssetAtPath<Sprite>(assetPath);
    //        if (sprite != null)
    //        {
    //            spriteList.Add(sprite);
    //        }
    //    }
    //    if (spriteList.Count > 0) atlas.Add(spriteList.ToArray());

    //    string dir = "Assets/ABAssets/AssetBundle/ui/prefabs/" + dirName;
    //    string filePath = dir + "/" + atlasName;
    //    if (!Directory.Exists(dir))
    //    {
    //        Directory.CreateDirectory(dir);
    //    }

    //    AssetDatabase.CreateAsset(atlas, filePath);
    //    AssetDatabase.SaveAssets();
    //    EditorUtility.ClearProgressBar();
    //}



    public static void GenerateSpriteAtlas()
    {
        atlasDataDic.Clear();
        CheckAssetFile("Assets/Art/UI/icon/");
        string spriteAtlasPath = "Assets/Bundles/UI/icon/";
        foreach (var item in atlasDataDic)
        {
            SpriteAtlas atlas = new SpriteAtlas();
            SetUpAtlasInfo(ref atlas);
            atlas.Add(item.Value.sprites.ToArray());

            if (Directory.Exists(spriteAtlasPath) == false)
            {
                Directory.CreateDirectory(spriteAtlasPath);
            }

            AssetDatabase.CreateAsset(atlas, spriteAtlasPath + item.Value.atlasName + ".spriteatlas");
            AssetDatabase.SaveAssets();
        }
    }

    /// <summary>
    /// 设定图集参数
    /// </summary>
    /// <param name="atlas"></param>
    public static void SetUpAtlasInfo(ref SpriteAtlas atlas)
    {
        atlas.SetIncludeInBuild(false);
        //A区域参数设定
        SpriteAtlasPackingSettings packSetting = new SpriteAtlasPackingSettings()
        {
            blockOffset = 1,
            enableRotation = false,
            enableTightPacking = false,
            padding = 2,
        };
        atlas.SetPackingSettings(packSetting);
        //B区域参数设定
        SpriteAtlasTextureSettings textureSetting = new SpriteAtlasTextureSettings()
        {
            readable = false,
            generateMipMaps = false,
            sRGB = true,
            filterMode = FilterMode.Bilinear,
        };
        atlas.SetTextureSettings(textureSetting);
        //C区域参数设定
        TextureImporterPlatformSettings platformSetting = new TextureImporterPlatformSettings()
        {
            maxTextureSize = (int)MAX_SPRITE_SIZE,
            format = TextureImporterFormat.Automatic,
            crunchedCompression = true,
            textureCompression = TextureImporterCompression.Compressed,
            compressionQuality = 50,
        };
        atlas.SetPlatformSettings(platformSetting);
    }

    public static void CheckAssetFile(string relativePath)
    {
        List<Sprite> sprites = GetFileSprites(relativePath);
        if (sprites != null && sprites.Count > 1)
        {
            string atlasname = GetAtlasNameFromPath(relativePath);
            string atlasPath = relativePath + atlasname;
            CreateSpriteAtlas(atlasname, atlasPath, sprites);
        }
        else
        {
            Debug.LogError($" {relativePath} 目录下没有Sprite");
        }

        DirectoryInfo direction = new DirectoryInfo(relativePath);
        if (direction == null) return;

        DirectoryInfo[] dirChild = direction.GetDirectories();
        foreach (var item in dirChild)
        {
            CheckAssetFile(relativePath + item.Name + "/");
        }
    }

    private static string GetAtlasNameFromPath(string path)
    {
        string[] strs = path.Split('/');
        var name = strs[strs.Length - 2];
        Debug.Log("path name " + name);
        return name;//path.Replace("/", "_");
    }

    private static void CreateSpriteAtlas(string atlasname, string atlasPath, List<Sprite> sprites)
    {
        if (atlasDataDic.ContainsKey(atlasPath))
        {
            Debug.LogError("警告，有相同名字的Sprite资源文件夹！！！");
            return;
        }
        AtlasData data = new AtlasData()
        {
            atlasName = atlasname.Replace(".asset", ""),
            assetPath = atlasPath,
            sprites = sprites
        };
        atlasDataDic.Add(atlasPath, data);
    }


    public static List<Sprite> GetFileSprites(string relativePath)
    {
        if (Directory.Exists(relativePath))
        {
            DirectoryInfo direction = new DirectoryInfo(relativePath);
            FileInfo[] files = direction.GetFiles("*");//只查找本文件夹下
            if (files == null) return null;

            List<Sprite> sprites = new List<Sprite>();
            foreach (var file in files)
            {
                if (file.Name.EndsWith(".meta")) continue;
                var item = AssetDatabase.LoadAssetAtPath<Sprite>(relativePath + file.Name);
                if (item != null && ChackSpritePackerState(item))
                {
                    sprites.Add((Sprite)item);
                }
            }
            return sprites;
        }
        return null;
    }

    private static bool ChackSpritePackerState(Sprite sprite)
    {
        if (sprite.rect.width > MAX_SPRITE_SIZE)
        {
            if (sprite.rect.width % 2 != 0 || sprite.rect.height % 2 != 0)
            {
                Debug.LogError($"{sprite.name}尺寸不符合压缩规范（宽高均为2的倍数），请注意");
            }
            return false;
        }

        if (sprite.rect.height > MAX_SPRITE_SIZE)
        {
            if (sprite.rect.width % 2 != 0 || sprite.rect.height % 2 != 0)
            {
                Debug.LogError($"{sprite.name}宽度不符合压缩规范（宽高均为2的倍数），请注意");
            }
            return false;
        }

        if (sprite.rect.width * sprite.rect.height > MAX_SPRITE_SIZE * MAX_SPRITE_SIZE)
        {
            Debug.LogError($"{sprite.name}尺寸过大，请注意");
            return false;
        }
        return true;
    }

}


public class AtlasData
{
    public string atlasName;
    public string assetPath;
    /// <summary>
    /// 缓存中的SpriteAtlas，不直接指向本地资源
    /// </summary>
    public SpriteAtlas atlas;
    public List<Sprite> sprites;

    //编辑器界面数据
    public bool isShowDital;
}