/*
 * @Author: delevin.ying 
 * @Date: 2022-11-26 15:48:59 
 * @Last Modified by: delevin.ying
 * @Last Modified time: 2022-11-29 14:17:34
 */

using System;

namespace FixMath
{

    public static class MathHelper
    {
        /// <summary>
        /// π
        /// </summary>
        public static readonly FixFloat64 Pi = FixFloat64.Pi;

        /// <summary>
        /// 二π
        /// </summary>
        public static readonly FixFloat64 TwoPi = FixFloat64.PiTimes2;

        /// <summary>
        ///  二分之一π
        /// </summary>
        public static readonly FixFloat64 PiOver2 = FixFloat64.PiOver2;

        /// <summary>
        /// 四分之一π
        /// </summary>
        public static readonly FixFloat64 PiOver4 = FixFloat64.Pi / new FixFloat64(4);


        //TODO:取模
        public static FixFloat64 IEEERemainder(FixFloat64 dividend, FixFloat64 divisor)
        {
            return dividend - divisor * FixFloat64.Round(dividend / divisor);
        }

        /// <summary>
        /// 把角度转换 到弧度 -π 到 π 
        /// </summary>
        /// <param name="angle">Angle to wrap.</param>
        /// <returns>Wrapped angle.</returns>
        public static FixFloat64 WrapAngle(FixFloat64 angle)
        {
            angle = IEEERemainder(angle, TwoPi);
            if (angle < -Pi)
            {
                angle += TwoPi;
                return angle;
            }
            if (angle >= Pi)
            {
                angle -= TwoPi;
            }
            return angle;
        }

        /// <summary>
        /// 得到约束范围内的值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static FixFloat64 Clamp(FixFloat64 value, FixFloat64 min, FixFloat64 max)
        {
            if (value < min)
            {
                return min;
            }
            if (value > max)
            {
                return max;
            }
            return value;
        }

        public static FixFloat64 Max(FixFloat64 a, FixFloat64 b)
        {
            return (a > b) ? a : b;
        }


        public static FixFloat64 Min(FixFloat64 a, FixFloat64 b)
        {
            return (a < b) ? a : b;
        }

        /// <summary>
        /// 角度转弧度
        /// </summary>
        /// <param name="degrees"></param>
        /// <returns></returns>
        public static FixFloat64 ToRadians(FixFloat64 degrees)
        {
            return degrees * (Pi / FixFloat64.C180);
        }

        /// <summary>
        /// 弧度转角度
        /// </summary>
        /// <param name="radians"></param>
        /// <returns></returns>
        public static FixFloat64 ToDegrees(FixFloat64 radians)
        {
            return radians * (FixFloat64.C180 / Pi);
        }
    }
}