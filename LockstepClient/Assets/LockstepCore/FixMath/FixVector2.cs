// /*
//  * @Author: delevin.ying 
//  * @Date: 2022-11-26 15:20:44 
//  * @Last Modified by: delevin.ying
//  * @Last Modified time: 2022-11-26 15:23:01
//  */


// using System;


// namespace FixMath
// {


//     [Serializable]
//     public struct FixVector2 : IEquatable<FixVector2>
//     {
//         /// <summary>
//         /// X component of the vector.
//         /// </summary>
//         public FixFloat64 X;

//         /// <summary>
//         /// Y component of the vector.
//         /// </summary>
//         public FixFloat64 Y;

//         public FixFloat64 x => X;

//         public FixFloat64 y => Y;

//         /// <summary>
//         /// Gets the zero vector.
//         /// </summary>
//         public static FixVector2 Zero => default(FixVector2);

//         /// <summary>
//         /// Gets a vector pointing along the X axis.
//         /// </summary>
//         public static FixVector2 UnitX
//         {
//             get
//             {
//                 FixVector2 result = default(FixVector2);
//                 result.X = FixFloat64.C1;
//                 return result;
//             }
//         }

//         /// <summary>
//         /// Gets a vector pointing along the Y axis.
//         /// </summary>
//         public static FixVector2 UnitY
//         {
//             get
//             {
//                 FixVector2 result = default(FixVector2);
//                 result.Y = FixFloat64.C1;
//                 return result;
//             }
//         }

//         /// <summary>
//         /// Constructs a new two dimensional vector.
//         /// </summary>
//         /// <param name="x">X component of the vector.</param>
//         /// <param name="y">Y component of the vector.</param>
//         public FixVector2(FixFloat64 x, FixFloat64 y)
//         {
//             X = x;
//             Y = y;
//         }

//         /// <summary>
//         /// Computes the squared length of the vector.
//         /// </summary>
//         /// <returns>Squared length of the vector.</returns>
//         public FixFloat64 LengthSquared()
//         {
//             return X * X + Y * Y;
//         }

//         /// <summary>
//         /// Computes the length of the vector.
//         /// </summary>
//         /// <returns>Length of the vector.</returns>
//         public FixFloat64 Length()
//         {
//             return FixFloat64.Sqrt(X * X + Y * Y);
//         }

//         /// <summary>
//         /// Gets a string representation of the vector.
//         /// </summary>
//         /// <returns>String representing the vector.</returns>
//         public override string ToString()
//         {
//             return string.Concat("{", X, ", ", Y, "}");
//         }

//         /// <summary>
//         /// Adds two vectors together.
//         /// </summary>
//         /// <param name="a">First vector to add.</param>
//         /// <param name="b">Second vector to add.</param>
//         /// <param name="sum">Sum of the two vectors.</param>
//         public static void Add(ref FixVector2 a, ref FixVector2 b, out FixVector2 sum)
//         {
//             sum.X = a.X + b.X;
//             sum.Y = a.Y + b.Y;
//         }

//         /// <summary>
//         /// Subtracts two vectors.
//         /// </summary>
//         /// <param name="a">Vector to subtract from.</param>
//         /// <param name="b">Vector to subtract from the first vector.</param>
//         /// <param name="difference">Result of the subtraction.</param>
//         public static void Subtract(ref FixVector2 a, ref FixVector2 b, out FixVector2 difference)
//         {
//             difference.X = a.X - b.X;
//             difference.Y = a.Y - b.Y;
//         }

//         /// <summary>
//         /// Scales a vector.
//         /// </summary>
//         /// <param name="v">Vector to scale.</param>
//         /// <param name="scale">Amount to scale.</param>
//         /// <param name="result">Scaled vector.</param>
//         public static void Multiply(ref FixVector2 v, FixFloat64 scale, out FixVector2 result)
//         {
//             result.X = v.X * scale;
//             result.Y = v.Y * scale;
//         }

//         /// <summary>
//         /// Multiplies two vectors on a per-component basis.
//         /// </summary>
//         /// <param name="a">First vector to multiply.</param>
//         /// <param name="b">Second vector to multiply.</param>
//         /// <param name="result">Result of the componentwise multiplication.</param>
//         public static void Multiply(ref FixVector2 a, ref FixVector2 b, out FixVector2 result)
//         {
//             result.X = a.X * b.X;
//             result.Y = a.Y * b.Y;
//         }

