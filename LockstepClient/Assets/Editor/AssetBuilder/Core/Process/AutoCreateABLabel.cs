/*
 * @Author: delevin.ying 
 * @Date: 2023-04-23 19:57:44 
 * @Last Modified by: delevin.ying
 * @Last Modified time: 2023-04-24 10:02:41
 */


using UnityEditor;
using UnityEngine;
using System.IO;
using AssetBuilderCore;

namespace AssetBuilder.Process
{
    public sealed class AutoCreateABLabel : IABProcess
    {

        // public 

        public void ExecuteProcess()
        {
            ClearABLabel();

            OnAutoCreateABLabel();
        }

        /// <summary>
        /// 清除AB标记
        /// </summary>
        private void ClearABLabel()
        {
            // 清空无用的 AB 标记
            AssetDatabase.RemoveUnusedAssetBundleNames();

            // AssetDatabase.RemoveAssetBundleName();
        }


        private void OnAutoCreateABLabel()
        {
            // 需要做标记的根目录
            string strNeedSetLabelRoot = string.Empty;
            // 目录信息（场景目录信息数组，表示所有根目录下场景目录）
            DirectoryInfo[] directoryDIRArray = null;

            // 定义需要打包资源的文件夹根目录
            strNeedSetLabelRoot = PathTools.GetABResourcesPath();
            //Debug.Log("strNeedSetLabelRoot = "+strNeedSetLabelRoot);

            DirectoryInfo dirTempInfo = new DirectoryInfo(strNeedSetLabelRoot);
            directoryDIRArray = dirTempInfo.GetDirectories();

            //2、 遍历本场景目录下所有的目录或者文件
            foreach (DirectoryInfo currentDir in directoryDIRArray)
            {
                //2.1:遍历本场景目录下所有的目录或者文件
                // 如果是目录，则继续“递归”访问里面的文件，直到定位到文件
                string tmpScenesDir = strNeedSetLabelRoot + "/" + currentDir.Name;       // Unity /xx/xx 全路径
                //DirectoryInfo tmpScenesDirInfo = new DirectoryInfo(tmpScenesDir);
                int tmpIndex = tmpScenesDir.LastIndexOf("/");
                string tmpScenesName = tmpScenesDir.Substring(tmpIndex + 1);         // 场景名称
                                                                                     //Debug.Log("tmpScenesDir = "+ tmpScenesDir);

                //2、2 递归调用方法，找到文件，则使用 AssetImporter 类，标记“包名”与 “后缀名”
                JudgeDirOrFileByRecursive(currentDir, tmpScenesName);
            }

            // 刷新
            AssetDatabase.Refresh();

            // 提示信息，标记包名完成
            Debug.Log("AssetBundle 本次操作设置标记完成");
        }

        /// <summary>
        /// 递归判断判断是否是目录或文件
        /// 是文件，修改 Asset Bundle 标记
        /// 是目录，则继续递归
        /// </summary>
        /// <param name="fileSystemInfo">当前文件信息（文件信息与目录信息可以相互转换）</param>
        /// <param name="sceneName">当前场景名称</param>
        private static void JudgeDirOrFileByRecursive(FileSystemInfo fileSystemInfo, string sceneName)
        {
            // 调试信息
            //Debug.Log("currentDir.Name = " + fileSystemInfo.Name);
            //Debug.Log("sceneName = " + sceneName);

            // 参数检查
            if (fileSystemInfo.Exists == false)
            {
                Debug.LogError("文件或者目录名称：" + fileSystemInfo + " 不存在，请检查");
                return;
            }

            // 得到当前目录下一级的文件信息集合
            DirectoryInfo directoryInfoObj = fileSystemInfo as DirectoryInfo;           // 文件信息转为目录信息
            FileSystemInfo[] fileSystemInfoArray = directoryInfoObj.GetFileSystemInfos();

            foreach (FileSystemInfo fileInfo in fileSystemInfoArray)
            {
                FileInfo fileInfoObj = fileInfo as FileInfo;

                // 文件类型
                if (fileInfoObj != null)
                {
                    // 修改此文件的 AssetBundle 标签
                    SetFileABLabel(fileInfoObj, sceneName);
                }
                // 目录类型
                else
                {
                    // 如果是目录，则递归调用
                    JudgeDirOrFileByRecursive(fileInfo, sceneName);
                }
            }
        }

        /// <summary>
        /// 给文件打 Asset Bundle 标记
        /// </summary>
        /// <param name="fileInfoObj">文件（文件信息）</param>
        /// <param name="scenesName">场景名称</param>
        static void SetFileABLabel(FileInfo fileInfoObj, string scenesName)
        {
            // 参数检查（*.meta 文件不做处理）
            if (fileInfoObj.Extension == ".meta")
            {
                return;
            }

            // 参数定义
            // AssetBundle 包名称
            string strABName = string.Empty;
            // 文件路径（相对路径）
            string strAssetFilePath = string.Empty;

            // 得到 AB 包名称
            strABName = GetABName(fileInfoObj, scenesName);
            // 获取资源文件的相对路径
            int tmpIndex = fileInfoObj.FullName.IndexOf("Assets");
            strAssetFilePath = fileInfoObj.FullName.Substring(tmpIndex);        // 得到文件相对路径


            // 给资源文件设置AB名称以及后缀
            AssetImporter tmpImportObj = AssetImporter.GetAtPath(strAssetFilePath);
            tmpImportObj.assetBundleName = strABName;

            // 判断文件是否是场景文件
            if (fileInfoObj.Extension == ".unity")
            {
                // 定义AB包的场景扩展名
                tmpImportObj.assetBundleVariant = "u3d";
            }
            else
            {
                // 定义AB包的非场景扩展名
                tmpImportObj.assetBundleVariant = "ab";
            }
        }

        /// <summary>
        /// 获取 AB 包的名称
        /// </summary>
        /// <param name="fileInfoObj">文件信息</param>
        /// <param name="scenesName">场景名称</param>
        /// AB 包名形成规则：
        ///     文件AB包名称 = “所在二级目录名称”（场景名称）+“三级目录名称”（类型名称）
        /// <returns></returns>
        static string GetABName(FileInfo fileInfoObj, string scenesName)
        {
            // 返回AB包名称
            string strABName = string.Empty;

            // win 路径
            string tmpWinPath = fileInfoObj.FullName;
            // 转为 Unity 路径格式
            string tmpUnityPath = tmpWinPath.Replace("\\", "/");

            // 定位“场景名称”后面字符位置
            int tmpSceneNamePosition = tmpUnityPath.IndexOf(scenesName) + scenesName.Length;
            // AB 包中 “类型名称”所在区域
            string strABFileNameArea = tmpUnityPath.Substring(tmpSceneNamePosition + 1);
            //测试
            //Debug.Log(" strABFileNameArea = " + strABFileNameArea);

            // 非场景资源
            if (strABFileNameArea.Contains("/"))
            {
                string[] tmpStrArray = strABFileNameArea.Split('/');

                //测试
                //Debug.Log("tmpStrArray[0] = "+ tmpStrArray[0]);

                // AB 包名称正式形成
                strABName = scenesName + "/" + tmpStrArray[0];
            }
            else
            {
                //####### 定义*.unity 文件形成的特殊 AB 包名称
                // strABName = scenesName+ "/" + scenesName;

                strABName = scenesName;//+ "/" + scenesName;
            }
            return strABName;
        }
    }
}