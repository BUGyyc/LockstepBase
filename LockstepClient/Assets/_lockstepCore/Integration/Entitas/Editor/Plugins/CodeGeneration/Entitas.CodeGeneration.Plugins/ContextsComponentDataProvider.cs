using System;
using System.Collections.Generic;
using System.Linq;
using DesperateDevs.Serialization;
using Entitas.CodeGeneration.Attributes;

namespace Entitas.CodeGeneration.Plugins;

public class ContextsComponentDataProvider : IComponentDataProvider, IConfigurable
{
	private readonly ContextNamesConfig _contextNamesConfig = new ContextNamesConfig();

	public Dictionary<string, string> defaultProperties => ((AbstractConfigurableConfig)_contextNamesConfig).get_defaultProperties();

	public void Configure(Preferences preferences)
	{
		((AbstractConfigurableConfig)_contextNamesConfig).Configure(preferences);
	}

	public void Provide(Type type, ComponentData data)
	{
		string[] contextNamesOrDefault = GetContextNamesOrDefault(type);
		data.SetContextNames(contextNamesOrDefault);
	}

	public string[] GetContextNames(Type type)
	{
		return (from attr in Attribute.GetCustomAttributes(type).OfType<ContextAttribute>()
			select attr.contextName).ToArray();
	}

	public string[] GetContextNamesOrDefault(Type type)
	{
		string[] array = GetContextNames(type);
		if (array.Length == 0)
		{
			array = new string[1] { _contextNamesConfig.contextNames[0] };
		}
		return array;
	}
}
