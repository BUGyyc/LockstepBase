using Entitas;
using System.Collections.Generic;

public static class SystemDefined
{
    public static ISystem[] Register()
    {
        List<ISystem> sysList = new List<ISystem>();


        //��������˳���Ӱ�쵽ECS �� System ��ִ���Ⱥ�
        sysList.Add(new CharacterSystem());



        return sysList.ToArray();
    }
}
