// /*
//  * @Author: delevin.ying 
//  * @Date: 2022-11-26 15:20:48 
//  * @Last Modified by: delevin.ying
//  * @Last Modified time: 2022-11-26 15:23:09
//  */
// /*
//  * @Author: delevin.ying 
//  * @Date: 2022-11-26 14:20:11 
//  * @Last Modified by: delevin.ying
//  * @Last Modified time: 2022-11-26 14:23:16
//  */
// using System;

// namespace FixMath
// {

//     [Serializable]
//     public struct FixVector3 : IEquatable<FixVector3>
//     {
//         /// <summary>
//         /// X component of the vector.
//         /// </summary>
//         public FixFloat64 X;

//         /// <summary>
//         /// Y component of the vector.
//         /// </summary>
//         public FixFloat64 Y;

//         /// <summary>
//         /// Z component of the vector.
//         /// </summary>
//         public FixFloat64 Z;

//         public FixFloat64 x => X;

//         public FixFloat64 y => Y;

//         public FixFloat64 z => Z;

//         /// <summary>
//         /// Gets the zero vector.
//         /// </summary>
//         public static FixVector3 Zero => default(FixVector3);

//         /// <summary>
//         /// Gets the up vector (0,1,0).
//         /// </summary>
//         public static FixVector3 Up
//         {
//             get
//             {
//                 FixVector3 result = default(FixVector3);
//                 result.X = FixFloat64.C0;
//                 result.Y = FixFloat64.C1;
//                 result.Z = FixFloat64.C0;
//                 return result;
//             }
//         }

//         /// <summary>
//         /// Gets the down vector (0,-1,0).
//         /// </summary>
//         public static FixVector3 Down
//         {
//             get
//             {
//                 FixVector3 result = default(FixVector3);
//                 result.X = FixFloat64.C0;
//                 result.Y = -1;
//                 result.Z = FixFloat64.C0;
//                 return result;
//             }
//         }

//         /// <summary>
//         /// Gets the right vector (1,0,0).
//         /// </summary>
//         public static FixVector3 Right
//         {
//             get
//             {
//                 FixVector3 result = default(FixVector3);
//                 result.X = FixFloat64.C1;
//                 result.Y = FixFloat64.C0;
//                 result.Z = FixFloat64.C0;
//                 return result;
//             }
//         }

//         /// <summary>
//         /// Gets the left vector (-1,0,0).
//         /// </summary>
//         public static FixVector3 Left
//         {
//             get
//             {
//                 FixVector3 result = default(FixVector3);
//                 result.X = -1;
//                 result.Y = FixFloat64.C0;
//                 result.Z = FixFloat64.C0;
//                 return result;
//             }
//         }

//         /// <summary>
//         /// Gets the forward vector (0,0,-1).
//         /// </summary>
//         public static FixVector3 Forward
//         {
//             get
//             {
//                 FixVector3 result = default(FixVector3);
//                 result.X = FixFloat64.C0;
//                 result.Y = FixFloat64.C0;
//                 result.Z = -1;
//                 return result;
//             }
//         }

//         /// <summary>
//         /// Gets the back vector (0,0,1).
//         /// </summary>
//         public static FixVector3 Backward
//         {
//             get
//             {
//                 FixVector3 result = default(FixVector3);
//                 result.X = FixFloat64.C0;
//                 result.Y = FixFloat64.C0;
//                 result.Z = FixFloat64.C1;
//                 return result;
//             }
//         }

//         /// <summary>
//         /// Gets a vector pointing along the X axis.
//         /// </summary>
//         public static FixVector3 UnitX
//         {
//             get
//             {
//                 FixVector3 result = default(FixVector3);
//                 result.X = FixFloat64.C1;
//                 return result;
//             }
//         }

//         /// <summary>
//         /// Gets a vector pointing along the Y axis.
//         /// </summary>
//         public static FixVector3 UnitY
//         {
//             get
//             {
//                 FixVector3 result = default(FixVector3);
//                 result.Y = FixFloat64.C1;
//                 return result;
//             }
//         }

