/*
 * @Author: delevin.ying 
 * @Date: 2022-11-26 15:36:13 
 * @Last Modified by: delevin.ying
 * @Last Modified time: 2022-11-26 15:41:15
 */

using System;

namespace FixMath
{

    /// <summary>
    /// 2 row, 2 column matrix.
    /// </summary>
    public struct FixMatrix2x2
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
        /// Gets the 2x2 identity matrix.
        /// </summary>
        public static FixMatrix2x2 Identity => new FixMatrix2x2(FixFloat64Util.C1, FixFloat64Util.C0, FixFloat64Util.C0, FixFloat64Util.C1);

        /// <summary>
        /// Constructs a new 2 row, 2 column matrix.
        /// </summary>
        /// <param name="m11">Value at row 1, column 1 of the matrix.</param>
        /// <param name="m12">Value at row 1, column 2 of the matrix.</param>
        /// <param name="m21">Value at row 2, column 1 of the matrix.</param>
        /// <param name="m22">Value at row 2, column 2 of the matrix.</param>
        public FixMatrix2x2(FixFloat64 m11, FixFloat64 m12, FixFloat64 m21, FixFloat64 m22)
        {
            M11 = m11;
            M12 = m12;
            M21 = m21;
            M22 = m22;
        }

        /// <summary>
        /// Adds the two matrices together on a per-element basis.
        /// </summary>
        /// <param name="a">First matrix to add.</param>
        /// <param name="b">Second matrix to add.</param>
        /// <param name="result">Sum of the two matrices.</param>
        public static void Add(ref FixMatrix2x2 a, ref FixMatrix2x2 b, out FixMatrix2x2 result)
        {
            FixFloat64 m = a.M11 + b.M11;
            FixFloat64 m2 = a.M12 + b.M12;
            FixFloat64 m3 = a.M21 + b.M21;
            FixFloat64 m4 = a.M22 + b.M22;
            result.M11 = m;
            result.M12 = m2;
            result.M21 = m3;
            result.M22 = m4;
        }

        /// <summary>
        /// Adds the two matrices together on a per-element basis.
        /// </summary>
        /// <param name="a">First matrix to add.</param>
        /// <param name="b">Second matrix to add.</param>
        /// <param name="result">Sum of the two matrices.</param>
        public static void Add(ref FixMatrix a, ref FixMatrix2x2 b, out FixMatrix2x2 result)
        {
            FixFloat64 m = a.M11 + b.M11;
            FixFloat64 m2 = a.M12 + b.M12;
            FixFloat64 m3 = a.M21 + b.M21;
            FixFloat64 m4 = a.M22 + b.M22;
            result.M11 = m;
            result.M12 = m2;
            result.M21 = m3;
            result.M22 = m4;
        }

        /// <summary>
        /// Adds the two matrices together on a per-element basis.
        /// </summary>
        /// <param name="a">First matrix to add.</param>
        /// <param name="b">Second matrix to add.</param>
        /// <param name="result">Sum of the two matrices.</param>
        public static void Add(ref FixMatrix2x2 a, ref FixMatrix b, out FixMatrix2x2 result)
        {
            FixFloat64 m = a.M11 + b.M11;
            FixFloat64 m2 = a.M12 + b.M12;
            FixFloat64 m3 = a.M21 + b.M21;
            FixFloat64 m4 = a.M22 + b.M22;
            result.M11 = m;
            result.M12 = m2;
            result.M21 = m3;
            result.M22 = m4;
        }

        /// <summary>
        /// Adds the two matrices together on a per-element basis.
        /// </summary>
        /// <param name="a">First matrix to add.</param>
        /// <param name="b">Second matrix to add.</param>
        /// <param name="result">Sum of the two matrices.</param>
        public static void Add(ref FixMatrix a, ref FixMatrix b, out FixMatrix2x2 result)
        {
            FixFloat64 m = a.M11 + b.M11;
            FixFloat64 m2 = a.M12 + b.M12;
            FixFloat64 m3 = a.M21 + b.M21;
            FixFloat64 m4 = a.M22 + b.M22;
            result.M11 = m;
            result.M12 = m2;
            result.M21 = m3;
            result.M22 = m4;
        }

