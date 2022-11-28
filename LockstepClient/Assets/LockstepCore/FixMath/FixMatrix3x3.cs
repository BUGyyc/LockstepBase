/*
 * @Author: delevin.ying 
 * @Date: 2022-11-26 15:37:30 
 * @Last Modified by: delevin.ying
 * @Last Modified time: 2022-11-26 15:47:23
 */

using System;

namespace FixMath
{

    /// <summary>
    /// 3 row, 3 column matrix.
    /// </summary>
    public struct FixMatrix3x3
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
        /// Value at row 1, column 3 of the matrix.
        /// </summary>
        public FixFloat64 M13;

        /// <summary>
        /// Value at row 2, column 1 of the matrix.
        /// </summary>
        public FixFloat64 M21;

        /// <summary>
        /// Value at row 2, column 2 of the matrix.
        /// </summary>
        public FixFloat64 M22;

        /// <summary>
        /// Value at row 2, column 3 of the matrix.
        /// </summary>
        public FixFloat64 M23;

        /// <summary>
        /// Value at row 3, column 1 of the matrix.
        /// </summary>
        public FixFloat64 M31;

        /// <summary>
        /// Value at row 3, column 2 of the matrix.
        /// </summary>
        public FixFloat64 M32;

        /// <summary>
        /// Value at row 3, column 3 of the matrix.
        /// </summary>
        public FixFloat64 M33;

        /// <summary>
        /// Gets the 3x3 identity matrix.
        /// </summary>
        public static FixMatrix3x3 Identity => new FixMatrix3x3(FixFloat64.C1, FixFloat64.C0, FixFloat64.C0, FixFloat64.C0, FixFloat64.C1, FixFloat64.C0, FixFloat64.C0, FixFloat64.C0, FixFloat64.C1);

        /// <summary>
        /// Gets or sets the backward vector of the matrix.
        /// </summary>
        public FixVector3 Backward
        {
            get
            {
                FixVector3 result = default(FixVector3);
                result.X = M31;
                result.Y = M32;
                result.Z = M33;
                return result;
            }
            set
            {
                M31 = value.X;
                M32 = value.Y;
                M33 = value.Z;
            }
        }

        /// <summary>
        /// Gets or sets the down vector of the matrix.
        /// </summary>
        public FixVector3 Down
        {
            get
            {
                FixVector3 result = default(FixVector3);
                result.X = -M21;
                result.Y = -M22;
                result.Z = -M23;
                return result;
            }
            set
            {
                M21 = -value.X;
                M22 = -value.Y;
                M23 = -value.Z;
            }
        }

        /// <summary>
        /// Gets or sets the forward vector of the matrix.
        /// </summary>
        public FixVector3 Forward
        {
            get
            {
                FixVector3 result = default(FixVector3);
                result.X = -M31;
                result.Y = -M32;
                result.Z = -M33;
                return result;
            }
            set
            {
                M31 = -value.X;
                M32 = -value.Y;
                M33 = -value.Z;
            }
        }

        /// <summary>
        /// Gets or sets the left vector of the matrix.
        /// </summary>
        public FixVector3 Left
        {
            get
            {
                FixVector3 result = default(FixVector3);
                result.X = -M11;
                result.Y = -M12;
                result.Z = -M13;
                return result;
            }
            set
            {
                M11 = -value.X;
                M12 = -value.Y;
                M13 = -value.Z;
            }
        }

        /// <summary>
        /// Gets or sets the right vector of the matrix.
        /// </summary>
        public FixVector3 Right
        {
            get
            {
                FixVector3 result = default(FixVector3);
                result.X = M11;
                result.Y = M12;
                result.Z = M13;
                return result;
            }
            set
            {
                M11 = value.X;
                M12 = value.Y;
                M13 = value.Z;
            }
        }

        /// <summary>
        /// Gets or sets the up vector of the matrix.
        /// </summary>
        public FixVector3 Up
        {
            get
            {
                FixVector3 result = default(FixVector3);
                result.X = M21;
                result.Y = M22;
                result.Z = M23;
                return result;
            }
            set
            {
                M21 = value.X;
                M22 = value.Y;
                M23 = value.Z;
            }
        }

        /// <summary>
        /// Constructs a new 3 row, 3 column matrix.
        /// </summary>
        /// <param name="m11">Value at row 1, column 1 of the matrix.</param>
        /// <param name="m12">Value at row 1, column 2 of the matrix.</param>
        /// <param name="m13">Value at row 1, column 3 of the matrix.</param>
        /// <param name="m21">Value at row 2, column 1 of the matrix.</param>
        /// <param name="m22">Value at row 2, column 2 of the matrix.</param>
        /// <param name="m23">Value at row 2, column 3 of the matrix.</param>
        /// <param name="m31">Value at row 3, column 1 of the matrix.</param>
        /// <param name="m32">Value at row 3, column 2 of the matrix.</param>
        /// <param name="m33">Value at row 3, column 3 of the matrix.</param>
        public FixMatrix3x3(FixFloat64 m11, FixFloat64 m12, FixFloat64 m13, FixFloat64 m21, FixFloat64 m22, FixFloat64 m23, FixFloat64 m31, FixFloat64 m32, FixFloat64 m33)
        {
            M11 = m11;
            M12 = m12;
            M13 = m13;
            M21 = m21;
            M22 = m22;
            M23 = m23;
            M31 = m31;
            M32 = m32;
            M33 = m33;
        }

        /// <summary>
        /// Adds the two matrices together on a per-element basis.
        /// </summary>
        /// <param name="a">First matrix to add.</param>
        /// <param name="b">Second matrix to add.</param>
        /// <param name="result">Sum of the two matrices.</param>
        public static void Add(ref FixMatrix3x3 a, ref FixMatrix3x3 b, out FixMatrix3x3 result)
        {
            FixFloat64 m = a.M11 + b.M11;
            FixFloat64 m2 = a.M12 + b.M12;
            FixFloat64 m3 = a.M13 + b.M13;
            FixFloat64 m4 = a.M21 + b.M21;
            FixFloat64 m5 = a.M22 + b.M22;
            FixFloat64 m6 = a.M23 + b.M23;
            FixFloat64 m7 = a.M31 + b.M31;
            FixFloat64 m8 = a.M32 + b.M32;
            FixFloat64 m9 = a.M33 + b.M33;
            result.M11 = m;
            result.M12 = m2;
            result.M13 = m3;
            result.M21 = m4;
            result.M22 = m5;
            result.M23 = m6;
            result.M31 = m7;
            result.M32 = m8;
            result.M33 = m9;
        }

