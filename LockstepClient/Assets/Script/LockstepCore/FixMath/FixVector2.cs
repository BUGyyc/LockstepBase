/*
 * @Author: delevin.ying 
 * @Date: 2022-11-26 15:20:44 
 * @Last Modified by: delevin.ying
 * @Last Modified time: 2022-11-28 17:44:33
 */


using System;


namespace FixMath
{


    [Serializable]
    public struct FixVector2 : IEquatable<FixVector2>
    {
        /// <summary>
        /// X component of the vector.
        /// </summary>
        public FixFloat64 X;

        /// <summary>
        /// Y component of the vector.
        /// </summary>
        public FixFloat64 Y;

        public FixFloat64 x => X;

        public FixFloat64 y => Y;

        public static FixVector2 Zero => default(FixVector2);

        public static FixVector2 UnitX
        {
            get
            {
                FixVector2 result = default(FixVector2);
                result.X = FixFloat64.C1;
                return result;
            }
        }

        public static FixVector2 UnitY
        {
            get
            {
                FixVector2 result = default(FixVector2);
                result.Y = FixFloat64.C1;
                return result;
            }
        }

        public FixVector2(FixFloat64 x, FixFloat64 y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// 平方长度
        /// </summary>
        /// <returns></returns>
        public FixFloat64 LengthSquared()
        {
            return X * X + Y * Y;
        }

        /// <summary>
        /// 实际长度
        /// </summary>
        /// <returns></returns>
        public FixFloat64 Length()
        {
            return FixFloat64.Sqrt(X * X + Y * Y);
        }

        public override string ToString()
        {
            return string.Concat("{", X, ", ", Y, "}");
        }

        public static void Add(ref FixVector2 a, ref FixVector2 b, out FixVector2 sum)
        {
            sum.X = a.X + b.X;
            sum.Y = a.Y + b.Y;
        }

        public static void Subtract(ref FixVector2 a, ref FixVector2 b, out FixVector2 difference)
        {
            difference.X = a.X - b.X;
            difference.Y = a.Y - b.Y;
        }

        public static void Multiply(ref FixVector2 v, FixFloat64 scale, out FixVector2 result)
        {
            result.X = v.X * scale;
            result.Y = v.Y * scale;
        }

        public static void Multiply(ref FixVector2 a, ref FixVector2 b, out FixVector2 result)
        {
            result.X = a.X * b.X;
            result.Y = a.Y * b.Y;
        }

        public static void Divide(ref FixVector2 v, FixFloat64 divisor, out FixVector2 result)
        {
            FixFloat64 fix = FixFloat64.C1 / divisor;
            result.X = v.X * fix;
            result.Y = v.Y * fix;
        }

        /// <summary>
        /// 点乘
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="dot"></param>
        public static void Dot(ref FixVector2 a, ref FixVector2 b, out FixFloat64 dot)
        {
            dot = a.X * b.X + a.Y * b.Y;
        }

        /// <summary>
        /// 点乘
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static FixFloat64 Dot(FixVector2 a, FixVector2 b)
        {
            return a.X * b.X + a.Y * b.Y;
        }

        public static FixVector2 Normalize(FixVector2 v)
        {
            Normalize(ref v, out var result);
            return result;
        }

        public static void Normalize(ref FixVector2 v, out FixVector2 result)
        {
            FixFloat64 fix = FixFloat64.C1 / FixFloat64.Sqrt(v.X * v.X + v.Y * v.Y);
            result.X = v.X * fix;
            result.Y = v.Y * fix;
        }

        /// <summary>
        /// 取反
        /// </summary>
        /// <param name="v"></param>
        /// <param name="negated"></param>
        public static void Negate(ref FixVector2 v, out FixVector2 negated)
        {
            negated.X = -v.X;
            negated.Y = -v.Y;
        }

        public static void Abs(ref FixVector2 v, out FixVector2 result)
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
        }

        public static FixVector2 Abs(FixVector2 v)
        {
            Abs(ref v, out var result);
            return result;
        }

        public static void Min(ref FixVector2 a, ref FixVector2 b, out FixVector2 min)
        {
            min.X = ((a.X < b.X) ? a.X : b.X);
            min.Y = ((a.Y < b.Y) ? a.Y : b.Y);
        }

        public static FixVector2 Min(FixVector2 a, FixVector2 b)
        {
            Min(ref a, ref b, out var min);
            return min;
        }

        public static void Max(ref FixVector2 a, ref FixVector2 b, out FixVector2 max)
        {
            max.X = ((a.X > b.X) ? a.X : b.X);
            max.Y = ((a.Y > b.Y) ? a.Y : b.Y);
        }

        public static FixVector2 Max(FixVector2 a, FixVector2 b)
        {
            Max(ref a, ref b, out var max);
            return max;
        }

        public void Normalize()
        {
            FixFloat64 fix = FixFloat64.C1 / FixFloat64.Sqrt(X * X + Y * Y);
            X *= fix;
            Y *= fix;
        }

        public static FixVector2 operator *(FixVector2 v, FixFloat64 f)
        {
            FixVector2 result = default(FixVector2);
            result.X = v.X * f;
            result.Y = v.Y * f;
            return result;
        }

        public static FixVector2 operator *(FixFloat64 f, FixVector2 v)
        {
            FixVector2 result = default(FixVector2);
            result.X = v.X * f;
            result.Y = v.Y * f;
            return result;
        }

        public static FixVector2 operator *(FixVector2 a, FixVector2 b)
        {
            Multiply(ref a, ref b, out var result);
            return result;
        }

        public static FixVector2 operator /(FixVector2 v, FixFloat64 f)
        {
            f = FixFloat64.C1 / f;
            FixVector2 result = default(FixVector2);
            result.X = v.X * f;
            result.Y = v.Y * f;
            return result;
        }

        public static FixVector2 operator -(FixVector2 a, FixVector2 b)
        {
            FixVector2 result = default(FixVector2);
            result.X = a.X - b.X;
            result.Y = a.Y - b.Y;
            return result;
        }

        public static FixVector2 operator +(FixVector2 a, FixVector2 b)
        {
            FixVector2 result = default(FixVector2);
            result.X = a.X + b.X;
            result.Y = a.Y + b.Y;
            return result;
        }

        public static FixVector2 operator -(FixVector2 v)
        {
            v.X = -v.X;
            v.Y = -v.Y;
            return v;
        }

        public static bool operator ==(FixVector2 a, FixVector2 b)
        {
            return a.X == b.X && a.Y == b.Y;
        }

        public static bool operator !=(FixVector2 a, FixVector2 b)
        {
            return a.X != b.X || a.Y != b.Y;
        }

        public bool Equals(FixVector2 other)
        {
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            if (obj is FixVector2)
            {
                return Equals((FixVector2)obj);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() + Y.GetHashCode();
        }
    }

}