using System.Collections.Generic;
using System.Linq;
using DesperateDevs.CodeGeneration;
using DesperateDevs.Serialization;
using DesperateDevs.Utils;

namespace Entitas.CodeGeneration.Plugins;

public class EntityIndexGenerator : ICodeGenerator, ICodeGenerationPlugin, IConfigurable
{
	private readonly IgnoreNamespacesConfig _ignoreNamespacesConfig = new IgnoreNamespacesConfig();

	private const string CLASS_TEMPLATE = "public partial class Contexts {\n\n${indexConstants}\n\n    [Entitas.CodeGeneration.Attributes.PostConstructor]\n    public void InitializeEntityIndices() {\n${addIndices}\n    }\n}\n\npublic static class ContextsExtensions {\n\n${getIndices}\n}";

	private const string INDEX_CONSTANTS_TEMPLATE = "    public const string ${IndexName} = \"${IndexName}\";";

	private const string ADD_INDEX_TEMPLATE = "        ${contextName}.AddEntityIndex(new ${IndexType}<${ContextName}Entity, ${KeyType}>(\n            ${IndexName},\n            ${contextName}.GetGroup(${ContextName}Matcher.${Matcher}),\n            (e, c) => ((${ComponentType})c).${MemberName}));";

	private const string ADD_CUSTOM_INDEX_TEMPLATE = "        ${contextName}.AddEntityIndex(new ${IndexType}(${contextName}));";

	private const string GET_INDEX_TEMPLATE = "    public static System.Collections.Generic.HashSet<${ContextName}Entity> GetEntitiesWith${IndexName}(this ${ContextName}Context context, ${KeyType} ${MemberName}) {\n        return ((${IndexType}<${ContextName}Entity, ${KeyType}>)context.GetEntityIndex(Contexts.${IndexName})).GetEntities(${MemberName});\n    }";

	private const string GET_PRIMARY_INDEX_TEMPLATE = "    public static ${ContextName}Entity GetEntityWith${IndexName}(this ${ContextName}Context context, ${KeyType} ${MemberName}) {\n        return ((${IndexType}<${ContextName}Entity, ${KeyType}>)context.GetEntityIndex(Contexts.${IndexName})).GetEntity(${MemberName});\n    }";

	private const string CUSTOM_METHOD_TEMPLATE = "    public static ${ReturnType} ${MethodName}(this ${ContextName}Context context, ${methodArgs}) {\n        return ((${IndexType})(context.GetEntityIndex(Contexts.${IndexName}))).${MethodName}(${args});\n    }\n";

	public string name => "Entity Index";

	public int priority => 0;

	public bool runInDryMode => true;

	public Dictionary<string, string> defaultProperties => ((AbstractConfigurableConfig)_ignoreNamespacesConfig).get_defaultProperties();

	public void Configure(Preferences preferences)
	{
		((AbstractConfigurableConfig)_ignoreNamespacesConfig).Configure(preferences);
	}

	public CodeGenFile[] Generate(CodeGeneratorData[] data)
	{
		EntityIndexData[] array = (from d in data.OfType<EntityIndexData>()
			orderby d.GetEntityIndexName()
			select d).ToArray();
		if (array.Length != 0)
		{
			return generateEntityIndices(array);
		}
		return (CodeGenFile[])(object)new CodeGenFile[0];
	}

	private CodeGenFile[] generateEntityIndices(EntityIndexData[] data)
	{
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Expected O, but got Unknown
		string newValue = string.Join("\n", data.Select((EntityIndexData d) => "    public const string ${IndexName} = \"${IndexName}\";".Replace("${IndexName}", d.GetHasMultiple() ? (d.GetEntityIndexName() + StringExtension.UppercaseFirst(d.GetMemberName())) : d.GetEntityIndexName())).ToArray());
		string newValue2 = string.Join("\n\n", data.Select(generateAddMethods).ToArray());
		string newValue3 = string.Join("\n\n", data.Select(generateGetMethods).ToArray());
		string text = "public partial class Contexts {\n\n${indexConstants}\n\n    [Entitas.CodeGeneration.Attributes.PostConstructor]\n    public void InitializeEntityIndices() {\n${addIndices}\n    }\n}\n\npublic static class ContextsExtensions {\n\n${getIndices}\n}".Replace("${indexConstants}", newValue).Replace("${addIndices}", newValue2).Replace("${getIndices}", newValue3);
		return (CodeGenFile[])(object)new CodeGenFile[1]
		{
			new CodeGenFile("Contexts.cs", text, GetType().FullName)
		};
	}

	private string generateAddMethods(EntityIndexData data)
	{
		return string.Join("\n", data.GetContextNames().Aggregate(new List<string>(), delegate(List<string> addMethods, string contextName)
		{
			addMethods.Add(generateAddMethod(data, contextName));
			return addMethods;
		}).ToArray());
	}

