using System.Collections.Generic;
using Entitas;
using Entitas.CodeGeneration.Attributes;

[DontGenerate(false)]
public sealed class PositionListenerComponent : IComponent
{
	public List<IPositionListener> value;
}
