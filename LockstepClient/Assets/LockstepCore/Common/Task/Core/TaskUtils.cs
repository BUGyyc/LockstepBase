﻿/*
 * @Author: delevin.ying 
 * @Date: 2023-04-15 19:05:04 
 * @Last Modified by: delevin.ying
 * @Last Modified time: 2023-04-15 19:37:38
 */
using System;

namespace TaskCore
{
    public class TaskUtil
    {
        public static bool DoAction(TaskCondition condition, Action action)
        {
            return false;
        }


        public static bool DoAction(Action action)
        {
            DoAction(new TaskCondition(), action);
            return false;
        }


        public static bool DoDelayFrameAction(int frame)
        {
            return false;
        }


        public static bool DoDelayTimeAction(float time)
        {
            return false;
        }


        public static bool DoDelayTimeAction(int time)
        {
            return false;
        }
    }



    public class Test
    {

        public void _test()
        {

            // TaskUtil.DoAction().

        }
    }
}