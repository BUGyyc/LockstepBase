using System;

namespace Entitas.VisualDebugging.Unity
{
    [Flags]
    public enum SystemInterfaceFlags
    {
        None = 0,
        IInitializeSystem = 2,
        IExecuteSystem = 4,
        ICleanupSystem = 8,
        ITearDownSystem = 0x10,
        IReactiveSystem = 0x20
    }
}