//         /// <summary>
//         /// Gets a vector pointing along the Z axis.
//         /// </summary>
//         public static FixVector3 UnitZ
//         {
//             get
//             {
//                 FixVector3 result = default(FixVector3);
//                 result.Z = FixFloat64.C1;
//                 return result;
//             }
//         }

//         /// <summary>
//         /// Constructs a new 3d vector.
//         /// </summary>
//         /// <param name="x">X component of the vector.</param>
//         /// <param name="y">Y component of the vector.</param>
//         /// <param name="z">Z component of the vector.</param>
//         public FixVector3(FixFloat64 x, FixFloat64 y, FixFloat64 z)
//         {
//             X = x;
//             Y = y;
//             Z = z;
//         }

//         /// <summary>
//         /// Constructs a new 3d vector.
//         /// </summary>
//         /// <param name="xy">X and Y components of the vector.</param>
//         /// <param name="z">Z component of the vector.</param>
//         public FixVector3(FixVector2 xy, FixFloat64 z)
//         {
//             X = xy.X;
//             Y = xy.Y;
//             Z = z;
//         }

//         /// <summary>
//         /// Constructs a new 3d vector.
//         /// </summary>
//         /// <param name="x">X component of the vector.</param>
//         /// <param name="yz">Y and Z components of the vector.</param>
//         public FixVector3(FixFloat64 x, FixVector2 yz)
//         {
//             X = x;
//             Y = yz.X;
//             Z = yz.Y;
//         }

//         /// <summary>
//         /// Computes the squared length of the vector.
//         /// </summary>
//         /// <returns>Squared length of the vector.</returns>
//         public FixFloat64 LengthSquared()
//         {
//             return X * X + Y * Y + Z * Z;
//         }

//         /// <summary>
//         /// Computes the length of the vector.
//         /// </summary>
//         /// <returns>Length of the vector.</returns>
//         public FixFloat64 Length()
//         {
//             return FixFloat64.Sqrt(X * X + Y * Y + Z * Z);
//         }

//         /// <summary>
//         /// Normalizes the vector.
//         /// </summary>
//         public void Normalize()
//         {
//             FixFloat64 fix = FixFloat64.C1 / FixFloat64.Sqrt(X * X + Y * Y + Z * Z);
//             X *= fix;
//             Y *= fix;
//             Z *= fix;
//         }

//         /// <summary>
//         /// Gets a string representation of the vector.
//         /// </summary>
//         /// <returns>String representing the vector.</returns>
//         public override string ToString()
//         {
//             return string.Concat("{", X, ", ", Y, ", ", Z, "}");
//         }

//         /// <summary>
//         /// Computes the dot product of two vectors.
//         /// </summary>
//         /// <param name="a">First vector in the product.</param>
//         /// <param name="b">Second vector in the product.</param>
//         /// <returns>Resulting dot product.</returns>
//         public static FixFloat64 Dot(FixVector3 a, FixVector3 b)
//         {
//             return a.X * b.X + a.Y * b.Y + a.Z * b.Z;
//         }

//         /// <summary>
//         /// Computes the dot product of two vectors.
//         /// </summary>
//         /// <param name="a">First vector in the product.</param>
//         /// <param name="b">Second vector in the product.</param>
//         /// <param name="product">Resulting dot product.</param>
//         public static void Dot(ref FixVector3 a, ref FixVector3 b, out FixFloat64 product)
//         {
//             product = a.X * b.X + a.Y * b.Y + a.Z * b.Z;
//         }

//         /// <summary>
//         /// Adds two vectors together.
//         /// </summary>
//         /// <param name="a">First vector to add.</param>
//         /// <param name="b">Second vector to add.</param>
//         /// <param name="sum">Sum of the two vectors.</param>
//         public static void Add(ref FixVector3 a, ref FixVector3 b, out FixVector3 sum)
//         {
//             sum.X = a.X + b.X;
//             sum.Y = a.Y + b.Y;
//             sum.Z = a.Z + b.Z;
//         }

