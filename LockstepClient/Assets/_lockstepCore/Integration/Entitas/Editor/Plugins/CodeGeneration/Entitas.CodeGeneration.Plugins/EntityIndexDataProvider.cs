using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DesperateDevs.CodeGeneration;
using DesperateDevs.CodeGeneration.CodeGenerator;
using DesperateDevs.Serialization;
using DesperateDevs.Utils;
using Entitas.CodeGeneration.Attributes;

namespace Entitas.CodeGeneration.Plugins;

public class EntityIndexDataProvider : IDataProvider, ICodeGenerationPlugin, IConfigurable, ICachable, IDoctor
{
	private readonly CodeGeneratorConfig _codeGeneratorConfig = new CodeGeneratorConfig();

	private readonly AssembliesConfig _assembliesConfig = new AssembliesConfig();

	private readonly IgnoreNamespacesConfig _ignoreNamespacesConfig = new IgnoreNamespacesConfig();

	private readonly ContextsComponentDataProvider _contextsComponentDataProvider = new ContextsComponentDataProvider();

	private readonly Type[] _types;

	public string name => "Entity Index";

	public int priority => 0;

	public bool runInDryMode => true;

	public Dictionary<string, string> defaultProperties => DictionaryExtension.Merge<string, string>(((AbstractConfigurableConfig)_assembliesConfig).get_defaultProperties(), new Dictionary<string, string>[2]
	{
		((AbstractConfigurableConfig)_ignoreNamespacesConfig).get_defaultProperties(),
		_contextsComponentDataProvider.defaultProperties
	});

	public Dictionary<string, object> objectCache { get; set; }

	public EntityIndexDataProvider()
		: this(null)
	{
	}

