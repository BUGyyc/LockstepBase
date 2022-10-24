using Entitas.CodeGeneration.Attributes;

namespace Entitas.CodeGeneration.Plugins;

public class EventData
{
	public readonly EventTarget eventTarget;

	public readonly EventType eventType;

	public readonly int priority;

	public EventData(EventTarget eventTarget, EventType eventType, int priority)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		this.eventTarget = eventTarget;
		this.eventType = eventType;
		this.priority = priority;
	}
}