        /// <summary>
        /// Adds the two matrices together on a per-element basis.
        /// </summary>
        /// <param name="a">First matrix to add.</param>
        /// <param name="b">Second matrix to add.</param>
        /// <param name="result">Sum of the two matrices.</param>
        public static void Add(ref FixMatrix a, ref FixMatrix3x3 b, out FixMatrix3x3 result)
        {
            FixFloat64 m = a.M11 + b.M11;
            FixFloat64 m2 = a.M12 + b.M12;
            FixFloat64 m3 = a.M13 + b.M13;
            FixFloat64 m4 = a.M21 + b.M21;
            FixFloat64 m5 = a.M22 + b.M22;
            FixFloat64 m6 = a.M23 + b.M23;
            FixFloat64 m7 = a.M31 + b.M31;
            FixFloat64 m8 = a.M32 + b.M32;
            FixFloat64 m9 = a.M33 + b.M33;
            result.M11 = m;
            result.M12 = m2;
            result.M13 = m3;
            result.M21 = m4;
            result.M22 = m5;
            result.M23 = m6;
            result.M31 = m7;
            result.M32 = m8;
            result.M33 = m9;
        }

        /// <summary>
        /// Adds the two matrices together on a per-element basis.
        /// </summary>
        /// <param name="a">First matrix to add.</param>
        /// <param name="b">Second matrix to add.</param>
        /// <param name="result">Sum of the two matrices.</param>
        public static void Add(ref FixMatrix3x3 a, ref FixMatrix b, out FixMatrix3x3 result)
        {
            FixFloat64 m = a.M11 + b.M11;
            FixFloat64 m2 = a.M12 + b.M12;
            FixFloat64 m3 = a.M13 + b.M13;
            FixFloat64 m4 = a.M21 + b.M21;
            FixFloat64 m5 = a.M22 + b.M22;
            FixFloat64 m6 = a.M23 + b.M23;
            FixFloat64 m7 = a.M31 + b.M31;
            FixFloat64 m8 = a.M32 + b.M32;
            FixFloat64 m9 = a.M33 + b.M33;
            result.M11 = m;
            result.M12 = m2;
            result.M13 = m3;
            result.M21 = m4;
            result.M22 = m5;
            result.M23 = m6;
            result.M31 = m7;
            result.M32 = m8;
            result.M33 = m9;
        }

        /// <summary>
        /// Adds the two matrices together on a per-element basis.
        /// </summary>
        /// <param name="a">First matrix to add.</param>
        /// <param name="b">Second matrix to add.</param>
        /// <param name="result">Sum of the two matrices.</param>
        public static void Add(ref FixMatrix a, ref FixMatrix b, out FixMatrix3x3 result)
        {
            FixFloat64 m = a.M11 + b.M11;
            FixFloat64 m2 = a.M12 + b.M12;
            FixFloat64 m3 = a.M13 + b.M13;
            FixFloat64 m4 = a.M21 + b.M21;
            FixFloat64 m5 = a.M22 + b.M22;
            FixFloat64 m6 = a.M23 + b.M23;
            FixFloat64 m7 = a.M31 + b.M31;
            FixFloat64 m8 = a.M32 + b.M32;
            FixFloat64 m9 = a.M33 + b.M33;
            result.M11 = m;
            result.M12 = m2;
            result.M13 = m3;
            result.M21 = m4;
            result.M22 = m5;
            result.M23 = m6;
            result.M31 = m7;
            result.M32 = m8;
            result.M33 = m9;
        }

        /// <summary>
        /// Creates a skew symmetric matrix M from vector A such that M * B for some other vector B is equivalent to the cross product of A and B.
        /// </summary>
        /// <param name="v">Vector to base the matrix on.</param>
        /// <param name="result">Skew-symmetric matrix result.</param>
        public static void CreateCrossProduct(ref FixVector3 v, out FixMatrix3x3 result)
        {
            result.M11 = FixFloat64.C0;
            result.M12 = -v.Z;
            result.M13 = v.Y;
            result.M21 = v.Z;
            result.M22 = FixFloat64.C0;
            result.M23 = -v.X;
            result.M31 = -v.Y;
            result.M32 = v.X;
            result.M33 = FixFloat64.C0;
        }

        /// <summary>
        /// Creates a 3x3 matrix from an XNA 4x4 matrix.
        /// </summary>
        /// <param name="matrix4X4">FixMatrix to extract a 3x3 matrix from.</param>
        /// <param name="matrix3X3">Upper 3x3 matrix extracted from the XNA matrix.</param>
        public static void CreateFromMatrix(ref FixMatrix matrix4X4, out FixMatrix3x3 matrix3X3)
        {
            matrix3X3.M11 = matrix4X4.M11;
            matrix3X3.M12 = matrix4X4.M12;
            matrix3X3.M13 = matrix4X4.M13;
            matrix3X3.M21 = matrix4X4.M21;
            matrix3X3.M22 = matrix4X4.M22;
            matrix3X3.M23 = matrix4X4.M23;
            matrix3X3.M31 = matrix4X4.M31;
            matrix3X3.M32 = matrix4X4.M32;
            matrix3X3.M33 = matrix4X4.M33;
        }

        /// <summary>
        /// Creates a 3x3 matrix from an XNA 4x4 matrix.
        /// </summary>
        /// <param name="matrix4X4">FixMatrix to extract a 3x3 matrix from.</param>
        /// <returns>Upper 3x3 matrix extracted from the XNA matrix.</returns>
        public static FixMatrix3x3 CreateFromMatrix(FixMatrix matrix4X4)
        {
            FixMatrix3x3 result = default(FixMatrix3x3);
            result.M11 = matrix4X4.M11;
            result.M12 = matrix4X4.M12;
            result.M13 = matrix4X4.M13;
            result.M21 = matrix4X4.M21;
            result.M22 = matrix4X4.M22;
            result.M23 = matrix4X4.M23;
            result.M31 = matrix4X4.M31;
            result.M32 = matrix4X4.M32;
            result.M33 = matrix4X4.M33;
            return result;
        }

