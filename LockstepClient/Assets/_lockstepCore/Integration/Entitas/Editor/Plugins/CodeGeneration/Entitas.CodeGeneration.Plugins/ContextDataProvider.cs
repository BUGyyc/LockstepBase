using System.Collections.Generic;
using System.Linq;
using DesperateDevs.CodeGeneration;
using DesperateDevs.Serialization;

namespace Entitas.CodeGeneration.Plugins;

public class ContextDataProvider : IDataProvider, ICodeGenerationPlugin, IConfigurable
{
	private readonly ContextNamesConfig _contextNamesConfig = new ContextNamesConfig();

	public string name => "Context";

	public int priority => 0;

	public bool runInDryMode => true;

	public Dictionary<string, string> defaultProperties => ((AbstractConfigurableConfig)_contextNamesConfig).get_defaultProperties();

	public void Configure(Preferences preferences)
	{
		((AbstractConfigurableConfig)_contextNamesConfig).Configure(preferences);
	}

	public CodeGeneratorData[] GetData()
	{
		return (CodeGeneratorData[])(object)_contextNamesConfig.contextNames.Select(delegate(string contextName)
		{
			ContextData contextData = new ContextData();
			contextData.SetContextName(contextName);
			return contextData;
		}).ToArray();
	}
}
