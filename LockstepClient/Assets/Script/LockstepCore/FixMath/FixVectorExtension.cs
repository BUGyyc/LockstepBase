/*
 * @Author: delevin.ying 
 * @Date: 2022-11-28 17:58:10 
 * @Last Modified by: delevin.ying
 * @Last Modified time: 2022-11-28 18:04:12
 */

using System;
using UnityEngine;

namespace FixMath
{
    public static class FixVectorExtension
    {
        // public const int BaseVal = 1000;
        public static FixVector3 ToFixVector3(this Vector3 self)
        {
            FixFloat64 x = (FixFloat64)self.x;
            FixFloat64 y = (FixFloat64)self.y;
            FixFloat64 z = (FixFloat64)self.z;

            return new FixVector3(x, y, z);
        }


        /// <summary>
        /// 一般来说，只能用在显示层，不希望数据层提前转换，不能浮点数不一致问题依然会出现
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static Vector3 ToVector3(this FixVector3 self)
        {
            float x = (float)self.X;
            float y = (float)self.Y;
            float z = (float)self.Z;
            return new Vector3(x, y, z);
        }
    }
}