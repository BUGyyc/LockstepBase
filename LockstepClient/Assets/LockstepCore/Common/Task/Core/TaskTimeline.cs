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

    public delegate bool ConditionAction();

    public delegate bool ConditionTrack();


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

        public TaskTrack LastTaskTrack()
        {
            if (headTrack == null || headTrack.next == null)
            {
                return headTrack;
            }

            var find = headTrack;
            while (find.next != null)
            {
                find = find.next;
            }
            return find;
        }


        public bool Execute(float deltaTime)
        {
            if (timelineLock)
            {
                // UnityEngine.Debug.LogError("timeline Lock " + this.GetHashCode());

                // UnityEngine.Debug.Log("Timeline actionQueue " + headTrack.actionQueue.Count);

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
                track = track.next;
            }

            //检测是否有空轨道，有的话，就删除
            // track = headTrack;
            // if (track.IsEmpty())
            // {
            //     headTrack = headTrack.next;
            //     track = headTrack;
            // }
            // while (track.next != null)
            // {
            //     if (track.next.IsEmpty())
            //     {
            //         track.next = track.next.next;
            //     }
            // }

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

        // /// <summary>
        // /// 条件通过才推进
        // /// </summary>
        // /// <returns></returns>
        // public TaskTimeline If(ConditionTrack condition)
        // {
        //     var track = LastTaskTrack();

        //     track.executeCondition = condition;

        //     return this;
        // }

        // public TaskTimeline If(ConditionAction condition)
        // {
        //     return this;
        // }

        public TaskTimeline If(ConditionAction condition)
        {
            if (condition == null) return this;
            var track = LastTaskTrack();

            return this;
        }


        public TaskTimeline EndIf()
        {
            return this;
        }



        // public TaskTimeline IfDo(ConditionAction condition, TAction action)
        // {
        //     if (action == null) return this;

        //     if (condition == null) return this;

        //     // TaskTrack track = null;
        //     // if (headTrack == null)
        //     // {
        //     //     UnityEngine.Debug.Log(" 头轨道是null ,创建头轨道 ");
        //     //     headTrack = new TaskTrack();
        //     //     track = headTrack;
        //     // }
        //     // else
        //     // {
        //     //     track = LastTaskTrack();
        //     // }
        //     // track.Add(action);

        //     return this;
        // }



        public TaskTimeline Do(TAction action)
        {
            if (action == null) return this;

            TaskTrack track = null;
            if (headTrack == null)
            {
                UnityEngine.Debug.Log(" 头轨道是null ,创建头轨道 ");
                headTrack = new TaskTrack();
                track = headTrack;
            }
            else
            {
                track = LastTaskTrack();
            }
            //TODO:对象池
            var actionContent = new TaskActionData();
            actionContent.call = action;
            track.Add(actionContent);

            return this;
        }

        /// <summary>
        /// 单线程下的假并行，其实就是在Timeline下单独开辟一条轨道，假的并行
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public TaskTimeline WithDo(TAction action)
        {
            if (action == null) return this;

            TaskTrack track = null;
            if (headTrack == null)
            {
                headTrack = new TaskTrack();
                track = headTrack;
            }
            else
            {
                track = new TaskTrack();
                var last = LastTaskTrack();
                last.next = track;
            }
            var actionContent = new TaskActionData();
            actionContent.call = action;
            track.Add(actionContent);

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


        public TaskTimeline LoopDo(int count)
        {
            return this;
        }


        public TaskTimeline Loop()
        {
            return this;
        }

        public TaskTimeline WaitFrame(int count)
        {
            return this;
        }

        public TaskTimeline WaitTime(int ms)
        {
            return this;
        }

        public TaskTimeline BreakLoop(ConditionAction condition)
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