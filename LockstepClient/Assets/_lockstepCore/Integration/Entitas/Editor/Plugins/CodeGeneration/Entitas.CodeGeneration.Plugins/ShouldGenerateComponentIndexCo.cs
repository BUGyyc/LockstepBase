using System;
using System.Collections.Generic;
using System.Linq;
using Entitas.CodeGeneration.Attributes;

namespace Entitas.CodeGeneration.Plugins;

public class ShouldGenerateComponentIndexComponentDataProvider : IComponentDataProvider
{
	public void Provide(Type type, ComponentData data)
	{
		data.ShouldGenerateIndex(getGenerateIndex(type));
	}

	private bool getGenerateIndex(Type type)
	{
		return Attribute.GetCustomAttributes(type).OfType<DontGenerateAttribute>().SingleOrDefault()?.generateIndex ?? true;
	}
}
public static class ShouldGenerateComponentIndexComponentDataExtension
{
	public const string COMPONENT_GENERATE_INDEX = "Component.Generate.Index";

	public static bool ShouldGenerateIndex(this ComponentData data)
	{
		return (bool)((Dictionary<string, object>)(object)data)["Component.Generate.Index"];
	}

	public static void ShouldGenerateIndex(this ComponentData data, bool generate)
	{
		((Dictionary<string, object>)(object)data)["Component.Generate.Index"] = generate;
	}
}
