/*
 * @Author: delevin.ying 
 * @Date: 2022-11-28 15:58:26 
 * @Last Modified by: delevin.ying
 * @Last Modified time: 2022-11-28 17:55:44
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



        public static readonly FixFloat64 C0 = (FixFloat64)0L;

        public static readonly FixFloat64 C1 = (FixFloat64)1L;

        public static readonly FixFloat64 C2 = (FixFloat64)2L;

        public static readonly FixFloat64 C3 = (FixFloat64)3L;

        public static readonly FixFloat64 C4 = (FixFloat64)4L;


        public static readonly FixFloat64 C180 = (FixFloat64)180L;


        public static readonly FixFloat64 C0p001 = (FixFloat64)0.001m;

        public static readonly FixFloat64 C0p5 = (FixFloat64)0.5m;

        public static readonly FixFloat64 C0p25 = (FixFloat64)0.25m;

        public static readonly FixFloat64 C1em09 = (FixFloat64)0.000000001m;

        public static readonly FixFloat64 C1em9 = (FixFloat64)0.000000001m;

        public static readonly FixFloat64 Cm1em9 = (FixFloat64)(-0.000000001m);

        public static readonly FixFloat64 C1em14 = (FixFloat64)0.00000000000001m;

        public static readonly FixFloat64 C0p1 = (FixFloat64)0.1m;


        public static readonly FixFloat64 Cm0p9999 = (FixFloat64)(-0.9999m);
        public static readonly FixFloat64 C1m1em12 = FixFloat64.One - (FixFloat64)0.000000000001m;

        public static readonly FixFloat64 GoldenRatio = FixFloat64.One + FixFloat64.Sqrt((FixFloat64)5) / (FixFloat64)2;

        public static readonly FixFloat64 OneTwelfth = FixFloat64.One / (FixFloat64)12;

        public static readonly FixFloat64 C0p0833333333 = (FixFloat64)0.0833333333m;

        public static readonly FixFloat64 C90000 = (FixFloat64)90000;

        public static readonly FixFloat64 C600000 = (FixFloat64)600000;

    }
}