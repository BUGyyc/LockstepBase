﻿
/*
 * @Author: delevin.ying 
 * @Date: 2022-11-26 14:20:11 
 * @Last Modified by: delevin.ying
 * @Last Modified time: 2022-11-26 14:23:16
 */
using System;

namespace FixMath
{

    [Serializable]
    public struct FixVector3 : IEquatable<FixVector3>
    {
        /// <summary>
        /// X component of the vector.
        /// </summary>
        public FixFloat64 X;

        /// <summary>
        /// Y component of the vector.
        /// </summary>
        public FixFloat64 Y;

        /// <summary>
        /// Z component of the vector.
        /// </summary>
        public FixFloat64 Z;

        public FixFloat64 x => X;

        public FixFloat64 y => Y;

        public FixFloat64 z => Z;


        public static FixVector3 Zero => default(FixVector3);

        public static FixVector3 Up
        {
            get
            {
                FixVector3 result = default(FixVector3);
                result.X = FixFloat64.C0;
                result.Y = FixFloat64.C1;
                result.Z = FixFloat64.C0;
                return result;
            }
        }

        public static FixVector3 Down
        {
            get
            {
                FixVector3 result = default(FixVector3);
                result.X = FixFloat64.C0;
                result.Y = (FixFloat64)(-1L);
                result.Z = FixFloat64.C0;
                return result;
            }
        }

        public static FixVector3 Right
        {
            get
            {
                FixVector3 result = default(FixVector3);
                result.X = FixFloat64.C1;
                result.Y = FixFloat64.C0;
                result.Z = FixFloat64.C0;
                return result;
            }
        }

        public static FixVector3 Left
        {
            get
            {
                FixVector3 result = default(FixVector3);
                result.X = (FixFloat64)(-1L);
                result.Y = FixFloat64.C0;
                result.Z = FixFloat64.C0;
                return result;
            }
        }

        public static FixVector3 Forward
        {
            get
            {
                FixVector3 result = default(FixVector3);
                result.X = FixFloat64.C0;
                result.Y = FixFloat64.C0;
                result.Z = (FixFloat64)(-1L);
                return result;
            }
        }

        public static FixVector3 Backward
        {
            get
            {
                FixVector3 result = default(FixVector3);
                result.X = FixFloat64.C0;
                result.Y = FixFloat64.C0;
                result.Z = FixFloat64.C1;
                return result;
            }
        }

        public static FixVector3 UnitX
        {
            get
            {
                FixVector3 result = default(FixVector3);
                result.X = FixFloat64.C1;
                return result;
            }
        }

        public static FixVector3 UnitY
        {
            get
            {
                FixVector3 result = default(FixVector3);
                result.Y = FixFloat64.C1;
                return result;
            }
        }

        public static FixVector3 UnitZ
        {
            get
            {
                FixVector3 result = default(FixVector3);
                result.Z = FixFloat64.C1;
                return result;
            }
        }

