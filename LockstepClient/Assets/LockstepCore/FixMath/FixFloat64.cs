using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace FixMath
{

    /// <summary>
    /// Represents a Q31.32 fixed-point number.
    /// </summary>
    public partial struct FixFloat64 : IEquatable<FixFloat64>, IComparable<FixFloat64>
    {
        readonly long m_rawValue;


        public long GetRawValue()
        {
            return m_rawValue;
        }

        /// <summary>
        /// 判断正负，0 表示 0
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int Sign(FixFloat64 value)
        {
            return
                value.m_rawValue < 0 ? -1 :
                value.m_rawValue > 0 ? 1 :
                0;
        }

        /// <summary>
        /// 绝对值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FixFloat64 Abs(FixFloat64 value)
        {
            if (value.m_rawValue == MIN_VALUE)
            {
                return MaxValue;
            }
            // 负数 反码 = 负数的反码加+1
            // http://www.strchr.com/optimized_abs_function
            var mask = value.m_rawValue >> 63;
            return new FixFloat64((value.m_rawValue + mask) ^ mask);
        }


        public static FixFloat64 FastAbs(FixFloat64 value)
        {
            var mask = value.m_rawValue >> 63;
            return new FixFloat64((value.m_rawValue + mask) ^ mask);
        }


        /// <summary>
        /// 向下取整
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FixFloat64 Floor(FixFloat64 value)
        {
            // 丢弃 尾部 0 - 31位
            return new FixFloat64((long)((ulong)value.m_rawValue & 0xFFFFFFFF00000000));
        }

        /// <summary>
        /// 向上取整
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FixFloat64 Ceiling(FixFloat64 value)
        {
            //判断小数部分是否有值
            var hasFractionalPart = (value.m_rawValue & 0x00000000FFFFFFFF) != 0;
            //如果小数部分有值，那么整数位进1
            return hasFractionalPart ? Floor(value) + One : value;
        }

        /// <summary>
        /// 四舍五入
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FixFloat64 Round(FixFloat64 value)
        {
            //取下小数部分
            var fractionalPart = value.m_rawValue & 0x00000000FFFFFFFF;
            //取下整数部分
            var integralPart = Floor(value);

            //小数部分对比大小
            if (fractionalPart < 0x80000000)
            {
                return integralPart;
            }
            if (fractionalPart > 0x80000000)
            {
                return integralPart + One;
            }

            //相等的特殊情况，如果 value ==0
            return (integralPart.m_rawValue & ONE) == 0
                       ? integralPart
                       : integralPart + One;
        }

        /// <summary>
        /// 加法
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static FixFloat64 operator +(FixFloat64 x, FixFloat64 y)
        {
            var xl = x.m_rawValue;
            var yl = y.m_rawValue;
            var sum = xl + yl;

            //补充溢出的判断
            if (((~(xl ^ yl) & (xl ^ sum)) & MIN_VALUE) != 0)
            {
                sum = xl > 0 ? MAX_VALUE : MIN_VALUE;
            }
            return new FixFloat64(sum);
        }

        public static FixFloat64 FastAdd(FixFloat64 x, FixFloat64 y)
        {
            return new FixFloat64(x.m_rawValue + y.m_rawValue);
        }

        /// <summary>
        /// 溢出
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static FixFloat64 operator -(FixFloat64 x, FixFloat64 y)
        {
            var xl = x.m_rawValue;
            var yl = y.m_rawValue;
            var diff = xl - yl;
            //溢出
            if ((((xl ^ yl) & (xl ^ diff)) & MIN_VALUE) != 0)
            {
                diff = xl < 0 ? MIN_VALUE : MAX_VALUE;
            }
            return new FixFloat64(diff);
        }

        public static FixFloat64 FastSub(FixFloat64 x, FixFloat64 y)
        {
            return new FixFloat64(x.m_rawValue - y.m_rawValue);
        }

        static long AddOverflowHelper(long x, long y, ref bool overflow)
        {
            var sum = x + y;
            // x + y overflows if sign(x) ^ sign(y) != sign(sum)
            overflow |= ((x ^ y ^ sum) & MIN_VALUE) != 0;
            return sum;
        }

        public static FixFloat64 operator *(FixFloat64 a, long b)
        {
            var fix = FixFloat64.FromRaw(b);
            return a * fix;
        }

        public static FixFloat64 operator *(long a, FixFloat64 b)
        {
            var fix = FixFloat64.FromRaw(a);
            return b * fix;
        }

        public static FixFloat64 operator *(FixFloat64 a, int b)
        {
            var fix = FixFloat64.FromRaw((long)b);
            return fix * a;
        }

        public static FixFloat64 operator *(int b, FixFloat64 a)
        {
            var fix = FixFloat64.FromRaw((long)b);
            return fix * a;
        }

        public static FixFloat64 operator *(FixFloat64 x, FixFloat64 y)
        {

            var xl = x.m_rawValue;
            var yl = y.m_rawValue;
            //小数位
            var xlo = (ulong)(xl & 0x00000000FFFFFFFF);
            //整数位 并且 降位
            var xhi = xl >> FRACTIONAL_PLACES;
            //小数位
            var ylo = (ulong)(yl & 0x00000000FFFFFFFF);
            //整数位 并且 降位
            var yhi = yl >> FRACTIONAL_PLACES;
            //小数位 乘 小数位
            var lolo = xlo * ylo;
            //小数位 乘 整数位
            var lohi = (long)xlo * yhi;
            //整数位 乘 小数位
            var hilo = xhi * (long)ylo;
            //整数位 乘 整数位
            var hihi = xhi * yhi;
            //小数位相乘的结果 只保留 二进制 32 位的精度，所以右移32位，同时也是为了方便二进制对齐，后面会进行相加，小数位必须是0-31位
            var loResult = lolo >> FRACTIONAL_PLACES;
            var midResult1 = lohi;
            var midResult2 = hilo;
            //把降位 的运算整数结果，再次放回到高32位上
            var hiResult = hihi << FRACTIONAL_PLACES;

            bool overflow = false;
            var sum = AddOverflowHelper((long)loResult, midResult1, ref overflow);
            sum = AddOverflowHelper(sum, midResult2, ref overflow);
            sum = AddOverflowHelper(sum, hiResult, ref overflow);

            bool opSignsEqual = ((xl ^ yl) & MIN_VALUE) == 0;

            //不同符号位的溢出判断
            if (opSignsEqual)
            {
                if (sum < 0 || (overflow && xl > 0))
                {
                    return MaxValue;
                }
            }
            else
            {
                if (sum > 0)
                {
                    return MinValue;
                }
            }

            //如果高32位，还存在进位，那么溢出了
            var topCarry = hihi >> FRACTIONAL_PLACES;
            if (topCarry != 0 && topCarry != -1 /*&& xl != -17 && yl != -17*/)
            {
                return opSignsEqual ? MaxValue : MinValue;
            }

            //符号相反时的溢出检查
            if (!opSignsEqual)
            {
                long posOp, negOp;
                if (xl > yl)
                {
                    posOp = xl;
                    negOp = yl;
                }
                else
                {
                    posOp = yl;
                    negOp = xl;
                }
                if (sum > negOp && negOp < -ONE && posOp > ONE)
                {
                    return MinValue;
                }
            }

            return new FixFloat64(sum);
        }

        public static FixFloat64 FastMul(FixFloat64 x, FixFloat64 y)
        {

            var xl = x.m_rawValue;
            var yl = y.m_rawValue;

            var xlo = (ulong)(xl & 0x00000000FFFFFFFF);
            var xhi = xl >> FRACTIONAL_PLACES;
            var ylo = (ulong)(yl & 0x00000000FFFFFFFF);
            var yhi = yl >> FRACTIONAL_PLACES;

            var lolo = xlo * ylo;
            var lohi = (long)xlo * yhi;
            var hilo = xhi * (long)ylo;
            var hihi = xhi * yhi;

            var loResult = lolo >> FRACTIONAL_PLACES;
            var midResult1 = lohi;
            var midResult2 = hilo;
            var hiResult = hihi << FRACTIONAL_PLACES;

            var sum = (long)loResult + midResult1 + midResult2 + hiResult;
            return new FixFloat64(sum);
        }

        /// <summary>
        /// 相当于求出 x 二进制最大位 与 long.MaxValue 如下表达
        ///   x * Math.Pow(2,num) = long.MaxValue
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static int CountLeadingZeroes(ulong x)
        {
            int result = 0;
            while ((x & 0xF000000000000000) == 0) { result += 4; x <<= 4; }
            while ((x & 0x8000000000000000) == 0) { result += 1; x <<= 1; }
            return result;
        }

        public static FixFloat64 operator /(FixFloat64 x, FixFloat64 y)
        {
            var xl = x.m_rawValue;
            var yl = y.m_rawValue;

            if (yl == 0)
            {
                throw new DivideByZeroException();
            }
            //先转符号
            var remainder = (ulong)(xl >= 0 ? xl : -xl);
            var divider = (ulong)(yl >= 0 ? yl : -yl);

            /// <summary>
            /// 无符号长整数 0
            /// </summary>
            var quotient = 0UL;

            //从第33位开始
            var bitPos = NUM_BITS / 2 + 1;


            //如果能整除 16 ，就提前算好
            while ((divider & 0xF) == 0 && bitPos >= 4)
            {
                divider >>= 4;
                bitPos -= 4;
            }

            while (remainder != 0 && bitPos >= 0)
            {
                // #region  这里算出 num 最多能进的位数
                //，肯定是不能超过 shift 数量的，不然就溢出了, bitPos 是实际除数产生的进位结果
                int shift = CountLeadingZeroes(remainder);
                if (shift > bitPos)
                {
                    //最大进位足够包容实际进位，那么就把实际进位给到最终进位  shift
                    shift = bitPos;
                }
                //最终进位给到
                remainder <<= shift;
                //实际进位 - 最大可进位 ,最终 num4 >=0
                bitPos -= shift;
                //把进位后的 remainder 去整除 divider，div 保留整除结果
                var div = remainder / divider;
                //进位后的 remainder 除 divider 取余
                remainder = remainder % divider;

                //还有剩余的进位 div >= 0）
                quotient += div << bitPos;

                //溢出判断
                if ((div & ~(0xFFFFFFFFFFFFFFFF >> bitPos)) != 0)
                {
                    return ((xl ^ yl) & MIN_VALUE) == 0 ? MaxValue : MinValue;
                }

                remainder <<= 1;
                --bitPos;
            }

            ++quotient;
            var result = (long)(quotient >> 1);
            //符号相反
            if (((xl ^ yl) & MIN_VALUE) != 0)
            {
                result = -result;
            }

            return new FixFloat64(result);
        }

        public static FixFloat64 operator %(FixFloat64 x, FixFloat64 y)
        {
            return new FixFloat64(
                x.m_rawValue == MIN_VALUE & y.m_rawValue == -1 ?
                0 :
                x.m_rawValue % y.m_rawValue);
        }

        public static FixFloat64 FastMod(FixFloat64 x, FixFloat64 y)
        {
            return new FixFloat64(x.m_rawValue % y.m_rawValue);
        }

        public static FixFloat64 operator -(FixFloat64 x)
        {
            return x.m_rawValue == MIN_VALUE ? MaxValue : new FixFloat64(-x.m_rawValue);
        }

        public static bool operator ==(FixFloat64 x, FixFloat64 y)
        {
            return x.m_rawValue == y.m_rawValue;
        }

        public static bool operator !=(FixFloat64 x, FixFloat64 y)
        {
            return x.m_rawValue != y.m_rawValue;
        }

        public static bool operator >(FixFloat64 x, FixFloat64 y)
        {
            return x.m_rawValue > y.m_rawValue;
        }

        public static bool operator <(FixFloat64 x, FixFloat64 y)
        {
            return x.m_rawValue < y.m_rawValue;
        }

        public static bool operator >=(FixFloat64 x, FixFloat64 y)
        {
            return x.m_rawValue >= y.m_rawValue;
        }

        public static bool operator <=(FixFloat64 x, FixFloat64 y)
        {
            return x.m_rawValue <= y.m_rawValue;
        }

        //TODO:
        internal static FixFloat64 Pow2(FixFloat64 x)
        {
            if (x.m_rawValue == 0)
            {
                return One;
            }

            // Avoid negative arguments by exploiting that exp(-x) = 1/exp(x).
            bool neg = x.m_rawValue < 0;
            if (neg)
            {
                x = -x;
            }

            if (x == One)
            {
                return neg ? One / (FixFloat64)2 : (FixFloat64)2;
            }
            if (x >= Log2Max)
            {
                return neg ? One / MaxValue : MaxValue;
            }
            if (x <= Log2Min)
            {
                return neg ? MaxValue : Zero;
            }

            /* The algorithm is based on the power series for exp(x):
             * http://en.wikipedia.org/wiki/Exponential_function#Formal_definition
             * 
             * From term n, we get term n+1 by multiplying with x/n.
             * When the sum term drops to zero, we can stop summing.
             */

            int integerPart = (int)Floor(x);
            // Take fractional part of exponent
            x = new FixFloat64(x.m_rawValue & 0x00000000FFFFFFFF);

            var result = One;
            var term = One;
            int i = 1;
            while (term.m_rawValue != 0)
            {
                term = FastMul(FastMul(x, term), Ln2) / (FixFloat64)i;
                result += term;
                i++;
            }

            result = FromRaw(result.m_rawValue << integerPart);
            if (neg)
            {
                result = One / result;
            }

            return result;
        }

        //TODO:
        internal static FixFloat64 Log2(FixFloat64 x)
        {
            if (x.m_rawValue <= 0)
            {
                throw new ArgumentOutOfRangeException("Non-positive value passed to Ln", "x");
            }

            // This implementation is based on Clay. S. Turner's fast binary logarithm
            // algorithm (C. S. Turner,  "A Fast Binary Logarithm Algorithm", IEEE Signal
            //     Processing Mag., pp. 124,140, Sep. 2010.)

            long b = 1U << (FRACTIONAL_PLACES - 1);
            long y = 0;

            long rawX = x.m_rawValue;
            while (rawX < ONE)
            {
                rawX <<= 1;
                y -= ONE;
            }

            while (rawX >= (ONE << 1))
            {
                rawX >>= 1;
                y += ONE;
            }

            var z = new FixFloat64(rawX);

            for (int i = 0; i < FRACTIONAL_PLACES; i++)
            {
                z = FastMul(z, z);
                if (z.m_rawValue >= (ONE << 1))
                {
                    z = new FixFloat64(z.m_rawValue >> 1);
                    y += b;
                }
                b >>= 1;
            }

            return new FixFloat64(y);
        }


        public static FixFloat64 Ln(FixFloat64 x)
        {
            return FastMul(Log2(x), Ln2);
        }

        /// <summary>
        /// Returns a specified number raised to the specified power.
        /// Provides about 5 digits of accuracy for the result.
        /// </summary>
        /// <exception cref="DivideByZeroException">
        /// The base was zero, with a negative exponent
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The base was negative, with a non-zero exponent
        /// </exception>
        public static FixFloat64 Pow(FixFloat64 b, FixFloat64 exp)
        {
            if (b == One)
            {
                return One;
            }
            if (exp.m_rawValue == 0)
            {
                return One;
            }
            if (b.m_rawValue == 0)
            {
                if (exp.m_rawValue < 0)
                {
                    throw new DivideByZeroException();
                }
                return Zero;
            }

            FixFloat64 log2 = Log2(b);
            return Pow2(exp * log2);
        }

        /// <summary>
        /// Returns the square root of a specified number.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The argument was negative.
        /// </exception>
        public static FixFloat64 Sqrt(FixFloat64 x)
        {
            var xl = x.m_rawValue;
            if (xl < 0)
            {
                // We cannot represent infinities like Single and Double, and Sqrt is
                // mathematically undefined for x < 0. So we just throw an exception.
                throw new ArgumentOutOfRangeException("Negative value passed to Sqrt", "x");
            }

            var num = (ulong)xl;
            var result = 0UL;

            // second-to-top bit
            var bit = 1UL << (NUM_BITS - 2);

            while (bit > num)
            {
                bit >>= 2;
            }

            // The main part is executed twice, in order to avoid
            // using 128 bit values in computations.
            for (var i = 0; i < 2; ++i)
            {
                // First we get the top 48 bits of the answer.
                while (bit != 0)
                {
                    if (num >= result + bit)
                    {
                        num -= result + bit;
                        result = (result >> 1) + bit;
                    }
                    else
                    {
                        result = result >> 1;
                    }
                    bit >>= 2;
                }

                if (i == 0)
                {
                    // Then process it again to get the lowest 16 bits.
                    if (num > (1UL << (NUM_BITS / 2)) - 1)
                    {
                        // The remainder 'num' is too large to be shifted left
                        // by 32, so we have to add 1 to result manually and
                        // adjust 'num' accordingly.
                        // num = a - (result + 0.5)^2
                        //       = num + result^2 - (result + 0.5)^2
                        //       = num - result - 0.5
                        num -= result;
                        num = (num << (NUM_BITS / 2)) - 0x80000000UL;
                        result = (result << (NUM_BITS / 2)) + 0x80000000UL;
                    }
                    else
                    {
                        num <<= (NUM_BITS / 2);
                        result <<= (NUM_BITS / 2);
                    }

                    bit = 1UL << (NUM_BITS / 2 - 2);
                }
            }
            // Finally, if next bit would have been 1, round the result upwards.
            if (num > result)
            {
                ++result;
            }
            return new FixFloat64((long)result);
        }

        /// <summary>
        /// Returns the Sine of x.
        /// The relative error is less than 1E-10 for x in [-2PI, 2PI], and less than 1E-7 in the worst case.
        /// </summary>
        public static FixFloat64 Sin(FixFloat64 x)
        {
            var clampedL = ClampSinValue(x.m_rawValue, out var flipHorizontal, out var flipVertical);
            var clamped = new FixFloat64(clampedL);

            // Find the two closest values in the LUT and perform linear interpolation
            // This is what kills the performance of this function on x86 - x64 is fine though
            var rawIndex = FastMul(clamped, LutInterval);
            var roundedIndex = Round(rawIndex);
            var indexError = FastSub(rawIndex, roundedIndex);

            var nearestValue = new FixFloat64(SinLut[flipHorizontal ?
                SinLut.Length - 1 - (int)roundedIndex :
                (int)roundedIndex]);
            var secondNearestValue = new FixFloat64(SinLut[flipHorizontal ?
                SinLut.Length - 1 - (int)roundedIndex - Sign(indexError) :
                (int)roundedIndex + Sign(indexError)]);

            var delta = FastMul(indexError, FastAbs(FastSub(nearestValue, secondNearestValue))).m_rawValue;
            var interpolatedValue = nearestValue.m_rawValue + (flipHorizontal ? -delta : delta);
            var finalValue = flipVertical ? -interpolatedValue : interpolatedValue;
            return new FixFloat64(finalValue);
        }

        /// <summary>
        /// Returns a rough approximation of the Sine of x.
        /// This is at least 3 times faster than Sin() on x86 and slightly faster than Math.Sin(),
        /// however its accuracy is limited to 4-5 decimals, for small enough values of x.
        /// </summary>
        public static FixFloat64 FastSin(FixFloat64 x)
        {
            var clampedL = ClampSinValue(x.m_rawValue, out bool flipHorizontal, out bool flipVertical);

            // Here we use the fact that the SinLut table has a number of entries
            // equal to (PI_OVER_2 >> 15) to use the angle to index directly into it
            var rawIndex = (uint)(clampedL >> 15);
            if (rawIndex >= LUT_SIZE)
            {
                rawIndex = LUT_SIZE - 1;
            }
            var nearestValue = SinLut[flipHorizontal ?
                SinLut.Length - 1 - (int)rawIndex :
                (int)rawIndex];
            return new FixFloat64(flipVertical ? -nearestValue : nearestValue);
        }


        static long ClampSinValue(long angle, out bool flipHorizontal, out bool flipVertical)
        {
            var largePI = 7244019458077122842;
            // Obtained from ((FixFloat64)1686629713.065252369824872831112M).m_rawValue
            // This is (2^29)*PI, where 29 is the largest N such that (2^N)*PI < MaxValue.
            // The idea is that this number contains way more precision than PI_TIMES_2,
            // and (((x % (2^29*PI)) % (2^28*PI)) % ... (2^1*PI) = x % (2 * PI)
            // In practice this gives us an error of about 1,25e-9 in the worst case scenario (Sin(MaxValue))
            // Whereas simply doing x % PI_TIMES_2 is the 2e-3 range.

            var clamped2Pi = angle;
            for (int i = 0; i < 29; ++i)
            {
                clamped2Pi %= (largePI >> i);
            }
            if (angle < 0)
            {
                clamped2Pi += PI_TIMES_2;
            }

            // The LUT contains values for 0 - PiOver2; every other value must be obtained by
            // vertical or horizontal mirroring
            flipVertical = clamped2Pi >= PI;
            // obtain (angle % PI) from (angle % 2PI) - much faster than doing another modulo
            var clampedPi = clamped2Pi;
            while (clampedPi >= PI)
            {
                clampedPi -= PI;
            }
            flipHorizontal = clampedPi >= PI_OVER_2;
            // obtain (angle % PI_OVER_2) from (angle % PI) - much faster than doing another modulo
            var clampedPiOver2 = clampedPi;
            if (clampedPiOver2 >= PI_OVER_2)
            {
                clampedPiOver2 -= PI_OVER_2;
            }
            return clampedPiOver2;
        }

        /// <summary>
        /// Returns the cosine of x.
        /// The relative error is less than 1E-10 for x in [-2PI, 2PI], and less than 1E-7 in the worst case.
        /// </summary>
        public static FixFloat64 Cos(FixFloat64 x)
        {
            var xl = x.m_rawValue;
            var rawAngle = xl + (xl > 0 ? -PI - PI_OVER_2 : PI_OVER_2);
            return Sin(new FixFloat64(rawAngle));
        }

        /// <summary>
        /// Returns a rough approximation of the cosine of x.
        /// See FastSin for more details.
        /// </summary>
        public static FixFloat64 FastCos(FixFloat64 x)
        {
            var xl = x.m_rawValue;
            var rawAngle = xl + (xl > 0 ? -PI - PI_OVER_2 : PI_OVER_2);
            return FastSin(new FixFloat64(rawAngle));
        }

        /// <summary>
        /// Returns the tangent of x.
        /// </summary>
        /// <remarks>
        /// This function is not well-tested. It may be wildly inaccurate.
        /// </remarks>
        public static FixFloat64 Tan(FixFloat64 x)
        {
            var clampedPi = x.m_rawValue % PI;
            var flip = false;
            if (clampedPi < 0)
            {
                clampedPi = -clampedPi;
                flip = true;
            }
            if (clampedPi > PI_OVER_2)
            {
                flip = !flip;
                clampedPi = PI_OVER_2 - (clampedPi - PI_OVER_2);
            }

            var clamped = new FixFloat64(clampedPi);

            // Find the two closest values in the LUT and perform linear interpolation
            var rawIndex = FastMul(clamped, LutInterval);
            var roundedIndex = Round(rawIndex);
            var indexError = FastSub(rawIndex, roundedIndex);

            var nearestValue = new FixFloat64(TanLut[(int)roundedIndex]);
            var secondNearestValue = new FixFloat64(TanLut[(int)roundedIndex + Sign(indexError)]);

            var delta = FastMul(indexError, FastAbs(FastSub(nearestValue, secondNearestValue))).m_rawValue;
            var interpolatedValue = nearestValue.m_rawValue + delta;
            var finalValue = flip ? -interpolatedValue : interpolatedValue;
            return new FixFloat64(finalValue);
        }

        /// <summary>
        /// Returns the arccos of of the specified number, calculated using Atan and Sqrt
        /// This function has at least 7 decimals of accuracy.
        /// </summary>
        public static FixFloat64 Acos(FixFloat64 x)
        {
            if (x < -One || x > One)
            {
                throw new ArgumentOutOfRangeException(nameof(x));
            }

            if (x.m_rawValue == 0) return PiOver2;

            var result = Atan(Sqrt(One - x * x) / x);
            return x.m_rawValue < 0 ? result + Pi : result;
        }

        /// <summary>
        /// Returns the arctan of of the specified number, calculated using Euler series
        /// This function has at least 7 decimals of accuracy.
        /// </summary>
        public static FixFloat64 Atan(FixFloat64 z)
        {
            if (z.m_rawValue == 0) return Zero;

            // Force positive values for argument
            // Atan(-z) = -Atan(z).
            var neg = z.m_rawValue < 0;
            if (neg)
            {
                z = -z;
            }

            FixFloat64 result;
            var two = (FixFloat64)2;
            var three = (FixFloat64)3;

            bool invert = z > One;
            if (invert) z = One / z;

            result = One;
            var term = One;

            var zSq = z * z;
            var zSq2 = zSq * two;
            var zSqPlusOne = zSq + One;
            var zSq12 = zSqPlusOne * two;
            var dividend = zSq2;
            var divisor = zSqPlusOne * three;

            for (var i = 2; i < 30; ++i)
            {
                term *= dividend / divisor;
                result += term;

                dividend += zSq2;
                divisor += zSq12;

                if (term.m_rawValue == 0) break;
            }

            result = result * z / zSqPlusOne;

            if (invert)
            {
                result = PiOver2 - result;
            }

            if (neg)
            {
                result = -result;
            }
            return result;
        }

        public static FixFloat64 Atan2(FixFloat64 y, FixFloat64 x)
        {
            var yl = y.m_rawValue;
            var xl = x.m_rawValue;
            if (xl == 0)
            {
                if (yl > 0)
                {
                    return PiOver2;
                }
                if (yl == 0)
                {
                    return Zero;
                }
                return -PiOver2;
            }
            FixFloat64 atan;
            var z = y / x;

            // Deal with overflow
            if (One + (FixFloat64)0.28M * z * z == MaxValue)
            {
                return y < Zero ? -PiOver2 : PiOver2;
            }

            if (Abs(z) < One)
            {
                atan = z / (One + (FixFloat64)0.28M * z * z);
                if (xl < 0)
                {
                    if (yl < 0)
                    {
                        return atan - Pi;
                    }
                    return atan + Pi;
                }
            }
            else
            {
                atan = PiOver2 - z / (z * z + (FixFloat64)0.28M);
                if (yl < 0)
                {
                    return atan - Pi;
                }
            }
            return atan;
        }

        public static explicit operator FixFloat64(long value)
        {
            return new FixFloat64(value * ONE);
        }

        public static explicit operator long(FixFloat64 value)
        {
            return value.m_rawValue >> FRACTIONAL_PLACES;
        }
        public static explicit operator FixFloat64(float value)
        {
            return new FixFloat64((long)(value * ONE));
        }
        public static explicit operator float(FixFloat64 value)
        {
            return (float)value.m_rawValue / ONE;
        }
        public static explicit operator FixFloat64(double value)
        {
            return new FixFloat64((long)(value * ONE));
        }
        public static explicit operator double(FixFloat64 value)
        {
            return (double)value.m_rawValue / ONE;
        }
        public static explicit operator FixFloat64(decimal value)
        {
            return new FixFloat64((long)(value * ONE));
        }
        public static explicit operator decimal(FixFloat64 value)
        {
            return (decimal)value.m_rawValue / ONE;
        }

        public override bool Equals(object obj)
        {
            return obj is FixFloat64 && ((FixFloat64)obj).m_rawValue == m_rawValue;
        }

        public override int GetHashCode()
        {
            return m_rawValue.GetHashCode();
        }

        public bool Equals(FixFloat64 other)
        {
            return m_rawValue == other.m_rawValue;
        }

        public int CompareTo(FixFloat64 other)
        {
            return m_rawValue.CompareTo(other.m_rawValue);
        }

        public override string ToString()
        {
            // Up to 10 decimal places
            // return ((decimal)this).ToString("0.##########");
#if UNITY_EDITOR
            UnityEngine.Debug.Log($"<color=red>[ToString()会失真]</color> <color=yellow>(Raw:{m_rawValue} toValue:{(decimal)this})</color> ");
#endif
            return ((decimal)this).ToString();
        }

        public static FixFloat64 FromRaw(long rawValue)
        {
            return new FixFloat64(rawValue);
        }

        internal static void GenerateSinLut()
        {
            using (var writer = new StreamWriter("Fix64SinLut.cs"))
            {
                writer.Write(
@"namespace FixMath.NET 
{
    partial struct FixFloat64 
    {
        public static readonly long[] SinLut = new[] 
        {");
                int lineCounter = 0;
                for (int i = 0; i < LUT_SIZE; ++i)
                {
                    var angle = i * Math.PI * 0.5 / (LUT_SIZE - 1);
                    if (lineCounter++ % 8 == 0)
                    {
                        writer.WriteLine();
                        writer.Write("            ");
                    }
                    var sin = Math.Sin(angle);
                    var rawValue = ((FixFloat64)sin).m_rawValue;
                    writer.Write(string.Format("0x{0:X}L, ", rawValue));
                }
                writer.Write(
@"
        };
    }
}");
            }
        }

        internal static void GenerateTanLut()
        {
            using (var writer = new StreamWriter("Fix64TanLut.cs"))
            {
                writer.Write(
@"namespace FixMath.NET 
{
    partial struct FixFloat64 
    {
        public static readonly long[] TanLut = new[] 
        {");
                int lineCounter = 0;
                for (int i = 0; i < LUT_SIZE; ++i)
                {
                    var angle = i * Math.PI * 0.5 / (LUT_SIZE - 1);
                    if (lineCounter++ % 8 == 0)
                    {
                        writer.WriteLine();
                        writer.Write("            ");
                    }
                    var tan = Math.Tan(angle);
                    if (tan > (double)MaxValue || tan < 0.0)
                    {
                        tan = (double)MaxValue;
                    }
                    var rawValue = (((decimal)tan > (decimal)MaxValue || tan < 0.0) ? MaxValue : (FixFloat64)tan).m_rawValue;
                    writer.Write(string.Format("0x{0:X}L, ", rawValue));
                }
                writer.Write(
@"
        };
    }
}");
            }
        }

        // turn into a Console Application and use this to generate the look-up tables
        //static void Main(string[] args)
        //{
        //    GenerateSinLut();
        //    GenerateTanLut();
        //}

        /// <summary>
        /// The underlying integer representation
        /// </summary>
        public long RawValue => m_rawValue;

        /// <summary>
        /// This is the constructor from raw value; it can only be used interally.
        /// </summary>
        /// <param name="rawValue"></param>
        FixFloat64(long rawValue)
        {
            m_rawValue = rawValue;
        }

        public FixFloat64(int value)
        {
            m_rawValue = value * ONE;
        }
    }
}
