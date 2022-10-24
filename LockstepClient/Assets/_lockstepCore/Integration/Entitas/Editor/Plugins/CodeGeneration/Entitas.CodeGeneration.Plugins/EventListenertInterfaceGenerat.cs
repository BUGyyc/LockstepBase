using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DesperateDevs.CodeGeneration;

namespace Entitas.CodeGeneration.Plugins;

public class EventListenertInterfaceGenerator : AbstractGenerator
{
	private const string TEMPLATE = "public interface I${EventListener} {\r\n    void On${EventComponentName}${EventType}(${ContextName}Entity entity${methodParameters});\r\n}\r\n";

	public override string name => "Event (Listener Interface)";

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
			//IL_00df: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e5: Expected O, but got Unknown
			MemberData[] array = data.GetMemberData();
			if (array.Length == 0)
			{
				array = new MemberData[1]
				{
					new MemberData("bool", data.PrefixedComponentName())
				};
			}
			string text = "public interface I${EventListener} {\r\n    void On${EventComponentName}${EventType}(${ContextName}Entity entity${methodParameters});\r\n}\r\n".Replace("${methodParameters}", data.GetEventMethodArgs(eventData, ", " + array.GetMethodParameters(newPrefix: false))).Replace(data, contextName, eventData);
			return new CodeGenFile("Events" + Path.DirectorySeparatorChar + "Interfaces" + Path.DirectorySeparatorChar + "I" + data.EventListener(contextName, eventData) + ".cs", text, GetType().FullName);
		}).ToArray();
	}
}
