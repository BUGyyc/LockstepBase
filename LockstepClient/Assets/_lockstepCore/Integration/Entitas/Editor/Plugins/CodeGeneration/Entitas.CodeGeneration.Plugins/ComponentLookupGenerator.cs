using System.Collections.Generic;
using System.IO;
using System.Linq;
using DesperateDevs.CodeGeneration;

namespace Entitas.CodeGeneration.Plugins;

public class ComponentLookupGenerator : AbstractGenerator
{
	private const string TEMPLATE = "public static class ${Lookup} {\n\n${componentConstantsList}\n\n${totalComponentsConstant}\n\n    public static readonly string[] componentNames = {\n${componentNamesList}\n    };\n\n    public static readonly System.Type[] componentTypes = {\n${componentTypesList}\n    };\n}\n";

	private const string COMPONENT_CONSTANT_TEMPLATE = "    public const int ${ComponentName} = ${Index};";

	private const string TOTAL_COMPONENTS_CONSTANT_TEMPLATE = "    public const int TotalComponents = ${totalComponents};";

	private const string COMPONENT_NAME_TEMPLATE = "        \"${ComponentName}\"";

	private const string COMPONENT_TYPE_TEMPLATE = "        typeof(${ComponentType})";

	public override string name => "Component (Lookup)";

	public override CodeGenFile[] Generate(CodeGeneratorData[] data)
	{
		CodeGenFile[] array = generateLookups((from d in data.OfType<ComponentData>()
			where d.ShouldGenerateIndex()
			select d).ToArray());
		HashSet<string> existingFileNames = new HashSet<string>(array.Select((CodeGenFile file) => file.get_fileName()));
		CodeGenFile[] second = (from file in generateEmptyLookups(data.OfType<ContextData>().ToArray())
			where !existingFileNames.Contains(file.get_fileName())
			select file).ToArray();
		return array.Concat(second).ToArray();
	}

	private CodeGenFile[] generateEmptyLookups(ContextData[] data)
	{
		ComponentData[] emptyData = new ComponentData[0];
		return data.Select((ContextData d) => generateComponentsLookupClass(d.GetContextName(), emptyData)).ToArray();
	}

	private CodeGenFile[] generateLookups(ComponentData[] data)
	{
		Dictionary<string, List<ComponentData>> dictionary = data.Aggregate(new Dictionary<string, List<ComponentData>>(), delegate(Dictionary<string, List<ComponentData>> dict, ComponentData d)
		{
			string[] contextNames = d.GetContextNames();
			foreach (string key2 in contextNames)
			{
				if (!dict.ContainsKey(key2))
				{
					dict.Add(key2, new List<ComponentData>());
				}
				dict[key2].Add(d);
			}
			return dict;
		});
		string[] array = dictionary.Keys.ToArray();
		foreach (string key in array)
		{
			dictionary[key] = dictionary[key].OrderBy((ComponentData d) => d.GetTypeName()).ToList();
		}
		return dictionary.Select((KeyValuePair<string, List<ComponentData>> kv) => generateComponentsLookupClass(kv.Key, kv.Value.ToArray())).ToArray();
	}

	private CodeGenFile generateComponentsLookupClass(string contextName, ComponentData[] data)
	{
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Expected O, but got Unknown
		string newValue = string.Join("\n", data.Select((ComponentData d, int index) => "    public const int ${ComponentName} = ${Index};".Replace("${ComponentName}", d.ComponentName()).Replace("${Index}", index.ToString())).ToArray());
		string newValue2 = "    public const int TotalComponents = ${totalComponents};".Replace("${totalComponents}", data.Length.ToString());
		string newValue3 = string.Join(",\n", data.Select((ComponentData d) => "        \"${ComponentName}\"".Replace("${ComponentName}", d.ComponentName())).ToArray());
		string newValue4 = string.Join(",\n", data.Select((ComponentData d) => "        typeof(${ComponentType})".Replace("${ComponentType}", d.GetTypeName())).ToArray());
		string text = "public static class ${Lookup} {\n\n${componentConstantsList}\n\n${totalComponentsConstant}\n\n    public static readonly string[] componentNames = {\n${componentNamesList}\n    };\n\n    public static readonly System.Type[] componentTypes = {\n${componentTypesList}\n    };\n}\n".Replace("${Lookup}", contextName + "ComponentsLookup").Replace("${componentConstantsList}", newValue).Replace("${totalComponentsConstant}", newValue2)
			.Replace("${componentNamesList}", newValue3)
			.Replace("${componentTypesList}", newValue4);
		return new CodeGenFile(contextName + Path.DirectorySeparatorChar + contextName + "ComponentsLookup.cs", text, GetType().FullName);
	}
}
