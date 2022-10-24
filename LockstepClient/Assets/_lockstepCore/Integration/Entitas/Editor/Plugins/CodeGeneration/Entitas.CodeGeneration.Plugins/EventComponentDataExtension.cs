using System.Collections.Generic;

namespace Entitas.CodeGeneration.Plugins;

public static class EventComponentDataExtension
{
	public const string COMPONENT_EVENT = "Component.Event";

	public const string COMPONENT_EVENT_DATA = "Component.Event.Data";

	public static bool IsEvent(this ComponentData data)
	{
		return (bool)((Dictionary<string, object>)(object)data)["Component.Event"];
	}

	public static void IsEvent(this ComponentData data, bool isEvent)
	{
		((Dictionary<string, object>)(object)data)["Component.Event"] = isEvent;
	}

	public static EventData[] GetEventData(this ComponentData data)
	{
		return (EventData[])((Dictionary<string, object>)(object)data)["Component.Event.Data"];
	}

	public static void SetEventData(this ComponentData data, EventData[] eventData)
	{
		((Dictionary<string, object>)(object)data)["Component.Event.Data"] = eventData;
	}
}
