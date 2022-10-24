using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DesperateDevs.CodeGeneration;

namespace Entitas.CodeGeneration.Plugins;

public class EventEntityApiGenerator : AbstractGenerator
{
	private const string TEMPLATE = "public partial class ${EntityType} {\n\n    public void Add${EventListener}(I${EventListener} value) {\n        var listeners = has${EventListener}\n            ? ${eventListener}.value\n            : new System.Collections.Generic.List<I${EventListener}>();\n        listeners.Add(value);\n        Replace${EventListener}(listeners);\n    }\n\n    public void Remove${EventListener}(I${EventListener} value, bool removeComponentWhenEmpty = true) {\n        var listeners = ${eventListener}.value;\n        listeners.Remove(value);\n        if (removeComponentWhenEmpty && listeners.Count == 0) {\n            Remove${EventListener}();\n        } else {\n            Replace${EventListener}(listeners);\n        }\n    }\n}\n";

	public override string name => "Event (Entity API)";

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
		return ((IEnumerable<EventData>)data.GetEventData()).Select((Func<EventData, CodeGenFile>)((EventData eventData) => new CodeGenFile(contextName + Path.DirectorySeparatorChar + "Components" + Path.DirectorySeparatorChar + contextName + EntitasStringExtension.AddComponentSuffix(data.EventListener(contextName, eventData)) + ".cs", "public partial class ${EntityType} {\n\n    public void Add${EventListener}(I${EventListener} value) {\n        var listeners = has${EventListener}\n            ? ${eventListener}.value\n            : new System.Collections.Generic.List<I${EventListener}>();\n        listeners.Add(value);\n        Replace${EventListener}(listeners);\n    }\n\n    public void Remove${EventListener}(I${EventListener} value, bool removeComponentWhenEmpty = true) {\n        var listeners = ${eventListener}.value;\n        listeners.Remove(value);\n        if (removeComponentWhenEmpty && listeners.Count == 0) {\n            Remove${EventListener}();\n        } else {\n            Replace${EventListener}(listeners);\n        }\n    }\n}\n".Replace(data, contextName, eventData), GetType().FullName))).ToArray();
	}
}
