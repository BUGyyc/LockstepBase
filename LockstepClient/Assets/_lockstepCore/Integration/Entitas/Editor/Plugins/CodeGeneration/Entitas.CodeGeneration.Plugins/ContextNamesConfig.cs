using System.Collections.Generic;
using DesperateDevs.Serialization;
using DesperateDevs.Utils;

namespace Entitas.CodeGeneration.Plugins;

public class ContextNamesConfig : AbstractConfigurableConfig
{
	private const string CONTEXTS_KEY = "Entitas.CodeGeneration.Plugins.Contexts";

	public override Dictionary<string, string> defaultProperties => new Dictionary<string, string> { { "Entitas.CodeGeneration.Plugins.Contexts", "Game, Input" } };

	public string[] contextNames
	{
		get
		{
			return StringExtension.ArrayFromCSV(base._preferences.get_Item("Entitas.CodeGeneration.Plugins.Contexts"));
		}
		set
		{
			base._preferences.set_Item("Entitas.CodeGeneration.Plugins.Contexts", StringExtension.ToCSV(value));
		}
	}
}
