using System.Collections.Generic;
using DesperateDevs.Serialization;
using DesperateDevs.Utils;

namespace Entitas.CodeGeneration.Plugins;

public class TemplatesConfig : AbstractConfigurableConfig
{
	private const string TEMPLATES_KEY = "Entitas.CodeGeneration.Plugins.Templates";

	public override Dictionary<string, string> defaultProperties => new Dictionary<string, string> { { "Entitas.CodeGeneration.Plugins.Templates", "Plugins/Entitas/Templates" } };

	public string[] templates
	{
		get
		{
			return StringExtension.ArrayFromCSV(base._preferences.get_Item("Entitas.CodeGeneration.Plugins.Templates"));
		}
		set
		{
			base._preferences.set_Item("Entitas.CodeGeneration.Plugins.Templates", StringExtension.ToCSV(value));
		}
	}
}
