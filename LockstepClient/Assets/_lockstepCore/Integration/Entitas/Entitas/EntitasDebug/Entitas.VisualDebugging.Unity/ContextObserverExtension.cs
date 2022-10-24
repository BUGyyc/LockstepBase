using UnityEngine;

namespace Entitas.VisualDebugging.Unity
{

    public static class ContextObserverExtension
    {
        public static ContextObserverBehaviour FindContextObserver(this IContext context)
        {
            ContextObserverBehaviour[] array = Object.FindObjectsOfType<ContextObserverBehaviour>();
            foreach (ContextObserverBehaviour contextObserverBehaviour in array)
            {
                if (contextObserverBehaviour.contextObserver.context == context)
                {
                    return contextObserverBehaviour;
                }
            }
            return null;
        }
    }
}