/*
 * @Author: delevin.ying 
 * @Date: 2023-04-15 17:45:15 
 * @Last Modified by: delevin.ying
 * @Last Modified time: 2023-04-15 18:44:22
 */

namespace TaskCore
{
    public class TaskAction : ITaskItem
    {
        public uint taskId { get; private set; }


        private void SetTaskId(uint Id = 0)
        {
            if (Id == 0)
            {

            }
        }
    }
}