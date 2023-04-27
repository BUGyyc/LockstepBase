/*
 * @Author: delevin.ying 
 * @Date: 2022-11-26 15:23:55 
 * @Last Modified by: delevin.ying
 * @Last Modified time: 2022-11-28 17:48:05
 */

using System;

namespace FixMath
{
    [Serializable]
    public struct FixVector4 : IEquatable<FixVector4>
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

        /// <summary>
        /// W component of the vector.
        /// </summary>
        public FixFloat64 W;

        public static FixVector4 Zero => default(FixVector4);

        /// <summary>
        /// X 轴
        /// </summary>
        /// <value></value>
        public static FixVector4 UnitX
        {
            get
            {
                FixVector4 result = default(FixVector4);
                result.X = FixFloat64.C1;
                return result;
            }
        }

        /// <summary>
        /// Y 轴
        /// </summary>
        /// <value></value>
        public static FixVector4 UnitY
        {
            get
            {
                FixVector4 result = default(FixVector4);
                result.Y = FixFloat64.C1;
                return result;
            }
        }

        /// <summary>
        /// Z 轴
        /// </summary>
        /// <value></value>
        public static FixVector4 UnitZ
        {
            get
            {
                FixVector4 result = default(FixVector4);
                result.Z = FixFloat64.C1;
                return result;
            }
        }

        /// <summary>
        /// W 轴
        /// </summary>
        /// <value></value>
        public static FixVector4 UnitW
        {
            get
            {
                FixVector4 result = default(FixVector4);
                result.W = FixFloat64.C1;
                return result;
            }
        }

