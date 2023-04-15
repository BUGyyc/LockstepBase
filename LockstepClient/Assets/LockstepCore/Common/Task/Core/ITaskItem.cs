/*
 * @Author: delevin.ying 
 * @Date: 2023-04-15 18:40:35 
 * @Last Modified by: delevin.ying
 * @Last Modified time: 2023-04-15 18:43:59
 */


namespace TaskCore
{
    public interface ITaskItem
    {
        public bool Create();

        public bool HasItem();

        public bool Execute();

        public void Destroy();
    }
}