using System;
using System.Collections.Generic;
using System.Linq;
using DesperateDevs.CodeGeneration;
using DesperateDevs.CodeGeneration.CodeGenerator;
using DesperateDevs.Serialization;
using DesperateDevs.Utils;
using Entitas.CodeGeneration.Attributes;

namespace Entitas.CodeGeneration.Plugins;

public class ComponentDataProvider : IDataProvider, ICodeGenerationPlugin, IConfigurable, ICachable, IDoctor
{
	private readonly CodeGeneratorConfig _codeGeneratorConfig = new CodeGeneratorConfig();

	private readonly AssembliesConfig _assembliesConfig = new AssembliesConfig();

	private readonly ContextsComponentDataProvider _contextsComponentDataProvider = new ContextsComponentDataProvider();

	private readonly IgnoreNamespacesConfig _ignoreNamespacesConfig = new IgnoreNamespacesConfig();

	private readonly Type[] _types;

	private readonly IComponentDataProvider[] _dataProviders;

	public string name => "Component";

	public int priority => 0;

	public bool runInDryMode => true;

	public Dictionary<string, string> defaultProperties
	{
		get
		{
			Dictionary<string, string>[] array = (from i in _dataProviders.OfType<IConfigurable>()
				select i.get_defaultProperties()).ToArray();
			return DictionaryExtension.Merge<string, string>(DictionaryExtension.Merge<string, string>(DictionaryExtension.Merge<string, string>(((AbstractConfigurableConfig)_assembliesConfig).get_defaultProperties(), new Dictionary<string, string>[1] { _contextsComponentDataProvider.defaultProperties }), new Dictionary<string, string>[1] { ((AbstractConfigurableConfig)_ignoreNamespacesConfig).get_defaultProperties() }), array);
		}
	}

	public Dictionary<string, object> objectCache { get; set; }

	private static IComponentDataProvider[] getComponentDataProviders()
	{
		return new IComponentDataProvider[9]
		{
			new ComponentTypeComponentDataProvider(),
			new MemberDataComponentDataProvider(),
			new ContextsComponentDataProvider(),
			new IsUniqueComponentDataProvider(),
			new FlagPrefixComponentDataProvider(),
			new ShouldGenerateComponentComponentDataProvider(),
			new ShouldGenerateMethodsComponentDataProvider(),
			new ShouldGenerateComponentIndexComponentDataProvider(),
			new EventComponentDataProvider()
		};
	}

	public ComponentDataProvider()
		: this(null)
	{
	}

	public ComponentDataProvider(Type[] types)
		: this(types, getComponentDataProviders())
	{
	}

	protected ComponentDataProvider(Type[] types, IComponentDataProvider[] dataProviders)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		_types = types;
		_dataProviders = dataProviders;
	}

	public void Configure(Preferences preferences)
	{
		((AbstractConfigurableConfig)_codeGeneratorConfig).Configure(preferences);
		((AbstractConfigurableConfig)_assembliesConfig).Configure(preferences);
		foreach (IConfigurable item in _dataProviders.OfType<IConfigurable>())
		{
			item.Configure(preferences);
		}
		_contextsComponentDataProvider.Configure(preferences);
		((AbstractConfigurableConfig)_ignoreNamespacesConfig).Configure(preferences);
	}

	public CodeGeneratorData[] GetData()
	{
		Type[] source = _types ?? PluginUtil.GetCachedAssemblyResolver(objectCache, _assembliesConfig.assemblies, _codeGeneratorConfig.get_searchPaths()).GetTypes();
		ComponentData[] redundantData = (from type in source
			where InterfaceTypeExtension.ImplementsInterface<IComponent>(type)
			where !type.IsAbstract
			select type).Select(createDataForComponent).ToArray();
		ComponentData[] prioData = (from type in source
			where !InterfaceTypeExtension.ImplementsInterface<IComponent>(type)
			where !type.IsGenericType
			select type).Where(hasContexts).SelectMany(createDataForNonComponent).ToArray();
		ComponentData[] array = merge(prioData, redundantData);
		ComponentData[] prioData2 = array.Where((ComponentData data) => data.IsEvent()).SelectMany(createDataForEvents).ToArray();
		return (CodeGeneratorData[])(object)merge(prioData2, array);
	}

	private ComponentData[] merge(ComponentData[] prioData, ComponentData[] redundantData)
	{
		ILookup<string, ComponentData> lookup = prioData.ToLookup((ComponentData data) => data.GetTypeName());
		return redundantData.Where((ComponentData data) => !lookup.Contains(data.GetTypeName())).Concat(prioData).ToArray();
	}

	private ComponentData createDataForComponent(Type type)
	{
		ComponentData componentData = new ComponentData();
		IComponentDataProvider[] dataProviders = _dataProviders;
		for (int i = 0; i < dataProviders.Length; i++)
		{
			dataProviders[i].Provide(type, componentData);
		}
		return componentData;
	}

	private ComponentData[] createDataForNonComponent(Type type)
	{
		return getComponentNames(type).Select(delegate(string componentName)
		{
			ComponentData componentData = createDataForComponent(type);
			componentData.SetTypeName(EntitasStringExtension.AddComponentSuffix(componentName));
			componentData.SetMemberData(new MemberData[1]
			{
				new MemberData(SerializationTypeExtension.ToCompilableString(type), "value")
			});
			return componentData;
		}).ToArray();
	}

	private ComponentData[] createDataForEvents(ComponentData data)
	{
		return data.GetContextNames().SelectMany((string contextName) => data.GetEventData().Select(delegate(EventData eventData)
		{
			ComponentData componentData = new ComponentData((CodeGeneratorData)(object)data);
			componentData.IsEvent(isEvent: false);
			componentData.IsUnique(isUnique: false);
			componentData.ShouldGenerateComponent(generate: false);
			string text = data.EventComponentName(eventData);
			string eventTypeSuffix = eventData.GetEventTypeSuffix();
			string text2 = ((componentData.GetContextNames().Length > 1) ? contextName : string.Empty) + text + EntitasStringExtension.AddListenerSuffix(eventTypeSuffix);
			componentData.SetTypeName(EntitasStringExtension.AddComponentSuffix(text2));
			componentData.SetMemberData(new MemberData[1]
			{
				new MemberData("System.Collections.Generic.List<I" + text2 + ">", "value")
			});
			componentData.SetContextNames(new string[1] { contextName });
			return componentData;
		}).ToArray()).ToArray();
	}

	private bool hasContexts(Type type)
	{
		return _contextsComponentDataProvider.GetContextNames(type).Length != 0;
	}

	private string[] getComponentNames(Type type)
	{
		ComponentNameAttribute val = Attribute.GetCustomAttributes(type).OfType<ComponentNameAttribute>().SingleOrDefault();
		if (val == null)
		{
			return new string[1] { EntitasStringExtension.AddComponentSuffix(SerializationTypeExtension.ShortTypeName(SerializationTypeExtension.ToCompilableString(type))) };
		}
		return val.componentNames;
	}

	public Diagnosis Diagnose()
	{
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Expected O, but got Unknown
		if (AppDomainExtension.GetAllTypes(AppDomain.CurrentDomain).Any((Type type) => type.FullName == "DesperateDevs.CodeGeneration.CodeGenerator.CLI.Program"))
		{
			string fullName = typeof(ComponentDataProvider).FullName;
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
