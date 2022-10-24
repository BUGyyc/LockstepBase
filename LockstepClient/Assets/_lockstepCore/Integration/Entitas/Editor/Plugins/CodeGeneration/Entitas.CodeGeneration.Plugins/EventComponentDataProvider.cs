using System;
using System.Linq;
using Entitas.CodeGeneration.Attributes;

namespace Entitas.CodeGeneration.Plugins;

public class EventComponentDataProvider : IComponentDataProvider
{
	public void Provide(Type type, ComponentData data)
	{
		EventAttribute[] array = Attribute.GetCustomAttributes(type).OfType<EventAttribute>().ToArray();
		if (array.Length != 0)
		{
			data.IsEvent(isEvent: true);
			EventData[] eventData = array.Select((EventAttribute attr) => new EventData(attr.eventTarget, attr.eventType, attr.priority)).ToArray();
			data.SetEventData(eventData);
		}
		else
		{
			data.IsEvent(isEvent: false);
		}
	}
}
