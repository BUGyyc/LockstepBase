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
    public partial class DelevinTask
    {
        public TaskTimeline mainTimeline;

        public Dictionary<uint, TaskTimeline> taskTimelineDic;

        private bool timelineLock = false;

        public void SetTimelineLock(bool value)
        {
            timelineLock = value;
        }

        public DelevinTask()
        {
            mainTimeline = new TaskTimeline(true);
            taskTimelineDic = new Dictionary<uint, TaskTimeline>();

            taskTimelineDic.Add(0, mainTimeline);
        }

        public bool HasTaskTimeline(uint id)
        {
            return false;
        }

        public void AddTaskTimeline(uint id)
        {
            if (mainTimeline == null) return;

            var taskTimeline = CreateTaskTimeline();
            if (taskTimeline == null) return;

            taskTimelineDic.Add(id, taskTimeline);
        }

        private TaskTimeline CreateTaskTimeline()
        {


            return null;
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

        public void Execute(LFloat deltaTime)
        {
            if (mainTimeline == null) return;

            if (timelineLock == true) return;

            var timeline = mainTimeline;

            //后续多线程运行Timeline
            while (timeline != null)
            {
                if (timeline.GetTimelineLock()) continue;

                var queue = timeline.taskTrackQueue;
                if (queue == null) continue;

                var taskAction = queue.Dequeue();

                while (taskAction != null)
                {
                    // taskAction.
                }

            }
        }

        public void ExecuteByFixedUpdate(LFloat deltaTime)
        {

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