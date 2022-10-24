using System;
using System.Linq;
using DesperateDevs.Utils;

namespace Entitas.CodeGeneration.Plugins;

public class MemberDataComponentDataProvider : IComponentDataProvider
{
	public void Provide(Type type, ComponentData data)
	{
		MemberData[] memberInfos = (from info in PublicMemberInfoExtension.GetPublicMemberInfos(type)
			select new MemberData(SerializationTypeExtension.ToCompilableString(info.type), info.name)).ToArray();
		data.SetMemberData(memberInfos);
	}
}
