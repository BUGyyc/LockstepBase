using ET;
using System.Collections;
using UnityEngine;

namespace ABCore
{
    public class UpdateABPipeline : Singleton<UpdateABPipeline>
    {

        /**
         * 1.对比本地版本号
         * 2.删除多余资源
         * 3.新增资源
         * 4.替换资源
         * 5.HybridCLR 更新DLL
         * 
         * 
         **/

        public UpdateABInfo updateInfo { private set; get; }

        private LoadABInfoProcess loadProcess;

        public override void Init()
        {
            base.Init();

            loadProcess = new LoadABInfoProcess();

            Download().Coroutine();
        }

        public void Test()
        {

        }

        private async ETTask Download()
        {
            updateInfo = await loadProcess.LoadABInfo("");
        }



        public override void Dispose()
        {
            //throw new System.NotImplementedException();
        }
    }




    public struct UpdateABInfo
    {
        public long versionKey;

        public string md5;

        public float versionTime;
    }
}