	public EntityIndexDataProvider(Type[] types)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		_types = types;
	}

	public void Configure(Preferences preferences)
	{
		((AbstractConfigurableConfig)_codeGeneratorConfig).Configure(preferences);
		((AbstractConfigurableConfig)_assembliesConfig).Configure(preferences);
		((AbstractConfigurableConfig)_ignoreNamespacesConfig).Configure(preferences);
		_contextsComponentDataProvider.Configure(preferences);
	}

	public CodeGeneratorData[] GetData()
	{
		Type[] source = _types ?? PluginUtil.GetCachedAssemblyResolver(objectCache, _assembliesConfig.assemblies, _codeGeneratorConfig.get_searchPaths()).GetTypes();
		IEnumerable<EntityIndexData> first = (from kv in (from type in source
				where !type.IsAbstract
				where InterfaceTypeExtension.ImplementsInterface<IComponent>(type)
				select type).ToDictionary((Type type) => type, (Type type) => PublicMemberInfoExtension.GetPublicMemberInfos(type))
			where kv.Value.Any((PublicMemberInfo info) => info.attributes.Any((AttributeInfo attr) => attr.attribute is AbstractEntityIndexAttribute))
			select kv).SelectMany((KeyValuePair<Type, List<PublicMemberInfo>> kv) => createEntityIndexData(kv.Key, kv.Value));
		IEnumerable<EntityIndexData> second = (from type in source
			where !type.IsAbstract
			where Attribute.IsDefined(type, typeof(CustomEntityIndexAttribute))
			select type).Select(createCustomEntityIndexData);
		return (CodeGeneratorData[])(object)first.Concat(second).ToArray();
	}

	private EntityIndexData[] createEntityIndexData(Type type, List<PublicMemberInfo> infos)
	{
		bool hasMultiple = infos.Count((PublicMemberInfo i) => i.attributes.Count((AttributeInfo attr) => attr.attribute is AbstractEntityIndexAttribute) == 1) > 1;
		return infos.Where((PublicMemberInfo i) => i.attributes.Count((AttributeInfo attr) => attr.attribute is AbstractEntityIndexAttribute) == 1).Select(delegate(PublicMemberInfo info)
		{
			//IL_0034: Unknown result type (might be due to invalid IL or missing references)
			//IL_003a: Expected O, but got Unknown
			EntityIndexData entityIndexData = new EntityIndexData();
			AbstractEntityIndexAttribute attribute = (AbstractEntityIndexAttribute)info.attributes.Single((AttributeInfo attr) => attr.attribute is AbstractEntityIndexAttribute).attribute;
			entityIndexData.SetEntityIndexType(getEntityIndexType(attribute));
			entityIndexData.IsCustom(isCustom: false);
			entityIndexData.SetEntityIndexName(SerializationTypeExtension.ToCompilableString(type).ToComponentName(_ignoreNamespacesConfig.ignoreNamespaces));
			entityIndexData.SetKeyType(SerializationTypeExtension.ToCompilableString(info.type));
			entityIndexData.SetComponentType(SerializationTypeExtension.ToCompilableString(type));
			entityIndexData.SetMemberName(info.name);
			entityIndexData.SetHasMultiple(hasMultiple);
			entityIndexData.SetContextNames(_contextsComponentDataProvider.GetContextNamesOrDefault(type));
			return entityIndexData;
		}).ToArray();
	}

	private EntityIndexData createCustomEntityIndexData(Type type)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Expected O, but got Unknown
		EntityIndexData entityIndexData = new EntityIndexData();
		CustomEntityIndexAttribute val = (CustomEntityIndexAttribute)type.GetCustomAttributes(typeof(CustomEntityIndexAttribute), inherit: false)[0];
		entityIndexData.SetEntityIndexType(SerializationTypeExtension.ToCompilableString(type));
		entityIndexData.IsCustom(isCustom: true);
		entityIndexData.SetEntityIndexName(SerializationTypeExtension.RemoveDots(SerializationTypeExtension.ToCompilableString(type)));
		entityIndexData.SetHasMultiple(hasMultiple: false);
		entityIndexData.SetContextNames(new string[1] { EntitasStringExtension.RemoveContextSuffix(SerializationTypeExtension.ShortTypeName(SerializationTypeExtension.ToCompilableString(val.contextType))) });
		MethodData[] methods = (from method in type.GetMethods(BindingFlags.Instance | BindingFlags.Public)
			where Attribute.IsDefined(method, typeof(EntityIndexGetMethodAttribute))
			select new MethodData(SerializationTypeExtension.ToCompilableString(method.ReturnType), method.Name, (from p in method.GetParameters()
				select new MemberData(SerializationTypeExtension.ToCompilableString(p.ParameterType), p.Name)).ToArray())).ToArray();
		entityIndexData.SetCustomMethods(methods);
		return entityIndexData;
	}

	private string getEntityIndexType(AbstractEntityIndexAttribute attribute)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Invalid comparison between Unknown and I4
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		EntityIndexType entityIndexType = attribute.entityIndexType;
		if ((int)entityIndexType != 0)
		{
			if ((int)entityIndexType == 1)
			{
				return "Entitas.PrimaryEntityIndex";
			}
			throw new Exception("Unhandled EntityIndexType: " + attribute.entityIndexType);
		}
		return "Entitas.EntityIndex";
	}

	public Diagnosis Diagnose()
	{
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Expected O, but got Unknown
		if (AppDomainExtension.GetAllTypes(AppDomain.CurrentDomain).Any((Type type) => type.FullName == "DesperateDevs.CodeGeneration.CodeGenerator.CLI.Program"))
		{
			string fullName = typeof(EntityIndexDataProvider).FullName;
			if (_codeGeneratorConfig.get_dataProviders().Contains(fullName))
			{
				return new Diagnosis(fullName + " loads and reflects " + string.Join(", ", _assembliesConfig.assemblies) + " and therefore doesn't support server mode!", "Don't use the code generator in server mode with " + fullName, (DiagnosisSeverity)1);
			}
		}
		return Diagnosis.get_Healthy();
	}

	public bool Fix()
	{
		return false;
	}
}
