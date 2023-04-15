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

        public Queue<TaskAction> taskActionQueue;

        // public 
    }
}