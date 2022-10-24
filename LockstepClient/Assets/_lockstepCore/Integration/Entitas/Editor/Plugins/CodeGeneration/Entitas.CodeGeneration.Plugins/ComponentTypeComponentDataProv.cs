using System;
using DesperateDevs.Utils;

namespace Entitas.CodeGeneration.Plugins;

public class ComponentTypeComponentDataProvider : IComponentDataProvider
{
	public void Provide(Type type, ComponentData data)
	{
		data.SetTypeName(SerializationTypeExtension.ToCompilableString(type));
	}
}
