using System.Collections.Generic;

namespace Entitas.CodeGeneration.Plugins;

public static class FlagPrefixComponentDataExtension
{
	public const string COMPONENT_FLAG_PREFIX = "Component.FlagPrefix";

	public static string GetFlagPrefix(this ComponentData data)
	{
		return (string)((Dictionary<string, object>)(object)data)["Component.FlagPrefix"];
	}

	public static void SetFlagPrefix(this ComponentData data, string prefix)
	{
		((Dictionary<string, object>)(object)data)["Component.FlagPrefix"] = prefix;
	}
}