        public FixVector4(FixFloat64 x, FixFloat64 y, FixFloat64 z, FixFloat64 w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public FixVector4(FixVector3 xyz, FixFloat64 w)
        {
            X = xyz.X;
            Y = xyz.Y;
            Z = xyz.Z;
            W = w;
        }

        public FixVector4(FixFloat64 x, FixVector3 yzw)
        {
            X = x;
            Y = yzw.X;
            Z = yzw.Y;
            W = yzw.Z;
        }

        public FixVector4(FixVector2 xy, FixFloat64 z, FixFloat64 w)
        {
            X = xy.X;
            Y = xy.Y;
            Z = z;
            W = w;
        }

        public FixVector4(FixFloat64 x, FixVector2 yz, FixFloat64 w)
        {
            X = x;
            Y = yz.X;
            Z = yz.Y;
            W = w;
        }

        public FixVector4(FixFloat64 x, FixFloat64 y, FixVector2 zw)
        {
            X = x;
            Y = y;
            Z = zw.X;
            W = zw.Y;
        }

        public FixVector4(FixVector2 xy, FixVector2 zw)
        {
            X = xy.X;
            Y = xy.Y;
            Z = zw.X;
            W = zw.Y;
        }

        /// <summary>
        /// 平方长度
        /// </summary>
        /// <returns></returns>
        public FixFloat64 LengthSquared()
        {
            return X * X + Y * Y + Z * Z + W * W;
        }

        /// <summary>
        /// 实际长度
        /// </summary>
        /// <returns></returns>
        public FixFloat64 Length()
        {
            return FixFloat64.Sqrt(X * X + Y * Y + Z * Z + W * W);
        }

        public void Normalize()
        {
            FixFloat64 fix = FixFloat64.C1 / FixFloat64.Sqrt(X * X + Y * Y + Z * Z + W * W);
            X *= fix;
            Y *= fix;
            Z *= fix;
            W *= fix;
        }

        public override string ToString()
        {
            return string.Concat("{", X, ", ", Y, ", ", Z, ", ", W, "}");
        }

        public static FixFloat64 Dot(FixVector4 a, FixVector4 b)
        {
            return a.X * b.X + a.Y * b.Y + a.Z * b.Z + a.W * b.W;
        }

        public static void Dot(ref FixVector4 a, ref FixVector4 b, out FixFloat64 product)
        {
            product = a.X * b.X + a.Y * b.Y + a.Z * b.Z + a.W * b.W;
        }

        public static void Add(ref FixVector4 a, ref FixVector4 b, out FixVector4 sum)
        {
            sum.X = a.X + b.X;
            sum.Y = a.Y + b.Y;
            sum.Z = a.Z + b.Z;
            sum.W = a.W + b.W;
        }

        public static void Subtract(ref FixVector4 a, ref FixVector4 b, out FixVector4 difference)
        {
            difference.X = a.X - b.X;
            difference.Y = a.Y - b.Y;
            difference.Z = a.Z - b.Z;
            difference.W = a.W - b.W;
        }

        public static void Multiply(ref FixVector4 v, FixFloat64 scale, out FixVector4 result)
        {
            result.X = v.X * scale;
            result.Y = v.Y * scale;
            result.Z = v.Z * scale;
            result.W = v.W * scale;
        }

        public static void Multiply(ref FixVector4 a, ref FixVector4 b, out FixVector4 result)
        {
            result.X = a.X * b.X;
            result.Y = a.Y * b.Y;
            result.Z = a.Z * b.Z;
            result.W = a.W * b.W;
        }

        public static void Divide(ref FixVector4 v, FixFloat64 divisor, out FixVector4 result)
        {
            FixFloat64 fix = FixFloat64.C1 / divisor;
            result.X = v.X * fix;
            result.Y = v.Y * fix;
            result.Z = v.Z * fix;
            result.W = v.W * fix;
        }

        public static FixVector4 operator *(FixVector4 v, FixFloat64 f)
        {
            FixVector4 result = default(FixVector4);
            result.X = v.X * f;
            result.Y = v.Y * f;
            result.Z = v.Z * f;
            result.W = v.W * f;
            return result;
        }

        public static FixVector4 operator *(FixFloat64 f, FixVector4 v)
        {
            FixVector4 result = default(FixVector4);
            result.X = v.X * f;
            result.Y = v.Y * f;
            result.Z = v.Z * f;
            result.W = v.W * f;
            return result;
        }

        public static FixVector4 operator *(FixVector4 a, FixVector4 b)
        {
            Multiply(ref a, ref b, out var result);
            return result;
        }

        public static FixVector4 operator /(FixVector4 v, FixFloat64 f)
        {
            f = FixFloat64.C1 / f;
            FixVector4 result = default(FixVector4);
            result.X = v.X * f;
            result.Y = v.Y * f;
            result.Z = v.Z * f;
            result.W = v.W * f;
            return result;
        }

        public static FixVector4 operator -(FixVector4 a, FixVector4 b)
        {
            FixVector4 result = default(FixVector4);
            result.X = a.X - b.X;
            result.Y = a.Y - b.Y;
            result.Z = a.Z - b.Z;
            result.W = a.W - b.W;
            return result;
        }

        public static FixVector4 operator +(FixVector4 a, FixVector4 b)
        {
            FixVector4 result = default(FixVector4);
            result.X = a.X + b.X;
            result.Y = a.Y + b.Y;
            result.Z = a.Z + b.Z;
            result.W = a.W + b.W;
            return result;
        }

        public static FixVector4 operator -(FixVector4 v)
        {
            v.X = -v.X;
            v.Y = -v.Y;
            v.Z = -v.Z;
            v.W = -v.W;
            return v;
        }

        public static bool operator ==(FixVector4 a, FixVector4 b)
        {
            return a.X == b.X && a.Y == b.Y && a.Z == b.Z && a.W == b.W;
        }

        public static bool operator !=(FixVector4 a, FixVector4 b)
        {
            return a.X != b.X || a.Y != b.Y || a.Z != b.Z || a.W != b.W;
        }

        public bool Equals(FixVector4 other)
        {
            return X == other.X && Y == other.Y && Z == other.Z && W == other.W;
        }

        public override bool Equals(object obj)
        {
            if (obj is FixVector4)
            {
                return Equals((FixVector4)obj);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() + Y.GetHashCode() + Z.GetHashCode() + W.GetHashCode();
        }

        /// <summary>
        /// 平方距离
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="distanceSquared"></param>
        public static void DistanceSquared(ref FixVector4 a, ref FixVector4 b, out FixFloat64 distanceSquared)
        {
            FixFloat64 fix = a.X - b.X;
            FixFloat64 fix2 = a.Y - b.Y;
            FixFloat64 fix3 = a.Z - b.Z;
            FixFloat64 fix4 = a.W - b.W;
            distanceSquared = fix * fix + fix2 * fix2 + fix3 * fix3 + fix4 * fix4;
        }

        /// <summary>
        /// 实际距离
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="distance"></param>
        public static void Distance(ref FixVector4 a, ref FixVector4 b, out FixFloat64 distance)
        {
            FixFloat64 fix = a.X - b.X;
            FixFloat64 fix2 = a.Y - b.Y;
            FixFloat64 fix3 = a.Z - b.Z;
            FixFloat64 fix4 = a.W - b.W;
            distance = FixFloat64.Sqrt(fix * fix + fix2 * fix2 + fix3 * fix3 + fix4 * fix4);
        }

        public static FixFloat64 Distance(FixVector4 a, FixVector4 b)
        {
            Distance(ref a, ref b, out var distance);
            return distance;
        }

        public static FixVector4 Normalize(FixVector4 v)
        {
            Normalize(ref v, out var result);
            return result;
        }

        public static void Normalize(ref FixVector4 v, out FixVector4 result)
        {
            FixFloat64 fix = FixFloat64.C1 / FixFloat64.Sqrt(v.X * v.X + v.Y * v.Y + v.Z * v.Z + v.W * v.W);
            result.X = v.X * fix;
            result.Y = v.Y * fix;
            result.Z = v.Z * fix;
            result.W = v.W * fix;
        }

        /// <summary>
        /// 取反
        /// </summary>
        /// <param name="v"></param>
        /// <param name="negated"></param>
        public static void Negate(ref FixVector4 v, out FixVector4 negated)
        {
            negated.X = -v.X;
            negated.Y = -v.Y;
            negated.Z = -v.Z;
            negated.W = -v.W;
        }


        public static void Abs(ref FixVector4 v, out FixVector4 result)
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
            if (v.W < FixFloat64.C0)
            {
                result.W = -v.W;
            }
            else
            {
                result.W = v.W;
            }
        }

        public static FixVector4 Abs(FixVector4 v)
        {
            Abs(ref v, out var result);
            return result;
        }

        public static void Min(ref FixVector4 a, ref FixVector4 b, out FixVector4 min)
        {
            min.X = ((a.X < b.X) ? a.X : b.X);
            min.Y = ((a.Y < b.Y) ? a.Y : b.Y);
            min.Z = ((a.Z < b.Z) ? a.Z : b.Z);
            min.W = ((a.W < b.W) ? a.W : b.W);
        }

        public static FixVector4 Min(FixVector4 a, FixVector4 b)
        {
            Min(ref a, ref b, out var min);
            return min;
        }

        public static void Max(ref FixVector4 a, ref FixVector4 b, out FixVector4 max)
        {
            max.X = ((a.X > b.X) ? a.X : b.X);
            max.Y = ((a.Y > b.Y) ? a.Y : b.Y);
            max.Z = ((a.Z > b.Z) ? a.Z : b.Z);
            max.W = ((a.W > b.W) ? a.W : b.W);
        }

        public static FixVector4 Max(FixVector4 a, FixVector4 b)
        {
            Max(ref a, ref b, out var max);
            return max;
        }

        /// <summary>
        /// 插值
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="interpolationAmount"></param>
        /// <returns></returns>
        public static FixVector4 Lerp(FixVector4 start, FixVector4 end, FixFloat64 interpolationAmount)
        {
            Lerp(ref start, ref end, interpolationAmount, out var result);
            return result;
        }

        public static void Lerp(ref FixVector4 start, ref FixVector4 end, FixFloat64 interpolationAmount, out FixVector4 result)
        {
            FixFloat64 fix = FixFloat64.C1 - interpolationAmount;
            result.X = start.X * fix + end.X * interpolationAmount;
            result.Y = start.Y * fix + end.Y * interpolationAmount;
            result.Z = start.Z * fix + end.Z * interpolationAmount;
            result.W = start.W * fix + end.W * interpolationAmount;
        }

        //TODO:
        /// <summary>
        /// 埃尔米特矩阵
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="tangent1"></param>
        /// <param name="value2"></param>
        /// <param name="tangent2"></param>
        /// <param name="interpolationAmount"></param>
        /// <param name="result"></param>
        public static void Hermite(ref FixVector4 value1, ref FixVector4 tangent1, ref FixVector4 value2, ref FixVector4 tangent2, FixFloat64 interpolationAmount, out FixVector4 result)
        {
            FixFloat64 fix = interpolationAmount * interpolationAmount;
            FixFloat64 fix2 = interpolationAmount * fix;
            FixFloat64 fix3 = FixFloat64.C2 * fix2 - FixFloat64.C3 * fix + FixFloat64.C1;
            FixFloat64 fix4 = fix2 - FixFloat64.C2 * fix + interpolationAmount;
            FixFloat64 fix5 = -2L * fix2 + FixFloat64.C3 * fix;
            FixFloat64 fix6 = fix2 - fix;
            result.X = value1.X * fix3 + value2.X * fix5 + tangent1.X * fix4 + tangent2.X * fix6;
            result.Y = value1.Y * fix3 + value2.Y * fix5 + tangent1.Y * fix4 + tangent2.Y * fix6;
            result.Z = value1.Z * fix3 + value2.Z * fix5 + tangent1.Z * fix4 + tangent2.Z * fix6;
            result.W = value1.W * fix3 + value2.W * fix5 + tangent1.W * fix4 + tangent2.W * fix6;
        }

        
        public static FixVector4 Hermite(FixVector4 value1, FixVector4 tangent1, FixVector4 value2, FixVector4 tangent2, FixFloat64 interpolationAmount)
        {
            Hermite(ref value1, ref tangent1, ref value2, ref tangent2, interpolationAmount, out var result);
            return result;
        }
    }
}