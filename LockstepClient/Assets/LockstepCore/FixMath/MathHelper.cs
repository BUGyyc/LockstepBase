/*
 * @Author: delevin.ying 
 * @Date: 2022-11-26 15:48:59 
 * @Last Modified by: delevin.ying
 * @Last Modified time: 2022-11-28 15:15:06
 */

using System;

namespace FixMath
{



    /// <summary>
    /// Contains helper math methods.
    /// </summary>
    public static class MathHelper
    {
        /// <summary>
        /// Approximate value of Pi.
        /// </summary>
        public static readonly FixFloat64 Pi = FixFloat64.Pi;

        /// <summary>
        /// Approximate value of Pi multiplied by two.
        /// </summary>
        public static readonly FixFloat64 TwoPi = FixFloat64.PiTimes2;

        /// <summary>
        /// Approximate value of Pi divided by two.
        /// </summary>
        public static readonly FixFloat64 PiOver2 = FixFloat64.PiOver2;

        /// <summary>
        /// Approximate value of Pi divided by four.
        /// </summary>
        public static readonly FixFloat64 PiOver4 = FixFloat64.Pi / new FixFloat64(4);

        /// <summary>
        /// Calculate remainder of of FixFloat64 division using same algorithm
        /// as Math.IEEERemainder
        /// </summary>
        /// <param name="dividend">Dividend</param>
        /// <param name="divisor">Divisor</param>
        /// <returns>Remainder</returns>
        public static FixFloat64 IEEERemainder(FixFloat64 dividend, FixFloat64 divisor)
        {
            return dividend - divisor * FixFloat64.Round(dividend / divisor);
        }

        /// <summary>
        /// Reduces the angle into a range from -Pi to Pi.
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
        /// Clamps a value between a minimum and maximum value.
        /// </summary>
        /// <param name="value">Value to clamp.</param>
        /// <param name="min">Minimum value.  If the value is less than this, the minimum is returned instead.</param>
        /// <param name="max">Maximum value.  If the value is more than this, the maximum is returned instead.</param>
        /// <returns>Clamped value.</returns>
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

        /// <summary>
        /// Returns the higher value of the two parameters.
        /// </summary>
        /// <param name="a">First value.</param>
        /// <param name="b">Second value.</param>
        /// <returns>Higher value of the two parameters.</returns>
        public static FixFloat64 Max(FixFloat64 a, FixFloat64 b)
        {
            return (a > b) ? a : b;
        }

        /// <summary>
        /// Returns the lower value of the two parameters.
        /// </summary>
        /// <param name="a">First value.</param>
        /// <param name="b">Second value.</param>
        /// <returns>Lower value of the two parameters.</returns>
        public static FixFloat64 Min(FixFloat64 a, FixFloat64 b)
        {
            return (a < b) ? a : b;
        }

        /// <summary>
        /// Converts degrees to radians.
        /// </summary>
        /// <param name="degrees">Degrees to convert.</param>
        /// <returns>Radians equivalent to the input degrees.</returns>
        public static FixFloat64 ToRadians(FixFloat64 degrees)
        {
            return degrees * (Pi / FixFloat64.C180);
        }

        /// <summary>
        /// Converts radians to degrees.
        /// </summary>
        /// <param name="radians">Radians to convert.</param>
        /// <returns>Degrees equivalent to the input radians.</returns>
        public static FixFloat64 ToDegrees(FixFloat64 radians)
        {
            return radians * (FixFloat64.C180 / Pi);
        }
    }
}