//         /// <summary>
//         /// Subtracts two vectors.
//         /// </summary>
//         /// <param name="a">Vector to subtract from.</param>
//         /// <param name="b">Vector to subtract from the first vector.</param>
//         /// <param name="difference">Result of the subtraction.</param>
//         public static void Subtract(ref FixVector3 a, ref FixVector3 b, out FixVector3 difference)
//         {
//             difference.X = a.X - b.X;
//             difference.Y = a.Y - b.Y;
//             difference.Z = a.Z - b.Z;
//         }

//         /// <summary>
//         /// Scales a vector.
//         /// </summary>
//         /// <param name="v">Vector to scale.</param>
//         /// <param name="scale">Amount to scale.</param>
//         /// <param name="result">Scaled vector.</param>
//         public static void Multiply(ref FixVector3 v, FixFloat64 scale, out FixVector3 result)
//         {
//             result.X = v.X * scale;
//             result.Y = v.Y * scale;
//             result.Z = v.Z * scale;
//         }

//         /// <summary>
//         /// Multiplies two vectors on a per-component basis.
//         /// </summary>
//         /// <param name="a">First vector to multiply.</param>
//         /// <param name="b">Second vector to multiply.</param>
//         /// <param name="result">Result of the componentwise multiplication.</param>
//         public static void Multiply(ref FixVector3 a, ref FixVector3 b, out FixVector3 result)
//         {
//             result.X = a.X * b.X;
//             result.Y = a.Y * b.Y;
//             result.Z = a.Z * b.Z;
//         }

//         /// <summary>
//         /// Divides a vector's components by some amount.
//         /// </summary>
//         /// <param name="v">Vector to divide.</param>
//         /// <param name="divisor">Value to divide the vector's components.</param>
//         /// <param name="result">Result of the division.</param>
//         public static void Divide(ref FixVector3 v, FixFloat64 divisor, out FixVector3 result)
//         {
//             FixFloat64 fix = FixFloat64.C1 / divisor;
//             result.X = v.X * fix;
//             result.Y = v.Y * fix;
//             result.Z = v.Z * fix;
//         }

//         /// <summary>
//         /// Scales a vector.
//         /// </summary>
//         /// <param name="v">Vector to scale.</param>
//         /// <param name="f">Amount to scale.</param>
//         /// <returns>Scaled vector.</returns>
//         public static FixVector3 operator *(FixVector3 v, FixFloat64 f)
//         {
//             FixVector3 result = default(FixVector3);
//             result.X = v.X * f;
//             result.Y = v.Y * f;
//             result.Z = v.Z * f;
//             return result;
//         }

//         /// <summary>
//         /// Scales a vector.
//         /// </summary>
//         /// <param name="v">Vector to scale.</param>
//         /// <param name="f">Amount to scale.</param>
//         /// <returns>Scaled vector.</returns>
//         public static FixVector3 operator *(FixFloat64 f, FixVector3 v)
//         {
//             FixVector3 result = default(FixVector3);
//             result.X = v.X * f;
//             result.Y = v.Y * f;
//             result.Z = v.Z * f;
//             return result;
//         }

//         /// <summary>
//         /// Multiplies two vectors on a per-component basis.
//         /// </summary>
//         /// <param name="a">First vector to multiply.</param>
//         /// <param name="b">Second vector to multiply.</param>
//         /// <returns>Result of the componentwise multiplication.</returns>
//         public static FixVector3 operator *(FixVector3 a, FixVector3 b)
//         {
//             Multiply(ref a, ref b, out var result);
//             return result;
//         }

//         /// <summary>
//         /// Divides a vector's components by some amount.
//         /// </summary>
//         /// <param name="v">Vector to divide.</param>
//         /// <param name="f">Value to divide the vector's components.</param>
//         /// <returns>Result of the division.</returns>
//         public static FixVector3 operator /(FixVector3 v, FixFloat64 f)
//         {
//             f = FixFloat64.C1 / f;
//             FixVector3 result = default(FixVector3);
//             result.X = v.X * f;
//             result.Y = v.Y * f;
//             result.Z = v.Z * f;
//             return result;
//         }

