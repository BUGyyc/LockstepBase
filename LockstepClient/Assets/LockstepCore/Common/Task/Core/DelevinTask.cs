/*
 * @Author: delevin.ying 
 * @Date: 2023-04-15 17:30:57 
 * @Last Modified by: delevin.ying
 * @Last Modified time: 2023-04-15 19:38:12
 */

using System.Collections.Generic;
using Lockstep;
namespace TaskCore
{
    public partial class DelevinTask : Singleton<DelevinTask>
    {
        public TaskTimeline head;

        public Dictionary<uint, TaskTimeline> taskTimelineDic;

        private bool globalTimelineLock = false;

        public void SetTimelineLock(bool value)
        {
            globalTimelineLock = value;
        }

        public DelevinTask()
        {
            // head = new TaskTimeline(true);
            taskTimelineDic = new Dictionary<uint, TaskTimeline>();

            // taskTimelineDic.Add(0, head);
        }

        public bool HasTaskTimeline(uint id)
        {
            return false;
        }

        // public void AddTaskTimeline(uint id)
        // {
        //     if (head == null) return;

        //     var taskTimeline = CreateTaskTimeline();
        //     if (taskTimeline == null) return;

        //     taskTimelineDic.Add(id, taskTimeline);
        // }

        public TaskTimeline CreateTaskTimeline()
        {
            var timeline = new TaskTimeline();
            UnityEngine.Debug.Log("创建TaskTimeline " + timeline.GetHashCode());

            if (head == null)
            {
                head = timeline;
            }
            else
            {
                head.next = timeline;
            }

            return timeline;
        }

        public void PauseTaskTimeline()
        {

        }

        public void ResumeTaskTimeline()
        {

        }

        private void DestroyTaskTimeline(uint id)
        {
            if (id == 0) return;

            if (taskTimelineDic.TryGetValue(id, out TaskTimeline timeline))
            {
                if (timeline.CanDestroy() == false) return;

                taskTimelineDic.Remove(id);
                timeline.Destroy();
            }
        }

        public void Execute(float deltaTime)
        {
            if (globalTimelineLock == true) return;

            var timeline = head;

            //后续多线程运行Timeline
            while (timeline != null)
            {
                timeline.Execute(deltaTime);

                timeline = timeline.next;
            }
        }

        public void ExecuteByFixedUpdate(LFloat deltaTime)
        {

        }

        // public void ExecuteByFixedUpdate(LFloat deltaTime)
        // {

        // }

        public override void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }



    public enum DelevinTaskExecuteMode
    {
        //由 Mono.Update驱动
        Default = 0,
        //固定物理步长驱动，Mono.FixedUpdate
        FixedUpdate,
        // 由 Mono.Update 驱动，但是尽量对齐时间，每帧时间是趋近于 设定时间的，但是是约等于
        FixedTimeUpdate,
    }
}