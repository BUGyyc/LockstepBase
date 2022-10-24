using UnityEngine;

namespace Entitas.VisualDebugging.Unity
{
    [ExecuteInEditMode]
    public class ContextObserverBehaviour : MonoBehaviour
    {
        private ContextObserver _contextObserver;

        public ContextObserver contextObserver => _contextObserver;

        public void Init(ContextObserver contextObserver)
        {
            _contextObserver = contextObserver;
            Update();
        }

        private void Update()
        {
            if (_contextObserver == null)
            {
                //((Component)this).get_gameObject().DestroyGameObject();
                ((Component)this).gameObject.DestroyGameObject();
            }
            else if ((Object)(object)_contextObserver.gameObject != (Object)null)
            {
                //((Object)_contextObserver.gameObject).set_name(_contextObserver.ToString());
                ((Object)_contextObserver.gameObject).name = (_contextObserver.ToString());
            }
        }

        private void OnDestroy()
        {
            _contextObserver.Deactivate();
        }
    }
}