        /// <summary>
        /// Constructs a uniform scaling matrix.
        /// </summary>
        /// <param name="scale">Value to use in the diagonal.</param>
        /// <param name="matrix">Scaling matrix.</param>
        public static void CreateScale(FixFloat64 scale, out FixMatrix3x3 matrix)
        {
            matrix = new FixMatrix3x3
            {
                M11 = scale,
                M22 = scale,
                M33 = scale
            };
        }

        /// <summary>
        /// Constructs a uniform scaling matrix.
        /// </summary>
        /// <param name="scale">Value to use in the diagonal.</param>
        /// <returns>Scaling matrix.</returns>
        public static FixMatrix3x3 CreateScale(FixFloat64 scale)
        {
            FixMatrix3x3 result = default(FixMatrix3x3);
            result.M11 = scale;
            result.M22 = scale;
            result.M33 = scale;
            return result;
        }

        /// <summary>
        /// Constructs a non-uniform scaling matrix.
        /// </summary>
        /// <param name="scale">Values defining the axis scales.</param>
        /// <param name="matrix">Scaling matrix.</param>
        public static void CreateScale(ref FixVector3 scale, out FixMatrix3x3 matrix)
        {
            matrix = new FixMatrix3x3
            {
                M11 = scale.X,
                M22 = scale.Y,
                M33 = scale.Z
            };
        }

        /// <summary>
        /// Constructs a non-uniform scaling matrix.
        /// </summary>
        /// <param name="scale">Values defining the axis scales.</param>
        /// <returns>Scaling matrix.</returns>
        public static FixMatrix3x3 CreateScale(ref FixVector3 scale)
        {
            FixMatrix3x3 result = default(FixMatrix3x3);
            result.M11 = scale.X;
            result.M22 = scale.Y;
            result.M33 = scale.Z;
            return result;
        }

        /// <summary>
        /// Constructs a non-uniform scaling matrix.
        /// </summary>
        /// <param name="x">Scaling along the x axis.</param>
        /// <param name="y">Scaling along the y axis.</param>
        /// <param name="z">Scaling along the z axis.</param>
        /// <param name="matrix">Scaling matrix.</param>
        public static void CreateScale(FixFloat64 x, FixFloat64 y, FixFloat64 z, out FixMatrix3x3 matrix)
        {
            matrix = new FixMatrix3x3
            {
                M11 = x,
                M22 = y,
                M33 = z
            };
        }

        /// <summary>
        /// Constructs a non-uniform scaling matrix.
        /// </summary>
        /// <param name="x">Scaling along the x axis.</param>
        /// <param name="y">Scaling along the y axis.</param>
        /// <param name="z">Scaling along the z axis.</param>
        /// <returns>Scaling matrix.</returns>
        public static FixMatrix3x3 CreateScale(FixFloat64 x, FixFloat64 y, FixFloat64 z)
        {
            FixMatrix3x3 result = default(FixMatrix3x3);
            result.M11 = x;
            result.M22 = y;
            result.M33 = z;
            return result;
        }

        /// <summary>
        /// Inverts the given matix.
        /// </summary>
        /// <param name="matrix">FixMatrix to be inverted.</param>
        /// <param name="result">Inverted matrix.</param>
        /// <returns>false if matrix is singular, true otherwise</returns>
        public static bool Invert(ref FixMatrix3x3 matrix, out FixMatrix3x3 result)
        {
            return FixMatrix3x6.Invert(ref matrix, out result);
        }

        /// <summary>
        /// Inverts the given matix.
        /// </summary>
        /// <param name="matrix">FixMatrix to be inverted.</param>
        /// <returns>Inverted matrix.</returns>
        public static FixMatrix3x3 Invert(FixMatrix3x3 matrix)
        {
            Invert(ref matrix, out var result);
            return result;
        }

        /// <summary>
        /// Inverts the largest nonsingular submatrix in the matrix, excluding 2x2's that involve M13 or M31, and excluding 1x1's that include nondiagonal elements.
        /// </summary>
        /// <param name="matrix">FixMatrix to be inverted.</param>
        /// <param name="result">Inverted matrix.</param>
        public static void AdaptiveInvert(ref FixMatrix3x3 matrix, out FixMatrix3x3 result)
        {
            if (!Invert(ref matrix, out result))
            {
                int subMatrixCode;
                FixFloat64 fix = FixFloat64.C1 / matrix.AdaptiveDeterminant(out subMatrixCode);
                FixFloat64 m;
                FixFloat64 m2;
                FixFloat64 m3;
                FixFloat64 m4;
                FixFloat64 m5;
                FixFloat64 m6;
                FixFloat64 m7;
                FixFloat64 m8;
                FixFloat64 m9;
                switch (subMatrixCode)
                {
                    case 1:
                        m = matrix.M22 * fix;
                        m2 = -matrix.M12 * fix;
                        m3 = FixFloat64.C0;
                        m4 = -matrix.M21 * fix;
                        m5 = matrix.M11 * fix;
                        m6 = FixFloat64.C0;
                        m7 = FixFloat64.C0;
                        m8 = FixFloat64.C0;
                        m9 = FixFloat64.C0;
                        break;
                    case 2:
                        m = FixFloat64.C0;
                        m2 = FixFloat64.C0;
                        m3 = FixFloat64.C0;
                        m4 = FixFloat64.C0;
                        m5 = matrix.M33 * fix;
                        m6 = -matrix.M23 * fix;
                        m7 = FixFloat64.C0;
                        m8 = -matrix.M32 * fix;
                        m9 = matrix.M22 * fix;
                        break;
                    case 3:
                        m = matrix.M33 * fix;
                        m2 = FixFloat64.C0;
                        m3 = -matrix.M13 * fix;
                        m4 = FixFloat64.C0;
                        m5 = FixFloat64.C0;
                        m6 = FixFloat64.C0;
                        m7 = -matrix.M31 * fix;
                        m8 = FixFloat64.C0;
                        m9 = matrix.M11 * fix;
                        break;
                    case 4:
                        m = FixFloat64.C1 / matrix.M11;
                        m2 = FixFloat64.C0;
                        m3 = FixFloat64.C0;
                        m4 = FixFloat64.C0;
                        m5 = FixFloat64.C0;
                        m6 = FixFloat64.C0;
                        m7 = FixFloat64.C0;
                        m8 = FixFloat64.C0;
                        m9 = FixFloat64.C0;
                        break;
                    case 5:
                        m = FixFloat64.C0;
                        m2 = FixFloat64.C0;
                        m3 = FixFloat64.C0;
                        m4 = FixFloat64.C0;
                        m5 = FixFloat64.C1 / matrix.M22;
                        m6 = FixFloat64.C0;
                        m7 = FixFloat64.C0;
                        m8 = FixFloat64.C0;
                        m9 = FixFloat64.C0;
                        break;
                    case 6:
                        m = FixFloat64.C0;
                        m2 = FixFloat64.C0;
                        m3 = FixFloat64.C0;
                        m4 = FixFloat64.C0;
                        m5 = FixFloat64.C0;
                        m6 = FixFloat64.C0;
                        m7 = FixFloat64.C0;
                        m8 = FixFloat64.C0;
                        m9 = FixFloat64.C1 / matrix.M33;
                        break;
                    default:
                        m = FixFloat64.C0;
                        m2 = FixFloat64.C0;
                        m3 = FixFloat64.C0;
                        m4 = FixFloat64.C0;
                        m5 = FixFloat64.C0;
                        m6 = FixFloat64.C0;
                        m7 = FixFloat64.C0;
                        m8 = FixFloat64.C0;
                        m9 = FixFloat64.C0;
                        break;
                }
                result.M11 = m;
                result.M12 = m2;
                result.M13 = m3;
                result.M21 = m4;
                result.M22 = m5;
                result.M23 = m6;
                result.M31 = m7;
                result.M32 = m8;
                result.M33 = m9;
            }
        }

