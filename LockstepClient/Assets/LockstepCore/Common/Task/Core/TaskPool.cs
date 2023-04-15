/*
 * @Author: delevin.ying 
 * @Date: 2023-04-15 19:26:35 
 * @Last Modified by: delevin.ying
 * @Last Modified time: 2023-04-15 19:28:22
 */


namespace TaskCore
{
    public class TaskPool
    {
        public static TaskPool Instance()
        {
            if (_instance == null) _instance = new TaskPool();

            return _instance;
        }

        private static TaskPool _instance;
    }
}