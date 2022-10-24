using System.Collections.Generic;

namespace Entitas.CodeGeneration.Plugins;

public static class ContextDataExtension
{
	public const string CONTEXT_NAME = "Context.Name";

	public static string GetContextName(this ContextData data)
	{
		return (string)((Dictionary<string, object>)(object)data)["Context.Name"];
	}

	public static void SetContextName(this ContextData data, string contextName)
	{
		((Dictionary<string, object>)(object)data)["Context.Name"] = contextName;
	}
}
