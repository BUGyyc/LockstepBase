namespace Entitas.VisualDebugging.Unity.Editor;

public static class VisualDebuggingEntitasExtension
{
	public static IEntity CreateEntity(this IContext context)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Expected O, but got Unknown
		return (IEntity)((object)context).GetType().GetMethod("CreateEntity").Invoke(context, null);
	}
}
