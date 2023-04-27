/*
 * @Author: delevin.ying 
 * @Date: 2022-11-26 15:37:08 
 * @Last Modified by: delevin.ying
 * @Last Modified time: 2022-11-26 15:43:57
 */
using System;

namespace FixMath
{

    /// <summary>
    /// 3 row, 2 column matrix.
    /// </summary>
    public struct FixMatrix3x2
    {
        /// <summary>
        /// Value at row 1, column 1 of the matrix.
        /// </summary>
        public FixFloat64 M11;

        /// <summary>
        /// Value at row 1, column 2 of the matrix.
        /// </summary>
        public FixFloat64 M12;

        /// <summary>
        /// Value at row 2, column 1 of the matrix.
        /// </summary>
        public FixFloat64 M21;

        /// <summary>
        /// Value at row 2, column 2 of the matrix.
        /// </summary>
        public FixFloat64 M22;

        /// <summary>
        /// Value at row 3, column 1 of the matrix.
        /// </summary>
        public FixFloat64 M31;

        /// <summary>
        /// Value at row 3, column 2 of the matrix.
        /// </summary>
        public FixFloat64 M32;

        /// <summary>
        /// Constructs a new 3 row, 2 column matrix.
        /// </summary>
        /// <param name="m11">Value at row 1, column 1 of the matrix.</param>
        /// <param name="m12">Value at row 1, column 2 of the matrix.</param>
        /// <param name="m21">Value at row 2, column 1 of the matrix.</param>
        /// <param name="m22">Value at row 2, column 2 of the matrix.</param>
        /// <param name="m31">Value at row 2, column 1 of the matrix.</param>
        /// <param name="m32">Value at row 2, column 2 of the matrix.</param>
        public FixMatrix3x2(FixFloat64 m11, FixFloat64 m12, FixFloat64 m21, FixFloat64 m22, FixFloat64 m31, FixFloat64 m32)
        {
            M11 = m11;
            M12 = m12;
            M21 = m21;
            M22 = m22;
            M31 = m31;
            M32 = m32;
        }

        /// <summary>
        /// Adds the two matrices together on a per-element basis.
        /// </summary>
        /// <param name="a">First matrix to add.</param>
        /// <param name="b">Second matrix to add.</param>
        /// <param name="result">Sum of the two matrices.</param>
        public static void Add(ref FixMatrix3x2 a, ref FixMatrix3x2 b, out FixMatrix3x2 result)
        {
            FixFloat64 m = a.M11 + b.M11;
            FixFloat64 m2 = a.M12 + b.M12;
            FixFloat64 m3 = a.M21 + b.M21;
            FixFloat64 m4 = a.M22 + b.M22;
            FixFloat64 m5 = a.M31 + b.M31;
            FixFloat64 m6 = a.M32 + b.M32;
            result.M11 = m;
            result.M12 = m2;
            result.M21 = m3;
            result.M22 = m4;
            result.M31 = m5;
            result.M32 = m6;
        }

        /// <summary>
        /// Multiplies the two matrices.
        /// </summary>
        /// <param name="a">First matrix to multiply.</param>
        /// <param name="b">Second matrix to multiply.</param>
        /// <param name="result">Product of the multiplication.</param>
        public static void Multiply(ref FixMatrix3x3 a, ref FixMatrix3x2 b, out FixMatrix3x2 result)
        {
            FixFloat64 m = a.M11 * b.M11 + a.M12 * b.M21 + a.M13 * b.M31;
            FixFloat64 m2 = a.M11 * b.M12 + a.M12 * b.M22 + a.M13 * b.M32;
            FixFloat64 m3 = a.M21 * b.M11 + a.M22 * b.M21 + a.M23 * b.M31;
            FixFloat64 m4 = a.M21 * b.M12 + a.M22 * b.M22 + a.M23 * b.M32;
            FixFloat64 m5 = a.M31 * b.M11 + a.M32 * b.M21 + a.M33 * b.M31;
            FixFloat64 m6 = a.M31 * b.M12 + a.M32 * b.M22 + a.M33 * b.M32;
            result.M11 = m;
            result.M12 = m2;
            result.M21 = m3;
            result.M22 = m4;
            result.M31 = m5;
            result.M32 = m6;
        }