//         /// <summary>
//         /// Subtracts two vectors.
//         /// </summary>
//         /// <param name="a">Vector to subtract from.</param>
//         /// <param name="b">Vector to subtract from the first vector.</param>
//         /// <returns>Result of the subtraction.</returns>
//         public static FixVector3 operator -(FixVector3 a, FixVector3 b)
//         {
//             FixVector3 result = default(FixVector3);
//             result.X = a.X - b.X;
//             result.Y = a.Y - b.Y;
//             result.Z = a.Z - b.Z;
//             return result;
//         }

//         /// <summary>
//         /// Adds two vectors together.
//         /// </summary>
//         /// <param name="a">First vector to add.</param>
//         /// <param name="b">Second vector to add.</param>
//         /// <returns>Sum of the two vectors.</returns>
//         public static FixVector3 operator +(FixVector3 a, FixVector3 b)
//         {
//             FixVector3 result = default(FixVector3);
//             result.X = a.X + b.X;
//             result.Y = a.Y + b.Y;
//             result.Z = a.Z + b.Z;
//             return result;
//         }

//         /// <summary>
//         /// Negates the vector.
//         /// </summary>
//         /// <param name="v">Vector to negate.</param>
//         /// <returns>Negated vector.</returns>
//         public static FixVector3 operator -(FixVector3 v)
//         {
//             v.X = -v.X;
//             v.Y = -v.Y;
//             v.Z = -v.Z;
//             return v;
//         }

//         /// <summary>
//         /// Tests two vectors for componentwise equivalence.
//         /// </summary>
//         /// <param name="a">First vector to test for equivalence.</param>
//         /// <param name="b">Second vector to test for equivalence.</param>
//         /// <returns>Whether the vectors were equivalent.</returns>
//         public static bool operator ==(FixVector3 a, FixVector3 b)
//         {
//             return a.X == b.X && a.Y == b.Y && a.Z == b.Z;
//         }

//         /// <summary>
//         /// Tests two vectors for componentwise inequivalence.
//         /// </summary>
//         /// <param name="a">First vector to test for inequivalence.</param>
//         /// <param name="b">Second vector to test for inequivalence.</param>
//         /// <returns>Whether the vectors were inequivalent.</returns>
//         public static bool operator !=(FixVector3 a, FixVector3 b)
//         {
//             return a.X != b.X || a.Y != b.Y || a.Z != b.Z;
//         }

//         /// <summary>
//         /// Indicates whether the current object is equal to another object of the same type.
//         /// </summary>
//         /// <returns>
//         /// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
//         /// </returns>
//         /// <param name="other">An object to compare with this object.</param>
//         public bool Equals(FixVector3 other)
//         {
//             return X == other.X && Y == other.Y && Z == other.Z;
//         }

//         /// <summary>
//         /// Indicates whether this instance and a specified object are equal.
//         /// </summary>
//         /// <returns>
//         /// true if <paramref name="obj" /> and this instance are the same type and represent the same value; otherwise, false.
//         /// </returns>
//         /// <param name="obj">Another object to compare to. </param><filterpriority>2</filterpriority>
//         public override bool Equals(object obj)
//         {
//             if (obj is FixVector3)
//             {
//                 return Equals((FixVector3)obj);
//             }
//             return false;
//         }

//         /// <summary>
//         /// Returns the hash code for this instance.
//         /// </summary>
//         /// <returns>
//         /// A 32-bit signed integer that is the hash code for this instance.
//         /// </returns>
//         /// <filterpriority>2</filterpriority>
//         public override int GetHashCode()
//         {
//             return X.GetHashCode() + Y.GetHashCode() + Z.GetHashCode();
//         }

