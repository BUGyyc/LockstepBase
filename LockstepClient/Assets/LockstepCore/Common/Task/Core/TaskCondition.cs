/*
 * @Author: delevin.ying 
 * @Date: 2023-04-15 18:52:17 
 * @Last Modified by: delevin.ying
 * @Last Modified time: 2023-04-15 18:57:31
 */

using System.Collections.Generic;

namespace TaskCore
{

    public class TaskCondition
    {
        public Queue<TaskConditionItem> conditionQueue;
    }


    public class TaskConditionItem
    {
        public TaskConditionMode mode;

        public System.Action callAction;

        public bool CheckItemResult()
        {
            return false;
        }
    }





    public enum TaskConditionMode
    {
        Default = 0,

        And = 1,
        Or = 2,

        Not = 3
    }


}