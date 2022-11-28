// /*
//  * @Author: delevin.ying 
//  * @Date: 2022-11-26 15:36:40 
//  * @Last Modified by: delevin.ying
//  * @Last Modified time: 2022-11-26 15:42:24
//  */
// using System;

// namespace FixMath
// {

//     /// <summary>
//     /// 2 row, 3 column matrix.
//     /// </summary>
//     public struct FixMatrix2x3
//     {
//         /// <summary>
//         /// Value at row 1, column 1 of the matrix.
//         /// </summary>
//         public FixFloat64 M11;

//         /// <summary>
//         /// Value at row 1, column 2 of the matrix.
//         /// </summary>
//         public FixFloat64 M12;

//         /// <summary>
//         /// Value at row 1, column 2 of the matrix.
//         /// </summary>
//         public FixFloat64 M13;

//         /// <summary>
//         /// Value at row 2, column 1 of the matrix.
//         /// </summary>
//         public FixFloat64 M21;

//         /// <summary>
//         /// Value at row 2, column 2 of the matrix.
//         /// </summary>
//         public FixFloat64 M22;

//         /// <summary>
//         /// Value at row 2, column 3 of the matrix.
//         /// </summary>
//         public FixFloat64 M23;

//         /// <summary>
//         /// Constructs a new 2 row, 2 column matrix.
//         /// </summary>
//         /// <param name="m11">Value at row 1, column 1 of the matrix.</param>
//         /// <param name="m12">Value at row 1, column 2 of the matrix.</param>
//         /// <param name="m13">Value at row 1, column 3 of the matrix.</param>
//         /// <param name="m21">Value at row 2, column 1 of the matrix.</param>
//         /// <param name="m22">Value at row 2, column 2 of the matrix.</param>
//         /// <param name="m23">Value at row 2, column 3 of the matrix.</param>
//         public FixMatrix2x3(FixFloat64 m11, FixFloat64 m12, FixFloat64 m13, FixFloat64 m21, FixFloat64 m22, FixFloat64 m23)
//         {
//             M11 = m11;
//             M12 = m12;
//             M13 = m13;
//             M21 = m21;
//             M22 = m22;
//             M23 = m23;
//         }

//         /// <summary>
//         /// Adds the two matrices together on a per-element basis.
//         /// </summary>
//         /// <param name="a">First matrix to add.</param>
//         /// <param name="b">Second matrix to add.</param>
//         /// <param name="result">Sum of the two matrices.</param>
//         public static void Add(ref FixMatrix2x3 a, ref FixMatrix2x3 b, out FixMatrix2x3 result)
//         {
//             FixFloat64 m = a.M11 + b.M11;
//             FixFloat64 m2 = a.M12 + b.M12;
//             FixFloat64 m3 = a.M13 + b.M13;
//             FixFloat64 m4 = a.M21 + b.M21;
//             FixFloat64 m5 = a.M22 + b.M22;
//             FixFloat64 m6 = a.M23 + b.M23;
//             result.M11 = m;
//             result.M12 = m2;
//             result.M13 = m3;
//             result.M21 = m4;
//             result.M22 = m5;
//             result.M23 = m6;
//         }

//         /// <summary>
//         /// Multiplies the two matrices.
//         /// </summary>
//         /// <param name="a">First matrix to multiply.</param>
//         /// <param name="b">Second matrix to multiply.</param>
//         /// <param name="result">Product of the multiplication.</param>
//         public static void Multiply(ref FixMatrix2x3 a, ref FixMatrix3x3 b, out FixMatrix2x3 result)
//         {
//             FixFloat64 m = a.M11 * b.M11 + a.M12 * b.M21 + a.M13 * b.M31;
//             FixFloat64 m2 = a.M11 * b.M12 + a.M12 * b.M22 + a.M13 * b.M32;
//             FixFloat64 m3 = a.M11 * b.M13 + a.M12 * b.M23 + a.M13 * b.M33;
//             FixFloat64 m4 = a.M21 * b.M11 + a.M22 * b.M21 + a.M23 * b.M31;
//             FixFloat64 m5 = a.M21 * b.M12 + a.M22 * b.M22 + a.M23 * b.M32;
//             FixFloat64 m6 = a.M21 * b.M13 + a.M22 * b.M23 + a.M23 * b.M33;
//             result.M11 = m;
//             result.M12 = m2;
//             result.M13 = m3;
//             result.M21 = m4;
//             result.M22 = m5;
//             result.M23 = m6;
//         }

