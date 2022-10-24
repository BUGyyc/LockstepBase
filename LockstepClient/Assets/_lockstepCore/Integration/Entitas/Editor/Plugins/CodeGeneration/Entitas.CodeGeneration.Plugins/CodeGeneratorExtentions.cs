using System.CodeDom.Compiler;
using System.Linq;
using DesperateDevs.Utils;

namespace Entitas.CodeGeneration.Plugins;

public static class CodeGeneratorExtentions
{
	public const string LOOKUP = "ComponentsLookup";

	private const string KEYWORD_PREFIX = "@";

	public static bool ignoreNamespaces;

	private static readonly CodeDomProvider provider = CodeDomProvider.CreateProvider("C#");

	public static string ComponentName(this ComponentData data)
	{
		return data.GetTypeName().ToComponentName(ignoreNamespaces);
	}

	public static string ComponentNameValidLowercaseFirst(this ComponentData data)
	{
		return StringExtension.LowercaseFirst(data.ComponentName()).AddPrefixIfIsKeyword();
	}

	public static string ComponentNameWithContext(this ComponentData data, string contextName)
	{
		return contextName + data.ComponentName();
	}

	public static string Replace(this string template, string contextName)
	{
		return template.Replace("${ContextName}", contextName).Replace("${contextName}", StringExtension.LowercaseFirst(contextName)).Replace("${ContextType}", EntitasStringExtension.AddContextSuffix(contextName))
			.Replace("${EntityType}", EntitasStringExtension.AddEntitySuffix(contextName))
			.Replace("${MatcherType}", EntitasStringExtension.AddMatcherSuffix(contextName))
			.Replace("${Lookup}", contextName + "ComponentsLookup");
	}

	public static string Replace(this string template, ComponentData data, string contextName)
	{
		return template.Replace(contextName).Replace("${ComponentType}", data.GetTypeName()).Replace("${ComponentName}", data.ComponentName())
			.Replace("${componentName}", StringExtension.LowercaseFirst(data.ComponentName()))
			.Replace("${validComponentName}", data.ComponentNameValidLowercaseFirst())
			.Replace("${prefixedComponentName}", data.PrefixedComponentName())
			.Replace("${newMethodParameters}", data.GetMemberData().GetMethodParameters(newPrefix: true))
			.Replace("${methodParameters}", data.GetMemberData().GetMethodParameters(newPrefix: false))
			.Replace("${newMethodArgs}", GetMethodArgs(data.GetMemberData(), newPrefix: true))
			.Replace("${methodArgs}", GetMethodArgs(data.GetMemberData(), newPrefix: false))
			.Replace("${Index}", contextName + "ComponentsLookup." + data.ComponentName());
	}

	public static string Replace(this string template, ComponentData data, string contextName, EventData eventData)
	{
		string text = data.EventListener(contextName, eventData);
		return template.Replace(data, contextName).Replace("${EventComponentName}", data.EventComponentName(eventData)).Replace("${EventListenerComponent}", EntitasStringExtension.AddComponentSuffix(text))
			.Replace("${Event}", data.Event(contextName, eventData))
			.Replace("${EventListener}", text)
			.Replace("${eventListener}", StringExtension.LowercaseFirst(text))
			.Replace("${EventType}", eventData.GetEventTypeSuffix());
	}

	public static string PrefixedComponentName(this ComponentData data)
	{
		return StringExtension.LowercaseFirst(data.GetFlagPrefix()) + data.ComponentName();
	}

	public static string Event(this ComponentData data, string contextName, EventData eventData)
	{
		return ((data.GetContextNames().Length > 1) ? contextName : string.Empty) + data.EventComponentName(eventData) + eventData.GetEventTypeSuffix();
	}

	public static string EventListener(this ComponentData data, string contextName, EventData eventData)
	{
		return EntitasStringExtension.AddListenerSuffix(data.Event(contextName, eventData));
	}

	public static string EventComponentName(this ComponentData data, EventData eventData)
	{
		string text = data.GetTypeName().ToComponentName(ignoreNamespaces);
		string text2 = data.GetTypeName().ToComponentName(ignoreNamespaces: true);
		return text.Replace(text2, eventData.GetEventPrefix() + text2);
	}

	public static string GetEventMethodArgs(this ComponentData data, EventData eventData, string args)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Invalid comparison between Unknown and I4
		if (data.GetMemberData().Length == 0)
		{
			return string.Empty;
		}
		if ((int)eventData.eventType != 1)
		{
			return args;
		}
		return string.Empty;
	}

	public static string GetEventTypeSuffix(this EventData eventData)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Invalid comparison between Unknown and I4
		if ((int)eventData.eventType != 1)
		{
			return string.Empty;
		}
		return "Removed";
	}

	public static string GetEventPrefix(this EventData eventData)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		if ((int)eventData.eventTarget != 0)
		{
			return string.Empty;
		}
		return "Any";
	}

	public static string GetMethodParameters(this MemberData[] memberData, bool newPrefix)
	{
		return string.Join(", ", memberData.Select((MemberData info) => info.type + (newPrefix ? (" new" + StringExtension.UppercaseFirst(info.name)) : (" " + info.name))).ToArray());
	}

	public static string GetMethodArgs(MemberData[] memberData, bool newPrefix)
	{
		return string.Join(", ", memberData.Select((MemberData info) => (!newPrefix) ? info.name : ("new" + StringExtension.UppercaseFirst(info.name))).ToArray());
	}

	public static string AddPrefixIfIsKeyword(this string name)
	{
		if (!provider.IsValidIdentifier(name))
		{
			name = "@" + name;
		}
		return name;
	}
}