        /// <summary>
        /// <para>Computes the adjugate transpose of a matrix.</para>
        /// <para>The adjugate transpose A of matrix M is: det(M) * transpose(invert(M))</para>
        /// <para>This is necessary when transforming normals (bivectors) with general linear transformations.</para>
        /// </summary>
        /// <param name="matrix">FixMatrix to compute the adjugate transpose of.</param>
        /// <param name="result">Adjugate transpose of the input matrix.</param>
        public static void AdjugateTranspose(ref FixMatrix3x3 matrix, out FixMatrix3x3 result)
        {
            FixFloat64 m = matrix.M22 * matrix.M33 - matrix.M23 * matrix.M32;
            FixFloat64 m2 = matrix.M13 * matrix.M32 - matrix.M33 * matrix.M12;
            FixFloat64 m3 = matrix.M12 * matrix.M23 - matrix.M22 * matrix.M13;
            FixFloat64 m4 = matrix.M23 * matrix.M31 - matrix.M21 * matrix.M33;
            FixFloat64 m5 = matrix.M11 * matrix.M33 - matrix.M13 * matrix.M31;
            FixFloat64 m6 = matrix.M13 * matrix.M21 - matrix.M11 * matrix.M23;
            FixFloat64 m7 = matrix.M21 * matrix.M32 - matrix.M22 * matrix.M31;
            FixFloat64 m8 = matrix.M12 * matrix.M31 - matrix.M11 * matrix.M32;
            FixFloat64 m9 = matrix.M11 * matrix.M22 - matrix.M12 * matrix.M21;
            result.M11 = m;
            result.M12 = m4;
            result.M13 = m7;
            result.M21 = m2;
            result.M22 = m5;
            result.M23 = m8;
            result.M31 = m3;
            result.M32 = m6;
            result.M33 = m9;
        }

        /// <summary>
        /// <para>Computes the adjugate transpose of a matrix.</para>
        /// <para>The adjugate transpose A of matrix M is: det(M) * transpose(invert(M))</para>
        /// <para>This is necessary when transforming normals (bivectors) with general linear transformations.</para>
        /// </summary>
        /// <param name="matrix">FixMatrix to compute the adjugate transpose of.</param>
        /// <returns>Adjugate transpose of the input matrix.</returns>
        public static FixMatrix3x3 AdjugateTranspose(FixMatrix3x3 matrix)
        {
            AdjugateTranspose(ref matrix, out var result);
            return result;
        }

        /// <summary>
        /// Multiplies the two matrices.
        /// </summary>
        /// <param name="a">First matrix to multiply.</param>
        /// <param name="b">Second matrix to multiply.</param>
        /// <returns>Product of the multiplication.</returns>
        public static FixMatrix3x3 operator *(FixMatrix3x3 a, FixMatrix3x3 b)
        {
            Multiply(ref a, ref b, out var result);
            return result;
        }

        /// <summary>
        /// Scales all components of the matrix by the given value.
        /// </summary>
        /// <param name="m">First matrix to multiply.</param>
        /// <param name="f">Scaling value to apply to all components of the matrix.</param>
        /// <returns>Product of the multiplication.</returns>
        public static FixMatrix3x3 operator *(FixMatrix3x3 m, FixFloat64 f)
        {
            Multiply(ref m, f, out var result);
            return result;
        }

        /// <summary>
        /// Scales all components of the matrix by the given value.
        /// </summary>
        /// <param name="m">First matrix to multiply.</param>
        /// <param name="f">Scaling value to apply to all components of the matrix.</param>
        /// <returns>Product of the multiplication.</returns>
        public static FixMatrix3x3 operator *(FixFloat64 f, FixMatrix3x3 m)
        {
            Multiply(ref m, f, out var result);
            return result;
        }

        /// <summary>
        /// Multiplies the two matrices.
        /// </summary>
        /// <param name="a">First matrix to multiply.</param>
        /// <param name="b">Second matrix to multiply.</param>
        /// <param name="result">Product of the multiplication.</param>
        public static void Multiply(ref FixMatrix3x3 a, ref FixMatrix3x3 b, out FixMatrix3x3 result)
        {
            FixFloat64 m = a.M11 * b.M11 + a.M12 * b.M21 + a.M13 * b.M31;
            FixFloat64 m2 = a.M11 * b.M12 + a.M12 * b.M22 + a.M13 * b.M32;
            FixFloat64 m3 = a.M11 * b.M13 + a.M12 * b.M23 + a.M13 * b.M33;
            FixFloat64 m4 = a.M21 * b.M11 + a.M22 * b.M21 + a.M23 * b.M31;
            FixFloat64 m5 = a.M21 * b.M12 + a.M22 * b.M22 + a.M23 * b.M32;
            FixFloat64 m6 = a.M21 * b.M13 + a.M22 * b.M23 + a.M23 * b.M33;
            FixFloat64 m7 = a.M31 * b.M11 + a.M32 * b.M21 + a.M33 * b.M31;
            FixFloat64 m8 = a.M31 * b.M12 + a.M32 * b.M22 + a.M33 * b.M32;
            FixFloat64 m9 = a.M31 * b.M13 + a.M32 * b.M23 + a.M33 * b.M33;
            result.M11 = m;
            result.M12 = m2;
            result.M13 = m3;
            result.M21 = m4;
            result.M22 = m5;
            result.M23 = m6;
            result.M31 = m7;
            result.M32 = m8;
            result.M33 = m9;
        }