//         /// <summary>
//         /// Computes the squared distance between two vectors.
//         /// </summary>
//         /// <param name="a">First vector.</param>
//         /// <param name="b">Second vector.</param>
//         /// <param name="distanceSquared">Squared distance between the two vectors.</param>
//         public static void DistanceSquared(ref FixVector3 a, ref FixVector3 b, out FixFloat64 distanceSquared)
//         {
//             FixFloat64 fix = a.X - b.X;
//             FixFloat64 fix2 = a.Y - b.Y;
//             FixFloat64 fix3 = a.Z - b.Z;
//             distanceSquared = fix * fix + fix2 * fix2 + fix3 * fix3;
//         }

//         /// <summary>
//         /// Computes the squared distance between two vectors.
//         /// </summary>
//         /// <param name="a">First vector.</param>
//         /// <param name="b">Second vector.</param>
//         /// <returns>Squared distance between the two vectors.</returns>
//         public static FixFloat64 DistanceSquared(FixVector3 a, FixVector3 b)
//         {
//             FixFloat64 fix = a.X - b.X;
//             FixFloat64 fix2 = a.Y - b.Y;
//             FixFloat64 fix3 = a.Z - b.Z;
//             return fix * fix + fix2 * fix2 + fix3 * fix3;
//         }

//         /// <summary>
//         /// Computes the distance between two two vectors.
//         /// </summary>
//         /// <param name="a">First vector.</param>
//         /// <param name="b">Second vector.</param>
//         /// <param name="distance">Distance between the two vectors.</param>
//         public static void Distance(ref FixVector3 a, ref FixVector3 b, out FixFloat64 distance)
//         {
//             FixFloat64 fix = a.X - b.X;
//             FixFloat64 fix2 = a.Y - b.Y;
//             FixFloat64 fix3 = a.Z - b.Z;
//             distance = FixFloat64.Sqrt(fix * fix + fix2 * fix2 + fix3 * fix3);
//         }

//         /// <summary>
//         /// Computes the distance between two two vectors.
//         /// </summary>
//         /// <param name="a">First vector.</param>
//         /// <param name="b">Second vector.</param>
//         /// <returns>Distance between the two vectors.</returns>
//         public static FixFloat64 Distance(FixVector3 a, FixVector3 b)
//         {
//             Distance(ref a, ref b, out var distance);
//             return distance;
//         }

//         /// <summary>
//         /// Computes the cross product between two vectors.
//         /// </summary>
//         /// <param name="a">First vector.</param>
//         /// <param name="b">Second vector.</param>
//         /// <returns>Cross product of the two vectors.</returns>
//         public static FixVector3 Cross(FixVector3 a, FixVector3 b)
//         {
//             Cross(ref a, ref b, out var result);
//             return result;
//         }

//         /// <summary>
//         /// Computes the cross product between two vectors.
//         /// </summary>
//         /// <param name="a">First vector.</param>
//         /// <param name="b">Second vector.</param>
//         /// <param name="result">Cross product of the two vectors.</param>
//         public static void Cross(ref FixVector3 a, ref FixVector3 b, out FixVector3 result)
//         {
//             FixFloat64 fix = a.Y * b.Z - a.Z * b.Y;
//             FixFloat64 fix2 = a.Z * b.X - a.X * b.Z;
//             FixFloat64 fix3 = a.X * b.Y - a.Y * b.X;
//             result.X = fix;
//             result.Y = fix2;
//             result.Z = fix3;
//         }

//         /// <summary>
//         /// Normalizes the given vector.
//         /// </summary>
//         /// <param name="v">Vector to normalize.</param>
//         /// <returns>Normalized vector.</returns>
//         public static FixVector3 Normalize(FixVector3 v)
//         {
//             Normalize(ref v, out var result);
//             return result;
//         }