        /// <summary>
        /// Constructs a uniform scaling matrix.
        /// </summary>
        /// <param name="scale">Value to use in the diagonal.</param>
        /// <param name="matrix">Scaling matrix.</param>
        public static void CreateScale(FixFloat64 scale, out FixMatrix2x2 matrix)
        {
            matrix.M11 = scale;
            matrix.M22 = scale;
            matrix.M12 = FixFloat64Util.C0;
            matrix.M21 = FixFloat64Util.C0;
        }

        /// <summary>
        /// Inverts the given matix.
        /// </summary>
        /// <param name="matrix">FixMatrix to be inverted.</param>
        /// <param name="result">Inverted matrix.</param>
        public static void Invert(ref FixMatrix2x2 matrix, out FixMatrix2x2 result)
        {
            FixFloat64 fix = FixFloat64Util.C1 / (matrix.M11 * matrix.M22 - matrix.M12 * matrix.M21);
            FixFloat64 m = matrix.M22 * fix;
            FixFloat64 m2 = -matrix.M12 * fix;
            FixFloat64 m3 = -matrix.M21 * fix;
            FixFloat64 m4 = matrix.M11 * fix;
            result.M11 = m;
            result.M12 = m2;
            result.M21 = m3;
            result.M22 = m4;
        }

        /// <summary>
        /// Multiplies the two matrices.
        /// </summary>
        /// <param name="a">First matrix to multiply.</param>
        /// <param name="b">Second matrix to multiply.</param>
        /// <param name="result">Product of the multiplication.</param>
        public static void Multiply(ref FixMatrix2x2 a, ref FixMatrix2x2 b, out FixMatrix2x2 result)
        {
            FixFloat64 m = a.M11 * b.M11 + a.M12 * b.M21;
            FixFloat64 m2 = a.M11 * b.M12 + a.M12 * b.M22;
            FixFloat64 m3 = a.M21 * b.M11 + a.M22 * b.M21;
            FixFloat64 m4 = a.M21 * b.M12 + a.M22 * b.M22;
            result.M11 = m;
            result.M12 = m2;
            result.M21 = m3;
            result.M22 = m4;
        }

        /// <summary>
        /// Multiplies the two matrices.
        /// </summary>
        /// <param name="a">First matrix to multiply.</param>
        /// <param name="b">Second matrix to multiply.</param>
        /// <param name="result">Product of the multiplication.</param>
        public static void Multiply(ref FixMatrix2x2 a, ref FixMatrix b, out FixMatrix2x2 result)
        {
            FixFloat64 m = a.M11 * b.M11 + a.M12 * b.M21;
            FixFloat64 m2 = a.M11 * b.M12 + a.M12 * b.M22;
            FixFloat64 m3 = a.M21 * b.M11 + a.M22 * b.M21;
            FixFloat64 m4 = a.M21 * b.M12 + a.M22 * b.M22;
            result.M11 = m;
            result.M12 = m2;
            result.M21 = m3;
            result.M22 = m4;
        }

        /// <summary>
        /// Multiplies the two matrices.
        /// </summary>
        /// <param name="a">First matrix to multiply.</param>
        /// <param name="b">Second matrix to multiply.</param>
        /// <param name="result">Product of the multiplication.</param>
        public static void Multiply(ref FixMatrix a, ref FixMatrix2x2 b, out FixMatrix2x2 result)
        {
            FixFloat64 m = a.M11 * b.M11 + a.M12 * b.M21;
            FixFloat64 m2 = a.M11 * b.M12 + a.M12 * b.M22;
            FixFloat64 m3 = a.M21 * b.M11 + a.M22 * b.M21;
            FixFloat64 m4 = a.M21 * b.M12 + a.M22 * b.M22;
            result.M11 = m;
            result.M12 = m2;
            result.M21 = m3;
            result.M22 = m4;
        }

