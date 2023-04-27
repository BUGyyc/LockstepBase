using System.Collections;
using UnityEngine;

namespace ABCore
{
    public interface IProcess
    {
        public void Init();

        public void ExecuteProcess();

        public void Exit(int exitCode);

    }
}