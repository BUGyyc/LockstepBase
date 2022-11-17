using System.Collections.Generic;
using Entitas;
using Entitas.CodeGeneration.Attributes;

[DontGenerate(false)]
// [Game]
public sealed class DestinationListenerComponent : IComponent
{
	public List<IDestinationListener> value;
}
