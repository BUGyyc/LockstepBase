/*
 * @Author: delevin.ying 
 * @Date: 2023-04-26 14:16:04 
 * @Last Modified by: delevin.ying
 * @Last Modified time: 2023-04-26 14:24:22
 */
using System.Collections;
using UnityEngine;
using ET;
using System;

namespace ABCore
{
    public class LoadABInfoProcess : IProcess
    {

        public async ETTask<UpdateABInfo> LoadABInfo(string bundlePackageName)
        {
            UpdateABInfo abInfo = new UpdateABInfo();

            string remoteVersion = await GetRemoteBundlePackageVersion(bundlePackageName);

            if (remoteVersion != null)
            {
                Debug.Log($"找到远程分包 {remoteVersion}");
            }

            return abInfo;

        }


        private async ETTask<string> GetRemoteBundlePackageVersion(string bundlePackageName)
        {
            byte[] data = await DownloadBundleHelper.DownloadDataByUrl(bundlePackageName);
            if (data == null)
            {
                Debug.LogError("error  get RemoteBundlePackageVersion fail");
                return null;
            }

            return System.Text.Encoding.UTF8.GetString(data);
        }

        public void ExecuteProcess()
        {
            // throw new System.NotImplementedException();

        }

        public void Exit(int exitCode)
        {
            throw new System.NotImplementedException();
        }

        public void Init()
        {
            // throw new System.NotImplementedException();

        }
    }
}