        /// <summary>
        /// Multiplies the two matrices.
        /// </summary>
        /// <param name="a">First matrix to multiply.</param>
        /// <param name="b">Second matrix to multiply.</param>
        /// <param name="result">Product of the multiplication.</param>
        public static void Multiply(ref FixMatrix3x3 a, ref FixMatrix b, out FixMatrix3x3 result)
        {
            FixFloat64 m = a.M11 * b.M11 + a.M12 * b.M21 + a.M13 * b.M31;
            FixFloat64 m2 = a.M11 * b.M12 + a.M12 * b.M22 + a.M13 * b.M32;
            FixFloat64 m3 = a.M11 * b.M13 + a.M12 * b.M23 + a.M13 * b.M33;
            FixFloat64 m4 = a.M21 * b.M11 + a.M22 * b.M21 + a.M23 * b.M31;
            FixFloat64 m5 = a.M21 * b.M12 + a.M22 * b.M22 + a.M23 * b.M32;
            FixFloat64 m6 = a.M21 * b.M13 + a.M22 * b.M23 + a.M23 * b.M33;
            FixFloat64 m7 = a.M31 * b.M11 + a.M32 * b.M21 + a.M33 * b.M31;
            FixFloat64 m8 = a.M31 * b.M12 + a.M32 * b.M22 + a.M33 * b.M32;
            FixFloat64 m9 = a.M31 * b.M13 + a.M32 * b.M23 + a.M33 * b.M33;
            result.M11 = m;
            result.M12 = m2;
            result.M13 = m3;
            result.M21 = m4;
            result.M22 = m5;
            result.M23 = m6;
            result.M31 = m7;
            result.M32 = m8;
            result.M33 = m9;
        }

        /// <summary>
        /// Multiplies the two matrices.
        /// </summary>
        /// <param name="a">First matrix to multiply.</param>
        /// <param name="b">Second matrix to multiply.</param>
        /// <param name="result">Product of the multiplication.</param>
        public static void Multiply(ref FixMatrix a, ref FixMatrix3x3 b, out FixMatrix3x3 result)
        {
            FixFloat64 m = a.M11 * b.M11 + a.M12 * b.M21 + a.M13 * b.M31;
            FixFloat64 m2 = a.M11 * b.M12 + a.M12 * b.M22 + a.M13 * b.M32;
            FixFloat64 m3 = a.M11 * b.M13 + a.M12 * b.M23 + a.M13 * b.M33;
            FixFloat64 m4 = a.M21 * b.M11 + a.M22 * b.M21 + a.M23 * b.M31;
            FixFloat64 m5 = a.M21 * b.M12 + a.M22 * b.M22 + a.M23 * b.M32;
            FixFloat64 m6 = a.M21 * b.M13 + a.M22 * b.M23 + a.M23 * b.M33;
            FixFloat64 m7 = a.M31 * b.M11 + a.M32 * b.M21 + a.M33 * b.M31;
            FixFloat64 m8 = a.M31 * b.M12 + a.M32 * b.M22 + a.M33 * b.M32;
            FixFloat64 m9 = a.M31 * b.M13 + a.M32 * b.M23 + a.M33 * b.M33;
            result.M11 = m;
            result.M12 = m2;
            result.M13 = m3;
            result.M21 = m4;
            result.M22 = m5;
            result.M23 = m6;
            result.M31 = m7;
            result.M32 = m8;
            result.M33 = m9;
        }

        /// <summary>
        /// Multiplies a transposed matrix with another matrix.
        /// </summary>
        /// <param name="matrix">FixMatrix to be multiplied.</param>
        /// <param name="transpose">FixMatrix to be transposed and multiplied.</param>
        /// <param name="result">Product of the multiplication.</param>
        public static void MultiplyTransposed(ref FixMatrix3x3 transpose, ref FixMatrix3x3 matrix, out FixMatrix3x3 result)
        {
            FixFloat64 m = transpose.M11 * matrix.M11 + transpose.M21 * matrix.M21 + transpose.M31 * matrix.M31;
            FixFloat64 m2 = transpose.M11 * matrix.M12 + transpose.M21 * matrix.M22 + transpose.M31 * matrix.M32;
            FixFloat64 m3 = transpose.M11 * matrix.M13 + transpose.M21 * matrix.M23 + transpose.M31 * matrix.M33;
            FixFloat64 m4 = transpose.M12 * matrix.M11 + transpose.M22 * matrix.M21 + transpose.M32 * matrix.M31;
            FixFloat64 m5 = transpose.M12 * matrix.M12 + transpose.M22 * matrix.M22 + transpose.M32 * matrix.M32;
            FixFloat64 m6 = transpose.M12 * matrix.M13 + transpose.M22 * matrix.M23 + transpose.M32 * matrix.M33;
            FixFloat64 m7 = transpose.M13 * matrix.M11 + transpose.M23 * matrix.M21 + transpose.M33 * matrix.M31;
            FixFloat64 m8 = transpose.M13 * matrix.M12 + transpose.M23 * matrix.M22 + transpose.M33 * matrix.M32;
            FixFloat64 m9 = transpose.M13 * matrix.M13 + transpose.M23 * matrix.M23 + transpose.M33 * matrix.M33;
            result.M11 = m;
            result.M12 = m2;
            result.M13 = m3;
            result.M21 = m4;
            result.M22 = m5;
            result.M23 = m6;
            result.M31 = m7;
            result.M32 = m8;
            result.M33 = m9;
        }

