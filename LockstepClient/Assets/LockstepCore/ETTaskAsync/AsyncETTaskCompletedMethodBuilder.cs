using System;
using System.Security;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace ET
{
    public struct AsyncETTaskCompletedMethodBuilder
    {

        //NOTE:  DebuggerHidden :  忽略断点调试


        // 1. Static Create method.
        [DebuggerHidden]
        public static AsyncETTaskCompletedMethodBuilder Create()
        {
            AsyncETTaskCompletedMethodBuilder builder = new AsyncETTaskCompletedMethodBuilder();
            return builder;
        }

        // 2. TaskLike Task property(void)
        public ETTaskCompleted Task => default;

        // 3. SetException
        [DebuggerHidden]
        public void SetException(Exception e)
        {
            ETTask.ExceptionHandler.Invoke(e);
        }

        // 4. SetResult
        [DebuggerHidden]
        public void SetResult()
        {
            // do nothing
        }

        // 5. AwaitOnCompleted
        [DebuggerHidden]
        public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : INotifyCompletion where TStateMachine : IAsyncStateMachine
        {
            awaiter.OnCompleted(stateMachine.MoveNext);
        }

        //NOTE:  SecuritySafeCritical ： 设置安全级别

        // 6. AwaitUnsafeOnCompleted
        [DebuggerHidden]
        [SecuritySafeCritical]
        public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : ICriticalNotifyCompletion where TStateMachine : IAsyncStateMachine
        {
            awaiter.UnsafeOnCompleted(stateMachine.MoveNext);
        }

        // 7. Start
        [DebuggerHidden]
        public void Start<TStateMachine>(ref TStateMachine stateMachine) where TStateMachine : IAsyncStateMachine
        {
            stateMachine.MoveNext();
        }

        // 8. SetStateMachine
        [DebuggerHidden]
        public void SetStateMachine(IAsyncStateMachine stateMachine)
        {
        }
    }
}