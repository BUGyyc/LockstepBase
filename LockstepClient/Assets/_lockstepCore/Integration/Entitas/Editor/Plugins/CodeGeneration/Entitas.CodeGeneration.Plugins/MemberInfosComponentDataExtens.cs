using System.Collections.Generic;

namespace Entitas.CodeGeneration.Plugins;

public static class MemberInfosComponentDataExtension
{
	public const string COMPONENT_MEMBER_DATA = "Component.MemberData";

	public static MemberData[] GetMemberData(this ComponentData data)
	{
		return (MemberData[])((Dictionary<string, object>)(object)data)["Component.MemberData"];
	}

	public static void SetMemberData(this ComponentData data, MemberData[] memberInfos)
	{
		((Dictionary<string, object>)(object)data)["Component.MemberData"] = memberInfos;
	}
}