        /// <summary>
        /// Multiplies a matrix with a transposed matrix.
        /// </summary>
        /// <param name="matrix">FixMatrix to be multiplied.</param>
        /// <param name="transpose">FixMatrix to be transposed and multiplied.</param>
        /// <param name="result">Product of the multiplication.</param>
        public static void MultiplyByTransposed(ref FixMatrix3x3 matrix, ref FixMatrix3x3 transpose, out FixMatrix3x3 result)
        {
            FixFloat64 m = matrix.M11 * transpose.M11 + matrix.M12 * transpose.M12 + matrix.M13 * transpose.M13;
            FixFloat64 m2 = matrix.M11 * transpose.M21 + matrix.M12 * transpose.M22 + matrix.M13 * transpose.M23;
            FixFloat64 m3 = matrix.M11 * transpose.M31 + matrix.M12 * transpose.M32 + matrix.M13 * transpose.M33;
            FixFloat64 m4 = matrix.M21 * transpose.M11 + matrix.M22 * transpose.M12 + matrix.M23 * transpose.M13;
            FixFloat64 m5 = matrix.M21 * transpose.M21 + matrix.M22 * transpose.M22 + matrix.M23 * transpose.M23;
            FixFloat64 m6 = matrix.M21 * transpose.M31 + matrix.M22 * transpose.M32 + matrix.M23 * transpose.M33;
            FixFloat64 m7 = matrix.M31 * transpose.M11 + matrix.M32 * transpose.M12 + matrix.M33 * transpose.M13;
            FixFloat64 m8 = matrix.M31 * transpose.M21 + matrix.M32 * transpose.M22 + matrix.M33 * transpose.M23;
            FixFloat64 m9 = matrix.M31 * transpose.M31 + matrix.M32 * transpose.M32 + matrix.M33 * transpose.M33;
            result.M11 = m;
            result.M12 = m2;
            result.M13 = m3;
            result.M21 = m4;
            result.M22 = m5;
            result.M23 = m6;
            result.M31 = m7;
            result.M32 = m8;
            result.M33 = m9;
        }

        /// <summary>
        /// Scales all components of the matrix.
        /// </summary>
        /// <param name="matrix">FixMatrix to scale.</param>
        /// <param name="scale">Amount to scale.</param>
        /// <param name="result">Scaled matrix.</param>
        public static void Multiply(ref FixMatrix3x3 matrix, FixFloat64 scale, out FixMatrix3x3 result)
        {
            result.M11 = matrix.M11 * scale;
            result.M12 = matrix.M12 * scale;
            result.M13 = matrix.M13 * scale;
            result.M21 = matrix.M21 * scale;
            result.M22 = matrix.M22 * scale;
            result.M23 = matrix.M23 * scale;
            result.M31 = matrix.M31 * scale;
            result.M32 = matrix.M32 * scale;
            result.M33 = matrix.M33 * scale;
        }

        /// <summary>
        /// Negates every element in the matrix.
        /// </summary>
        /// <param name="matrix">FixMatrix to negate.</param>
        /// <param name="result">Negated matrix.</param>
        public static void Negate(ref FixMatrix3x3 matrix, out FixMatrix3x3 result)
        {
            result.M11 = -matrix.M11;
            result.M12 = -matrix.M12;
            result.M13 = -matrix.M13;
            result.M21 = -matrix.M21;
            result.M22 = -matrix.M22;
            result.M23 = -matrix.M23;
            result.M31 = -matrix.M31;
            result.M32 = -matrix.M32;
            result.M33 = -matrix.M33;
        }

        /// <summary>
        /// Subtracts the two matrices from each other on a per-element basis.
        /// </summary>
        /// <param name="a">First matrix to subtract.</param>
        /// <param name="b">Second matrix to subtract.</param>
        /// <param name="result">Difference of the two matrices.</param>
        public static void Subtract(ref FixMatrix3x3 a, ref FixMatrix3x3 b, out FixMatrix3x3 result)
        {
            FixFloat64 m = a.M11 - b.M11;
            FixFloat64 m2 = a.M12 - b.M12;
            FixFloat64 m3 = a.M13 - b.M13;
            FixFloat64 m4 = a.M21 - b.M21;
            FixFloat64 m5 = a.M22 - b.M22;
            FixFloat64 m6 = a.M23 - b.M23;
            FixFloat64 m7 = a.M31 - b.M31;
            FixFloat64 m8 = a.M32 - b.M32;
            FixFloat64 m9 = a.M33 - b.M33;
            result.M11 = m;
            result.M12 = m2;
            result.M13 = m3;
            result.M21 = m4;
            result.M22 = m5;
            result.M23 = m6;
            result.M31 = m7;
            result.M32 = m8;
            result.M33 = m9;
        }

        /// <summary>
        /// Creates a 4x4 matrix from a 3x3 matrix.
        /// </summary>
        /// <param name="a">3x3 matrix.</param>
        /// <param name="b">Created 4x4 matrix.</param>
        public static void ToMatrix4X4(ref FixMatrix3x3 a, out FixMatrix b)
        {
            b.M11 = a.M11;
            b.M12 = a.M12;
            b.M13 = a.M13;
            b.M21 = a.M21;
            b.M22 = a.M22;
            b.M23 = a.M23;
            b.M31 = a.M31;
            b.M32 = a.M32;
            b.M33 = a.M33;
            b.M44 = FixFloat64.C1;
            b.M14 = FixFloat64.C0;
            b.M24 = FixFloat64.C0;
            b.M34 = FixFloat64.C0;
            b.M41 = FixFloat64.C0;
            b.M42 = FixFloat64.C0;
            b.M43 = FixFloat64.C0;
        }

        /// <summary>
        /// Creates a 4x4 matrix from a 3x3 matrix.
        /// </summary>
        /// <param name="a">3x3 matrix.</param>
        /// <returns>Created 4x4 matrix.</returns>
        public static FixMatrix ToMatrix4X4(FixMatrix3x3 a)
        {
            FixMatrix result = default(FixMatrix);
            result.M11 = a.M11;
            result.M12 = a.M12;
            result.M13 = a.M13;
            result.M21 = a.M21;
            result.M22 = a.M22;
            result.M23 = a.M23;
            result.M31 = a.M31;
            result.M32 = a.M32;
            result.M33 = a.M33;
            result.M44 = FixFloat64.C1;
            result.M14 = FixFloat64.C0;
            result.M24 = FixFloat64.C0;
            result.M34 = FixFloat64.C0;
            result.M41 = FixFloat64.C0;
            result.M42 = FixFloat64.C0;
            result.M43 = FixFloat64.C0;
            return result;
        }

