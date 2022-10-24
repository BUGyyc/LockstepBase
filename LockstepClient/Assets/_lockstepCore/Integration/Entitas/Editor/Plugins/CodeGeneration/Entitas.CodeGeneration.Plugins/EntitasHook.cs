using System;
using System.Collections.Generic;
using System.Linq;
using DesperateDevs.Analytics;
using DesperateDevs.CodeGeneration;
using DesperateDevs.CodeGeneration.CodeGenerator;
using DesperateDevs.Utils;

namespace Entitas.CodeGeneration.Plugins;

public class EntitasHook : CodeGeneratorTrackingHook
{
	protected override string name => "entitas";

	protected override TrackingData GetData()
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Expected O, but got Unknown
		Type[] allTypes = AppDomainExtension.GetAllTypes(AppDomain.CurrentDomain);
		UserTrackingData val = new UserTrackingData();
		((Dictionary<string, string>)(object)val).Add("x", "v:" + EntitasResources.GetVersion() + ",e:" + (allTypes.Any((Type type) => type.FullName == "DesperateDevs.CodeGeneration.CodeGenerator.CLI.Program") ? "s" : "u") + ",p:" + (allTypes.Any((Type type) => type.FullName == "DesperateDevs.Roslyn.CodeGeneration.Plugins.PluginUtil") ? "1" : "0") + ",f:" + base._files.Length + ",cp:" + base._files.Count((CodeGenFile f) => f.get_fileName().EndsWith("Component.cs", StringComparison.OrdinalIgnoreCase)) + ",cx:" + base._files.Count((CodeGenFile f) => f.get_fileName().EndsWith("Context.cs", StringComparison.OrdinalIgnoreCase)) + ",l:" + base._files.Select((CodeGenFile file) => StringExtension.ToUnixLineEndings(file.get_fileContent())).Sum((string content) => content.Split('\n').Length));
		return (TrackingData)(object)val;
	}
}
