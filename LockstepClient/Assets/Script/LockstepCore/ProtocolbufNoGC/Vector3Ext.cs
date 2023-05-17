/*
 * @Author: delevin.ying
 * @Date: 2023-05-15 15:47:29
 * @Last Modified by: delevin.ying
 * @Last Modified time: 2023-05-15 16:10:43
 */


using Protocol;

public static class Vector3Ext
{

    public const int ExtBase = 1000;

    public static Int3 ToInt3(this UnityEngine.Vector3 v3)
    {
        Int3 i3 = new Int3();
        i3.x = (int)(v3.x * ExtBase);
        i3.y = (int)(v3.y * ExtBase);
        i3.z = (int)(v3.z * ExtBase);
        return i3;
    }

    public static Int3 ToInt3(this Lockstep.LVector3 lv3){
        Int3 i3 = new Int3();
        i3.x = lv3._x;
        i3.y = lv3._y;
        i3.z = lv3._z;
        return i3;
    }


}