//         /// <summary>
//         /// Normalizes the given vector.
//         /// </summary>
//         /// <param name="v">Vector to normalize.</param>
//         /// <param name="result">Normalized vector.</param>
//         public static void Normalize(ref FixVector3 v, out FixVector3 result)
//         {
//             FixFloat64 fix = FixFloat64.C1 / FixFloat64.Sqrt(v.X * v.X + v.Y * v.Y + v.Z * v.Z);
//             result.X = v.X * fix;
//             result.Y = v.Y * fix;
//             result.Z = v.Z * fix;
//         }

//         /// <summary>
//         /// Negates a vector.
//         /// </summary>
//         /// <param name="v">Vector to negate.</param>
//         /// <param name="negated">Negated vector.</param>
//         public static void Negate(ref FixVector3 v, out FixVector3 negated)
//         {
//             negated.X = -v.X;
//             negated.Y = -v.Y;
//             negated.Z = -v.Z;
//         }

//         /// <summary>
//         /// Computes the absolute value of the input vector.
//         /// </summary>
//         /// <param name="v">Vector to take the absolute value of.</param>
//         /// <param name="result">Vector with nonnegative elements.</param>
//         public static void Abs(ref FixVector3 v, out FixVector3 result)
//         {
//             if (v.X < FixFloat64.C0)
//             {
//                 result.X = -v.X;
//             }
//             else
//             {
//                 result.X = v.X;
//             }
//             if (v.Y < FixFloat64.C0)
//             {
//                 result.Y = -v.Y;
//             }
//             else
//             {
//                 result.Y = v.Y;
//             }
//             if (v.Z < FixFloat64.C0)
//             {
//                 result.Z = -v.Z;
//             }
//             else
//             {
//                 result.Z = v.Z;
//             }
//         }

//         /// <summary>
//         /// Computes the absolute value of the input vector.
//         /// </summary>
//         /// <param name="v">Vector to take the absolute value of.</param>
//         /// <returns>Vector with nonnegative elements.</returns>
//         public static FixVector3 Abs(FixVector3 v)
//         {
//             Abs(ref v, out var result);
//             return result;
//         }

//         /// <summary>
//         /// Creates a vector from the lesser values in each vector.
//         /// </summary>
//         /// <param name="a">First input vector to compare values from.</param>
//         /// <param name="b">Second input vector to compare values from.</param>
//         /// <param name="min">Vector containing the lesser values of each vector.</param>
//         public static void Min(ref FixVector3 a, ref FixVector3 b, out FixVector3 min)
//         {
//             min.X = ((a.X < b.X) ? a.X : b.X);
//             min.Y = ((a.Y < b.Y) ? a.Y : b.Y);
//             min.Z = ((a.Z < b.Z) ? a.Z : b.Z);
//         }

//         /// <summary>
//         /// Creates a vector from the lesser values in each vector.
//         /// </summary>
//         /// <param name="a">First input vector to compare values from.</param>
//         /// <param name="b">Second input vector to compare values from.</param>
//         /// <returns>Vector containing the lesser values of each vector.</returns>
//         public static FixVector3 Min(FixVector3 a, FixVector3 b)
//         {
//             Min(ref a, ref b, out var min);
//             return min;
//         }

//         /// <summary>
//         /// Creates a vector from the greater values in each vector.
//         /// </summary>
//         /// <param name="a">First input vector to compare values from.</param>
//         /// <param name="b">Second input vector to compare values from.</param>
//         /// <param name="max">Vector containing the greater values of each vector.</param>
//         public static void Max(ref FixVector3 a, ref FixVector3 b, out FixVector3 max)
//         {
//             max.X = ((a.X > b.X) ? a.X : b.X);
//             max.Y = ((a.Y > b.Y) ? a.Y : b.Y);
//             max.Z = ((a.Z > b.Z) ? a.Z : b.Z);
//         }

//         /// <summary>
//         /// Creates a vector from the greater values in each vector.
//         /// </summary>
//         /// <param name="a">First input vector to compare values from.</param>
//         /// <param name="b">Second input vector to compare values from.</param>
//         /// <returns>Vector containing the greater values of each vector.</returns>
//         public static FixVector3 Max(FixVector3 a, FixVector3 b)
//         {
//             Max(ref a, ref b, out var max);
//             return max;
//         }