//         /// <summary>
//         /// Multiplies the two matrices.
//         /// </summary>
//         /// <param name="a">First matrix to multiply.</param>
//         /// <param name="b">Second matrix to multiply.</param>
//         /// <param name="result">Product of the multiplication.</param>
//         public static void Multiply(ref FixMatrix2x3 a, ref FixMatrix b, out FixMatrix2x3 result)
//         {
//             FixFloat64 m = a.M11 * b.M11 + a.M12 * b.M21 + a.M13 * b.M31;
//             FixFloat64 m2 = a.M11 * b.M12 + a.M12 * b.M22 + a.M13 * b.M32;
//             FixFloat64 m3 = a.M11 * b.M13 + a.M12 * b.M23 + a.M13 * b.M33;
//             FixFloat64 m4 = a.M21 * b.M11 + a.M22 * b.M21 + a.M23 * b.M31;
//             FixFloat64 m5 = a.M21 * b.M12 + a.M22 * b.M22 + a.M23 * b.M32;
//             FixFloat64 m6 = a.M21 * b.M13 + a.M22 * b.M23 + a.M23 * b.M33;
//             result.M11 = m;
//             result.M12 = m2;
//             result.M13 = m3;
//             result.M21 = m4;
//             result.M22 = m5;
//             result.M23 = m6;
//         }

//         /// <summary>
//         /// Negates every element in the matrix.
//         /// </summary>
//         /// <param name="matrix">Matrix to negate.</param>
//         /// <param name="result">Negated matrix.</param>
//         public static void Negate(ref FixMatrix2x3 matrix, out FixMatrix2x3 result)
//         {
//             FixFloat64 m = -matrix.M11;
//             FixFloat64 m2 = -matrix.M12;
//             FixFloat64 m3 = -matrix.M13;
//             FixFloat64 m4 = -matrix.M21;
//             FixFloat64 m5 = -matrix.M22;
//             FixFloat64 m6 = -matrix.M23;
//             result.M11 = m;
//             result.M12 = m2;
//             result.M13 = m3;
//             result.M21 = m4;
//             result.M22 = m5;
//             result.M23 = m6;
//         }

//         /// <summary>
//         /// Subtracts the two matrices from each other on a per-element basis.
//         /// </summary>
//         /// <param name="a">First matrix to subtract.</param>
//         /// <param name="b">Second matrix to subtract.</param>
//         /// <param name="result">Difference of the two matrices.</param>
//         public static void Subtract(ref FixMatrix2x3 a, ref FixMatrix2x3 b, out FixMatrix2x3 result)
//         {
//             FixFloat64 m = a.M11 - b.M11;
//             FixFloat64 m2 = a.M12 - b.M12;
//             FixFloat64 m3 = a.M13 - b.M13;
//             FixFloat64 m4 = a.M21 - b.M21;
//             FixFloat64 m5 = a.M22 - b.M22;
//             FixFloat64 m6 = a.M23 - b.M23;
//             result.M11 = m;
//             result.M12 = m2;
//             result.M13 = m3;
//             result.M21 = m4;
//             result.M22 = m5;
//             result.M23 = m6;
//         }

//         /// <summary>
//         /// Transforms the vector by the matrix.
//         /// </summary>
//         /// <param name="v">Vector2 to transform.  Considered to be a row vector for purposes of multiplication.</param>
//         /// <param name="matrix">Matrix to use as the transformation.</param>
//         /// <param name="result">Row vector product of the transformation.</param>
//         public static void Transform(ref FixVector2 v, ref FixMatrix2x3 matrix, out FixVector3 result)
//         {
//             result.X = v.X * matrix.M11 + v.Y * matrix.M21;
//             result.Y = v.X * matrix.M12 + v.Y * matrix.M22;
//             result.Z = v.X * matrix.M13 + v.Y * matrix.M23;
//         }

//         /// <summary>
//         /// Transforms the vector by the matrix.
//         /// </summary>
//         /// <param name="v">Vector2 to transform.  Considered to be a column vector for purposes of multiplication.</param>
//         /// <param name="matrix">Matrix to use as the transformation.</param>
//         /// <param name="result">Column vector product of the transformation.</param>
//         public static void Transform(ref FixVector3 v, ref FixMatrix2x3 matrix, out FixVector2 result)
//         {
//             result.X = matrix.M11 * v.X + matrix.M12 * v.Y + matrix.M13 * v.Z;
//             result.Y = matrix.M21 * v.X + matrix.M22 * v.Y + matrix.M23 * v.Z;
//         }

//         /// <summary>
//         /// Computes the transposed matrix of a matrix.
//         /// </summary>
//         /// <param name="matrix">Matrix to transpose.</param>
//         /// <param name="result">Transposed matrix.</param>
//         public static void Transpose(ref FixMatrix2x3 matrix, out FixMatrix3x2 result)
//         {
//             result.M11 = matrix.M11;
//             result.M12 = matrix.M21;
//             result.M21 = matrix.M12;
//             result.M22 = matrix.M22;
//             result.M31 = matrix.M13;
//             result.M32 = matrix.M23;
//         }

//         /// <summary>
//         /// Creates a string representation of the matrix.
//         /// </summary>
//         /// <returns>A string representation of the matrix.</returns>
//         public override string ToString()
//         {
//             return string.Concat("{", M11, ", ", M12, ", ", M13, "} {", M21, ", ", M22, ", ", M23, "}");
//         }
//     }
// }