using System.Collections.Generic;
using DesperateDevs.Serialization;

namespace Entitas.CodeGeneration.Plugins;

public class IgnoreNamespacesConfig : AbstractConfigurableConfig
{
	private const string IGNORE_NAMESPACES_KEY = "Entitas.CodeGeneration.Plugins.IgnoreNamespaces";

	public override Dictionary<string, string> defaultProperties => new Dictionary<string, string> { { "Entitas.CodeGeneration.Plugins.IgnoreNamespaces", "false" } };

	public bool ignoreNamespaces
	{
		get
		{
			return base._preferences.get_Item("Entitas.CodeGeneration.Plugins.IgnoreNamespaces") == "true";
		}
		set
		{
			base._preferences.set_Item("Entitas.CodeGeneration.Plugins.IgnoreNamespaces", value.ToString());
		}
	}
}
