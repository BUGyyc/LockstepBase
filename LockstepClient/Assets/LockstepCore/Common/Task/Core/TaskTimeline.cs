/*
 * @Author: delevin.ying 
 * @Date: 2023-04-15 17:41:07 
 * @Last Modified by: delevin.ying
 * @Last Modified time: 2023-04-15 18:50:00
 */

using System.Collections.Generic;


namespace TaskCore
{
    public delegate void TAction();


    public class TaskTimeline
    {
        // public Queue<TaskTrack> taskTrackQueue;
        //链表的方式存储下一个任务管道
        public TaskTimeline next;

        //轨道的头指针
        public TaskTrack headTrack;



        #region CoreData

        private int frameIndexOnCreate;
        private int frameIndexOnStop;
        private int frameIndexOnResume;

        private float timeOnCreate;
        private float timeOnStop;
        private float timeOnResume;

        private int lastExecuteFrameIndex;

        #endregion




        #region  TempData
        private bool neverDestroy = false;
        private bool timelineLock = false;

        public void SetTimelineLock(bool value)
        {
            timelineLock = value;
        }

        public bool GetTimelineLock()
        {
            return timelineLock;
        }

        public bool CanDestroy() { return !neverDestroy; }
        #endregion

        public TaskTimeline(bool canDestroy = true)
        {
            neverDestroy = canDestroy;
            // createMainPipeline();
            this.timelineLock = true;
        }

        public TaskTimeline()
        {
            // createMainPipeline();
            this.timelineLock = true;
        }

        ~TaskTimeline()
        {

        }

        public void Destroy()
        {

        }


        public bool Execute(float deltaTime)
        {
            if (timelineLock)
            {
                UnityEngine.Debug.LogError("timeline Lock " + this.GetHashCode());
                return false;
            }
            else
            {
                // UnityEngine.Debug.Log("timeline Execute " + this.GetHashCode());
            }

            if (headTrack == null)
            {
                // UnityEngine.Debug.LogError("timeline 中头轨道是 null ");
                return false;
            }

            var track = headTrack;

            while (track != null)
            {
                track.Execute(deltaTime);
            }

            //检测是否有空轨道，有的话，就删除
            track = headTrack;
            if (track.IsEmpty())
            {
                headTrack = headTrack.next;
                track = headTrack;
            }
            while (track.next != null)
            {
                if (track.next.IsEmpty())
                {
                    track.next = track.next.next;
                }
            }

            return true;
        }


        public TaskTimeline DelayFrame(int frame)
        {
            return this;
        }

        /// <summary>
        /// 延迟毫秒数
        /// </summary>
        /// <param name="ms"></param>
        /// <returns></returns>
        public TaskTimeline DelayTime(int ms)
        {
            return this;
        }

        /// <summary>
        /// 延迟秒速
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public TaskTimeline DelayTime(float s)
        {
            return this;
        }

        /// <summary>
        /// 条件通过才推进
        /// </summary>
        /// <returns></returns>
        public TaskTimeline If()
        {
            return this;
        }


        public TaskTimeline Do(TAction action)
        {
            if (action != null)
            {
                if (headTrack != null)
                {
                    headTrack.Add(action);
                }
            }
            return this;
        }

        public TaskTimeline Do(TAction action, uint trackUid)
        {

            return this;
        }

        public TaskTimeline Do(TAction action, short trackIndex)
        {
            return this;
        }


        public TaskTimeline LoopDo()
        {
            return this;
        }


        public TaskTimeline Start()
        {
            this.SetTimelineLock(false);
            UnityEngine.Debug.Log("启动Timeline " + this.GetHashCode());
            return this;
        }


        public TaskTimeline Stop()
        {
            return this;
        }

        public TaskTimeline Resume()
        {
            return this;
        }





        // private void createMainPipeline()
        // {
        //     taskTrackQueue = new Queue<TaskTrack>();
        // }
    }

    public static class TaskTimelineExt
    {
        public static TaskTimeline TaskTimeline(this object self)
        {
            var taskTimeline = Singleton<DelevinTask>.instance.CreateTaskTimeline();

            return taskTimeline;
        }


        public static TaskTimeline TaskTimeline(this UnityEngine.GameObject self)
        {
            var taskTimeline = Singleton<DelevinTask>.instance.CreateTaskTimeline();

            return taskTimeline;
        }
    }
}