        /// <summary>
        /// Transforms the vector by the matrix.
        /// </summary>
        /// <param name="v">FixVector3 to transform.</param>
        /// <param name="matrix">FixMatrix to use as the transformation.</param>
        /// <param name="result">Product of the transformation.</param>
        public static void Transform(ref FixVector3 v, ref FixMatrix3x3 matrix, out FixVector3 result)
        {
            FixFloat64 x = v.X;
            FixFloat64 y = v.Y;
            FixFloat64 z = v.Z;
            result.X = x * matrix.M11 + y * matrix.M21 + z * matrix.M31;
            result.Y = x * matrix.M12 + y * matrix.M22 + z * matrix.M32;
            result.Z = x * matrix.M13 + y * matrix.M23 + z * matrix.M33;
        }

        /// <summary>
        /// Transforms the vector by the matrix.
        /// </summary>
        /// <param name="v">FixVector3 to transform.</param>
        /// <param name="matrix">FixMatrix to use as the transformation.</param>
        /// <returns>Product of the transformation.</returns>
        public static FixVector3 Transform(FixVector3 v, FixMatrix3x3 matrix)
        {
            FixFloat64 x = v.X;
            FixFloat64 y = v.Y;
            FixFloat64 z = v.Z;
            FixVector3 result = default(FixVector3);
            result.X = x * matrix.M11 + y * matrix.M21 + z * matrix.M31;
            result.Y = x * matrix.M12 + y * matrix.M22 + z * matrix.M32;
            result.Z = x * matrix.M13 + y * matrix.M23 + z * matrix.M33;
            return result;
        }

        /// <summary>
        /// Transforms the vector by the matrix's transpose.
        /// </summary>
        /// <param name="v">FixVector3 to transform.</param>
        /// <param name="matrix">FixMatrix to use as the transformation transpose.</param>
        /// <param name="result">Product of the transformation.</param>
        public static void TransformTranspose(ref FixVector3 v, ref FixMatrix3x3 matrix, out FixVector3 result)
        {
            FixFloat64 x = v.X;
            FixFloat64 y = v.Y;
            FixFloat64 z = v.Z;
            result.X = x * matrix.M11 + y * matrix.M12 + z * matrix.M13;
            result.Y = x * matrix.M21 + y * matrix.M22 + z * matrix.M23;
            result.Z = x * matrix.M31 + y * matrix.M32 + z * matrix.M33;
        }

        /// <summary>
        /// Transforms the vector by the matrix's transpose.
        /// </summary>
        /// <param name="v">FixVector3 to transform.</param>
        /// <param name="matrix">FixMatrix to use as the transformation transpose.</param>
        /// <returns>Product of the transformation.</returns>
        public static FixVector3 TransformTranspose(FixVector3 v, FixMatrix3x3 matrix)
        {
            FixFloat64 x = v.X;
            FixFloat64 y = v.Y;
            FixFloat64 z = v.Z;
            FixVector3 result = default(FixVector3);
            result.X = x * matrix.M11 + y * matrix.M12 + z * matrix.M13;
            result.Y = x * matrix.M21 + y * matrix.M22 + z * matrix.M23;
            result.Z = x * matrix.M31 + y * matrix.M32 + z * matrix.M33;
            return result;
        }

        /// <summary>
        /// Computes the transposed matrix of a matrix.
        /// </summary>
        /// <param name="matrix">FixMatrix to transpose.</param>
        /// <param name="result">Transposed matrix.</param>
        public static void Transpose(ref FixMatrix3x3 matrix, out FixMatrix3x3 result)
        {
            FixFloat64 m = matrix.M12;
            FixFloat64 m2 = matrix.M13;
            FixFloat64 m3 = matrix.M21;
            FixFloat64 m4 = matrix.M23;
            FixFloat64 m5 = matrix.M31;
            FixFloat64 m6 = matrix.M32;
            result.M11 = matrix.M11;
            result.M12 = m3;
            result.M13 = m5;
            result.M21 = m;
            result.M22 = matrix.M22;
            result.M23 = m6;
            result.M31 = m2;
            result.M32 = m4;
            result.M33 = matrix.M33;
        }

        /// <summary>
        /// Computes the transposed matrix of a matrix.
        /// </summary>
        /// <param name="matrix">FixMatrix to transpose.</param>
        /// <param name="result">Transposed matrix.</param>
        public static void Transpose(ref FixMatrix matrix, out FixMatrix3x3 result)
        {
            FixFloat64 m = matrix.M12;
            FixFloat64 m2 = matrix.M13;
            FixFloat64 m3 = matrix.M21;
            FixFloat64 m4 = matrix.M23;
            FixFloat64 m5 = matrix.M31;
            FixFloat64 m6 = matrix.M32;
            result.M11 = matrix.M11;
            result.M12 = m3;
            result.M13 = m5;
            result.M21 = m;
            result.M22 = matrix.M22;
            result.M23 = m6;
            result.M31 = m2;
            result.M32 = m4;
            result.M33 = matrix.M33;
        }

        /// <summary>
        /// Transposes the matrix in-place.
        /// </summary>
        public void Transpose()
        {
            FixFloat64 m = M12;
            M12 = M21;
            M21 = m;
            m = M13;
            M13 = M31;
            M31 = m;
            m = M23;
            M23 = M32;
            M32 = m;
        }

        /// <summary>
        /// Creates a string representation of the matrix.
        /// </summary>
        /// <returns>A string representation of the matrix.</returns>
        public override string ToString()
        {
            return string.Concat("{", M11, ", ", M12, ", ", M13, "} {", M21, ", ", M22, ", ", M23, "} {", M31, ", ", M32, ", ", M33, "}");
        }