        /// <summary>
        /// Multiplies the two matrices.
        /// </summary>
        /// <param name="a">First matrix to multiply.</param>
        /// <param name="b">Second matrix to multiply.</param>
        /// <param name="result">Product of the multiplication.</param>
        public static void Multiply(ref FixMatrix a, ref FixMatrix3x2 b, out FixMatrix3x2 result)
        {
            FixFloat64 m = a.M11 * b.M11 + a.M12 * b.M21 + a.M13 * b.M31;
            FixFloat64 m2 = a.M11 * b.M12 + a.M12 * b.M22 + a.M13 * b.M32;
            FixFloat64 m3 = a.M21 * b.M11 + a.M22 * b.M21 + a.M23 * b.M31;
            FixFloat64 m4 = a.M21 * b.M12 + a.M22 * b.M22 + a.M23 * b.M32;
            FixFloat64 m5 = a.M31 * b.M11 + a.M32 * b.M21 + a.M33 * b.M31;
            FixFloat64 m6 = a.M31 * b.M12 + a.M32 * b.M22 + a.M33 * b.M32;
            result.M11 = m;
            result.M12 = m2;
            result.M21 = m3;
            result.M22 = m4;
            result.M31 = m5;
            result.M32 = m6;
        }

        /// <summary>
        /// Negates every element in the matrix.
        /// </summary>
        /// <param name="matrix">Matrix to negate.</param>
        /// <param name="result">Negated matrix.</param>
        public static void Negate(ref FixMatrix3x2 matrix, out FixMatrix3x2 result)
        {
            FixFloat64 m = -matrix.M11;
            FixFloat64 m2 = -matrix.M12;
            FixFloat64 m3 = -matrix.M21;
            FixFloat64 m4 = -matrix.M22;
            FixFloat64 m5 = -matrix.M31;
            FixFloat64 m6 = -matrix.M32;
            result.M11 = m;
            result.M12 = m2;
            result.M21 = m3;
            result.M22 = m4;
            result.M31 = m5;
            result.M32 = m6;
        }

        /// <summary>
        /// Subtracts the two matrices from each other on a per-element basis.
        /// </summary>
        /// <param name="a">First matrix to subtract.</param>
        /// <param name="b">Second matrix to subtract.</param>
        /// <param name="result">Difference of the two matrices.</param>
        public static void Subtract(ref FixMatrix3x2 a, ref FixMatrix3x2 b, out FixMatrix3x2 result)
        {
            FixFloat64 m = a.M11 - b.M11;
            FixFloat64 m2 = a.M12 - b.M12;
            FixFloat64 m3 = a.M21 - b.M21;
            FixFloat64 m4 = a.M22 - b.M22;
            FixFloat64 m5 = a.M31 - b.M31;
            FixFloat64 m6 = a.M32 - b.M32;
            result.M11 = m;
            result.M12 = m2;
            result.M21 = m3;
            result.M22 = m4;
            result.M31 = m5;
            result.M32 = m6;
        }

        /// <summary>
        /// Transforms the vector by the matrix.
        /// </summary>
        /// <param name="v">Vector2 to transform.  Considered to be a column vector for purposes of multiplication.</param>
        /// <param name="matrix">Matrix to use as the transformation.</param>
        /// <param name="result">Column vector product of the transformation.</param>
        public static void Transform(ref FixVector2 v, ref FixMatrix3x2 matrix, out FixVector3 result)
        {
            result.X = matrix.M11 * v.X + matrix.M12 * v.Y;
            result.Y = matrix.M21 * v.X + matrix.M22 * v.Y;
            result.Z = matrix.M31 * v.X + matrix.M32 * v.Y;
        }

        /// <summary>
        /// Transforms the vector by the matrix.
        /// </summary>
        /// <param name="v">Vector2 to transform.  Considered to be a row vector for purposes of multiplication.</param>
        /// <param name="matrix">Matrix to use as the transformation.</param>
        /// <param name="result">Row vector product of the transformation.</param>
        public static void Transform(ref FixVector3 v, ref FixMatrix3x2 matrix, out FixVector2 result)
        {
            result.X = v.X * matrix.M11 + v.Y * matrix.M21 + v.Z * matrix.M31;
            result.Y = v.X * matrix.M12 + v.Y * matrix.M22 + v.Z * matrix.M32;
        }

        /// <summary>
        /// Computes the transposed matrix of a matrix.
        /// </summary>
        /// <param name="matrix">Matrix to transpose.</param>
        /// <param name="result">Transposed matrix.</param>
        public static void Transpose(ref FixMatrix3x2 matrix, out FixMatrix2x3 result)
        {
            result.M11 = matrix.M11;
            result.M12 = matrix.M21;
            result.M13 = matrix.M31;
            result.M21 = matrix.M12;
            result.M22 = matrix.M22;
            result.M23 = matrix.M32;
        }

        /// <summary>
        /// Creates a string representation of the matrix.
        /// </summary>
        /// <returns>A string representation of the matrix.</returns>
        public override string ToString()
        {
            return string.Concat("{", M11, ", ", M12, "} {", M21, ", ", M22, "} {", M31, ", ", M32, "}");
        }
    }
}