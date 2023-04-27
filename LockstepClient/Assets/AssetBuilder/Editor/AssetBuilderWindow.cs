/*
 * @Author: delevin.ying 
 * @Date: 2023-04-23 17:47:41 
 * @Last Modified by: delevin.ying
 * @Last Modified time: 2023-04-27 16:47:11
 */

using UnityEditor;
using UnityEngine;
using System.IO;
using System.Collections.Generic;
using AssetBuilder.Process;
using UnityEngine.Rendering;

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
            GlobalAssetConfig globalCfg = AssetDatabase.LoadAssetAtPath<GlobalAssetConfig>(AssetBuilderSetting.GlobalAssetCfgPath);

            if (globalCfg == null)
            {
                LogMaster.E("没有找到配置");
                return;
            }


            List<EditorBuildSettingsScene> editorSceneList = new List<EditorBuildSettingsScene>();
            foreach (var item in globalCfg.OriginSceneList)
            {
                editorSceneList.Add(new EditorBuildSettingsScene(AssetDatabase.GetAssetPath(item), true));
            }

            EditorBuildSettings.scenes = editorSceneList.ToArray();

            //TODO: 加密资源处理

            //处理Shader
            HashSet<string> shaderSet = new HashSet<string>();
            Object graphicsSettings =
#if UNITY_2020_1_OR_NEWER
                       GraphicsSettings.GetGraphicsSettings();
#else
                AssetDatabase.LoadAssetAtPath<GraphicsSettings>("ProjectSettings/GraphicsSettings.asset");
#endif
            SerializedObject serializedObject = new SerializedObject(graphicsSettings);
            SerializedProperty serializedProperty = serializedObject.FindProperty("m_AlwaysIncludedShaders");
            if (serializedProperty.isArray)
            {
                for (int i = 0; i < serializedProperty.arraySize; i++)
                {
                    SerializedProperty property = serializedProperty.GetArrayElementAtIndex(i);
                    Object shaderInfo = property.objectReferenceValue;
                    if (!shaderSet.Contains(shaderInfo.name))
                    {
                        shaderSet.Add(shaderInfo.name);
                    }
                }
            }

            //TODO:构建APK、SubAssetBundle
            List<SubAssetBundleConfig> subABList = globalCfg.subABCfgList;
            //寻找需要加载的资源包
            HashSet<string> findAllNeedLoadAsset = new HashSet<string>();
            foreach (var assetItem in subABList)
            {
                BuildAssetBundle();
            }

        }


        private void BuildAssetBundle(GlobalAssetConfig globalCfg, SubAssetBundleConfig subABCfg, HashSet<string> needLoadAssetPath, HashSet<string> shaderPath)
        {
            //直接引用的资源文件、以及依赖相关的文件
            Dictionary<string, string[]> needLoadAndDepends = new Dictionary<string, string[]>();
            //所有被点名需要的资源包
            string[] Paths = subABCfg.assetPathList.ToArray();
            //统计依赖资源的次数
            Dictionary<string, int> dependenciesCount = new Dictionary<string, int>();
            //记录需要的Shader文件
            HashSet<string> shaders = new HashSet<string>();
            //选定 主动加载的文件
            HashSet<string> files = new HashSet<string>();
            for (var i = 0; i < Paths.Length; i++)
            {
                string item = Paths[i];
                //STEP: 从指定文件夹下，去查找所有的文件，并记录下来
                BuildAssetsTools.GetChildFiles(item, files);
            }

            //把子包中的场景记录进去
            SceneAsset[] sceneAssets = subABCfg.sceneList.ToArray();
            for (var i = 0; i < sceneAssets.Length; i++)
            {
                var sceneAsset = sceneAssets[i];
                string scenePath = AssetDatabase.GetAssetPath(sceneAsset);
                if (files.Contains(scenePath) == false)
                {
                    files.Add(scenePath);
                }
            }

            //TODO: 组包

            //TODO: 所有需要主动加载的资源

            List<string> needRemoveFile = new List<string>();
            // Dictionary<string,List<string>> 
            foreach (var file in files)
            {
                if (BuildAssetsTools.IsShaderAsset(file))
                {
                    Shader shader = AssetDatabase.LoadAssetAtPath<Shader>(file);
                    if (shader != null && shaderPath.Contains(shader.name))
                    {
                        //已经被IncludeShader 包含，不需要再做处理
                        continue;
                    }
                    if (shaders.Contains(file) == false)
                    {
                        shaders.Add(file);
                    }
                    if (needRemoveFile.Contains(file) == false)
                    {
                        needRemoveFile.Add(file);
                    }
                    continue;
                }

                //处理依赖文件
                string[] depends = AssetDatabase.GetDependencies(file);
                List<string> dependResultList = new List<string>();
                for (var i = 0; i < depends.Length; i++)
                {
                    string depend = depends[i];
                    if (depend.EndsWith(".cs")) { continue; }

                    if (BuildAssetsTools.IsShaderAsset(depend))
                    {
                        Shader shader = AssetDatabase.LoadAssetAtPath<Shader>(depend);
                        if (shader != null && shaderPath.Contains(shader.name)) { continue; }

                        if (shaders.Contains(depend) == false)
                        {
                            shaders.Add(depend);
                        }
                        continue;
                    }

                    if (depend.StartsWith("Assets/") == false) { continue; }

                    if (BuildAssetsTools.CantLoadFile(file) == false)
                    {
                        continue;
                    }
                }

                needLoadAndDepends.Add(file, dependResultList.ToArray());

                //清理多余文件
                foreach (var item in needRemoveFile)
                {
                    files.Remove(item);
                }

                //复合依赖文件
                HashSet<string> compoundDepends = new HashSet<string>();
                
            }
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