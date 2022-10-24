using System;
using System.Linq;
using Entitas.CodeGeneration.Attributes;

namespace Entitas.CodeGeneration.Plugins;

public class IsUniqueComponentDataProvider : IComponentDataProvider
{
	public void Provide(Type type, ComponentData data)
	{
		bool isUnique = Attribute.GetCustomAttributes(type).OfType<UniqueAttribute>().Any();
		data.IsUnique(isUnique);
	}
}
