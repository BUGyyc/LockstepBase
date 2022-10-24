using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DesperateDevs.CodeGeneration;
using Entitas.CodeGeneration.Attributes;

namespace Entitas.CodeGeneration.Plugins;

public class EventSystemGenerator : AbstractGenerator
{
	private const string ANY_TARGET_TEMPLATE = "public sealed class ${Event}EventSystem : Entitas.ReactiveSystem<${EntityType}> {\r\n\r\n    readonly Entitas.IGroup<${EntityType}> _listeners;\r\n    readonly System.Collections.Generic.List<${EntityType}> _entityBuffer;\r\n    readonly System.Collections.Generic.List<I${EventListener}> _listenerBuffer;\r\n\r\n    public ${Event}EventSystem(Contexts contexts) : base(contexts.${contextName}) {\r\n        _listeners = contexts.${contextName}.GetGroup(${MatcherType}.${EventListener});\r\n        _entityBuffer = new System.Collections.Generic.List<${EntityType}>();\r\n        _listenerBuffer = new System.Collections.Generic.List<I${EventListener}>();\r\n    }\r\n\r\n    protected override Entitas.ICollector<${EntityType}> GetTrigger(Entitas.IContext<${EntityType}> context) {\r\n        return Entitas.CollectorContextExtension.CreateCollector(\r\n            context, Entitas.TriggerOnEventMatcherExtension.${GroupEvent}(${MatcherType}.${ComponentName})\r\n        );\r\n    }\r\n\r\n    protected override bool Filter(${EntityType} entity) {\r\n        return ${filter};\r\n    }\r\n\r\n    protected override void Execute(System.Collections.Generic.List<${EntityType}> entities) {\r\n        foreach (var e in entities) {\r\n            ${cachedAccess}\r\n            foreach (var listenerEntity in _listeners.GetEntities(_entityBuffer)) {\r\n                _listenerBuffer.Clear();\r\n                _listenerBuffer.AddRange(listenerEntity.${eventListener}.value);\r\n                foreach (var listener in _listenerBuffer) {\r\n                    listener.On${EventComponentName}${EventType}(e${methodArgs});\r\n                }\r\n            }\r\n        }\r\n    }\r\n}\r\n";

	private const string SELF_TARGET_TEMPLATE = "public sealed class ${Event}EventSystem : Entitas.ReactiveSystem<${EntityType}> {\r\n\r\n    readonly System.Collections.Generic.List<I${EventListener}> _listenerBuffer;\r\n\r\n    public ${Event}EventSystem(Contexts contexts) : base(contexts.${contextName}) {\r\n        _listenerBuffer = new System.Collections.Generic.List<I${EventListener}>();\r\n    }\r\n\r\n    protected override Entitas.ICollector<${EntityType}> GetTrigger(Entitas.IContext<${EntityType}> context) {\r\n        return Entitas.CollectorContextExtension.CreateCollector(\r\n            context, Entitas.TriggerOnEventMatcherExtension.${GroupEvent}(${MatcherType}.${ComponentName})\r\n        );\r\n    }\r\n\r\n    protected override bool Filter(${EntityType} entity) {\r\n        return ${filter};\r\n    }\r\n\r\n    protected override void Execute(System.Collections.Generic.List<${EntityType}> entities) {\r\n        foreach (var e in entities) {\r\n            ${cachedAccess}\r\n            _listenerBuffer.Clear();\r\n            _listenerBuffer.AddRange(e.${eventListener}.value);\r\n            foreach (var listener in _listenerBuffer) {\r\n                listener.On${ComponentName}${EventType}(e${methodArgs});\r\n            }\r\n        }\r\n    }\r\n}\r\n";

	public override string name => "Event (System)";

	public override CodeGenFile[] Generate(CodeGeneratorData[] data)
	{
		return (from d in data.OfType<ComponentData>()
			where d.IsEvent()
			select d).SelectMany(generate).ToArray();
	}

	private CodeGenFile[] generate(ComponentData data)
	{
		return data.GetContextNames().SelectMany((string contextName) => generate(contextName, data)).ToArray();
	}