//         /// <summary>
//         /// Divides a vector's components by some amount.
//         /// </summary>
//         /// <param name="v">Vector to divide.</param>
//         /// <param name="divisor">Value to divide the vector's components.</param>
//         /// <param name="result">Result of the division.</param>
//         public static void Divide(ref FixVector2 v, FixFloat64 divisor, out FixVector2 result)
//         {
//             FixFloat64 fix = FixFloat64.C1 / divisor;
//             result.X = v.X * fix;
//             result.Y = v.Y * fix;
//         }

//         /// <summary>
//         /// Computes the dot product of the two vectors.
//         /// </summary>
//         /// <param name="a">First vector of the dot product.</param>
//         /// <param name="b">Second vector of the dot product.</param>
//         /// <param name="dot">Dot product of the two vectors.</param>
//         public static void Dot(ref FixVector2 a, ref FixVector2 b, out FixFloat64 dot)
//         {
//             dot = a.X * b.X + a.Y * b.Y;
//         }

//         /// <summary>
//         /// Computes the dot product of the two vectors.
//         /// </summary>
//         /// <param name="a">First vector of the dot product.</param>
//         /// <param name="b">Second vector of the dot product.</param>
//         /// <returns>Dot product of the two vectors.</returns>
//         public static FixFloat64 Dot(FixVector2 a, FixVector2 b)
//         {
//             return a.X * b.X + a.Y * b.Y;
//         }

//         /// <summary>
//         /// Normalizes the vector.
//         /// </summary>
//         /// <param name="v">Vector to normalize.</param>
//         /// <returns>Normalized copy of the vector.</returns>
//         public static FixVector2 Normalize(FixVector2 v)
//         {
//             Normalize(ref v, out var result);
//             return result;
//         }

//         /// <summary>
//         /// Normalizes the vector.
//         /// </summary>
//         /// <param name="v">Vector to normalize.</param>
//         /// <param name="result">Normalized vector.</param>
//         public static void Normalize(ref FixVector2 v, out FixVector2 result)
//         {
//             FixFloat64 fix = FixFloat64.C1 / FixFloat64.Sqrt(v.X * v.X + v.Y * v.Y);
//             result.X = v.X * fix;
//             result.Y = v.Y * fix;
//         }

//         /// <summary>
//         /// Negates the vector.
//         /// </summary>
//         /// <param name="v">Vector to negate.</param>
//         /// <param name="negated">Negated version of the vector.</param>
//         public static void Negate(ref FixVector2 v, out FixVector2 negated)
//         {
//             negated.X = -v.X;
//             negated.Y = -v.Y;
//         }

//         /// <summary>
//         /// Computes the absolute value of the input vector.
//         /// </summary>
//         /// <param name="v">Vector to take the absolute value of.</param>
//         /// <param name="result">Vector with nonnegative elements.</param>
//         public static void Abs(ref FixVector2 v, out FixVector2 result)
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
//         }

//         /// <summary>
//         /// Computes the absolute value of the input vector.
//         /// </summary>
//         /// <param name="v">Vector to take the absolute value of.</param>
//         /// <returns>Vector with nonnegative elements.</returns>
//         public static FixVector2 Abs(FixVector2 v)
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
//         public static void Min(ref FixVector2 a, ref FixVector2 b, out FixVector2 min)
//         {
//             min.X = ((a.X < b.X) ? a.X : b.X);
//             min.Y = ((a.Y < b.Y) ? a.Y : b.Y);
//         }

//         /// <summary>
//         /// Creates a vector from the lesser values in each vector.
//         /// </summary>
//         /// <param name="a">First input vector to compare values from.</param>
//         /// <param name="b">Second input vector to compare values from.</param>
//         /// <returns>Vector containing the lesser values of each vector.</returns>
//         public static FixVector2 Min(FixVector2 a, FixVector2 b)
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
//         public static void Max(ref FixVector2 a, ref FixVector2 b, out FixVector2 max)
//         {
//             max.X = ((a.X > b.X) ? a.X : b.X);
//             max.Y = ((a.Y > b.Y) ? a.Y : b.Y);
//         }

//         /// <summary>
//         /// Creates a vector from the greater values in each vector.
//         /// </summary>
//         /// <param name="a">First input vector to compare values from.</param>
//         /// <param name="b">Second input vector to compare values from.</param>
//         /// <returns>Vector containing the greater values of each vector.</returns>
//         public static FixVector2 Max(FixVector2 a, FixVector2 b)
//         {
//             Max(ref a, ref b, out var max);
//             return max;
//         }

//         /// <summary>
//         /// Normalizes the vector.
//         /// </summary>
//         public void Normalize()
//         {
//             FixFloat64 fix = FixFloat64.C1 / FixFloat64.Sqrt(X * X + Y * Y);
//             X *= fix;
//             Y *= fix;
//         }

