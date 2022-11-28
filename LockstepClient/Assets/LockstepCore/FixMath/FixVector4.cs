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

        /// <summary>
        /// Gets the zero vector.
        /// </summary>
        public static FixVector4 Zero => default(FixVector4);

        /// <summary>
        /// Gets a vector pointing along the X axis.
        /// </summary>
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
        /// Gets a vector pointing along the Y axis.
        /// </summary>
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
        /// Gets a vector pointing along the Z axis.
        /// </summary>
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
        /// Gets a vector pointing along the W axis.
        /// </summary>
        public static FixVector4 UnitW
        {
            get
            {
                FixVector4 result = default(FixVector4);
                result.W = FixFloat64.C1;
                return result;
            }
        }

        /// <summary>
        /// Constructs a new 3d vector.
        /// </summary>
        /// <param name="x">X component of the vector.</param>
        /// <param name="y">Y component of the vector.</param>
        /// <param name="z">Z component of the vector.</param>
        /// <param name="w">W component of the vector.</param>
        public FixVector4(FixFloat64 x, FixFloat64 y, FixFloat64 z, FixFloat64 w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        /// <summary>
        /// Constructs a new 3d vector.
        /// </summary>
        /// <param name="xyz">X, Y, and Z components of the vector.</param>
        /// <param name="w">W component of the vector.</param>
        public FixVector4(FixVector3 xyz, FixFloat64 w)
        {
            X = xyz.X;
            Y = xyz.Y;
            Z = xyz.Z;
            W = w;
        }

        /// <summary>
        /// Constructs a new 3d vector.
        /// </summary>
        /// <param name="x">X component of the vector.</param>
        /// <param name="yzw">Y, Z, and W components of the vector.</param>
        public FixVector4(FixFloat64 x, FixVector3 yzw)
        {
            X = x;
            Y = yzw.X;
            Z = yzw.Y;
            W = yzw.Z;
        }

        /// <summary>
        /// Constructs a new 3d vector.
        /// </summary>
        /// <param name="xy">X and Y components of the vector.</param>
        /// <param name="z">Z component of the vector.</param>
        /// <param name="w">W component of the vector.</param>
        public FixVector4(FixVector2 xy, FixFloat64 z, FixFloat64 w)
        {
            X = xy.X;
            Y = xy.Y;
            Z = z;
            W = w;
        }

        /// <summary>
        /// Constructs a new 3d vector.
        /// </summary>
        /// <param name="x">X component of the vector.</param>
        /// <param name="yz">Y and Z components of the vector.</param>
        /// <param name="w">W component of the vector.</param>
        public FixVector4(FixFloat64 x, FixVector2 yz, FixFloat64 w)
        {
            X = x;
            Y = yz.X;
            Z = yz.Y;
            W = w;
        }

        /// <summary>
        /// Constructs a new 3d vector.
        /// </summary>
        /// <param name="x">X component of the vector.</param>
        /// <param name="y">Y and Z components of the vector.</param>
        /// <param name="zw">W component of the vector.</param>
        public FixVector4(FixFloat64 x, FixFloat64 y, FixVector2 zw)
        {
            X = x;
            Y = y;
            Z = zw.X;
            W = zw.Y;
        }

        /// <summary>
        /// Constructs a new 3d vector.
        /// </summary>
        /// <param name="xy">X and Y components of the vector.</param>
        /// <param name="zw">Z and W components of the vector.</param>
        public FixVector4(FixVector2 xy, FixVector2 zw)
        {
            X = xy.X;
            Y = xy.Y;
            Z = zw.X;
            W = zw.Y;
        }

        /// <summary>
        /// Computes the squared length of the vector.
        /// </summary>
        /// <returns>Squared length of the vector.</returns>
        public FixFloat64 LengthSquared()
        {
            return X * X + Y * Y + Z * Z + W * W;
        }

        /// <summary>
        /// Computes the length of the vector.
        /// </summary>
        /// <returns>Length of the vector.</returns>
        public FixFloat64 Length()
        {
            return FixFloat64.Sqrt(X * X + Y * Y + Z * Z + W * W);
        }

        /// <summary>
        /// Normalizes the vector.
        /// </summary>
        public void Normalize()
        {
            FixFloat64 fix = FixFloat64.C1 / FixFloat64.Sqrt(X * X + Y * Y + Z * Z + W * W);
            X *= fix;
            Y *= fix;
            Z *= fix;
            W *= fix;
        }

        /// <summary>
        /// Gets a string representation of the vector.
        /// </summary>
        /// <returns>String representing the vector.</returns>
        public override string ToString()
        {
            return string.Concat("{", X, ", ", Y, ", ", Z, ", ", W, "}");
        }

        /// <summary>
        /// Computes the dot product of two vectors.
        /// </summary>
        /// <param name="a">First vector in the product.</param>
        /// <param name="b">Second vector in the product.</param>
        /// <returns>Resulting dot product.</returns>
        public static FixFloat64 Dot(FixVector4 a, FixVector4 b)
        {
            return a.X * b.X + a.Y * b.Y + a.Z * b.Z + a.W * b.W;
        }

        /// <summary>
        /// Computes the dot product of two vectors.
        /// </summary>
        /// <param name="a">First vector in the product.</param>
        /// <param name="b">Second vector in the product.</param>
        /// <param name="product">Resulting dot product.</param>
        public static void Dot(ref FixVector4 a, ref FixVector4 b, out FixFloat64 product)
        {
            product = a.X * b.X + a.Y * b.Y + a.Z * b.Z + a.W * b.W;
        }

        /// <summary>
        /// Adds two vectors together.
        /// </summary>
        /// <param name="a">First vector to add.</param>
        /// <param name="b">Second vector to add.</param>
        /// <param name="sum">Sum of the two vectors.</param>
        public static void Add(ref FixVector4 a, ref FixVector4 b, out FixVector4 sum)
        {
            sum.X = a.X + b.X;
            sum.Y = a.Y + b.Y;
            sum.Z = a.Z + b.Z;
            sum.W = a.W + b.W;
        }

        /// <summary>
        /// Subtracts two vectors.
        /// </summary>
        /// <param name="a">Vector to subtract from.</param>
        /// <param name="b">Vector to subtract from the first vector.</param>
        /// <param name="difference">Result of the subtraction.</param>
        public static void Subtract(ref FixVector4 a, ref FixVector4 b, out FixVector4 difference)
        {
            difference.X = a.X - b.X;
            difference.Y = a.Y - b.Y;
            difference.Z = a.Z - b.Z;
            difference.W = a.W - b.W;
        }

        /// <summary>
        /// Scales a vector.
        /// </summary>
        /// <param name="v">Vector to scale.</param>
        /// <param name="scale">Amount to scale.</param>
        /// <param name="result">Scaled vector.</param>
        public static void Multiply(ref FixVector4 v, FixFloat64 scale, out FixVector4 result)
        {
            result.X = v.X * scale;
            result.Y = v.Y * scale;
            result.Z = v.Z * scale;
            result.W = v.W * scale;
        }

        /// <summary>
        /// Multiplies two vectors on a per-component basis.
        /// </summary>
        /// <param name="a">First vector to multiply.</param>
        /// <param name="b">Second vector to multiply.</param>
        /// <param name="result">Result of the componentwise multiplication.</param>
        public static void Multiply(ref FixVector4 a, ref FixVector4 b, out FixVector4 result)
        {
            result.X = a.X * b.X;
            result.Y = a.Y * b.Y;
            result.Z = a.Z * b.Z;
            result.W = a.W * b.W;
        }

        /// <summary>
        /// Divides a vector's components by some amount.
        /// </summary>
        /// <param name="v">Vector to divide.</param>
        /// <param name="divisor">Value to divide the vector's components.</param>
        /// <param name="result">Result of the division.</param>
        public static void Divide(ref FixVector4 v, FixFloat64 divisor, out FixVector4 result)
        {
            FixFloat64 fix = FixFloat64.C1 / divisor;
            result.X = v.X * fix;
            result.Y = v.Y * fix;
            result.Z = v.Z * fix;
            result.W = v.W * fix;
        }

        /// <summary>
        /// Scales a vector.
        /// </summary>
        /// <param name="v">Vector to scale.</param>
        /// <param name="f">Amount to scale.</param>
        /// <returns>Scaled vector.</returns>
        public static FixVector4 operator *(FixVector4 v, FixFloat64 f)
        {
            FixVector4 result = default(FixVector4);
            result.X = v.X * f;
            result.Y = v.Y * f;
            result.Z = v.Z * f;
            result.W = v.W * f;
            return result;
        }

        /// <summary>
        /// Scales a vector.
        /// </summary>
        /// <param name="v">Vector to scale.</param>
        /// <param name="f">Amount to scale.</param>
        /// <returns>Scaled vector.</returns>
        public static FixVector4 operator *(FixFloat64 f, FixVector4 v)
        {
            FixVector4 result = default(FixVector4);
            result.X = v.X * f;
            result.Y = v.Y * f;
            result.Z = v.Z * f;
            result.W = v.W * f;
            return result;
        }

        /// <summary>
        /// Multiplies two vectors on a per-component basis.
        /// </summary>
        /// <param name="a">First vector to multiply.</param>
        /// <param name="b">Second vector to multiply.</param>
        /// <returns>Result of the componentwise multiplication.</returns>
        public static FixVector4 operator *(FixVector4 a, FixVector4 b)
        {
            Multiply(ref a, ref b, out var result);
            return result;
        }

        /// <summary>
        /// Divides a vector's components by some amount.
        /// </summary>
        /// <param name="v">Vector to divide.</param>
        /// <param name="f">Value to divide the vector's components.</param>
        /// <returns>Result of the division.</returns>
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

        /// <summary>
        /// Subtracts two vectors.
        /// </summary>
        /// <param name="a">Vector to subtract from.</param>
        /// <param name="b">Vector to subtract from the first vector.</param>
        /// <returns>Result of the subtraction.</returns>
        public static FixVector4 operator -(FixVector4 a, FixVector4 b)
        {
            FixVector4 result = default(FixVector4);
            result.X = a.X - b.X;
            result.Y = a.Y - b.Y;
            result.Z = a.Z - b.Z;
            result.W = a.W - b.W;
            return result;
        }

        /// <summary>
        /// Adds two vectors together.
        /// </summary>
        /// <param name="a">First vector to add.</param>
        /// <param name="b">Second vector to add.</param>
        /// <returns>Sum of the two vectors.</returns>
        public static FixVector4 operator +(FixVector4 a, FixVector4 b)
        {
            FixVector4 result = default(FixVector4);
            result.X = a.X + b.X;
            result.Y = a.Y + b.Y;
            result.Z = a.Z + b.Z;
            result.W = a.W + b.W;
            return result;
        }

        /// <summary>
        /// Negates the vector.
        /// </summary>
        /// <param name="v">Vector to negate.</param>
        /// <returns>Negated vector.</returns>
        public static FixVector4 operator -(FixVector4 v)
        {
            v.X = -v.X;
            v.Y = -v.Y;
            v.Z = -v.Z;
            v.W = -v.W;
            return v;
        }

        /// <summary>
        /// Tests two vectors for componentwise equivalence.
        /// </summary>
        /// <param name="a">First vector to test for equivalence.</param>
        /// <param name="b">Second vector to test for equivalence.</param>
        /// <returns>Whether the vectors were equivalent.</returns>
        public static bool operator ==(FixVector4 a, FixVector4 b)
        {
            return a.X == b.X && a.Y == b.Y && a.Z == b.Z && a.W == b.W;
        }

        /// <summary>
        /// Tests two vectors for componentwise inequivalence.
        /// </summary>
        /// <param name="a">First vector to test for inequivalence.</param>
        /// <param name="b">Second vector to test for inequivalence.</param>
        /// <returns>Whether the vectors were inequivalent.</returns>
        public static bool operator !=(FixVector4 a, FixVector4 b)
        {
            return a.X != b.X || a.Y != b.Y || a.Z != b.Z || a.W != b.W;
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals(FixVector4 other)
        {
            return X == other.X && Y == other.Y && Z == other.Z && W == other.W;
        }

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <returns>
        /// true if <paramref name="obj" /> and this instance are the same type and represent the same value; otherwise, false.
        /// </returns>
        /// <param name="obj">Another object to compare to. </param><filterpriority>2</filterpriority>
        public override bool Equals(object obj)
        {
            if (obj is FixVector4)
            {
                return Equals((FixVector4)obj);
            }
            return false;
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>
        /// A 32-bit signed integer that is the hash code for this instance.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            return X.GetHashCode() + Y.GetHashCode() + Z.GetHashCode() + W.GetHashCode();
        }

        /// <summary>
        /// Computes the squared distance between two vectors.
        /// </summary>
        /// <param name="a">First vector.</param>
        /// <param name="b">Second vector.</param>
        /// <param name="distanceSquared">Squared distance between the two vectors.</param>
        public static void DistanceSquared(ref FixVector4 a, ref FixVector4 b, out FixFloat64 distanceSquared)
        {
            FixFloat64 fix = a.X - b.X;
            FixFloat64 fix2 = a.Y - b.Y;
            FixFloat64 fix3 = a.Z - b.Z;
            FixFloat64 fix4 = a.W - b.W;
            distanceSquared = fix * fix + fix2 * fix2 + fix3 * fix3 + fix4 * fix4;
        }

        /// <summary>
        /// Computes the distance between two two vectors.
        /// </summary>
        /// <param name="a">First vector.</param>
        /// <param name="b">Second vector.</param>
        /// <param name="distance">Distance between the two vectors.</param>
        public static void Distance(ref FixVector4 a, ref FixVector4 b, out FixFloat64 distance)
        {
            FixFloat64 fix = a.X - b.X;
            FixFloat64 fix2 = a.Y - b.Y;
            FixFloat64 fix3 = a.Z - b.Z;
            FixFloat64 fix4 = a.W - b.W;
            distance = FixFloat64.Sqrt(fix * fix + fix2 * fix2 + fix3 * fix3 + fix4 * fix4);
        }

        /// <summary>
        /// Computes the distance between two two vectors.
        /// </summary>
        /// <param name="a">First vector.</param>
        /// <param name="b">Second vector.</param>
        /// <returns>Distance between the two vectors.</returns>
        public static FixFloat64 Distance(FixVector4 a, FixVector4 b)
        {
            Distance(ref a, ref b, out var distance);
            return distance;
        }

        /// <summary>
        /// Normalizes the given vector.
        /// </summary>
        /// <param name="v">Vector to normalize.</param>
        /// <returns>Normalized vector.</returns>
        public static FixVector4 Normalize(FixVector4 v)
        {
            Normalize(ref v, out var result);
            return result;
        }

        /// <summary>
        /// Normalizes the given vector.
        /// </summary>
        /// <param name="v">Vector to normalize.</param>
        /// <param name="result">Normalized vector.</param>
        public static void Normalize(ref FixVector4 v, out FixVector4 result)
        {
            FixFloat64 fix = FixFloat64.C1 / FixFloat64.Sqrt(v.X * v.X + v.Y * v.Y + v.Z * v.Z + v.W * v.W);
            result.X = v.X * fix;
            result.Y = v.Y * fix;
            result.Z = v.Z * fix;
            result.W = v.W * fix;
        }

        /// <summary>
        /// Negates a vector.
        /// </summary>
        /// <param name="v">Vector to negate.</param>
        /// <param name="negated">Negated vector.</param>
        public static void Negate(ref FixVector4 v, out FixVector4 negated)
        {
            negated.X = -v.X;
            negated.Y = -v.Y;
            negated.Z = -v.Z;
            negated.W = -v.W;
        }

        /// <summary>
        /// Computes the absolute value of the input vector.
        /// </summary>
        /// <param name="v">Vector to take the absolute value of.</param>
        /// <param name="result">Vector with nonnegative elements.</param>
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

        /// <summary>
        /// Computes the absolute value of the input vector.
        /// </summary>
        /// <param name="v">Vector to take the absolute value of.</param>
        /// <returns>Vector with nonnegative elements.</returns>
        public static FixVector4 Abs(FixVector4 v)
        {
            Abs(ref v, out var result);
            return result;
        }

        /// <summary>
        /// Creates a vector from the lesser values in each vector.
        /// </summary>
        /// <param name="a">First input vector to compare values from.</param>
        /// <param name="b">Second input vector to compare values from.</param>
        /// <param name="min">Vector containing the lesser values of each vector.</param>
        public static void Min(ref FixVector4 a, ref FixVector4 b, out FixVector4 min)
        {
            min.X = ((a.X < b.X) ? a.X : b.X);
            min.Y = ((a.Y < b.Y) ? a.Y : b.Y);
            min.Z = ((a.Z < b.Z) ? a.Z : b.Z);
            min.W = ((a.W < b.W) ? a.W : b.W);
        }

        /// <summary>
        /// Creates a vector from the lesser values in each vector.
        /// </summary>
        /// <param name="a">First input vector to compare values from.</param>
        /// <param name="b">Second input vector to compare values from.</param>
        /// <returns>Vector containing the lesser values of each vector.</returns>
        public static FixVector4 Min(FixVector4 a, FixVector4 b)
        {
            Min(ref a, ref b, out var min);
            return min;
        }

        /// <summary>
        /// Creates a vector from the greater values in each vector.
        /// </summary>
        /// <param name="a">First input vector to compare values from.</param>
        /// <param name="b">Second input vector to compare values from.</param>
        /// <param name="max">Vector containing the greater values of each vector.</param>
        public static void Max(ref FixVector4 a, ref FixVector4 b, out FixVector4 max)
        {
            max.X = ((a.X > b.X) ? a.X : b.X);
            max.Y = ((a.Y > b.Y) ? a.Y : b.Y);
            max.Z = ((a.Z > b.Z) ? a.Z : b.Z);
            max.W = ((a.W > b.W) ? a.W : b.W);
        }

        /// <summary>
        /// Creates a vector from the greater values in each vector.
        /// </summary>
        /// <param name="a">First input vector to compare values from.</param>
        /// <param name="b">Second input vector to compare values from.</param>
        /// <returns>Vector containing the greater values of each vector.</returns>
        public static FixVector4 Max(FixVector4 a, FixVector4 b)
        {
            Max(ref a, ref b, out var max);
            return max;
        }

        /// <summary>
        /// Computes an interpolated state between two vectors.
        /// </summary>
        /// <param name="start">Starting location of the interpolation.</param>
        /// <param name="end">Ending location of the interpolation.</param>
        /// <param name="interpolationAmount">Amount of the end location to use.</param>
        /// <returns>Interpolated intermediate state.</returns>
        public static FixVector4 Lerp(FixVector4 start, FixVector4 end, FixFloat64 interpolationAmount)
        {
            Lerp(ref start, ref end, interpolationAmount, out var result);
            return result;
        }

        /// <summary>
        /// Computes an interpolated state between two vectors.
        /// </summary>
        /// <param name="start">Starting location of the interpolation.</param>
        /// <param name="end">Ending location of the interpolation.</param>
        /// <param name="interpolationAmount">Amount of the end location to use.</param>
        /// <param name="result">Interpolated intermediate state.</param>
        public static void Lerp(ref FixVector4 start, ref FixVector4 end, FixFloat64 interpolationAmount, out FixVector4 result)
        {
            FixFloat64 fix = FixFloat64.C1 - interpolationAmount;
            result.X = start.X * fix + end.X * interpolationAmount;
            result.Y = start.Y * fix + end.Y * interpolationAmount;
            result.Z = start.Z * fix + end.Z * interpolationAmount;
            result.W = start.W * fix + end.W * interpolationAmount;
        }

        /// <summary>
        /// Computes an intermediate location using hermite interpolation.
        /// </summary>
        /// <param name="value1">First position.</param>
        /// <param name="tangent1">Tangent associated with the first position.</param>
        /// <param name="value2">Second position.</param>
        /// <param name="tangent2">Tangent associated with the second position.</param>
        /// <param name="interpolationAmount">Amount of the second point to use.</param>
        /// <param name="result">Interpolated intermediate state.</param>
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

        /// <summary>
        /// Computes an intermediate location using hermite interpolation.
        /// </summary>
        /// <param name="value1">First position.</param>
        /// <param name="tangent1">Tangent associated with the first position.</param>
        /// <param name="value2">Second position.</param>
        /// <param name="tangent2">Tangent associated with the second position.</param>
        /// <param name="interpolationAmount">Amount of the second point to use.</param>
        /// <returns>Interpolated intermediate state.</returns>
        public static FixVector4 Hermite(FixVector4 value1, FixVector4 tangent1, FixVector4 value2, FixVector4 tangent2, FixFloat64 interpolationAmount)
        {
            Hermite(ref value1, ref tangent1, ref value2, ref tangent2, interpolationAmount, out var result);
            return result;
        }
    }
}