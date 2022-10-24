using DesperateDevs.Utils;

namespace Entitas.CodeGeneration.Plugins;

public static class ComponentDataExtension
{
	public static string ToComponentName(this string fullTypeName, bool ignoreNamespaces)
	{
		if (!ignoreNamespaces)
		{
			return EntitasStringExtension.RemoveComponentSuffix(SerializationTypeExtension.RemoveDots(fullTypeName));
		}
		return EntitasStringExtension.RemoveComponentSuffix(SerializationTypeExtension.ShortTypeName(fullTypeName));
	}
}