        /// <summary>
        /// Calculates the determinant of largest nonsingular submatrix, excluding 2x2's that involve M13 or M31, and excluding all 1x1's that involve nondiagonal elements.
        /// </summary>
        /// <param name="subMatrixCode">Represents the submatrix that was used to compute the determinant.
        /// 0 is the full 3x3.  1 is the upper left 2x2.  2 is the lower right 2x2.  3 is the four corners.
        /// 4 is M11.  5 is M22.  6 is M33.</param>
        /// <returns>The matrix's determinant.</returns>
        internal FixFloat64 AdaptiveDeterminant(out int subMatrixCode)
        {
            FixFloat64 fix = M11 * M22 - M12 * M21;
            if (fix != FixFloat64.C0)
            {
                subMatrixCode = 1;
                return fix;
            }
            fix = M22 * M33 - M23 * M32;
            if (fix != FixFloat64.C0)
            {
                subMatrixCode = 2;
                return fix;
            }
            fix = M11 * M33 - M13 * M12;
            if (fix != FixFloat64.C0)
            {
                subMatrixCode = 3;
                return fix;
            }
            if (M11 != FixFloat64.C0)
            {
                subMatrixCode = 4;
                return M11;
            }
            if (M22 != FixFloat64.C0)
            {
                subMatrixCode = 5;
                return M22;
            }
            if (M33 != FixFloat64.C0)
            {
                subMatrixCode = 6;
                return M33;
            }
            subMatrixCode = -1;
            return FixFloat64.C0;
        }

        /// <summary>
        /// Creates a 3x3 matrix representing the orientation stored in the quaternion.
        /// </summary>
        /// <param name="quaternion">Quaternion to use to create a matrix.</param>
        /// <param name="result">FixMatrix representing the quaternion's orientation.</param>
        public static void CreateFromQuaternion(ref FixQuaternion quaternion, out FixMatrix3x3 result)
        {
            FixFloat64 fix = quaternion.X + quaternion.X;
            FixFloat64 fix2 = quaternion.Y + quaternion.Y;
            FixFloat64 fix3 = quaternion.Z + quaternion.Z;
            FixFloat64 fix4 = fix * quaternion.X;
            FixFloat64 fix5 = fix2 * quaternion.Y;
            FixFloat64 fix6 = fix3 * quaternion.Z;
            FixFloat64 fix7 = fix * quaternion.Y;
            FixFloat64 fix8 = fix * quaternion.Z;
            FixFloat64 fix9 = fix * quaternion.W;
            FixFloat64 fix10 = fix2 * quaternion.Z;
            FixFloat64 fix11 = fix2 * quaternion.W;
            FixFloat64 fix12 = fix3 * quaternion.W;
            result.M11 = FixFloat64.C1 - fix5 - fix6;
            result.M21 = fix7 - fix12;
            result.M31 = fix8 + fix11;
            result.M12 = fix7 + fix12;
            result.M22 = FixFloat64.C1 - fix4 - fix6;
            result.M32 = fix10 - fix9;
            result.M13 = fix8 - fix11;
            result.M23 = fix10 + fix9;
            result.M33 = FixFloat64.C1 - fix4 - fix5;
        }

        /// <summary>
        /// Creates a 3x3 matrix representing the orientation stored in the quaternion.
        /// </summary>
        /// <param name="quaternion">Quaternion to use to create a matrix.</param>
        /// <returns>FixMatrix representing the quaternion's orientation.</returns>
        public static FixMatrix3x3 CreateFromQuaternion(FixQuaternion quaternion)
        {
            CreateFromQuaternion(ref quaternion, out var result);
            return result;
        }

        /// <summary>
        /// Computes the outer product of the given vectors.
        /// </summary>
        /// <param name="a">First vector.</param>
        /// <param name="b">Second vector.</param>
        /// <param name="result">Outer product result.</param>
        public static void CreateOuterProduct(ref FixVector3 a, ref FixVector3 b, out FixMatrix3x3 result)
        {
            result.M11 = a.X * b.X;
            result.M12 = a.X * b.Y;
            result.M13 = a.X * b.Z;
            result.M21 = a.Y * b.X;
            result.M22 = a.Y * b.Y;
            result.M23 = a.Y * b.Z;
            result.M31 = a.Z * b.X;
            result.M32 = a.Z * b.Y;
            result.M33 = a.Z * b.Z;
        }

        /// <summary>
        /// Creates a matrix representing a rotation of a given angle around a given axis.
        /// </summary>
        /// <param name="axis">Axis around which to rotate.</param>
        /// <param name="angle">Amount to rotate.</param>
        /// <returns>FixMatrix representing the rotation.</returns>
        public static FixMatrix3x3 CreateFromAxisAngle(FixVector3 axis, FixFloat64 angle)
        {
            CreateFromAxisAngle(ref axis, angle, out var result);
            return result;
        }

        /// <summary>
        /// Creates a matrix representing a rotation of a given angle around a given axis.
        /// </summary>
        /// <param name="axis">Axis around which to rotate.</param>
        /// <param name="angle">Amount to rotate.</param>
        /// <param name="result">FixMatrix representing the rotation.</param>
        public static void CreateFromAxisAngle(ref FixVector3 axis, FixFloat64 angle, out FixMatrix3x3 result)
        {
            FixFloat64 fix = axis.X * axis.X;
            FixFloat64 fix2 = axis.Y * axis.Y;
            FixFloat64 fix3 = axis.Z * axis.Z;
            FixFloat64 fix4 = axis.X * axis.Y;
            FixFloat64 fix5 = axis.X * axis.Z;
            FixFloat64 fix6 = axis.Y * axis.Z;
            FixFloat64 fix7 = FixFloat64.Sin(angle);
            FixFloat64 fix8 = FixFloat64.C1 - FixFloat64.Cos(angle);
            result.M11 = FixFloat64.C1 + fix8 * (fix - FixFloat64.C1);
            result.M21 = -axis.Z * fix7 + fix8 * fix4;
            result.M31 = axis.Y * fix7 + fix8 * fix5;
            result.M12 = axis.Z * fix7 + fix8 * fix4;
            result.M22 = FixFloat64.C1 + fix8 * (fix2 - FixFloat64.C1);
            result.M32 = -axis.X * fix7 + fix8 * fix6;
            result.M13 = -axis.Y * fix7 + fix8 * fix5;
            result.M23 = axis.X * fix7 + fix8 * fix6;
            result.M33 = FixFloat64.C1 + fix8 * (fix3 - FixFloat64.C1);
        }
    }
}