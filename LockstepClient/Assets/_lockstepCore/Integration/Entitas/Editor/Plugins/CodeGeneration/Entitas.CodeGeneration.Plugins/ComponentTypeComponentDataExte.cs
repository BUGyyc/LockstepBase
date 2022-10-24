using System.Collections.Generic;

namespace Entitas.CodeGeneration.Plugins;

public static class ComponentTypeComponentDataExtension
{
	public const string COMPONENT_TYPE = "Component.TypeName";

	public static string GetTypeName(this ComponentData data)
	{
		return (string)((Dictionary<string, object>)(object)data)["Component.TypeName"];
	}

	public static void SetTypeName(this ComponentData data, string fullTypeName)
	{
		((Dictionary<string, object>)(object)data)["Component.TypeName"] = fullTypeName;
	}
}