	private CodeGenFile[] generate(string contextName, ComponentData data)
	{
		return ((IEnumerable<EventData>)data.GetEventData()).Select((Func<EventData, CodeGenFile>)delegate(EventData eventData)
		{
			//IL_0079: Unknown result type (might be due to invalid IL or missing references)
			//IL_007f: Invalid comparison between Unknown and I4
			//IL_008e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0094: Invalid comparison between Unknown and I4
			//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
			//IL_0176: Unknown result type (might be due to invalid IL or missing references)
			//IL_017c: Expected O, but got Unknown
			string newValue = data.GetEventMethodArgs(eventData, ", " + ((data.GetMemberData().Length == 0) ? data.PrefixedComponentName() : getMethodArgs(data.GetMemberData())));
			string newValue2 = ((data.GetMemberData().Length == 0) ? string.Empty : ("var component = e." + data.ComponentNameValidLowercaseFirst() + ";"));
			if ((int)eventData.eventType == 1)
			{
				newValue = string.Empty;
				newValue2 = string.Empty;
			}
			string obj = (((int)eventData.eventTarget == 1) ? "public sealed class ${Event}EventSystem : Entitas.ReactiveSystem<${EntityType}> {\r\n\r\n    readonly System.Collections.Generic.List<I${EventListener}> _listenerBuffer;\r\n\r\n    public ${Event}EventSystem(Contexts contexts) : base(contexts.${contextName}) {\r\n        _listenerBuffer = new System.Collections.Generic.List<I${EventListener}>();\r\n    }\r\n\r\n    protected override Entitas.ICollector<${EntityType}> GetTrigger(Entitas.IContext<${EntityType}> context) {\r\n        return Entitas.CollectorContextExtension.CreateCollector(\r\n            context, Entitas.TriggerOnEventMatcherExtension.${GroupEvent}(${MatcherType}.${ComponentName})\r\n        );\r\n    }\r\n\r\n    protected override bool Filter(${EntityType} entity) {\r\n        return ${filter};\r\n    }\r\n\r\n    protected override void Execute(System.Collections.Generic.List<${EntityType}> entities) {\r\n        foreach (var e in entities) {\r\n            ${cachedAccess}\r\n            _listenerBuffer.Clear();\r\n            _listenerBuffer.AddRange(e.${eventListener}.value);\r\n            foreach (var listener in _listenerBuffer) {\r\n                listener.On${ComponentName}${EventType}(e${methodArgs});\r\n            }\r\n        }\r\n    }\r\n}\r\n" : "public sealed class ${Event}EventSystem : Entitas.ReactiveSystem<${EntityType}> {\r\n\r\n    readonly Entitas.IGroup<${EntityType}> _listeners;\r\n    readonly System.Collections.Generic.List<${EntityType}> _entityBuffer;\r\n    readonly System.Collections.Generic.List<I${EventListener}> _listenerBuffer;\r\n\r\n    public ${Event}EventSystem(Contexts contexts) : base(contexts.${contextName}) {\r\n        _listeners = contexts.${contextName}.GetGroup(${MatcherType}.${EventListener});\r\n        _entityBuffer = new System.Collections.Generic.List<${EntityType}>();\r\n        _listenerBuffer = new System.Collections.Generic.List<I${EventListener}>();\r\n    }\r\n\r\n    protected override Entitas.ICollector<${EntityType}> GetTrigger(Entitas.IContext<${EntityType}> context) {\r\n        return Entitas.CollectorContextExtension.CreateCollector(\r\n            context, Entitas.TriggerOnEventMatcherExtension.${GroupEvent}(${MatcherType}.${ComponentName})\r\n        );\r\n    }\r\n\r\n    protected override bool Filter(${EntityType} entity) {\r\n        return ${filter};\r\n    }\r\n\r\n    protected override void Execute(System.Collections.Generic.List<${EntityType}> entities) {\r\n        foreach (var e in entities) {\r\n            ${cachedAccess}\r\n            foreach (var listenerEntity in _listeners.GetEntities(_entityBuffer)) {\r\n                _listenerBuffer.Clear();\r\n                _listenerBuffer.AddRange(listenerEntity.${eventListener}.value);\r\n                foreach (var listener in _listenerBuffer) {\r\n                    listener.On${EventComponentName}${EventType}(e${methodArgs});\r\n                }\r\n            }\r\n        }\r\n    }\r\n}\r\n");
			EventType eventType = eventData.eventType;
			string text = obj.Replace("${GroupEvent}", ((object)(EventType)(ref eventType)).ToString()).Replace("${filter}", getFilter(data, contextName, eventData)).Replace("${cachedAccess}", newValue2)
				.Replace("${methodArgs}", newValue)
				.Replace(data, contextName, eventData);
			return new CodeGenFile("Events" + Path.DirectorySeparatorChar + "Systems" + Path.DirectorySeparatorChar + data.Event(contextName, eventData) + "EventSystem.cs", text, GetType().FullName);
		}).ToArray();
	}

	private string getFilter(ComponentData data, string contextName, EventData eventData)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Invalid comparison between Unknown and I4
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Invalid comparison between Unknown and I4
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Invalid comparison between Unknown and I4
		string text = string.Empty;
		if (data.GetMemberData().Length == 0)
		{
			EventType eventType = eventData.eventType;
			if ((int)eventType != 0)
			{
				if ((int)eventType == 1)
				{
					text = "!entity." + data.PrefixedComponentName();
				}
			}
			else
			{
				text = "entity." + data.PrefixedComponentName();
			}
		}
		else
		{
			EventType eventType = eventData.eventType;
			if ((int)eventType != 0)
			{
				if ((int)eventType == 1)
				{
					text = "!entity.has" + data.ComponentName();
				}
			}
			else
			{
				text = "entity.has" + data.ComponentName();
			}
		}
		if ((int)eventData.eventTarget == 1)
		{
			text = text + " && entity.has" + data.EventListener(contextName, eventData);
		}
		return text;
	}

	private string getMethodArgs(MemberData[] memberData)
	{
		return string.Join(", ", memberData.Select((MemberData info) => "component." + info.name).ToArray());
	}
}
