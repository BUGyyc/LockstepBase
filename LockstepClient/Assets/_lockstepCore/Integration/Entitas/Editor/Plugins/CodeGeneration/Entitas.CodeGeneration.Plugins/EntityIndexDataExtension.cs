using System.Collections.Generic;

namespace Entitas.CodeGeneration.Plugins;

public static class EntityIndexDataExtension
{
	public const string ENTITY_INDEX_TYPE = "EntityIndex.Type";

	public const string ENTITY_INDEX_IS_CUSTOM = "EntityIndex.Custom";

	public const string ENTITY_INDEX_CUSTOM_METHODS = "EntityIndex.CustomMethods";

	public const string ENTITY_INDEX_NAME = "EntityIndex.Name";

	public const string ENTITY_INDEX_CONTEXT_NAMES = "EntityIndex.ContextNames";

	public const string ENTITY_INDEX_KEY_TYPE = "EntityIndex.KeyType";

	public const string ENTITY_INDEX_COMPONENT_TYPE = "EntityIndex.ComponentType";

	public const string ENTITY_INDEX_MEMBER_NAME = "EntityIndex.MemberName";

	public const string ENTITY_INDEX_HAS_MULTIPLE = "EntityIndex.HasMultiple";

	public static string GetEntityIndexType(this EntityIndexData data)
	{
		return (string)((Dictionary<string, object>)(object)data)["EntityIndex.Type"];
	}

	public static void SetEntityIndexType(this EntityIndexData data, string type)
	{
		((Dictionary<string, object>)(object)data)["EntityIndex.Type"] = type;
	}

	public static bool IsCustom(this EntityIndexData data)
	{
		return (bool)((Dictionary<string, object>)(object)data)["EntityIndex.Custom"];
	}

	public static void IsCustom(this EntityIndexData data, bool isCustom)
	{
		((Dictionary<string, object>)(object)data)["EntityIndex.Custom"] = isCustom;
	}

	public static MethodData[] GetCustomMethods(this EntityIndexData data)
	{
		return (MethodData[])((Dictionary<string, object>)(object)data)["EntityIndex.CustomMethods"];
	}

	public static void SetCustomMethods(this EntityIndexData data, MethodData[] methods)
	{
		((Dictionary<string, object>)(object)data)["EntityIndex.CustomMethods"] = methods;
	}

	public static string GetEntityIndexName(this EntityIndexData data)
	{
		return (string)((Dictionary<string, object>)(object)data)["EntityIndex.Name"];
	}

	public static void SetEntityIndexName(this EntityIndexData data, string name)
	{
		((Dictionary<string, object>)(object)data)["EntityIndex.Name"] = name;
	}

	public static string[] GetContextNames(this EntityIndexData data)
	{
		return (string[])((Dictionary<string, object>)(object)data)["EntityIndex.ContextNames"];
	}

	public static void SetContextNames(this EntityIndexData data, string[] contextNames)
	{
		((Dictionary<string, object>)(object)data)["EntityIndex.ContextNames"] = contextNames;
	}

	public static string GetKeyType(this EntityIndexData data)
	{
		return (string)((Dictionary<string, object>)(object)data)["EntityIndex.KeyType"];
	}

	public static void SetKeyType(this EntityIndexData data, string type)
	{
		((Dictionary<string, object>)(object)data)["EntityIndex.KeyType"] = type;
	}

	public static string GetComponentType(this EntityIndexData data)
	{
		return (string)((Dictionary<string, object>)(object)data)["EntityIndex.ComponentType"];
	}

	public static void SetComponentType(this EntityIndexData data, string type)
	{
		((Dictionary<string, object>)(object)data)["EntityIndex.ComponentType"] = type;
	}

	public static string GetMemberName(this EntityIndexData data)
	{
		return (string)((Dictionary<string, object>)(object)data)["EntityIndex.MemberName"];
	}

	public static void SetMemberName(this EntityIndexData data, string memberName)
	{
		((Dictionary<string, object>)(object)data)["EntityIndex.MemberName"] = memberName;
	}

	public static bool GetHasMultiple(this EntityIndexData data)
	{
		return (bool)((Dictionary<string, object>)(object)data)["EntityIndex.HasMultiple"];
	}

	public static void SetHasMultiple(this EntityIndexData data, bool hasMultiple)
	{
		((Dictionary<string, object>)(object)data)["EntityIndex.HasMultiple"] = hasMultiple;
	}
}
