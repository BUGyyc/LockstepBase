using Entitas;
using System.Collections.Generic;

[Game]
public class TestComponent : IComponent
{
    public uint testID;
    public string name;

    public List<int> list;
}

