namespace Entitas.VisualDebugging.Unity
{
    public class SystemInfo
    {
        public SystemInfo parentSystemInfo;

        public bool isActive;

        private readonly ISystem _system;

        private readonly SystemInterfaceFlags _interfaceFlags;

        private readonly string _systemName;

        private double _accumulatedExecutionDuration;

        private double _minExecutionDuration;

        private double _maxExecutionDuration;

        private int _executionDurationsCount;

        private double _accumulatedCleanupDuration;

        private double _minCleanupDuration;

        private double _maxCleanupDuration;

        private int _cleanupDurationsCount;

        public ISystem system => _system;

        public string systemName => _systemName;

        public bool isInitializeSystems => (_interfaceFlags & SystemInterfaceFlags.IInitializeSystem) == SystemInterfaceFlags.IInitializeSystem;

        public bool isExecuteSystems => (_interfaceFlags & SystemInterfaceFlags.IExecuteSystem) == SystemInterfaceFlags.IExecuteSystem;

        public bool isCleanupSystems => (_interfaceFlags & SystemInterfaceFlags.ICleanupSystem) == SystemInterfaceFlags.ICleanupSystem;

        public bool isTearDownSystems => (_interfaceFlags & SystemInterfaceFlags.ITearDownSystem) == SystemInterfaceFlags.ITearDownSystem;

        public bool isReactiveSystems => (_interfaceFlags & SystemInterfaceFlags.IReactiveSystem) == SystemInterfaceFlags.IReactiveSystem;

        public double initializationDuration { get; set; }

        public double accumulatedExecutionDuration => _accumulatedExecutionDuration;

        public double minExecutionDuration => _minExecutionDuration;

        public double maxExecutionDuration => _maxExecutionDuration;

        public double averageExecutionDuration
        {
            get
            {
                if (_executionDurationsCount != 0)
                {
                    return _accumulatedExecutionDuration / (double)_executionDurationsCount;
                }
                return 0.0;
            }
        }

        public double accumulatedCleanupDuration => _accumulatedCleanupDuration;

        public double minCleanupDuration => _minCleanupDuration;

        public double maxCleanupDuration => _maxCleanupDuration;

        public double averageCleanupDuration
        {
            get
            {
                if (_cleanupDurationsCount != 0)
                {
                    return _accumulatedCleanupDuration / (double)_cleanupDurationsCount;
                }
                return 0.0;
            }
        }

        public double cleanupDuration { get; set; }

        public double teardownDuration { get; set; }

        public bool areAllParentsActive
        {
            get
            {
                if (parentSystemInfo != null)
                {
                    if (parentSystemInfo.isActive)
                    {
                        return parentSystemInfo.areAllParentsActive;
                    }
                    return false;
                }
                return true;
            }
        }

        public SystemInfo(ISystem system)
        {
            _system = system;
            _interfaceFlags = getInterfaceFlags(system);
            DebugSystems debugSystems = system as DebugSystems;
            _systemName = ((debugSystems != null) ? debugSystems.name : system.GetType().Name.RemoveSystemSuffix());
            isActive = true;
        }

        public void AddExecutionDuration(double executionDuration)
        {
            if (executionDuration < _minExecutionDuration || _minExecutionDuration == 0.0)
            {
                _minExecutionDuration = executionDuration;
            }
            if (executionDuration > _maxExecutionDuration)
            {
                _maxExecutionDuration = executionDuration;
            }
            _accumulatedExecutionDuration += executionDuration;
            _executionDurationsCount++;
        }

        public void AddCleanupDuration(double cleanupDuration)
        {
            if (cleanupDuration < _minCleanupDuration || _minCleanupDuration == 0.0)
            {
                _minCleanupDuration = cleanupDuration;
            }
            if (cleanupDuration > _maxCleanupDuration)
            {
                _maxCleanupDuration = cleanupDuration;
            }
            _accumulatedCleanupDuration += cleanupDuration;
            _cleanupDurationsCount++;
        }

        public void ResetDurations()
        {
            _accumulatedExecutionDuration = 0.0;
            _executionDurationsCount = 0;
            _accumulatedCleanupDuration = 0.0;
            _cleanupDurationsCount = 0;
        }

        private static SystemInterfaceFlags getInterfaceFlags(ISystem system)
        {
            SystemInterfaceFlags systemInterfaceFlags = SystemInterfaceFlags.None;
            if (system is IInitializeSystem)
            {
                systemInterfaceFlags |= SystemInterfaceFlags.IInitializeSystem;
            }
            if (system is IReactiveSystem)
            {
                systemInterfaceFlags |= SystemInterfaceFlags.IReactiveSystem;
            }
            else if (system is IExecuteSystem)
            {
                systemInterfaceFlags |= SystemInterfaceFlags.IExecuteSystem;
            }
            if (system is ICleanupSystem)
            {
                systemInterfaceFlags |= SystemInterfaceFlags.ICleanupSystem;
            }
            if (system is ITearDownSystem)
            {
                systemInterfaceFlags |= SystemInterfaceFlags.ITearDownSystem;
            }
            return systemInterfaceFlags;
        }
    }
}