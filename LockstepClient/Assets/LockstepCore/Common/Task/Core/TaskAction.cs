/*
 * @Author: delevin.ying 
 * @Date: 2023-04-15 17:45:15 
 * @Last Modified by: delevin.ying
 * @Last Modified time: 2023-04-15 18:49:28
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
        public void ResetData()
        {
            taskId = 0;
        }



        public bool Execute()
        {
            // throw new System.NotImplementedException();

            executeTaskAction();
            return true;
        }


        private void executeTaskAction()
        {

        }




        public void Destroy()
        {
            // throw new System.NotImplementedException();
        }






        public bool HasItem()
        {
            // throw new System.NotImplementedException();

            return false;
        }

        public bool Create()
        {
            // throw new System.NotImplementedException();
            return false;
        }
    }
}