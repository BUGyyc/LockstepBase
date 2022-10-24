using System;
using System.Collections.Generic;
using DesperateDevs.Utils;

namespace Entitas.CodeGeneration.Plugins;

public class ShouldGenerateComponentComponentDataProvider : IComponentDataProvider
{
	public void Provide(Type type, ComponentData data)
	{
		bool flag = !InterfaceTypeExtension.ImplementsInterface<IComponent>(type);
		data.ShouldGenerateComponent(flag);
		if (flag)
		{
			data.SetObjectTypeName(SerializationTypeExtension.ToCompilableString(type));
		}
	}
}
public static class ShouldGenerateComponentComponentDataExtension
{
	public const string COMPONENT_GENERATE_COMPONENT = "Component.Generate.Object";

	public const string COMPONENT_OBJECT_TYPE = "Component.ObjectTypeName";

	public static bool ShouldGenerateComponent(this ComponentData data)
	{
		return (bool)((Dictionary<string, object>)(object)data)["Component.Generate.Object"];
	}

	public static void ShouldGenerateComponent(this ComponentData data, bool generate)
	{
		((Dictionary<string, object>)(object)data)["Component.Generate.Object"] = generate;
	}

	public static string GetObjectTypeName(this ComponentData data)
	{
		return (string)((Dictionary<string, object>)(object)data)["Component.ObjectTypeName"];
	}

	public static void SetObjectTypeName(this ComponentData data, string type)
	{
		((Dictionary<string, object>)(object)data)["Component.ObjectTypeName"] = type;
	}
}
