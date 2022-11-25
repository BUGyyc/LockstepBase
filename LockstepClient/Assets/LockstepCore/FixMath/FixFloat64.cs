/*
 * @Author: delevin.ying 
 * @Date: 2022-11-25 10:41:49 
 * @Last Modified by: delevin.ying
 * @Last Modified time: 2022-11-25 10:55:44
 */

using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace FixMath
{

    [Serializable]
    public struct FixFloat64 : IEquatable<FixFloat64>, IComparable<FixFloat64>
    {
        public long RawValue;

        public static readonly decimal Precision = (decimal)new FixFloat64(1L);

        public static readonly FixFloat64 MaxValue = new FixFloat64(long.MaxValue);

        public static readonly FixFloat64 MinValue = new FixFloat64(long.MinValue);

        public static readonly FixFloat64 MinusOne = new FixFloat64(-4294967296L);

        public static readonly FixFloat64 One = new FixFloat64(4294967296L);

        public static readonly FixFloat64 Two = 2;

        public static readonly FixFloat64 Three = 3;

        public static readonly FixFloat64 Zero = default(FixFloat64);

        public static readonly FixFloat64 C0p28 = 0.28m;

        public static readonly FixFloat64 Pi = new FixFloat64(13493037704L);

        public static readonly FixFloat64 PiOver2 = new FixFloat64(6746518852L);

        public static readonly FixFloat64 PiOver4 = new FixFloat64(3373259426L);

        public static readonly FixFloat64 PiTimes2 = new FixFloat64(26986075409L);

        public static readonly FixFloat64 PiInv = 0.3183098861837906715377675267m;

        public static readonly FixFloat64 PiOver2Inv = 0.6366197723675813430755350535m;

        public static readonly FixFloat64 E = new FixFloat64(11674931554L);

        public static readonly FixFloat64 EPow4 = new FixFloat64(234497268814L);

        public static readonly FixFloat64 Ln2 = new FixFloat64(2977044471L);

        public static readonly FixFloat64 Log2Max = new FixFloat64(133143986176L);

        public static readonly FixFloat64 Log2Min = new FixFloat64(-137438953472L);

        public const int FRACTIONAL_PLACES = 32;

        private static readonly FixFloat64 LutInterval = 205886 / PiOver2;

        private const long MAX_VALUE = long.MaxValue;

        private const long MIN_VALUE = long.MinValue;

        private const int NUM_BITS = 64;

        private const long ONE = 4294967296L;

        private const long PI_TIMES_2 = 26986075409L;

        private const long PI = 13493037704L;

        private const long PI_OVER_2 = 6746518852L;

        private const long PI_OVER_4 = 3373259426L;

        private const long E_RAW = 11674931554L;

        private const long EPOW4 = 234497268814L;

        private const long LN2 = 2977044471L;

        private const long LOG2MAX = 133143986176L;

        private const long LOG2MIN = -137438953472L;

        private const int LUT_SIZE = 205887;

        private FixFloat64(long rawValue)
        {
            RawValue = rawValue;
        }

        public FixFloat64(int value)
        {
            RawValue = value * 4294967296L;
        }

        public static int SignI(FixFloat64 value)
        {
            return (value.RawValue < 0) ? (-1) : ((value.RawValue > 0) ? 1 : 0);
        }

        public static FixFloat64 Sign(FixFloat64 v)
        {
            long rawValue = v.RawValue;
            return (rawValue < 0) ? MinusOne : ((rawValue > 0) ? One : Zero);
        }

        //TODO:
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FixFloat64 Abs(FixFloat64 value)
        {
            if (value.RawValue == long.MinValue)
            {
                return MaxValue;
            }
            long num = value.RawValue >> 63;
            return new FixFloat64((value.RawValue + num) ^ num);
        }

        public static FixFloat64 Floor(FixFloat64 value)
        {
            return new FixFloat64(value.RawValue & -4294967296L);
        }

        public static FixFloat64 Log2(FixFloat64 x)
        {
            if (x.RawValue <= 0)
            {
                throw new ArgumentOutOfRangeException("Non-positive value passed to Ln", "x");
            }
            long num = 2147483648L;
            long num2 = 0L;
            long num3 = x.RawValue;
            while (num3 < 4294967296L)
            {
                num3 <<= 1;
                num2 -= 4294967296L;
            }
            while (num3 >= 8589934592L)
            {
                num3 >>= 1;
                num2 += 4294967296L;
            }
            FixFloat64 fix = FromRaw(num3);
            for (int i = 0; i < 32; i++)
            {
                fix *= fix;
                if (fix.RawValue >= 8589934592L)
                {
                    fix = FromRaw(fix.RawValue >> 1);
                    num2 += num;
                }
                num >>= 1;
            }
            return FromRaw(num2);
        }

        public static FixFloat64 Ln(FixFloat64 x)
        {
            return Log2(x) * Ln2;
        }

        public static FixFloat64 Pow2(FixFloat64 x)
        {
            if (x.RawValue == 0)
            {
                return One;
            }
            bool flag = x.RawValue < 0;
            if (flag)
            {
                x = -x;
            }
            if (x == One)
            {
                return flag ? (One / Two) : Two;
            }
            if (x >= Log2Max)
            {
                return flag ? (One / MaxValue) : MaxValue;
            }
            if (x <= Log2Min)
            {
                return flag ? MaxValue : Zero;
            }
            int num = (int)(long)Floor(x);
            x = FractionalPart(x);
            FixFloat64 one = One;
            FixFloat64 fix = One;
            int num2 = 1;
            while (fix.RawValue != 0)
            {
                fix = x * fix * Ln2 / num2;
                one += fix;
                num2++;
            }
            one = FromRaw(one.RawValue << num);
            if (flag)
            {
                one = One / one;
            }
            return one;
        }

        public static FixFloat64 Pow(FixFloat64 b, FixFloat64 exp)
        {
            if (b == One)
            {
                return One;
            }
            if (exp.RawValue == 0)
            {
                return One;
            }
            if (b.RawValue == 0)
            {
                return Zero;
            }
            FixFloat64 y = Log2(b);
            return Pow2(SafeMul(exp, y));
        }

        public static FixFloat64 Acos(FixFloat64 x)
        {
            if (x.RawValue == 0)
            {
                return PiOver2;
            }
            FixFloat64 fix = Atan(Sqrt(One - x * x) / x);
            if (x.RawValue < 0)
            {
                return fix + Pi;
            }
            return fix;
        }

        public static FixFloat64 Ceiling(FixFloat64 value)
        {
            return ((value.RawValue & 0xFFFFFFFFu) != 0) ? (Floor(value) + One) : value;
        }

        public static FixFloat64 FractionalPart(FixFloat64 value)
        {
            return FromRaw(value.RawValue & 0xFFFFFFFFu);
        }

        public static FixFloat64 Round(FixFloat64 value)
        {
            long num = value.RawValue & 0xFFFFFFFFu;
            FixFloat64 fix = Floor(value);
            if (num < 2147483648u)
            {
                return fix;
            }
            if (num > 2147483648u)
            {
                return fix + One;
            }
            return ((fix.RawValue & 0x100000000L) == 0L) ? fix : (fix + One);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FixFloat64 operator +(FixFloat64 x, FixFloat64 y)
        {
            return new FixFloat64(x.RawValue + y.RawValue);
        }

        public static FixFloat64 SafeAdd(FixFloat64 x, FixFloat64 y)
        {
            long rawValue = x.RawValue;
            long rawValue2 = y.RawValue;
            long num = rawValue + rawValue2;
            if ((~(rawValue ^ rawValue2) & (rawValue ^ num) & long.MinValue) != 0)
            {
                num = ((rawValue > 0) ? long.MaxValue : long.MinValue);
            }
            return new FixFloat64(num);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FixFloat64 operator -(FixFloat64 x, FixFloat64 y)
        {
            return new FixFloat64(x.RawValue - y.RawValue);
        }

        public static FixFloat64 SafeSub(FixFloat64 x, FixFloat64 y)
        {
            long rawValue = x.RawValue;
            long rawValue2 = y.RawValue;
            long num = rawValue - rawValue2;
            if (((rawValue ^ rawValue2) & (rawValue ^ num) & long.MinValue) != 0)
            {
                num = ((rawValue < 0) ? long.MinValue : long.MaxValue);
            }
            return new FixFloat64(num);
        }

        private static long AddOverflowHelper(long x, long y, ref bool overflow)
        {
            long num = x + y;
            overflow |= ((x ^ y ^ num) & long.MinValue) != 0;
            return num;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FixFloat64 operator *(FixFloat64 x, FixFloat64 y)
        {
            long rawValue = x.RawValue;
            long rawValue2 = y.RawValue;
            ulong num = (ulong)(rawValue & 0xFFFFFFFFu);
            long num2 = rawValue >> 32;
            ulong num3 = (ulong)(rawValue2 & 0xFFFFFFFFu);
            long num4 = rawValue2 >> 32;
            ulong num5 = num * num3;
            long num6 = (long)num * num4;
            long num7 = num2 * (long)num3;
            long num8 = num2 * num4;
            ulong num9 = num5 >> 32;
            long num10 = num6;
            long num11 = num7;
            long num12 = num8 << 32;
            long rawValue3 = (long)num9 + num10 + num11 + num12;
            return new FixFloat64(rawValue3);
        }

        public static FixFloat64 SafeMul(FixFloat64 x, FixFloat64 y)
        {
            long rawValue = x.RawValue;
            long rawValue2 = y.RawValue;
            ulong num = (ulong)(rawValue & 0xFFFFFFFFu);
            long num2 = rawValue >> 32;
            ulong num3 = (ulong)(rawValue2 & 0xFFFFFFFFu);
            long num4 = rawValue2 >> 32;
            ulong num5 = num * num3;
            long num6 = (long)num * num4;
            long num7 = num2 * (long)num3;
            long num8 = num2 * num4;
            ulong x2 = num5 >> 32;
            long y2 = num6;
            long y3 = num7;
            long y4 = num8 << 32;
            bool overflow = false;
            long x3 = AddOverflowHelper((long)x2, y2, ref overflow);
            x3 = AddOverflowHelper(x3, y3, ref overflow);
            x3 = AddOverflowHelper(x3, y4, ref overflow);
            bool flag = ((rawValue ^ rawValue2) & long.MinValue) == 0;
            if (flag)
            {
                if (x3 < 0 || (overflow && rawValue > 0))
                {
                    return MaxValue;
                }
            }
            else if (x3 > 0)
            {
                return MinValue;
            }
            long num9 = num8 >> 32;
            if (num9 != 0L && num9 != -1)
            {
                return flag ? MaxValue : MinValue;
            }
            if (!flag)
            {
                long num10;
                long num11;
                if (rawValue > rawValue2)
                {
                    num10 = rawValue;
                    num11 = rawValue2;
                }
                else
                {
                    num10 = rawValue2;
                    num11 = rawValue;
                }
                if (x3 > num11 && num11 < -4294967296L && num10 > 4294967296L)
                {
                    return MinValue;
                }
            }
            return new FixFloat64(x3);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int CountLeadingZeroes(ulong x)
        {
            int num = 0;
            while ((x & 0xF000000000000000uL) == 0)
            {
                num += 4;
                x <<= 4;
            }
            while ((x & 0x8000000000000000uL) == 0)
            {
                num++;
                x <<= 1;
            }
            return num;
        }

        public static FixFloat64 operator /(FixFloat64 x, FixFloat64 y)
        {
            long rawValue = x.RawValue;
            long rawValue2 = y.RawValue;
            if (rawValue2 == 0)
            {
                return MaxValue;
            }
            ulong num = (ulong)((rawValue >= 0) ? rawValue : (-rawValue));
            ulong num2 = (ulong)((rawValue2 >= 0) ? rawValue2 : (-rawValue2));
            ulong num3 = 0uL;
            int num4 = 33;
            while ((num2 & 0xF) == 0L && num4 >= 4)
            {
                num2 >>= 4;
                num4 -= 4;
            }
            while (num != 0L && num4 >= 0)
            {
                int num5 = CountLeadingZeroes(num);
                if (num5 > num4)
                {
                    num5 = num4;
                }
                num <<= num5;
                num4 -= num5;
                ulong num6 = num / num2;
                num %= num2;
                num3 += num6 << num4;
                if ((num6 & ~(ulong.MaxValue >> num4)) != 0)
                {
                    return (((rawValue ^ rawValue2) & long.MinValue) == 0L) ? MaxValue : MinValue;
                }
                num <<= 1;
                num4--;
            }
            num3++;
            long num7 = (long)(num3 >> 1);
            if (((rawValue ^ rawValue2) & long.MinValue) != 0)
            {
                num7 = -num7;
            }
            return new FixFloat64(num7);
        }

        public static FixFloat64 operator %(FixFloat64 x, FixFloat64 y)
        {
            return new FixFloat64(((x.RawValue == long.MinValue) & (y.RawValue == -1)) ? 0 : (x.RawValue % y.RawValue));
        }

        public static FixFloat64 FastMod(FixFloat64 x, FixFloat64 y)
        {
            return new FixFloat64(x.RawValue % y.RawValue);
        }

        public static FixFloat64 operator -(FixFloat64 x)
        {
            return (x.RawValue == long.MinValue) ? MaxValue : new FixFloat64(-x.RawValue);
        }

        public static bool operator ==(FixFloat64 x, FixFloat64 y)
        {
            return x.RawValue == y.RawValue;
        }

        public static bool operator !=(FixFloat64 x, FixFloat64 y)
        {
            return x.RawValue != y.RawValue;
        }

        public static bool operator >(FixFloat64 x, FixFloat64 y)
        {
            return x.RawValue > y.RawValue;
        }

        public static bool operator <(FixFloat64 x, FixFloat64 y)
        {
            return x.RawValue < y.RawValue;
        }

        public static bool operator >=(FixFloat64 x, FixFloat64 y)
        {
            return x.RawValue >= y.RawValue;
        }

        public static bool operator <=(FixFloat64 x, FixFloat64 y)
        {
            return x.RawValue <= y.RawValue;
        }

        public static FixFloat64 Sqrt(FixFloat64 x)
        {
            long rawValue = x.RawValue;
            if (rawValue < 0)
            {
                throw new ArgumentOutOfRangeException("Negative value passed to Sqrt", "x");
            }
            ulong num = (ulong)rawValue;
            ulong num2 = 0uL;
            ulong num3;
            for (num3 = 4611686018427387904uL; num3 > num; num3 >>= 2)
            {
            }
            for (int i = 0; i < 2; i++)
            {
                while (num3 != 0)
                {
                    if (num >= num2 + num3)
                    {
                        num -= num2 + num3;
                        num2 = (num2 >> 1) + num3;
                    }
                    else
                    {
                        num2 >>= 1;
                    }
                    num3 >>= 2;
                }
                if (i == 0)
                {
                    if (num > uint.MaxValue)
                    {
                        num -= num2;
                        num = (num << 32) - 2147483648u;
                        num2 = (num2 << 32) + 2147483648u;
                    }
                    else
                    {
                        num <<= 32;
                        num2 <<= 32;
                    }
                    num3 = 1073741824uL;
                }
            }
            if (num > num2)
            {
                num2++;
            }
            return new FixFloat64((long)num2);
        }

        public static FixFloat64 Sin(FixFloat64 x)
        {
            bool flipHorizontal;
            bool flipVertical;
            long rawValue = ClampSinValue(x.RawValue, out flipHorizontal, out flipVertical);
            FixFloat64 fix = new FixFloat64(rawValue);
            FixFloat64 fix2 = fix * LutInterval;
            FixFloat64 fix3 = Round(fix2);
            FixFloat64 fix4 = fix2 - fix3;
            FixFloat64 fix5 = new FixFloat64(FixMathExtension.SinLut[(int)(flipHorizontal ? (FixMathExtension.SinLut.Length - 1 - (int)(long)fix3) : ((long)fix3))]);
            FixFloat64 fix6 = new FixFloat64(FixMathExtension.SinLut[flipHorizontal ? (FixMathExtension.SinLut.Length - 1 - (int)(long)fix3 - SignI(fix4)) : ((int)(long)fix3 + SignI(fix4))]);
            long rawValue2 = (fix4 * Abs(fix5 - fix6)).RawValue;
            long num = fix5.RawValue + (flipHorizontal ? (-rawValue2) : rawValue2);
            long rawValue3 = (flipVertical ? (-num) : num);
            return new FixFloat64(rawValue3);
        }

        public static FixFloat64 FastSin(FixFloat64 x)
        {
            bool flipHorizontal;
            bool flipVertical;
            long num = ClampSinValue(x.RawValue, out flipHorizontal, out flipVertical);
            uint num2 = (uint)(num >> 15);
            if (num2 >= 205887)
            {
                num2 = 205886u;
            }
            long num3 = FixMathExtension.SinLut[flipHorizontal ? (FixMathExtension.SinLut.Length - 1 - (int)num2) : ((int)num2)];
            return new FixFloat64(flipVertical ? (-num3) : num3);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static long ClampSinValue(long angle, out bool flipHorizontal, out bool flipVertical)
        {
            long num = angle % 26986075409L;
            if (angle < 0)
            {
                num += 26986075409L;
            }
            flipVertical = num >= 13493037704L;
            long num2;
            for (num2 = num; num2 >= 13493037704L; num2 -= 13493037704L)
            {
            }
            flipHorizontal = num2 >= 6746518852L;
            long num3 = num2;
            if (num3 >= 6746518852L)
            {
                num3 -= 6746518852L;
            }
            return num3;
        }

        public static FixFloat64 Cos(FixFloat64 x)
        {
            long rawValue = x.RawValue;
            long rawValue2 = rawValue + ((rawValue > 0) ? (-20239556556L) : 6746518852L);
            return Sin(new FixFloat64(rawValue2));
        }

        public static FixFloat64 FastCos(FixFloat64 x)
        {
            long rawValue = x.RawValue;
            long rawValue2 = rawValue + ((rawValue > 0) ? (-20239556556L) : 6746518852L);
            return FastSin(new FixFloat64(rawValue2));
        }

        public static FixFloat64 Tan(FixFloat64 x)
        {
            long num = x.RawValue % 13493037704L;
            bool flag = false;
            if (num < 0)
            {
                num = -num;
                flag = true;
            }
            if (num > 6746518852L)
            {
                flag = !flag;
                num = 6746518852L - (num - 6746518852L);
            }
            FixFloat64 fix = new FixFloat64(num);
            FixFloat64 fix2 = fix * LutInterval;
            FixFloat64 fix3 = Round(fix2);
            FixFloat64 fix4 = fix2 - fix3;
            FixFloat64 fix5 = new FixFloat64(FixMathExtension.TanLut[(int)(long)fix3]);
            FixFloat64 fix6 = new FixFloat64(FixMathExtension.TanLut[(int)(long)fix3 + SignI(fix4)]);
            long rawValue = (fix4 * Abs(fix5 - fix6)).RawValue;
            long num2 = fix5.RawValue + rawValue;
            long rawValue2 = (flag ? (-num2) : num2);
            return new FixFloat64(rawValue2);
        }

        public static FixFloat64 FastAtan2(FixFloat64 y, FixFloat64 x)
        {
            long rawValue = y.RawValue;
            long rawValue2 = x.RawValue;
            if (rawValue2 == 0)
            {
                if (rawValue > 0)
                {
                    return PiOver2;
                }
                if (rawValue == 0)
                {
                    return Zero;
                }
                return -PiOver2;
            }
            FixFloat64 fix = y / x;
            if (SafeAdd(One, SafeMul(SafeMul(C0p28, fix), fix)) == MaxValue)
            {
                return (y.RawValue < 0) ? (-PiOver2) : PiOver2;
            }
            FixFloat64 fix2;
            if (Abs(fix) < One)
            {
                fix2 = fix / (One + C0p28 * fix * fix);
                if (rawValue2 < 0)
                {
                    if (rawValue < 0)
                    {
                        return fix2 - Pi;
                    }
                    return fix2 + Pi;
                }
            }
            else
            {
                fix2 = PiOver2 - fix / (fix * fix + C0p28);
                if (rawValue < 0)
                {
                    return fix2 - Pi;
                }
            }
            return fix2;
        }

        public static FixFloat64 Atan(FixFloat64 z)
        {
            if (z.RawValue == 0)
            {
                return Zero;
            }
            bool flag = z.RawValue < 0;
            if (flag)
            {
                z = -z;
            }
            FixFloat64 fix;
            if (z == One)
            {
                fix = PiOver4;
            }
            else
            {
                bool flag2 = z > One;
                if (flag2)
                {
                    z = One / z;
                }
                fix = One;
                FixFloat64 one = One;
                FixFloat64 fix2 = z * z;
                FixFloat64 fix3 = fix2 * Two;
                FixFloat64 fix4 = fix2 + One;
                FixFloat64 fix5 = fix4 * Two;
                FixFloat64 fix6 = fix3;
                FixFloat64 fix7 = fix4 * Three;
                for (int i = 2; i < 30; i++)
                {
                    one *= fix6 / fix7;
                    fix += one;
                    fix6 += fix3;
                    fix7 += fix5;
                    if (one.RawValue == 0)
                    {
                        break;
                    }
                }
                fix = fix * z / fix4;
                if (flag2)
                {
                    fix = PiOver2 - fix;
                }
            }
            if (flag)
            {
                fix = -fix;
            }
            return fix;
        }

        public static FixFloat64 Atan2(FixFloat64 y, FixFloat64 x)
        {
            long rawValue = y.RawValue;
            long rawValue2 = x.RawValue;
            if (rawValue2 == 0)
            {
                if (rawValue > 0)
                {
                    return PiOver2;
                }
                if (rawValue == 0)
                {
                    return Zero;
                }
                return -PiOver2;
            }
            FixFloat64 fix = y / x;
            if (SafeAdd(One, SafeMul(SafeMul(0.28m, fix), fix)) == MaxValue)
            {
                return (y.RawValue < 0) ? (-PiOver2) : PiOver2;
            }
            FixFloat64 fix2 = Atan(fix);
            if (rawValue2 < 0)
            {
                if (rawValue < 0)
                {
                    return fix2 - Pi;
                }
                return fix2 + Pi;
            }
            return fix2;
        }

        public static implicit operator FixFloat64(int value)
        {
            return new FixFloat64(value);
        }

        public static explicit operator FixFloat64(long value)
        {
            return new FixFloat64(value * 4294967296L);
        }

        public static explicit operator long(FixFloat64 value)
        {
            return value.RawValue >> 32;
        }

        public static explicit operator FixFloat64(float value)
        {
            return new FixFloat64((long)(value * 4.2949673E+09f));
        }

        public static explicit operator float(FixFloat64 value)
        {
            return (float)value.RawValue / 4.2949673E+09f;
        }

        public static explicit operator FixFloat64(double value)
        {
            return new FixFloat64((long)(value * 4294967296.0));
        }

        public static explicit operator double(FixFloat64 value)
        {
            return (double)value.RawValue / 4294967296.0;
        }

        public static implicit operator FixFloat64(decimal value)
        {
            return new FixFloat64((long)(value * new decimal(4294967296L)));
        }

        public static explicit operator decimal(FixFloat64 value)
        {
            return (decimal)value.RawValue / new decimal(4294967296L);
        }

        public override bool Equals(object obj)
        {
            return obj is FixFloat64 && ((FixFloat64)obj).RawValue == RawValue;
        }

        public override int GetHashCode()
        {
            return RawValue.GetHashCode();
        }

        public bool Equals(FixFloat64 other)
        {
            return RawValue == other.RawValue;
        }

        public int CompareTo(FixFloat64 other)
        {
            return RawValue.CompareTo(other.RawValue);
        }

        public override string ToString()
        {
            return ((decimal)this).ToString();
        }

        public static FixFloat64 FromRaw(long rawValue)
        {
            return new FixFloat64(rawValue);
        }

        internal static void GenerateSinLut()
        {
            using StreamWriter streamWriter = new StreamWriter("FixFloat64SinLut.cs");
            streamWriter.Write("namespace FixMath.NET {\r\n    partial struct FixFloat64 {\r\n        public static readonly long[] SinLut = new[] {");
            int num = 0;
            for (int i = 0; i < 205887; i++)
            {
                double a = (double)i * Math.PI * 0.5 / 205886.0;
                if (num++ % 8 == 0)
                {
                    streamWriter.WriteLine();
                    streamWriter.Write("            ");
                }
                double num2 = Math.Sin(a);
                long rawValue = ((FixFloat64)num2).RawValue;
                streamWriter.Write($"0x{rawValue:X}L, ");
            }
            streamWriter.Write("\r\n        };\r\n    }\r\n}");
        }

        internal static void GenerateTanLut()
        {
            using StreamWriter streamWriter = new StreamWriter("FixFloat64TanLut.cs");
            streamWriter.Write("namespace FixMath.NET {\r\n    partial struct FixFloat64 {\r\n        public static readonly long[] TanLut = new[] {");
            int num = 0;
            for (int i = 0; i < 205887; i++)
            {
                double a = (double)i * Math.PI * 0.5 / 205886.0;
                if (num++ % 8 == 0)
                {
                    streamWriter.WriteLine();
                    streamWriter.Write("            ");
                }
                double num2 = Math.Tan(a);
                if (num2 > (double)MaxValue || num2 < 0.0)
                {
                    num2 = (double)MaxValue;
                }
                FixFloat64 obj = (((decimal)num2 > (decimal)MaxValue || num2 < 0.0) ? MaxValue : ((FixFloat64)num2));
                long rawValue = obj.RawValue;
                streamWriter.Write($"0x{rawValue:X}L, ");
            }
            streamWriter.Write("\r\n        };\r\n    }\r\n}");
        }
    }
}