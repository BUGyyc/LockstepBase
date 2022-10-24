using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DesperateDevs.CodeGeneration;

namespace Entitas.CodeGeneration.Plugins;

public class EventListenerComponentGenerator : AbstractGenerator
{
	private const string TEMPLATE = "[Entitas.CodeGeneration.Attributes.DontGenerate(false)]\npublic sealed class ${EventListenerComponent} : Entitas.IComponent {\n    public System.Collections.Generic.List<I${EventListener}> value;\n}\n";

	public override string name => "Event (Listener Component)";

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
		return ((IEnumerable<EventData>)data.GetEventData()).Select((Func<EventData, CodeGenFile>)((EventData eventData) => new CodeGenFile("Events" + Path.DirectorySeparatorChar + "Components" + Path.DirectorySeparatorChar + EntitasStringExtension.AddComponentSuffix(data.EventListener(contextName, eventData)) + ".cs", "[Entitas.CodeGeneration.Attributes.DontGenerate(false)]\npublic sealed class ${EventListenerComponent} : Entitas.IComponent {\n    public System.Collections.Generic.List<I${EventListener}> value;\n}\n".Replace(data, contextName, eventData), GetType().FullName))).ToArray();
	}
}
