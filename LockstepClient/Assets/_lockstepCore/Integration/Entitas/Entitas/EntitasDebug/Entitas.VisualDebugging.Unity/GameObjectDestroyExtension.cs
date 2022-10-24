using UnityEngine;

namespace Entitas.VisualDebugging.Unity
{

    public static class GameObjectDestroyExtension
    {
        public static void DestroyGameObject(this GameObject gameObject)
        {
            //if (Application.get_isPlaying())
            if (Application.isPlaying)
            {
                Object.Destroy((Object)(object)gameObject);
            }
            else
            {
                Object.DestroyImmediate((Object)(object)gameObject);
            }
        }
    }
}