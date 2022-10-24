using System;

namespace Entitas
{

    public static class EntitasStringExtension
    {
        public const string CONTEXT_SUFFIX = "Context";

        public const string ENTITY_SUFFIX = "Entity";

        public const string COMPONENT_SUFFIX = "Component";

        public const string SYSTEM_SUFFIX = "System";

        public const string MATCHER_SUFFIX = "Matcher";

        public const string LISTENER_SUFFIX = "Listener";

        public static string AddContextSuffix(this string str)
        {
            return addSuffix(str, "Context");
        }

        public static string RemoveContextSuffix(this string str)
        {
            return removeSuffix(str, "Context");
        }

        public static bool HasContextSuffix(this string str)
        {
            return hasSuffix(str, "Context");
        }

        public static string AddEntitySuffix(this string str)
        {
            return addSuffix(str, "Entity");
        }

        public static string RemoveEntitySuffix(this string str)
        {
            return removeSuffix(str, "Entity");
        }

        public static bool HasEntitySuffix(this string str)
        {
            return hasSuffix(str, "Entity");
        }

        public static string AddComponentSuffix(this string str)
        {
            return addSuffix(str, "Component");
        }

        public static string RemoveComponentSuffix(this string str)
        {
            return removeSuffix(str, "Component");
        }

        public static bool HasComponentSuffix(this string str)
        {
            return hasSuffix(str, "Component");
        }

        public static string AddSystemSuffix(this string str)
        {
            return addSuffix(str, "System");
        }

        public static string RemoveSystemSuffix(this string str)
        {
            return removeSuffix(str, "System");
        }

        public static bool HasSystemSuffix(this string str)
        {
            return hasSuffix(str, "System");
        }

        public static string AddMatcherSuffix(this string str)
        {
            return addSuffix(str, "Matcher");
        }

        public static string RemoveMatcherSuffix(this string str)
        {
            return removeSuffix(str, "Matcher");
        }

        public static bool HasMatcherSuffix(this string str)
        {
            return hasSuffix(str, "Matcher");
        }

        public static string AddListenerSuffix(this string str)
        {
            return addSuffix(str, "Listener");
        }

        public static string RemoveListenerSuffix(this string str)
        {
            return removeSuffix(str, "Listener");
        }

        public static bool HasListenerSuffix(this string str)
        {
            return hasSuffix(str, "Listener");
        }

        private static string addSuffix(string str, string suffix)
        {
            if (!hasSuffix(str, suffix))
            {
                return str + suffix;
            }
            return str;
        }

        private static string removeSuffix(string str, string suffix)
        {
            if (!hasSuffix(str, suffix))
            {
                return str;
            }
            return str.Substring(0, str.Length - suffix.Length);
        }

        private static bool hasSuffix(string str, string suffix)
        {
            return str.EndsWith(suffix, StringComparison.Ordinal);
        }
    }
}