        /// <summary>
        /// Multiplies the two matrices.
        /// </summary>
        /// <param name="a">First matrix to multiply.</param>
        /// <param name="b">Second matrix to multiply.</param>
        /// <param name="result">Product of the multiplication.</param>
        public static void Multiply(ref FixMatrix2x3 a, ref FixMatrix3x2 b, out FixMatrix2x2 result)
        {
            result.M11 = a.M11 * b.M11 + a.M12 * b.M21 + a.M13 * b.M31;
            result.M12 = a.M11 * b.M12 + a.M12 * b.M22 + a.M13 * b.M32;
            result.M21 = a.M21 * b.M11 + a.M22 * b.M21 + a.M23 * b.M31;
            result.M22 = a.M21 * b.M12 + a.M22 * b.M22 + a.M23 * b.M32;
        }

        /// <summary>
        /// Negates every element in the matrix.
        /// </summary>
        /// <param name="matrix">FixMatrix to negate.</param>
        /// <param name="result">Negated matrix.</param>
        public static void Negate(ref FixMatrix2x2 matrix, out FixMatrix2x2 result)
        {
            FixFloat64 m = -matrix.M11;
            FixFloat64 m2 = -matrix.M12;
            FixFloat64 m3 = -matrix.M21;
            FixFloat64 m4 = -matrix.M22;
            result.M11 = m;
            result.M12 = m2;
            result.M21 = m3;
            result.M22 = m4;
        }

        /// <summary>
        /// Subtracts the two matrices from each other on a per-element basis.
        /// </summary>
        /// <param name="a">First matrix to subtract.</param>
        /// <param name="b">Second matrix to subtract.</param>
        /// <param name="result">Difference of the two matrices.</param>
        public static void Subtract(ref FixMatrix2x2 a, ref FixMatrix2x2 b, out FixMatrix2x2 result)
        {
            FixFloat64 m = a.M11 - b.M11;
            FixFloat64 m2 = a.M12 - b.M12;
            FixFloat64 m3 = a.M21 - b.M21;
            FixFloat64 m4 = a.M22 - b.M22;
            result.M11 = m;
            result.M12 = m2;
            result.M21 = m3;
            result.M22 = m4;
        }

        /// <summary>
        /// Transforms the vector by the matrix.
        /// </summary>
        /// <param name="v">Vector2 to transform.</param>
        /// <param name="matrix">FixMatrix to use as the transformation.</param>
        /// <param name="result">Product of the transformation.</param>
        public static void Transform(ref FixVector2 v, ref FixMatrix2x2 matrix, out FixVector2 result)
        {
            FixFloat64 x = v.X;
            FixFloat64 y = v.Y;
            result.X = x * matrix.M11 + y * matrix.M21;
            result.Y = x * matrix.M12 + y * matrix.M22;
        }

        /// <summary>
        /// Computes the transposed matrix of a matrix.
        /// </summary>
        /// <param name="matrix">FixMatrix to transpose.</param>
        /// <param name="result">Transposed matrix.</param>
        public static void Transpose(ref FixMatrix2x2 matrix, out FixMatrix2x2 result)
        {
            FixFloat64 m = matrix.M12;
            result.M11 = matrix.M11;
            result.M12 = matrix.M21;
            result.M21 = m;
            result.M22 = matrix.M22;
        }

        /// <summary>
        /// Transposes the matrix in-place.
        /// </summary>
        public void Transpose()
        {
            FixFloat64 m = M21;
            M21 = M12;
            M12 = m;
        }

        /// <summary>
        /// Creates a string representation of the matrix.
        /// </summary>
        /// <returns>A string representation of the matrix.</returns>
        public override string ToString()
        {
            return string.Concat("{", M11, ", ", M12, "} {", M21, ", ", M22, "}");
        }

        /// <summary>
        /// Calculates the determinant of the matrix.
        /// </summary>
        /// <returns>The matrix's determinant.</returns>
        public FixFloat64 Determinant()
        {
            return M11 * M22 - M12 * M21;
        }
    }
}