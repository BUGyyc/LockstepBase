/*
 * @Author: delevin.ying 
 * @Date: 2023-04-15 17:43:48 
 * @Last Modified by: delevin.ying
 * @Last Modified time: 2023-04-15 18:44:08
 */
using System.Collections.Generic;

namespace TaskCore
{
    public class TaskTrack
    {
        public uint trackId { private set; get; }

        public Queue<TaskAction> actionQueue;

        // public 
        public TaskTrack next;

        public TaskTrack Add(TAction action)
        {
            return this;
        }


        public bool Execute(float deltaTime)
        {
            if (actionQueue == null) return false;
            if (actionQueue.Count == 0) return false;

            var action = actionQueue.Peek();

            action.Execute(deltaTime);
            return true;
        }

        /// <summary>
        /// 空轨道
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            if (actionQueue == null) return true;
            if (actionQueue.Count == 0) return true;

            return false;
        }
    }
}