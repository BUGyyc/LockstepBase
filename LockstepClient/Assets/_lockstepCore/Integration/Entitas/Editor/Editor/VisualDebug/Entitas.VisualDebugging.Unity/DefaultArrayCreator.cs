using System;

namespace Entitas.VisualDebugging.Unity.Editor;

public class DefaultArrayCreator : IDefaultInstanceCreator
{
	public bool HandlesType(Type type)
	{
		return type.IsArray;
	}

	public object CreateDefault(Type type)
	{
		int arrayRank = type.GetArrayRank();
		return Array.CreateInstance(type.GetElementType(), new int[arrayRank]);
	}
}
