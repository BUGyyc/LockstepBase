using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DesperateDevs.Serialization;
using DesperateDevs.Unity.Editor;
using DesperateDevs.Utils;
using UnityEditor;
using UnityEngine;

namespace Entitas.VisualDebugging.Unity.Editor;

public static class EntityDrawer
{
	public struct ComponentInfo
	{
		public int index;

		public string name;

		public Type type;
	}

	private const string DEFAULT_INSTANCE_CREATOR_TEMPLATE_FORMAT = "using System;\nusing Entitas.VisualDebugging.Unity.Editor;\n\npublic class Default${ShortType}InstanceCreator : IDefaultInstanceCreator {\n\n    public bool HandlesType(Type type) {\n        return type == typeof(${Type});\n    }\n\n    public object CreateDefault(Type type) {\n        // TODO return an instance of type ${Type}\n        throw new NotImplementedException();\n    }\n}\n";

	private const string TYPE_DRAWER_TEMPLATE_FORMAT = "using System;\nusing Entitas;\nusing Entitas.VisualDebugging.Unity.Editor;\n\npublic class ${ShortType}TypeDrawer : ITypeDrawer {\n\n    public bool HandlesType(Type type) {\n        return type == typeof(${Type});\n    }\n\n    public object DrawAndGetNewValue(Type memberType, string memberName, object value, object target) {\n        // TODO draw the type ${Type}\n        throw new NotImplementedException();\n    }\n}\n";

	private static Dictionary<string, bool[]> _contextToUnfoldedComponents;

	private static Dictionary<string, string[]> _contextToComponentMemberSearch;

	private static Dictionary<string, GUIStyle[]> _contextToColoredBoxStyles;

	private static Dictionary<string, ComponentInfo[]> _contextToComponentInfos;

	private static GUIStyle _foldoutStyle;

	private static string _componentNameSearchString;

	public static readonly IDefaultInstanceCreator[] _defaultInstanceCreators;

	public static readonly ITypeDrawer[] _typeDrawers;

	public static readonly IComponentDrawer[] _componentDrawers;

	public static Dictionary<string, bool[]> contextToUnfoldedComponents
	{
		get
		{
			if (_contextToUnfoldedComponents == null)
			{
				_contextToUnfoldedComponents = new Dictionary<string, bool[]>();
			}
			return _contextToUnfoldedComponents;
		}
	}

	public static Dictionary<string, string[]> contextToComponentMemberSearch
	{
		get
		{
			if (_contextToComponentMemberSearch == null)
			{
				_contextToComponentMemberSearch = new Dictionary<string, string[]>();
			}
			return _contextToComponentMemberSearch;
		}
	}

	public static Dictionary<string, GUIStyle[]> contextToColoredBoxStyles
	{
		get
		{
			if (_contextToColoredBoxStyles == null)
			{
				_contextToColoredBoxStyles = new Dictionary<string, GUIStyle[]>();
			}
			return _contextToColoredBoxStyles;
		}
	}

	public static Dictionary<string, ComponentInfo[]> contextToComponentInfos
	{
		get
		{
			if (_contextToComponentInfos == null)
			{
				_contextToComponentInfos = new Dictionary<string, ComponentInfo[]>();
			}
			return _contextToComponentInfos;
		}
	}

	public static GUIStyle foldoutStyle
	{
		get
		{
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			if (_foldoutStyle == null)
			{
				_foldoutStyle = new GUIStyle(EditorStyles.get_foldout());
				_foldoutStyle.set_fontStyle((FontStyle)1);
			}
			return _foldoutStyle;
		}
	}

	public static string componentNameSearchString
	{
		get
		{
			if (_componentNameSearchString == null)
			{
				_componentNameSearchString = string.Empty;
			}
			return _componentNameSearchString;
		}
		set
		{
			_componentNameSearchString = value;
		}
	}