//         /// <summary>
//         /// Scales a vector.
//         /// </summary>
//         /// <param name="v">Vector to scale.</param>
//         /// <param name="f">Amount to scale.</param>
//         /// <returns>Scaled vector.</returns>
//         public static FixVector2 operator *(FixVector2 v, FixFloat64 f)
//         {
//             FixVector2 result = default(FixVector2);
//             result.X = v.X * f;
//             result.Y = v.Y * f;
//             return result;
//         }

//         /// <summary>
//         /// Scales a vector.
//         /// </summary>
//         /// <param name="v">Vector to scale.</param>
//         /// <param name="f">Amount to scale.</param>
//         /// <returns>Scaled vector.</returns>
//         public static FixVector2 operator *(FixFloat64 f, FixVector2 v)
//         {
//             FixVector2 result = default(FixVector2);
//             result.X = v.X * f;
//             result.Y = v.Y * f;
//             return result;
//         }

//         /// <summary>
//         /// Multiplies two vectors on a per-component basis.
//         /// </summary>
//         /// <param name="a">First vector to multiply.</param>
//         /// <param name="b">Second vector to multiply.</param>
//         /// <returns>Result of the componentwise multiplication.</returns>
//         public static FixVector2 operator *(FixVector2 a, FixVector2 b)
//         {
//             Multiply(ref a, ref b, out var result);
//             return result;
//         }

//         /// <summary>
//         /// Divides a vector.
//         /// </summary>
//         /// <param name="v">Vector to divide.</param>
//         /// <param name="f">Amount to divide.</param>
//         /// <returns>Divided vector.</returns>
//         public static FixVector2 operator /(FixVector2 v, FixFloat64 f)
//         {
//             f = FixFloat64.C1 / f;
//             FixVector2 result = default(FixVector2);
//             result.X = v.X * f;
//             result.Y = v.Y * f;
//             return result;
//         }

//         /// <summary>
//         /// Subtracts two vectors.
//         /// </summary>
//         /// <param name="a">Vector to be subtracted from.</param>
//         /// <param name="b">Vector to subtract from the first vector.</param>
//         /// <returns>Resulting difference.</returns>
//         public static FixVector2 operator -(FixVector2 a, FixVector2 b)
//         {
//             FixVector2 result = default(FixVector2);
//             result.X = a.X - b.X;
//             result.Y = a.Y - b.Y;
//             return result;
//         }

//         /// <summary>
//         /// Adds two vectors.
//         /// </summary>
//         /// <param name="a">First vector to add.</param>
//         /// <param name="b">Second vector to add.</param>
//         /// <returns>Sum of the addition.</returns>
//         public static FixVector2 operator +(FixVector2 a, FixVector2 b)
//         {
//             FixVector2 result = default(FixVector2);
//             result.X = a.X + b.X;
//             result.Y = a.Y + b.Y;
//             return result;
//         }

//         /// <summary>
//         /// Negates the vector.
//         /// </summary>
//         /// <param name="v">Vector to negate.</param>
//         /// <returns>Negated vector.</returns>
//         public static FixVector2 operator -(FixVector2 v)
//         {
//             v.X = -v.X;
//             v.Y = -v.Y;
//             return v;
//         }

//         /// <summary>
//         /// Tests two vectors for componentwise equivalence.
//         /// </summary>
//         /// <param name="a">First vector to test for equivalence.</param>
//         /// <param name="b">Second vector to test for equivalence.</param>
//         /// <returns>Whether the vectors were equivalent.</returns>
//         public static bool operator ==(FixVector2 a, FixVector2 b)
//         {
//             return a.X == b.X && a.Y == b.Y;
//         }

//         /// <summary>
//         /// Tests two vectors for componentwise inequivalence.
//         /// </summary>
//         /// <param name="a">First vector to test for inequivalence.</param>
//         /// <param name="b">Second vector to test for inequivalence.</param>
//         /// <returns>Whether the vectors were inequivalent.</returns>
//         public static bool operator !=(FixVector2 a, FixVector2 b)
//         {
//             return a.X != b.X || a.Y != b.Y;
//         }

//         /// <summary>
//         /// Indicates whether the current object is equal to another object of the same type.
//         /// </summary>
//         /// <returns>
//         /// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
//         /// </returns>
//         /// <param name="other">An object to compare with this object.</param>
//         public bool Equals(FixVector2 other)
//         {
//             return X == other.X && Y == other.Y;
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
//             if (obj is FixVector2)
//             {
//                 return Equals((FixVector2)obj);
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
//             return X.GetHashCode() + Y.GetHashCode();
//         }
//     }

// }