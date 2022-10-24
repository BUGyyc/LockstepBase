using System.Collections.Generic;
using DesperateDevs.Serialization;
using DesperateDevs.Utils;

namespace Entitas.CodeGeneration.Plugins;

public class AssembliesConfig : AbstractConfigurableConfig
{
	private const string ASSEMBLIES_KEY = "Entitas.CodeGeneration.Plugins.Assemblies";

	public override Dictionary<string, string> defaultProperties => new Dictionary<string, string> { { "Entitas.CodeGeneration.Plugins.Assemblies", "Library/ScriptAssemblies/Assembly-CSharp.dll" } };

	public string[] assemblies
	{
		get
		{
			return StringExtension.ArrayFromCSV(base._preferences.get_Item("Entitas.CodeGeneration.Plugins.Assemblies"));
		}
		set
		{
			base._preferences.set_Item("Entitas.CodeGeneration.Plugins.Assemblies", StringExtension.ToCSV(value));
		}
	}
}
