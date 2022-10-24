using System;
using BEPUutilities;
using Lockstep.Core.Logic.Interfaces;
using Lockstep.Core.Logic.Serialization;
using Lockstep.Core.Logic.Serialization.Utils;

public class CreateHeroCommand : ICommand
{
	public ushort Tag => 4;

	public void Deserialize(Deserializer reader)
	{
		throw new NotImplementedException();
	}

	public void Execute(InputEntity inputEntity)
	{
		throw new NotImplementedException();
	}

	public void Serialize(Serializer writer)
	{
		throw new NotImplementedException();
	}
}