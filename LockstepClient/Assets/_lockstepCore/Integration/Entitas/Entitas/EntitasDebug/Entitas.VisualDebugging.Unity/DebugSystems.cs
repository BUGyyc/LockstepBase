using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace Entitas.VisualDebugging.Unity
{

    public class DebugSystems : Systems
    {
        public static AvgResetInterval avgResetInterval = AvgResetInterval.Never;

        public bool paused;

        private string _name;

        private List<ISystem> _systems;

        private GameObject _gameObject;

        private SystemInfo _systemInfo;

        private List<SystemInfo> _initializeSystemInfos;

        private List<SystemInfo> _executeSystemInfos;

        private List<SystemInfo> _cleanupSystemInfos;

        private List<SystemInfo> _tearDownSystemInfos;

        private Stopwatch _stopwatch;

        private double _executeDuration;

        private double _cleanupDuration;

        public int totalInitializeSystemsCount
        {
            get
            {
                int num = 0;
                foreach (IInitializeSystem initializeSystem in _initializeSystems)
                {
                    num += (initializeSystem as DebugSystems)?.totalInitializeSystemsCount ?? 1;
                }
                return num;
            }
        }

        public int totalExecuteSystemsCount
        {
            get
            {
                int num = 0;
                foreach (IExecuteSystem executeSystem in _executeSystems)
                {
                    num += (executeSystem as DebugSystems)?.totalExecuteSystemsCount ?? 1;
                }
                return num;
            }
        }

        public int totalCleanupSystemsCount
        {
            get
            {
                int num = 0;
                foreach (ICleanupSystem cleanupSystem in _cleanupSystems)
                {
                    num += (cleanupSystem as DebugSystems)?.totalCleanupSystemsCount ?? 1;
                }
                return num;
            }
        }

        public int totalTearDownSystemsCount
        {
            get
            {
                int num = 0;
                foreach (ITearDownSystem tearDownSystem in _tearDownSystems)
                {
                    num += (tearDownSystem as DebugSystems)?.totalTearDownSystemsCount ?? 1;
                }
                return num;
            }
        }

        public int totalSystemsCount
        {
            get
            {
                int num = 0;
                foreach (ISystem system in _systems)
                {
                    num += (system as DebugSystems)?.totalSystemsCount ?? 1;
                }
                return num;
            }
        }

        public int initializeSystemsCount => _initializeSystems.Count;

        public int executeSystemsCount => _executeSystems.Count;

        public int cleanupSystemsCount => _cleanupSystems.Count;

        public int tearDownSystemsCount => _tearDownSystems.Count;

        public string name => _name;

        public GameObject gameObject => _gameObject;

        public SystemInfo systemInfo => _systemInfo;

        public double executeDuration => _executeDuration;

        public double cleanupDuration => _cleanupDuration;

        public SystemInfo[] initializeSystemInfos => _initializeSystemInfos.ToArray();

        public SystemInfo[] executeSystemInfos => _executeSystemInfos.ToArray();

        public SystemInfo[] cleanupSystemInfos => _cleanupSystemInfos.ToArray();

        public SystemInfo[] tearDownSystemInfos => _tearDownSystemInfos.ToArray();

        public DebugSystems(string name)
        {
            initialize(name);
        }

        protected DebugSystems(bool noInit)
        {
        }

        protected void initialize(string name)
        {
            //IL_0009: Unknown result type (might be due to invalid IL or missing references)
            //IL_0013: Expected O, but got Unknown
            _name = name;
            _gameObject = new GameObject(name);
            _gameObject.AddComponent<DebugSystemsBehaviour>().Init(this);
            _systemInfo = new SystemInfo(this);
            _systems = new List<ISystem>();
            _initializeSystemInfos = new List<SystemInfo>();
            _executeSystemInfos = new List<SystemInfo>();
            _cleanupSystemInfos = new List<SystemInfo>();
            _tearDownSystemInfos = new List<SystemInfo>();
            _stopwatch = new Stopwatch();
        }

        public override Systems Add(ISystem system)
        {
            _systems.Add(system);
            SystemInfo systemInfo;
            if (system is DebugSystems debugSystems)
            {
                systemInfo = debugSystems.systemInfo;
                //debugSystems.gameObject.get_transform().SetParent(_gameObject.get_transform(), false);
                debugSystems.gameObject.transform.SetParent(_gameObject.transform, false);
            }
            else
            {
                systemInfo = new SystemInfo(system);
            }
            systemInfo.parentSystemInfo = _systemInfo;
            if (systemInfo.isInitializeSystems)
            {
                _initializeSystemInfos.Add(systemInfo);
            }
            if (systemInfo.isExecuteSystems || systemInfo.isReactiveSystems)
            {
                _executeSystemInfos.Add(systemInfo);
            }
            if (systemInfo.isCleanupSystems)
            {
                _cleanupSystemInfos.Add(systemInfo);
            }
            if (systemInfo.isTearDownSystems)
            {
                _tearDownSystemInfos.Add(systemInfo);
            }
            return base.Add(system);
        }

        public void ResetDurations()
        {
            foreach (SystemInfo executeSystemInfo in _executeSystemInfos)
            {
                executeSystemInfo.ResetDurations();
            }
            foreach (ISystem system in _systems)
            {
                if (system is DebugSystems debugSystems)
                {
                    debugSystems.ResetDurations();
                }
            }
        }

        public override void Initialize()
        {
            for (int i = 0; i < _initializeSystems.Count; i++)
            {
                SystemInfo systemInfo = _initializeSystemInfos[i];
                if (systemInfo.isActive)
                {
                    _stopwatch.Reset();
                    _stopwatch.Start();
                    _initializeSystems[i].Initialize();
                    _stopwatch.Stop();
                    systemInfo.initializationDuration = _stopwatch.Elapsed.TotalMilliseconds;
                }
            }
        }

        public override void Execute()
        {
            if (!paused)
            {
                StepExecute();
            }
        }

        public override void Cleanup()
        {
            if (!paused)
            {
                StepCleanup();
            }
        }

        public void StepExecute()
        {
            _executeDuration = 0.0;
            if (Time.frameCount % (int)avgResetInterval == 0)
            {
                ResetDurations();
            }
            for (int i = 0; i < _executeSystems.Count; i++)
            {
                SystemInfo systemInfo = _executeSystemInfos[i];
                if (systemInfo.isActive)
                {
                    _stopwatch.Reset();
                    _stopwatch.Start();
                    _executeSystems[i].Execute();
                    _stopwatch.Stop();
                    double totalMilliseconds = _stopwatch.Elapsed.TotalMilliseconds;
                    _executeDuration += totalMilliseconds;
                    systemInfo.AddExecutionDuration(totalMilliseconds);
                }
            }
        }

        public void StepCleanup()
        {
            _cleanupDuration = 0.0;
            for (int i = 0; i < _cleanupSystems.Count; i++)
            {
                SystemInfo systemInfo = _cleanupSystemInfos[i];
                if (systemInfo.isActive)
                {
                    _stopwatch.Reset();
                    _stopwatch.Start();
                    _cleanupSystems[i].Cleanup();
                    _stopwatch.Stop();
                    double totalMilliseconds = _stopwatch.Elapsed.TotalMilliseconds;
                    _cleanupDuration += totalMilliseconds;
                    systemInfo.AddCleanupDuration(totalMilliseconds);
                }
            }
        }

        public override void TearDown()
        {
            for (int i = 0; i < _tearDownSystems.Count; i++)
            {
                SystemInfo systemInfo = _tearDownSystemInfos[i];
                if (systemInfo.isActive)
                {
                    _stopwatch.Reset();
                    _stopwatch.Start();
                    _tearDownSystems[i].TearDown();
                    _stopwatch.Stop();
                    systemInfo.teardownDuration = _stopwatch.Elapsed.TotalMilliseconds;
                }
            }
        }
    }
}