//         /// <summary>
//         /// Computes an interpolated state between two vectors.
//         /// </summary>
//         /// <param name="start">Starting location of the interpolation.</param>
//         /// <param name="end">Ending location of the interpolation.</param>
//         /// <param name="interpolationAmount">Amount of the end location to use.</param>
//         /// <returns>Interpolated intermediate state.</returns>
//         public static FixVector3 Lerp(FixVector3 start, FixVector3 end, FixFloat64 interpolationAmount)
//         {
//             Lerp(ref start, ref end, interpolationAmount, out var result);
//             return result;
//         }

//         /// <summary>
//         /// Computes an interpolated state between two vectors.
//         /// </summary>
//         /// <param name="start">Starting location of the interpolation.</param>
//         /// <param name="end">Ending location of the interpolation.</param>
//         /// <param name="interpolationAmount">Amount of the end location to use.</param>
//         /// <param name="result">Interpolated intermediate state.</param>
//         public static void Lerp(ref FixVector3 start, ref FixVector3 end, FixFloat64 interpolationAmount, out FixVector3 result)
//         {
//             FixFloat64 fix = FixFloat64.C1 - interpolationAmount;
//             result.X = start.X * fix + end.X * interpolationAmount;
//             result.Y = start.Y * fix + end.Y * interpolationAmount;
//             result.Z = start.Z * fix + end.Z * interpolationAmount;
//         }

//         /// <summary>
//         /// Computes an intermediate location using hermite interpolation.
//         /// </summary>
//         /// <param name="value1">First position.</param>
//         /// <param name="tangent1">Tangent associated with the first position.</param>
//         /// <param name="value2">Second position.</param>
//         /// <param name="tangent2">Tangent associated with the second position.</param>
//         /// <param name="interpolationAmount">Amount of the second point to use.</param>
//         /// <param name="result">Interpolated intermediate state.</param>
//         public static void Hermite(ref FixVector3 value1, ref FixVector3 tangent1, ref FixVector3 value2, ref FixVector3 tangent2, FixFloat64 interpolationAmount, out FixVector3 result)
//         {
//             FixFloat64 fix = interpolationAmount * interpolationAmount;
//             FixFloat64 fix2 = interpolationAmount * fix;
//             FixFloat64 fix3 = FixFloat64.C2 * fix2 - FixFloat64.C3 * fix + FixFloat64.C1;
//             FixFloat64 fix4 = fix2 - FixFloat64.C2 * fix + interpolationAmount;
//             FixFloat64 fix5 = -2 * fix2 + FixFloat64.C3 * fix;
//             FixFloat64 fix6 = fix2 - fix;
//             result.X = value1.X * fix3 + value2.X * fix5 + tangent1.X * fix4 + tangent2.X * fix6;
//             result.Y = value1.Y * fix3 + value2.Y * fix5 + tangent1.Y * fix4 + tangent2.Y * fix6;
//             result.Z = value1.Z * fix3 + value2.Z * fix5 + tangent1.Z * fix4 + tangent2.Z * fix6;
//         }

//         /// <summary>
//         /// Computes an intermediate location using hermite interpolation.
//         /// </summary>
//         /// <param name="value1">First position.</param>
//         /// <param name="tangent1">Tangent associated with the first position.</param>
//         /// <param name="value2">Second position.</param>
//         /// <param name="tangent2">Tangent associated with the second position.</param>
//         /// <param name="interpolationAmount">Amount of the second point to use.</param>
//         /// <returns>Interpolated intermediate state.</returns>
//         public static FixVector3 Hermite(FixVector3 value1, FixVector3 tangent1, FixVector3 value2, FixVector3 tangent2, FixFloat64 interpolationAmount)
//         {
//             Hermite(ref value1, ref tangent1, ref value2, ref tangent2, interpolationAmount, out var result);
//             return result;
//         }
//     }

// }