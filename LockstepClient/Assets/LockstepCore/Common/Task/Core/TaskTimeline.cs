/*
 * @Author: delevin.ying 
 * @Date: 2023-04-15 17:41:07 
 * @Last Modified by: delevin.ying
 * @Last Modified time: 2023-04-15 18:34:57
 */

using System.Collections.Generic;


namespace TaskCore
{
    public class TaskTimeline
    {
        public Queue<TaskTrack> taskTrackQueue;
        //链表的方式存储下一个任务管道
        public TaskTimeline nextTimeline;

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

        public TaskTimeline(bool canDestroy = true)
        {
            neverDestroy = canDestroy;
            // createMainPipeline();
        }

        public ~TaskTimeline()
        {

        }

        public void Destroy()
        {

        }

        public TaskTimeline()
        {
            // createMainPipeline();
        }

        // private void createMainPipeline()
        // {
        //     taskTrackQueue = new Queue<TaskTrack>();
        // }
    }
}