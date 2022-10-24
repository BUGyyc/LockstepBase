using UnityEngine;

namespace Entitas.VisualDebugging.Unity
{

    public class DebugSystemsBehaviour : MonoBehaviour
    {
        private DebugSystems _systems;

        public DebugSystems systems => _systems;

        public void Init(DebugSystems systems)
        {
            _systems = systems;
        }
    }
}