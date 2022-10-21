using Entitas;
using System.Collections.Generic;

public static class SystemDefined
{
    public static ISystem[] Register()
    {
        List<ISystem> sysList = new List<ISystem>();


        //这里的添加顺序会影响到ECS 中 System 的执行先后
        sysList.Add(new CharacterSystem());



        return sysList.ToArray();
    }
}
