using System.Collections.Generic;
using DesperateDevs.Utils;

namespace Entitas.CodeGeneration.Plugins;

public static class PluginUtil
{
	public const string ASSEMBLY_RESOLVER_KEY = "Entitas.CodeGeneration.Plugins.AssemblyResolver";

	public static AssemblyResolver GetCachedAssemblyResolver(Dictionary<string, object> objectCache, string[] assemblies, string[] basePaths)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Expected O, but got Unknown
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Expected O, but got Unknown
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Expected O, but got Unknown
		if (!objectCache.TryGetValue("Entitas.CodeGeneration.Plugins.AssemblyResolver", out var value))
		{
			value = (object)new AssemblyResolver(false, basePaths);
			AssemblyResolver val = (AssemblyResolver)value;
			foreach (string text in assemblies)
			{
				val.Load(text);
			}
			objectCache.Add("Entitas.CodeGeneration.Plugins.AssemblyResolver", value);
		}
		return (AssemblyResolver)value;
	}
}
