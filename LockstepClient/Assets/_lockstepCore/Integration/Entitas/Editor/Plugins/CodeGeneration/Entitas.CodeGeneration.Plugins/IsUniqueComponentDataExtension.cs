using System.Collections.Generic;

namespace Entitas.CodeGeneration.Plugins;

public static class IsUniqueComponentDataExtension
{
	public const string COMPONENT_IS_UNIQUE = "Component.Unique";

	public static bool IsUnique(this ComponentData data)
	{
		return (bool)((Dictionary<string, object>)(object)data)["Component.Unique"];
	}

	public static void IsUnique(this ComponentData data, bool isUnique)
	{
		((Dictionary<string, object>)(object)data)["Component.Unique"] = isUnique;
	}
}
