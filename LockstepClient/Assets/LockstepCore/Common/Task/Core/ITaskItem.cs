/*
 * @Author: delevin.ying 
 * @Date: 2023-04-15 18:40:35 
 * @Last Modified by: delevin.ying
 * @Last Modified time: 2023-04-15 18:47:32
 */


namespace TaskCore
{
    public interface ITaskItem
    {
        public bool Create();

        public bool HasItem();

        public bool Execute();

        public void Destroy();

        public void ResetData();
    }
}