	public static void DrawEntity(IEntity entity)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		Color backgroundColor = GUI.get_backgroundColor();
		GUI.set_backgroundColor(Color.get_red());
		if (GUILayout.Button("Destroy Entity", (GUILayoutOption[])(object)new GUILayoutOption[0]))
		{
			entity.Destroy();
		}
		GUI.set_backgroundColor(backgroundColor);
		DrawComponents(entity);
		EditorGUILayout.Space();
		EditorGUILayout.LabelField("Retained by (" + ((IAERC)entity).get_retainCount() + ")", EditorStyles.get_boldLabel(), (GUILayoutOption[])(object)new GUILayoutOption[0]);
		IAERC aerc = entity.get_aerc();
		SafeAERC val = (SafeAERC)(object)((aerc is SafeAERC) ? aerc : null);
		if (val == null)
		{
			return;
		}
		EditorLayout.BeginVerticalBox();
		foreach (object item in from o in val.get_owners()
			orderby o.GetType().Name
			select o)
		{
			EditorGUILayout.BeginHorizontal((GUILayoutOption[])(object)new GUILayoutOption[0]);
			EditorGUILayout.LabelField(item.ToString(), (GUILayoutOption[])(object)new GUILayoutOption[0]);
			if (EditorLayout.MiniButton("Release"))
			{
				((IAERC)entity).Release(item);
			}
			EditorGUILayout.EndHorizontal();
		}
		EditorLayout.EndVerticalBox();
	}

	public static void DrawMultipleEntities(IEntity[] entities)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		EditorGUILayout.Space();
		EditorGUILayout.BeginHorizontal((GUILayoutOption[])(object)new GUILayoutOption[0]);
		IEntity val = entities[0];
		int num = drawAddComponentMenu(val);
		IEntity[] array;
		if (num >= 0)
		{
			Type type = val.get_contextInfo().componentTypes[num];
			array = entities;
			foreach (IEntity obj in array)
			{
				IComponent val2 = obj.CreateComponent(num, type);
				obj.AddComponent(num, val2);
			}
		}
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.Space();
		Color backgroundColor = GUI.get_backgroundColor();
		GUI.set_backgroundColor(Color.get_red());
		if (GUILayout.Button("Destroy selected entities", (GUILayoutOption[])(object)new GUILayoutOption[0]))
		{
			array = entities;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Destroy();
			}
		}
		GUI.set_backgroundColor(backgroundColor);
		EditorGUILayout.Space();
		array = entities;
		foreach (IEntity val3 in array)
		{
			EditorGUILayout.BeginHorizontal((GUILayoutOption[])(object)new GUILayoutOption[0]);
			EditorGUILayout.LabelField(((object)val3).ToString(), (GUILayoutOption[])(object)new GUILayoutOption[0]);
			backgroundColor = GUI.get_backgroundColor();
			GUI.set_backgroundColor(Color.get_red());
			if (EditorLayout.MiniButton("Destroy Entity"))
			{
				val3.Destroy();
			}
			GUI.set_backgroundColor(backgroundColor);
			EditorGUILayout.EndHorizontal();
		}
	}

	public static void DrawComponents(IEntity entity)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		bool[] unfoldedComponents = getUnfoldedComponents(entity);
		string[] componentMemberSearch = getComponentMemberSearch(entity);
		EditorLayout.BeginVerticalBox();
		EditorGUILayout.BeginHorizontal((GUILayoutOption[])(object)new GUILayoutOption[0]);
		EditorGUILayout.LabelField("Components (" + entity.GetComponents().Length + ")", EditorStyles.get_boldLabel(), (GUILayoutOption[])(object)new GUILayoutOption[0]);
		if (EditorLayout.MiniButtonLeft("▸"))
		{
			for (int i = 0; i < unfoldedComponents.Length; i++)
			{
				unfoldedComponents[i] = false;
			}
		}
		if (EditorLayout.MiniButtonRight("▾"))
		{
			for (int j = 0; j < unfoldedComponents.Length; j++)
			{
				unfoldedComponents[j] = true;
			}
		}
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.Space();
		int num = drawAddComponentMenu(entity);
		if (num >= 0)
		{
			Type type = entity.get_contextInfo().componentTypes[num];
			IComponent val = entity.CreateComponent(num, type);
			entity.AddComponent(num, val);
		}
		EditorGUILayout.Space();
		componentNameSearchString = EditorLayout.SearchTextField(componentNameSearchString);
		EditorGUILayout.Space();
		int[] componentIndices = entity.GetComponentIndices();
		IComponent[] components = entity.GetComponents();
		for (int k = 0; k < components.Length; k++)
		{
			DrawComponent(unfoldedComponents, componentMemberSearch, entity, componentIndices[k], components[k]);
		}
		EditorLayout.EndVerticalBox();
	}

	public static void DrawComponent(bool[] unfoldedComponents, string[] componentMemberSearch, IEntity entity, int index, IComponent component)
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		Type type = ((object)component).GetType();
		string text = EntitasStringExtension.RemoveComponentSuffix(type.Name);
		if (!EditorLayout.MatchesSearchString(text.ToLower(), componentNameSearchString.ToLower()))
		{
			return;
		}
		EditorGUILayout.BeginVertical(getColoredBoxStyle(entity, index), (GUILayoutOption[])(object)new GUILayoutOption[0]);
		if (!Attribute.IsDefined(type, typeof(DontDrawComponentAttribute)))
		{
			List<PublicMemberInfo> publicMemberInfos = PublicMemberInfoExtension.GetPublicMemberInfos(type);
			EditorGUILayout.BeginHorizontal((GUILayoutOption[])(object)new GUILayoutOption[0]);
			if (publicMemberInfos.Count == 0)
			{
				EditorGUILayout.LabelField(text, EditorStyles.get_boldLabel(), (GUILayoutOption[])(object)new GUILayoutOption[0]);
			}
			else
			{
				unfoldedComponents[index] = EditorLayout.Foldout(unfoldedComponents[index], text, foldoutStyle, 11);
				if (unfoldedComponents[index])
				{
					componentMemberSearch[index] = ((publicMemberInfos.Count > 5) ? EditorLayout.SearchTextField(componentMemberSearch[index]) : string.Empty);
				}
			}
			if (EditorLayout.MiniButton("-"))
			{
				entity.RemoveComponent(index);
			}
			EditorGUILayout.EndHorizontal();
			if (unfoldedComponents[index])
			{
				IComponent val = entity.CreateComponent(index, type);
				PublicMemberInfoExtension.CopyPublicMemberValues((object)component, (object)val);
				bool flag = false;
				IComponentDrawer componentDrawer = getComponentDrawer(type);
				if (componentDrawer != null)
				{
					EditorGUI.BeginChangeCheck();
					componentDrawer.DrawComponent(val);
					flag = EditorGUI.EndChangeCheck();
				}
				else
				{
					foreach (PublicMemberInfo item in publicMemberInfos)
					{
						if (EditorLayout.MatchesSearchString(item.name.ToLower(), componentMemberSearch[index].ToLower()))
						{
							object value = item.GetValue((object)val);
							if (DrawObjectMember((value == null) ? item.type : value.GetType(), item.name, value, (object)val, (Action<object, object>)item.SetValue))
							{
								flag = true;
							}
						}
					}
				}
				if (flag)
				{
					entity.ReplaceComponent(index, val);
				}
				else
				{
					entity.GetComponentPool(index).Push(val);
				}
			}
		}
		else
		{
			EditorGUILayout.LabelField(text, "[DontDrawComponent]", EditorStyles.get_boldLabel(), (GUILayoutOption[])(object)new GUILayoutOption[0]);
		}
		EditorLayout.EndVerticalBox();
	}

	public static bool DrawObjectMember(Type memberType, string memberName, object value, object target, Action<object, object> setValue)
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Expected O, but got Unknown
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		if (value == null)
		{
			EditorGUI.BeginChangeCheck();
			bool flag = memberType == typeof(Object) || memberType.IsSubclassOf(typeof(Object));
			EditorGUILayout.BeginHorizontal((GUILayoutOption[])(object)new GUILayoutOption[0]);
			if (flag)
			{
				setValue(target, EditorGUILayout.ObjectField(memberName, (Object)value, memberType, true, (GUILayoutOption[])(object)new GUILayoutOption[0]));
			}
			else
			{
				EditorGUILayout.LabelField(memberName, "null", (GUILayoutOption[])(object)new GUILayoutOption[0]);
			}
			if (EditorLayout.MiniButton("new " + SerializationTypeExtension.ShortTypeName(SerializationTypeExtension.ToCompilableString(memberType))) && CreateDefault(memberType, out var defaultValue))
			{
				setValue(target, defaultValue);
			}
			EditorGUILayout.EndHorizontal();
			return EditorGUI.EndChangeCheck();
		}
		if (!memberType.IsValueType)
		{
			EditorGUILayout.BeginHorizontal((GUILayoutOption[])(object)new GUILayoutOption[0]);
			EditorGUILayout.BeginVertical((GUILayoutOption[])(object)new GUILayoutOption[0]);
		}
		EditorGUI.BeginChangeCheck();
		ITypeDrawer typeDrawer = getTypeDrawer(memberType);
		if (typeDrawer != null)
		{
			object arg = typeDrawer.DrawAndGetNewValue(memberType, memberName, value, target);
			setValue(target, arg);
		}
		else
		{
			Type type = target.GetType();
			if (!InterfaceTypeExtension.ImplementsInterface<IComponent>(type) || !Attribute.IsDefined(type, typeof(DontDrawComponentAttribute)))
			{
				EditorGUILayout.LabelField(memberName, value.ToString(), (GUILayoutOption[])(object)new GUILayoutOption[0]);
				int indentLevel = EditorGUI.get_indentLevel();
				EditorGUI.set_indentLevel(EditorGUI.get_indentLevel() + 1);
				EditorGUILayout.BeginVertical((GUILayoutOption[])(object)new GUILayoutOption[0]);
				foreach (PublicMemberInfo publicMemberInfo in PublicMemberInfoExtension.GetPublicMemberInfos(memberType))
				{
					object value2 = publicMemberInfo.GetValue(value);
					DrawObjectMember((value2 == null) ? publicMemberInfo.type : value2.GetType(), publicMemberInfo.name, value2, value, (Action<object, object>)publicMemberInfo.SetValue);
					if (memberType.IsValueType)
					{
						setValue(target, value);
					}
				}
				EditorGUILayout.EndVertical();
				EditorGUI.set_indentLevel(indentLevel);
			}
			else
			{
				drawUnsupportedType(memberType, memberName, value);
			}
		}
		if (!memberType.IsValueType)
		{
			EditorGUILayout.EndVertical();
			if (EditorLayout.MiniButton("×"))
			{
				setValue(target, null);
			}
			EditorGUILayout.EndHorizontal();
		}
		return EditorGUI.EndChangeCheck();
	}

	public static bool CreateDefault(Type type, out object defaultValue)
	{
		try
		{
			defaultValue = Activator.CreateInstance(type);
			return true;
		}
		catch (Exception)
		{
			IDefaultInstanceCreator[] defaultInstanceCreators = _defaultInstanceCreators;
			foreach (IDefaultInstanceCreator defaultInstanceCreator in defaultInstanceCreators)
			{
				if (defaultInstanceCreator.HandlesType(type))
				{
					defaultValue = defaultInstanceCreator.CreateDefault(type);
					return true;
				}
			}
		}
		string text = SerializationTypeExtension.ToCompilableString(type);
		if (EditorUtility.DisplayDialog("No IDefaultInstanceCreator found", "There's no IDefaultInstanceCreator implementation to handle the type '" + text + "'.\nProviding an IDefaultInstanceCreator enables you to create instances for that type.\n\nDo you want to generate an IDefaultInstanceCreator implementation for '" + text + "'?\n", "Generate", "Cancel"))
		{
			GenerateIDefaultInstanceCreator(text);
		}
		defaultValue = null;
		return false;
	}

	private static int drawAddComponentMenu(IEntity entity)
	{
		ComponentInfo[] array = (from info in getComponentInfos(entity)
			where !entity.HasComponent(info.index)
			select info).ToArray();
		string[] array2 = array.Select((ComponentInfo info) => info.name).ToArray();
		int num = EditorGUILayout.Popup("Add Component", -1, array2, (GUILayoutOption[])(object)new GUILayoutOption[0]);
		if (num >= 0)
		{
			return array[num].index;
		}
		return -1;
	}

	private static void drawUnsupportedType(Type memberType, string memberName, object value)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		EditorGUILayout.BeginHorizontal((GUILayoutOption[])(object)new GUILayoutOption[0]);
		EditorGUILayout.LabelField(memberName, value.ToString(), (GUILayoutOption[])(object)new GUILayoutOption[0]);
		if (EditorLayout.MiniButton("Missing ITypeDrawer"))
		{
			string text = SerializationTypeExtension.ToCompilableString(memberType);
			if (EditorUtility.DisplayDialog("No ITypeDrawer found", "There's no ITypeDrawer implementation to handle the type '" + text + "'.\nProviding an ITypeDrawer enables you draw instances for that type.\n\nDo you want to generate an ITypeDrawer implementation for '" + text + "'?\n", "Generate", "Cancel"))
			{
				GenerateITypeDrawer(text);
			}
		}
		EditorGUILayout.EndHorizontal();
	}

	public static void GenerateIDefaultInstanceCreator(string typeName)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Expected O, but got Unknown
		string defaultInstanceCreatorFolderPath = ConfigurableConfigExtension.CreateAndConfigure<VisualDebuggingConfig>(new Preferences("Entitas.properties", Environment.UserName + ".userproperties")).defaultInstanceCreatorFolderPath;
		string filePath = defaultInstanceCreatorFolderPath + Path.DirectorySeparatorChar + "Default" + SerializationTypeExtension.ShortTypeName(typeName) + "InstanceCreator.cs";
		string template = "using System;\nusing Entitas.VisualDebugging.Unity.Editor;\n\npublic class Default${ShortType}InstanceCreator : IDefaultInstanceCreator {\n\n    public bool HandlesType(Type type) {\n        return type == typeof(${Type});\n    }\n\n    public object CreateDefault(Type type) {\n        // TODO return an instance of type ${Type}\n        throw new NotImplementedException();\n    }\n}\n".Replace("${Type}", typeName).Replace("${ShortType}", SerializationTypeExtension.ShortTypeName(typeName));
		generateTemplate(defaultInstanceCreatorFolderPath, filePath, template);
	}

	public static void GenerateITypeDrawer(string typeName)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Expected O, but got Unknown
		string typeDrawerFolderPath = ConfigurableConfigExtension.CreateAndConfigure<VisualDebuggingConfig>(new Preferences("Entitas.properties", Environment.UserName + ".userproperties")).typeDrawerFolderPath;
		string filePath = typeDrawerFolderPath + Path.DirectorySeparatorChar + SerializationTypeExtension.ShortTypeName(typeName) + "TypeDrawer.cs";
		string template = "using System;\nusing Entitas;\nusing Entitas.VisualDebugging.Unity.Editor;\n\npublic class ${ShortType}TypeDrawer : ITypeDrawer {\n\n    public bool HandlesType(Type type) {\n        return type == typeof(${Type});\n    }\n\n    public object DrawAndGetNewValue(Type memberType, string memberName, object value, object target) {\n        // TODO draw the type ${Type}\n        throw new NotImplementedException();\n    }\n}\n".Replace("${Type}", typeName).Replace("${ShortType}", SerializationTypeExtension.ShortTypeName(typeName));
		generateTemplate(typeDrawerFolderPath, filePath, template);
	}

	private static void generateTemplate(string folder, string filePath, string template)
	{
		if (!Directory.Exists(folder))
		{
			Directory.CreateDirectory(folder);
		}
		File.WriteAllText(filePath, template);
		EditorApplication.set_isPlaying(false);
		AssetDatabase.Refresh();
		Selection.set_activeObject(AssetDatabase.LoadMainAssetAtPath(filePath));
	}

	static EntityDrawer()
	{
		_defaultInstanceCreators = AppDomainExtension.GetInstancesOf<IDefaultInstanceCreator>(AppDomain.CurrentDomain);
		_typeDrawers = AppDomainExtension.GetInstancesOf<ITypeDrawer>(AppDomain.CurrentDomain);
		_componentDrawers = AppDomainExtension.GetInstancesOf<IComponentDrawer>(AppDomain.CurrentDomain);
	}

	private static bool[] getUnfoldedComponents(IEntity entity)
	{
		if (!contextToUnfoldedComponents.TryGetValue(entity.get_contextInfo().name, out var value))
		{
			value = new bool[entity.get_totalComponents()];
			for (int i = 0; i < value.Length; i++)
			{
				value[i] = true;
			}
			contextToUnfoldedComponents.Add(entity.get_contextInfo().name, value);
		}
		return value;
	}

	private static string[] getComponentMemberSearch(IEntity entity)
	{
		if (!contextToComponentMemberSearch.TryGetValue(entity.get_contextInfo().name, out var value))
		{
			value = new string[entity.get_totalComponents()];
			for (int i = 0; i < value.Length; i++)
			{
				value[i] = string.Empty;
			}
			contextToComponentMemberSearch.Add(entity.get_contextInfo().name, value);
		}
		return value;
	}

	private static ComponentInfo[] getComponentInfos(IEntity entity)
	{
		if (!contextToComponentInfos.TryGetValue(entity.get_contextInfo().name, out var value))
		{
			ContextInfo contextInfo = entity.get_contextInfo();
			List<ComponentInfo> list = new List<ComponentInfo>(contextInfo.componentTypes.Length);
			for (int i = 0; i < contextInfo.componentTypes.Length; i++)
			{
				list.Add(new ComponentInfo
				{
					index = i,
					name = contextInfo.componentNames[i],
					type = contextInfo.componentTypes[i]
				});
			}
			value = list.ToArray();
			contextToComponentInfos.Add(entity.get_contextInfo().name, value);
		}
		return value;
	}

	private static GUIStyle getColoredBoxStyle(IEntity entity, int index)
	{
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Expected O, but got Unknown
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		if (!contextToColoredBoxStyles.TryGetValue(entity.get_contextInfo().name, out var value))
		{
			value = (GUIStyle[])(object)new GUIStyle[entity.get_totalComponents()];
			for (int i = 0; i < value.Length; i++)
			{
				Color color = Color.HSVToRGB((float)i / (float)entity.get_totalComponents(), 0.7f, 1f);
				color.a = 0.15f;
				GUIStyle val = new GUIStyle(GUI.get_skin().get_box());
				val.get_normal().set_background(createTexture(2, 2, color));
				value[i] = val;
			}
			contextToColoredBoxStyles.Add(entity.get_contextInfo().name, value);
		}
		return value[index];
	}

	private static Texture2D createTexture(int width, int height, Color color)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Expected O, but got Unknown
		Color[] array = (Color[])(object)new Color[width * height];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = color;
		}
		Texture2D val = new Texture2D(width, height);
		val.SetPixels(array);
		val.Apply();
		return val;
	}

	private static IComponentDrawer getComponentDrawer(Type type)
	{
		IComponentDrawer[] componentDrawers = _componentDrawers;
		foreach (IComponentDrawer componentDrawer in componentDrawers)
		{
			if (componentDrawer.HandlesType(type))
			{
				return componentDrawer;
			}
		}
		return null;
	}

	private static ITypeDrawer getTypeDrawer(Type type)
	{
		ITypeDrawer[] typeDrawers = _typeDrawers;
		foreach (ITypeDrawer typeDrawer in typeDrawers)
		{
			if (typeDrawer.HandlesType(type))
			{
				return typeDrawer;
			}
		}
		return null;
	}
}