	private string generateAddMethod(EntityIndexData data, string contextName)
	{
		if (!data.IsCustom())
		{
			return generateMethods(data, contextName);
		}
		return generateCustomMethods(data);
	}

	private string generateCustomMethods(EntityIndexData data)
	{
		return "        ${contextName}.AddEntityIndex(new ${IndexType}(${contextName}));".Replace("${contextName}", StringExtension.LowercaseFirst(data.GetContextNames()[0])).Replace("${IndexType}", data.GetEntityIndexType());
	}

	private string generateMethods(EntityIndexData data, string contextName)
	{
		return "        ${contextName}.AddEntityIndex(new ${IndexType}<${ContextName}Entity, ${KeyType}>(\n            ${IndexName},\n            ${contextName}.GetGroup(${ContextName}Matcher.${Matcher}),\n            (e, c) => ((${ComponentType})c).${MemberName}));".Replace("${contextName}", StringExtension.LowercaseFirst(contextName)).Replace("${ContextName}", contextName).Replace("${IndexName}", data.GetHasMultiple() ? (data.GetEntityIndexName() + StringExtension.UppercaseFirst(data.GetMemberName())) : data.GetEntityIndexName())
			.Replace("${Matcher}", data.GetEntityIndexName())
			.Replace("${IndexType}", data.GetEntityIndexType())
			.Replace("${KeyType}", data.GetKeyType())
			.Replace("${ComponentType}", data.GetComponentType())
			.Replace("${MemberName}", data.GetMemberName())
			.Replace("${componentName}", StringExtension.LowercaseFirst(data.GetComponentType().ToComponentName(_ignoreNamespacesConfig.ignoreNamespaces)).AddPrefixIfIsKeyword());
	}

	private string generateGetMethods(EntityIndexData data)
	{
		return string.Join("\n\n", data.GetContextNames().Aggregate(new List<string>(), delegate(List<string> getMethods, string contextName)
		{
			getMethods.Add(generateGetMethod(data, contextName));
			return getMethods;
		}).ToArray());
	}

	private string generateGetMethod(EntityIndexData data, string contextName)
	{
		string text = "";
		if (data.GetEntityIndexType() == "Entitas.EntityIndex")
		{
			text = "    public static System.Collections.Generic.HashSet<${ContextName}Entity> GetEntitiesWith${IndexName}(this ${ContextName}Context context, ${KeyType} ${MemberName}) {\n        return ((${IndexType}<${ContextName}Entity, ${KeyType}>)context.GetEntityIndex(Contexts.${IndexName})).GetEntities(${MemberName});\n    }";
		}
		else
		{
			if (!(data.GetEntityIndexType() == "Entitas.PrimaryEntityIndex"))
			{
				return getCustomMethods(data);
			}
			text = "    public static ${ContextName}Entity GetEntityWith${IndexName}(this ${ContextName}Context context, ${KeyType} ${MemberName}) {\n        return ((${IndexType}<${ContextName}Entity, ${KeyType}>)context.GetEntityIndex(Contexts.${IndexName})).GetEntity(${MemberName});\n    }";
		}
		return text.Replace("${ContextName}", contextName).Replace("${IndexName}", data.GetHasMultiple() ? (data.GetEntityIndexName() + StringExtension.UppercaseFirst(data.GetMemberName())) : data.GetEntityIndexName()).Replace("${IndexType}", data.GetEntityIndexType())
			.Replace("${KeyType}", data.GetKeyType())
			.Replace("${MemberName}", data.GetMemberName());
	}

	private string getCustomMethods(EntityIndexData data)
	{
		return string.Join("\n", (from m in data.GetCustomMethods()
			select "    public static ${ReturnType} ${MethodName}(this ${ContextName}Context context, ${methodArgs}) {\n        return ((${IndexType})(context.GetEntityIndex(Contexts.${IndexName}))).${MethodName}(${args});\n    }\n".Replace("${ReturnType}", m.returnType).Replace("${MethodName}", m.methodName).Replace("${ContextName}", data.GetContextNames()[0])
				.Replace("${methodArgs}", string.Join(", ", m.parameters.Select((MemberData p) => p.type + " " + p.name).ToArray()))
				.Replace("${IndexType}", data.GetEntityIndexType())
				.Replace("${IndexName}", data.GetHasMultiple() ? (data.GetEntityIndexName() + StringExtension.UppercaseFirst(data.GetMemberName())) : data.GetEntityIndexName())
				.Replace("${args}", string.Join(", ", m.parameters.Select((MemberData p) => p.name).ToArray()))).ToArray());
	}
}
