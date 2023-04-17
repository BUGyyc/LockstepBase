/*
 * @Author: delevin.ying 
 * @Date: 2023-04-15 17:45:15 
 * @Last Modified by: delevin.ying
 * @Last Modified time: 2023-04-15 19:26:04
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

        public TaskActionState state { private set; get; }

        public TAction call;

        /// <summary>
        /// 满足条件才启动 Action,并且执行第一帧
        /// </summary>
        public TaskCondition startCondition;

        /// <summary>
        /// 满足条件才驱动这一帧
        /// </summary>
        public TaskCondition executeCondition;

        /// <summary>
        /// 满足条件就退出
        /// </summary>
        public TaskCondition exitCondition;

        public void ResetData()
        {
            taskId = 0;
            startCondition = null;
            executeCondition = null;
            exitCondition = null;
        }

        private void Awake()
        {
            if (startCondition == null) state = TaskActionState.Start;
            else if (startCondition.CheckResult())
            {
                state = TaskActionState.Start;
            }
        }

        private void Start()
        {
            if (executeCondition == null)
            {
                //第一帧先执行
                executeTaskAction();
                state = TaskActionState.Execute;
            }
            else if (executeCondition.CheckResult())
            {
                //第一帧先执行
                executeTaskAction();
                //然后切换状态
                state = TaskActionState.Execute;
            }
        }

        public bool Execute(float deltaTime)
        {
            //对运行的逻辑
            switch (state)
            {
                case TaskActionState.Awake:
                    Awake();
                    //立刻判断Start。避免一帧只能切一次
                    Start();
                    break;
                case TaskActionState.Start:
                    Start();
                    break;
                case TaskActionState.Execute:
                    executeTaskAction();
                    break;
            }

            //TODO:
            state = TaskActionState.Destroy;

            if (exitCondition != null && exitCondition.CheckResult())
            {

                Destroy();
                //TODO:退出逻辑
            }



            return true;
        }

        public bool Execute() { return false; }


        private void executeTaskAction()
        {
            if (call != null)
            {
                call();
            }
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

    public enum TaskActionState
    {
        Awake = 0,

        Start = 1,
        Execute = 2,
        Stop = 3,

        Destroy
    }
}