using System;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace GameEditor
{
    public class DebugPlayWindow : EditorWindow
    {
        [MenuItem("调试工具/运行客户端")]
        public static void RunBattleSceneClient()
        {
            if (!UnityEngine.SceneManagement.SceneManager.GetActiveScene().name.Equals(GameSceneSetting.LaunchClient))
            {
                EditorSceneManager.OpenScene(GameSceneSetting.LaunchClientPath);
            }
            EditorApplication.ExecuteMenuItem("Edit/Play");
        }


        [MenuItem("调试工具/运行Host客户端")]
        public static void RunBattleSceneServer()
        {
            if (!UnityEngine.SceneManagement.SceneManager.GetActiveScene().name.Equals(GameSceneSetting.LaunchServer))
            {
                EditorSceneManager.OpenScene(GameSceneSetting.LaunchServerPath);
            }
            EditorApplication.ExecuteMenuItem("Edit/Play");
        }

        [MenuItem("调试工具/运行AB测试场景")]
        public static void RunAssetBundleScene()
        {
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name.Equals(GameSceneSetting.ABTestScene) == false)
            {
                EditorSceneManager.OpenScene(GameSceneSetting.ABTestScene);
            }
            EditorApplication.ExecuteMenuItem("Edit/Play");
        }
    }
}