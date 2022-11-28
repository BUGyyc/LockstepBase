/*
 * @Author: delevin.ying 
 * @Date: 2022-11-28 15:58:26 
 * @Last Modified by: delevin.ying
 * @Last Modified time: 2022-11-28 16:02:35
 */

using System;

namespace FixMath
{
    partial struct FixFloat64
    {

        // 最小精度 is 2^-32, that is 2.3283064365386962890625E-10
        public static readonly decimal Precision = (decimal)(new FixFloat64(1L));//0.00000000023283064365386962890625m;
        public static readonly FixFloat64 MaxValue = new FixFloat64(MAX_VALUE);
        public static readonly FixFloat64 MinValue = new FixFloat64(MIN_VALUE);
        public static readonly FixFloat64 One = new FixFloat64(ONE);
        public static readonly FixFloat64 Zero = new FixFloat64();
        /// <summary>
        /// The value of Pi
        /// </summary>
        public static readonly FixFloat64 Pi = new FixFloat64(PI);
        public static readonly FixFloat64 PiOver2 = new FixFloat64(PI_OVER_2);
        public static readonly FixFloat64 PiTimes2 = new FixFloat64(PI_TIMES_2);
        public static readonly FixFloat64 PiInv = (FixFloat64)0.3183098861837906715377675267M;
        public static readonly FixFloat64 PiOver2Inv = (FixFloat64)0.6366197723675813430755350535M;
        const long MAX_VALUE = long.MaxValue;
        const long MIN_VALUE = long.MinValue;
        const int NUM_BITS = 64;
        const int FRACTIONAL_PLACES = 32;
        const long ONE = 1L << FRACTIONAL_PLACES;
        const long PI_TIMES_2 = 0x6487ED511;
        const long PI = 0x3243F6A88;
        const long PI_OVER_2 = 0x1921FB544;
        const long LN2 = 0xB17217F7;
        const long LOG2MAX = 0x1F00000000;
        const long LOG2MIN = -0x2000000000;
        const int LUT_SIZE = (int)(PI_OVER_2 >> 15);

       

        static readonly FixFloat64 Log2Max = new FixFloat64(LOG2MAX);
        static readonly FixFloat64 Log2Min = new FixFloat64(LOG2MIN);
        static readonly FixFloat64 Ln2 = new FixFloat64(LN2);

        static readonly FixFloat64 LutInterval = (FixFloat64)(LUT_SIZE - 1) / PiOver2;
    }
}