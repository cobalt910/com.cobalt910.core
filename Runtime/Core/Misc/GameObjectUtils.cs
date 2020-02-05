using UnityEngine;

namespace ProjectCore.Misc
{
    public static class GameObjectUtils
    {
        public static void AutoResolveComponent<T>(this GameObject container, out T component) where T : Component
        {
            Debug.LogError("Component has not been assigned in inspector, try auto-resolve.");
            component = container.GetComponent<T>();
            if (component == null) component = container.GetComponentInChildren<T>();
        }
    }
}