        public FixVector3(FixFloat64 x, FixFloat64 y, FixFloat64 z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public FixVector3(FixVector2 xy, FixFloat64 z)
        {
            X = xy.X;
            Y = xy.Y;
            Z = z;
        }

        public FixVector3(FixFloat64 x, FixVector2 yz)
        {
            X = x;
            Y = yz.X;
            Z = yz.Y;
        }

        /// <summary>
        /// 平方长度
        /// </summary>
        /// <returns></returns>
        public FixFloat64 LengthSquared()
        {
            return X * X + Y * Y + Z * Z;
        }

        /// <summary>
        /// 真实长度
        /// </summary>
        /// <returns></returns>
        public FixFloat64 Length()
        {
            return FixFloat64.Sqrt(X * X + Y * Y + Z * Z);
        }

        public void Normalize()
        {
            FixFloat64 fix = FixFloat64.C1 / FixFloat64.Sqrt(X * X + Y * Y + Z * Z);
            X *= fix;
            Y *= fix;
            Z *= fix;
        }

        public override string ToString()
        {
            return string.Concat("{", X, ", ", Y, ", ", Z, "}");
        }

        /// <summary>
        /// 点乘
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static FixFloat64 Dot(FixVector3 a, FixVector3 b)
        {
            return a.X * b.X + a.Y * b.Y + a.Z * b.Z;
        }

        public static void Dot(ref FixVector3 a, ref FixVector3 b, out FixFloat64 product)
        {
            product = a.X * b.X + a.Y * b.Y + a.Z * b.Z;
        }

        public static void Add(ref FixVector3 a, ref FixVector3 b, out FixVector3 sum)
        {
            sum.X = a.X + b.X;
            sum.Y = a.Y + b.Y;
            sum.Z = a.Z + b.Z;
        }

        public static void Subtract(ref FixVector3 a, ref FixVector3 b, out FixVector3 difference)
        {
            difference.X = a.X - b.X;
            difference.Y = a.Y - b.Y;
            difference.Z = a.Z - b.Z;
        }

        public static void Multiply(ref FixVector3 v, FixFloat64 scale, out FixVector3 result)
        {
            result.X = v.X * scale;
            result.Y = v.Y * scale;
            result.Z = v.Z * scale;
        }

        public static void Multiply(ref FixVector3 a, ref FixVector3 b, out FixVector3 result)
        {
            result.X = a.X * b.X;
            result.Y = a.Y * b.Y;
            result.Z = a.Z * b.Z;
        }

        public static void Divide(ref FixVector3 v, FixFloat64 divisor, out FixVector3 result)
        {
            FixFloat64 fix = FixFloat64.C1 / divisor;
            result.X = v.X * fix;
            result.Y = v.Y * fix;
            result.Z = v.Z * fix;
        }

        public static FixVector3 operator *(FixVector3 v, FixFloat64 f)
        {
            FixVector3 result = default(FixVector3);
            result.X = v.X * f;
            result.Y = v.Y * f;
            result.Z = v.Z * f;
            return result;
        }

        public static FixVector3 operator *(FixFloat64 f, FixVector3 v)
        {
            FixVector3 result = default(FixVector3);
            result.X = v.X * f;
            result.Y = v.Y * f;
            result.Z = v.Z * f;
            return result;
        }

        public static FixVector3 operator *(FixVector3 a, FixVector3 b)
        {
            Multiply(ref a, ref b, out var result);
            return result;
        }

        public static FixVector3 operator /(FixVector3 v, FixFloat64 f)
        {
            f = FixFloat64.C1 / f;
            FixVector3 result = default(FixVector3);
            result.X = v.X * f;
            result.Y = v.Y * f;
            result.Z = v.Z * f;
            return result;
        }

        public static FixVector3 operator -(FixVector3 a, FixVector3 b)
        {
            FixVector3 result = default(FixVector3);
            result.X = a.X - b.X;
            result.Y = a.Y - b.Y;
            result.Z = a.Z - b.Z;
            return result;
        }

        public static FixVector3 operator +(FixVector3 a, FixVector3 b)
        {
            FixVector3 result = default(FixVector3);
            result.X = a.X + b.X;
            result.Y = a.Y + b.Y;
            result.Z = a.Z + b.Z;
            return result;
        }

        public static FixVector3 operator -(FixVector3 v)
        {
            v.X = -v.X;
            v.Y = -v.Y;
            v.Z = -v.Z;
            return v;
        }

        public static bool operator ==(FixVector3 a, FixVector3 b)
        {
            return a.X == b.X && a.Y == b.Y && a.Z == b.Z;
        }

        public static bool operator !=(FixVector3 a, FixVector3 b)
        {
            return a.X != b.X || a.Y != b.Y || a.Z != b.Z;
        }

        public bool Equals(FixVector3 other)
        {
            return X == other.X && Y == other.Y && Z == other.Z;
        }

        public override bool Equals(object obj)
        {
            if (obj is FixVector3)
            {
                return Equals((FixVector3)obj);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() + Y.GetHashCode() + Z.GetHashCode();
        }

        /// <summary>
        /// 平方长度
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="distanceSquared"></param>
        public static void DistanceSquared(ref FixVector3 a, ref FixVector3 b, out FixFloat64 distanceSquared)
        {
            FixFloat64 fix = a.X - b.X;
            FixFloat64 fix2 = a.Y - b.Y;
            FixFloat64 fix3 = a.Z - b.Z;
            distanceSquared = fix * fix + fix2 * fix2 + fix3 * fix3;
        }

        public static FixFloat64 DistanceSquared(FixVector3 a, FixVector3 b)
        {
            FixFloat64 fix = a.X - b.X;
            FixFloat64 fix2 = a.Y - b.Y;
            FixFloat64 fix3 = a.Z - b.Z;
            return fix * fix + fix2 * fix2 + fix3 * fix3;
        }

        public static void Distance(ref FixVector3 a, ref FixVector3 b, out FixFloat64 distance)
        {
            FixFloat64 fix = a.X - b.X;
            FixFloat64 fix2 = a.Y - b.Y;
            FixFloat64 fix3 = a.Z - b.Z;
            distance = FixFloat64.Sqrt(fix * fix + fix2 * fix2 + fix3 * fix3);
        }

        public static FixFloat64 Distance(FixVector3 a, FixVector3 b)
        {
            Distance(ref a, ref b, out var distance);
            return distance;
        }

        /// <summary>
        /// 叉乘
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static FixVector3 Cross(FixVector3 a, FixVector3 b)
        {
            Cross(ref a, ref b, out var result);
            return result;
        }

        public static void Cross(ref FixVector3 a, ref FixVector3 b, out FixVector3 result)
        {
            FixFloat64 fix = a.Y * b.Z - a.Z * b.Y;
            FixFloat64 fix2 = a.Z * b.X - a.X * b.Z;
            FixFloat64 fix3 = a.X * b.Y - a.Y * b.X;
            result.X = fix;
            result.Y = fix2;
            result.Z = fix3;
        }

        public static FixVector3 Normalize(FixVector3 v)
        {
            Normalize(ref v, out var result);
            return result;
        }

        public static void Normalize(ref FixVector3 v, out FixVector3 result)
        {
            FixFloat64 fix = FixFloat64.C1 / FixFloat64.Sqrt(v.X * v.X + v.Y * v.Y + v.Z * v.Z);
            result.X = v.X * fix;
            result.Y = v.Y * fix;
            result.Z = v.Z * fix;
        }

        /// <summary>
        /// 取反
        /// </summary>
        /// <param name="v"></param>
        /// <param name="negated"></param>
        public static void Negate(ref FixVector3 v, out FixVector3 negated)
        {
            negated.X = -v.X;
            negated.Y = -v.Y;
            negated.Z = -v.Z;
        }

        public static void Abs(ref FixVector3 v, out FixVector3 result)
        {
            if (v.X < FixFloat64.C0)
            {
                result.X = -v.X;
            }
            else
            {
                result.X = v.X;
            }
            if (v.Y < FixFloat64.C0)
            {
                result.Y = -v.Y;
            }
            else
            {
                result.Y = v.Y;
            }
            if (v.Z < FixFloat64.C0)
            {
                result.Z = -v.Z;
            }
            else
            {
                result.Z = v.Z;
            }
        }

        public static FixVector3 Abs(FixVector3 v)
        {
            Abs(ref v, out var result);
            return result;
        }

        public static void Min(ref FixVector3 a, ref FixVector3 b, out FixVector3 min)
        {
            min.X = ((a.X < b.X) ? a.X : b.X);
            min.Y = ((a.Y < b.Y) ? a.Y : b.Y);
            min.Z = ((a.Z < b.Z) ? a.Z : b.Z);
        }

        public static FixVector3 Min(FixVector3 a, FixVector3 b)
        {
            Min(ref a, ref b, out var min);
            return min;
        }

        public static void Max(ref FixVector3 a, ref FixVector3 b, out FixVector3 max)
        {
            max.X = ((a.X > b.X) ? a.X : b.X);
            max.Y = ((a.Y > b.Y) ? a.Y : b.Y);
            max.Z = ((a.Z > b.Z) ? a.Z : b.Z);
        }

        public static FixVector3 Max(FixVector3 a, FixVector3 b)
        {
            Max(ref a, ref b, out var max);
            return max;
        }

        public static FixVector3 Lerp(FixVector3 start, FixVector3 end, FixFloat64 interpolationAmount)
        {
            Lerp(ref start, ref end, interpolationAmount, out var result);
            return result;
        }

        public static void Lerp(ref FixVector3 start, ref FixVector3 end, FixFloat64 interpolationAmount, out FixVector3 result)
        {
            FixFloat64 fix = FixFloat64.C1 - interpolationAmount;
            result.X = start.X * fix + end.X * interpolationAmount;
            result.Y = start.Y * fix + end.Y * interpolationAmount;
            result.Z = start.Z * fix + end.Z * interpolationAmount;
        }

        //TODO:
        public static void Hermite(ref FixVector3 value1, ref FixVector3 tangent1, ref FixVector3 value2, ref FixVector3 tangent2, FixFloat64 interpolationAmount, out FixVector3 result)
        {
            FixFloat64 fix = interpolationAmount * interpolationAmount;
            FixFloat64 fix2 = interpolationAmount * fix;
            FixFloat64 fix3 = FixFloat64.C2 * fix2 - FixFloat64.C3 * fix + FixFloat64.C1;
            FixFloat64 fix4 = fix2 - FixFloat64.C2 * fix + interpolationAmount;
            FixFloat64 fix5 = -2 * fix2 + FixFloat64.C3 * fix;
            FixFloat64 fix6 = fix2 - fix;
            result.X = value1.X * fix3 + value2.X * fix5 + tangent1.X * fix4 + tangent2.X * fix6;
            result.Y = value1.Y * fix3 + value2.Y * fix5 + tangent1.Y * fix4 + tangent2.Y * fix6;
            result.Z = value1.Z * fix3 + value2.Z * fix5 + tangent1.Z * fix4 + tangent2.Z * fix6;
        }

        public static FixVector3 Hermite(FixVector3 value1, FixVector3 tangent1, FixVector3 value2, FixVector3 tangent2, FixFloat64 interpolationAmount)
        {
            Hermite(ref value1, ref tangent1, ref value2, ref tangent2, interpolationAmount, out var result);
            return result;
        }
    }

}