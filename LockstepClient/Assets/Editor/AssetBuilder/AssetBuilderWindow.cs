/*
 * @Author: delevin.ying 
 * @Date: 2023-04-23 17:47:41 
 * @Last Modified by: delevin.ying
 * @Last Modified time: 2023-04-23 20:19:02
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
        }





    }
}