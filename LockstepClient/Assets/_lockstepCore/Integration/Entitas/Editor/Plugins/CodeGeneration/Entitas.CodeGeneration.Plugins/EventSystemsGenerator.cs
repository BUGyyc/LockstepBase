using System.Collections.Generic;
using System.IO;
using System.Linq;
using DesperateDevs.CodeGeneration;

namespace Entitas.CodeGeneration.Plugins;

public class EventSystemsGenerator : AbstractGenerator
{
	private struct DataTuple
	{
		public ComponentData componentData;

		public EventData eventData;
	}

	private const string TEMPLATE = "public sealed class ${ContextName}EventSystems : Feature {\r\n\r\n    public ${ContextName}EventSystems(Contexts contexts) {\r\n${systemsList}\r\n    }\r\n}\r\n";

	private const string SYSTEM_ADD_TEMPLATE = "        Add(new ${Event}EventSystem(contexts)); // priority: ${priority}";

	public override string name => "Event (Systems)";

	public override CodeGenFile[] Generate(CodeGeneratorData[] data)
	{
		return generate((from d in data.OfType<ComponentData>()
			where d.IsEvent()
			select d).ToArray());
	}

	private CodeGenFile[] generate(ComponentData[] data)
	{
		Dictionary<string, List<ComponentData>> dictionary = data.Aggregate(new Dictionary<string, List<ComponentData>>(), delegate(Dictionary<string, List<ComponentData>> dict, ComponentData d)
		{
			string[] contextNames = d.GetContextNames();
			foreach (string key2 in contextNames)
			{
				if (!dict.ContainsKey(key2))
				{
					dict.Add(key2, new List<ComponentData>());
				}
				dict[key2].Add(d);
			}
			return dict;
		});
		Dictionary<string, List<DataTuple>> dictionary2 = new Dictionary<string, List<DataTuple>>();
		string[] array = dictionary.Keys.ToArray();
		foreach (string key in array)
		{
			List<DataTuple> value = (from tuple in dictionary[key].SelectMany((ComponentData d) => d.GetEventData().Select(delegate(EventData eventData)
				{
					DataTuple result = default(DataTuple);
					result.componentData = d;
					result.eventData = eventData;
					return result;
				}).ToArray())
				orderby tuple.eventData.priority, tuple.componentData.ComponentName()
				select tuple).ToList();
			dictionary2.Add(key, value);
		}
		return generate(dictionary2);
	}

	private CodeGenFile[] generate(Dictionary<string, List<DataTuple>> contextNameToDataTuple)
	{
		return contextNameToDataTuple.Select((KeyValuePair<string, List<DataTuple>> kv) => generateSystem(kv.Key, kv.Value.ToArray())).ToArray();
	}

	private CodeGenFile generateSystem(string contextName, DataTuple[] data)
	{
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Expected O, but got Unknown
		string text = "public sealed class ${ContextName}EventSystems : Feature {\r\n\r\n    public ${ContextName}EventSystems(Contexts contexts) {\r\n${systemsList}\r\n    }\r\n}\r\n".Replace("${systemsList}", generateSystemList(contextName, data)).Replace(contextName);
		return new CodeGenFile("Events" + Path.DirectorySeparatorChar + contextName + "EventSystems.cs", text, GetType().FullName);
	}

	private string generateSystemList(string contextName, DataTuple[] data)
	{
		return string.Join("\n", data.SelectMany((DataTuple tuple) => generateSystemListForData(contextName, tuple)).ToArray());
	}

	private string[] generateSystemListForData(string contextName, DataTuple data)
	{
		return (from ctxName in data.componentData.GetContextNames()
			where ctxName == contextName
			select generateAddSystem(ctxName, data)).ToArray();
	}

	private string generateAddSystem(string contextName, DataTuple data)
	{
		return "        Add(new ${Event}EventSystem(contexts)); // priority: ${priority}".Replace(data.componentData, contextName, data.eventData).Replace("${priority}", data.eventData.priority.ToString()).Replace("${Event}", data.componentData.Event(contextName, data.eventData));
	}
}
