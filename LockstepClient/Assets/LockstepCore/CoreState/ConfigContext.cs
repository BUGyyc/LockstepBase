using System;
using Entitas;

public sealed class ConfigContext : Context<ConfigEntity>
{
	public ConfigContext()
		: base(0, 0, new ContextInfo("Config", ConfigComponentsLookup.componentNames, ConfigComponentsLookup.componentTypes), (Func<IEntity, IAERC>)((IEntity entity) => (IAERC)new SafeAERC(entity)), (Func<ConfigEntity>)(() => new ConfigEntity()))
	{
	}//IL_0012: Unknown result type (might be due to invalid IL or missing references)
	//IL_005a: Expected O, but got Unknown

}
