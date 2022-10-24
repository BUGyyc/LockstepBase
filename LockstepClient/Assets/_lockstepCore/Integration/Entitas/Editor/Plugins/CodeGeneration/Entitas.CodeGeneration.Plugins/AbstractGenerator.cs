using System.Collections.Generic;
using DesperateDevs.CodeGeneration;
using DesperateDevs.Serialization;

namespace Entitas.CodeGeneration.Plugins;

public abstract class AbstractGenerator : ICodeGenerator, ICodeGenerationPlugin, IConfigurable
{
	private readonly IgnoreNamespacesConfig _ignoreNamespacesConfig = new IgnoreNamespacesConfig();

	public abstract string name { get; }

	public int priority => 0;

	public bool runInDryMode => true;

	public Dictionary<string, string> defaultProperties => ((AbstractConfigurableConfig)_ignoreNamespacesConfig).get_defaultProperties();

	public void Configure(Preferences preferences)
	{
		((AbstractConfigurableConfig)_ignoreNamespacesConfig).Configure(preferences);
		CodeGeneratorExtentions.ignoreNamespaces = _ignoreNamespacesConfig.ignoreNamespaces;
	}

	public abstract CodeGenFile[] Generate(CodeGeneratorData[] data);
}
