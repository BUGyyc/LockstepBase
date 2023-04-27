using System.Collections;
using UnityEngine;
using ET;
using UnityEngine.Networking;
using System.Net;

namespace ABCore
{
    public static class DownloadBundleHelper
    {
        public static async ETTask<byte[]> DownloadDataByUrl(string url)
        {
            byte[] data = await DownloadData(url);
            if (data == null)
            {
                return null;
            }
            return data;
        }

        private static async ETTask<byte[]> DownloadData(string url)
        {
            using (UnityWebRequest webReq = UnityWebRequest.Get(url))
            {
                UnityWebRequestAsyncOperation operation = webReq.SendWebRequest();

                ETTask waitDownload = ETTask.Create(true);
                operation.completed += (asyncOperation) =>
                {
                    waitDownload.SetResult();
                };

                await waitDownload;

#if UNITY_2020_1_OR_NEWER
                if (webReq.result != UnityWebRequest.Result.Success)
#else
                if (!string.IsNullOrEmpty(webRequest.error))
#endif
                {
                    Debug.Log("下载Bundle失败 重试\n" + webReq.error + "\nURL：" + url);
                    return null;
                }
                return webReq.downloadHandler.data;
            }
        }

    }
}