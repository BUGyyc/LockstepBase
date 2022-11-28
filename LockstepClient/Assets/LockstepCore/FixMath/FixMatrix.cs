/*
 * @Author: delevin.ying 
 * @Date: 2022-11-26 15:29:13 
 * @Last Modified by: delevin.ying
 * @Last Modified time: 2022-11-28 17:56:30
 */

using System;


namespace FixMath
{
    public struct FixMatrix
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
        /// Value at row 1, column 4 of the matrix.
        /// </summary>
        public FixFloat64 M14;

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
        /// Value at row 2, column 4 of the matrix.
        /// </summary>
        public FixFloat64 M24;

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
        /// Value at row 3, column 4 of the matrix.
        /// </summary>
        public FixFloat64 M34;

        /// <summary>
        /// Value at row 4, column 1 of the matrix.
        /// </summary>
        public FixFloat64 M41;

        /// <summary>
        /// Value at row 4, column 2 of the matrix.
        /// </summary>
        public FixFloat64 M42;

        /// <summary>
        /// Value at row 4, column 3 of the matrix.
        /// </summary>
        public FixFloat64 M43;

        /// <summary>
        /// Value at row 4, column 4 of the matrix.
        /// </summary>
        public FixFloat64 M44;

        /// <summary>
        /// Gets or sets the translation component of the transform.
        /// </summary>
        public FixVector3 Translation
        {
            get
            {
                FixVector3 result = default(FixVector3);
                result.X = M41;
                result.Y = M42;
                result.Z = M43;
                return result;
            }
            set
            {
                M41 = value.X;
                M42 = value.Y;
                M43 = value.Z;
            }
        }

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
        /// Gets the 4x4 identity matrix.
        /// </summary>
        public static FixMatrix Identity
        {
            get
            {
                FixMatrix result = default(FixMatrix);
                result.M11 = FixFloat64.C1;
                result.M12 = FixFloat64.C0;
                result.M13 = FixFloat64.C0;
                result.M14 = FixFloat64.C0;
                result.M21 = FixFloat64.C0;
                result.M22 = FixFloat64.C1;
                result.M23 = FixFloat64.C0;
                result.M24 = FixFloat64.C0;
                result.M31 = FixFloat64.C0;
                result.M32 = FixFloat64.C0;
                result.M33 = FixFloat64.C1;
                result.M34 = FixFloat64.C0;
                result.M41 = FixFloat64.C0;
                result.M42 = FixFloat64.C0;
                result.M43 = FixFloat64.C0;
                result.M44 = FixFloat64.C1;
                return result;
            }
        }

        /// <summary>
        /// Constructs a new 4 row, 4 column matrix.
        /// </summary>
        /// <param name="m11">Value at row 1, column 1 of the matrix.</param>
        /// <param name="m12">Value at row 1, column 2 of the matrix.</param>
        /// <param name="m13">Value at row 1, column 3 of the matrix.</param>
        /// <param name="m14">Value at row 1, column 4 of the matrix.</param>
        /// <param name="m21">Value at row 2, column 1 of the matrix.</param>
        /// <param name="m22">Value at row 2, column 2 of the matrix.</param>
        /// <param name="m23">Value at row 2, column 3 of the matrix.</param>
        /// <param name="m24">Value at row 2, column 4 of the matrix.</param>
        /// <param name="m31">Value at row 3, column 1 of the matrix.</param>
        /// <param name="m32">Value at row 3, column 2 of the matrix.</param>
        /// <param name="m33">Value at row 3, column 3 of the matrix.</param>
        /// <param name="m34">Value at row 3, column 4 of the matrix.</param>
        /// <param name="m41">Value at row 4, column 1 of the matrix.</param>
        /// <param name="m42">Value at row 4, column 2 of the matrix.</param>
        /// <param name="m43">Value at row 4, column 3 of the matrix.</param>
        /// <param name="m44">Value at row 4, column 4 of the matrix.</param>
        public FixMatrix(FixFloat64 m11, FixFloat64 m12, FixFloat64 m13, FixFloat64 m14, FixFloat64 m21, FixFloat64 m22, FixFloat64 m23, FixFloat64 m24, FixFloat64 m31, FixFloat64 m32, FixFloat64 m33, FixFloat64 m34, FixFloat64 m41, FixFloat64 m42, FixFloat64 m43, FixFloat64 m44)
        {
            M11 = m11;
            M12 = m12;
            M13 = m13;
            M14 = m14;
            M21 = m21;
            M22 = m22;
            M23 = m23;
            M24 = m24;
            M31 = m31;
            M32 = m32;
            M33 = m33;
            M34 = m34;
            M41 = m41;
            M42 = m42;
            M43 = m43;
            M44 = m44;
        }

        /// <summary>
        /// Computes the determinant of the matrix.
        /// </summary>
        /// <returns></returns>
        public FixFloat64 Determinant()
        {
            FixFloat64 fix = M33 * M44 - M34 * M43;
            FixFloat64 fix2 = M32 * M44 - M34 * M42;
            FixFloat64 fix3 = M32 * M43 - M33 * M42;
            FixFloat64 fix4 = M31 * M44 - M34 * M41;
            FixFloat64 fix5 = M31 * M43 - M33 * M41;
            FixFloat64 fix6 = M31 * M42 - M32 * M41;
            return M11 * (M22 * fix - M23 * fix2 + M24 * fix3) - M12 * (M21 * fix - M23 * fix4 + M24 * fix5) + M13 * (M21 * fix2 - M22 * fix4 + M24 * fix6) - M14 * (M21 * fix3 - M22 * fix5 + M23 * fix6);
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
            m = M14;
            M14 = M41;
            M41 = m;
            m = M23;
            M23 = M32;
            M32 = m;
            m = M24;
            M24 = M42;
            M42 = m;
            m = M34;
            M34 = M43;
            M43 = m;
        }

        /// <summary>
        /// Creates a matrix representing the given axis and angle rotation.
        /// </summary>
        /// <param name="axis">Axis around which to rotate.</param>
        /// <param name="angle">Angle to rotate around the axis.</param>
        /// <returns>FixMatrix created from the axis and angle.</returns>
        public static FixMatrix CreateFromAxisAngle(FixVector3 axis, FixFloat64 angle)
        {
            CreateFromAxisAngle(ref axis, angle, out var result);
            return result;
        }

        /// <summary>
        /// Creates a matrix representing the given axis and angle rotation.
        /// </summary>
        /// <param name="axis">Axis around which to rotate.</param>
        /// <param name="angle">Angle to rotate around the axis.</param>
        /// <param name="result">FixMatrix created from the axis and angle.</param>
        public static void CreateFromAxisAngle(ref FixVector3 axis, FixFloat64 angle, out FixMatrix result)
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
            result.M41 = FixFloat64.C0;
            result.M12 = axis.Z * fix7 + fix8 * fix4;
            result.M22 = FixFloat64.C1 + fix8 * (fix2 - FixFloat64.C1);
            result.M32 = -axis.X * fix7 + fix8 * fix6;
            result.M42 = FixFloat64.C0;
            result.M13 = -axis.Y * fix7 + fix8 * fix5;
            result.M23 = axis.X * fix7 + fix8 * fix6;
            result.M33 = FixFloat64.C1 + fix8 * (fix3 - FixFloat64.C1);
            result.M43 = FixFloat64.C0;
            result.M14 = FixFloat64.C0;
            result.M24 = FixFloat64.C0;
            result.M34 = FixFloat64.C0;
            result.M44 = FixFloat64.C1;
        }

        /// <summary>
        /// Creates a rotation matrix from a quaternion.
        /// </summary>
        /// <param name="quaternion">Quaternion to convert.</param>
        /// <param name="result">Rotation matrix created from the quaternion.</param>
        public static void CreateFromQuaternion(ref FixQuaternion quaternion, out FixMatrix result)
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
            result.M41 = FixFloat64.C0;
            result.M12 = fix7 + fix12;
            result.M22 = FixFloat64.C1 - fix4 - fix6;
            result.M32 = fix10 - fix9;
            result.M42 = FixFloat64.C0;
            result.M13 = fix8 - fix11;
            result.M23 = fix10 + fix9;
            result.M33 = FixFloat64.C1 - fix4 - fix5;
            result.M43 = FixFloat64.C0;
            result.M14 = FixFloat64.C0;
            result.M24 = FixFloat64.C0;
            result.M34 = FixFloat64.C0;
            result.M44 = FixFloat64.C1;
        }

        /// <summary>
        /// Creates a rotation matrix from a quaternion.
        /// </summary>
        /// <param name="quaternion">Quaternion to convert.</param>
        /// <returns>Rotation matrix created from the quaternion.</returns>
        public static FixMatrix CreateFromQuaternion(FixQuaternion quaternion)
        {
            CreateFromQuaternion(ref quaternion, out var result);
            return result;
        }

        /// <summary>
        /// Multiplies two matrices together.
        /// </summary>
        /// <param name="a">First matrix to multiply.</param>
        /// <param name="b">Second matrix to multiply.</param>
        /// <param name="result">Combined transformation.</param>
        public static void Multiply(ref FixMatrix a, ref FixMatrix b, out FixMatrix result)
        {
            FixFloat64 m = a.M11 * b.M11 + a.M12 * b.M21 + a.M13 * b.M31 + a.M14 * b.M41;
            FixFloat64 m2 = a.M11 * b.M12 + a.M12 * b.M22 + a.M13 * b.M32 + a.M14 * b.M42;
            FixFloat64 m3 = a.M11 * b.M13 + a.M12 * b.M23 + a.M13 * b.M33 + a.M14 * b.M43;
            FixFloat64 m4 = a.M11 * b.M14 + a.M12 * b.M24 + a.M13 * b.M34 + a.M14 * b.M44;
            FixFloat64 m5 = a.M21 * b.M11 + a.M22 * b.M21 + a.M23 * b.M31 + a.M24 * b.M41;
            FixFloat64 m6 = a.M21 * b.M12 + a.M22 * b.M22 + a.M23 * b.M32 + a.M24 * b.M42;
            FixFloat64 m7 = a.M21 * b.M13 + a.M22 * b.M23 + a.M23 * b.M33 + a.M24 * b.M43;
            FixFloat64 m8 = a.M21 * b.M14 + a.M22 * b.M24 + a.M23 * b.M34 + a.M24 * b.M44;
            FixFloat64 m9 = a.M31 * b.M11 + a.M32 * b.M21 + a.M33 * b.M31 + a.M34 * b.M41;
            FixFloat64 m10 = a.M31 * b.M12 + a.M32 * b.M22 + a.M33 * b.M32 + a.M34 * b.M42;
            FixFloat64 m11 = a.M31 * b.M13 + a.M32 * b.M23 + a.M33 * b.M33 + a.M34 * b.M43;
            FixFloat64 m12 = a.M31 * b.M14 + a.M32 * b.M24 + a.M33 * b.M34 + a.M34 * b.M44;
            FixFloat64 m13 = a.M41 * b.M11 + a.M42 * b.M21 + a.M43 * b.M31 + a.M44 * b.M41;
            FixFloat64 m14 = a.M41 * b.M12 + a.M42 * b.M22 + a.M43 * b.M32 + a.M44 * b.M42;
            FixFloat64 m15 = a.M41 * b.M13 + a.M42 * b.M23 + a.M43 * b.M33 + a.M44 * b.M43;
            FixFloat64 m16 = a.M41 * b.M14 + a.M42 * b.M24 + a.M43 * b.M34 + a.M44 * b.M44;
            result.M11 = m;
            result.M12 = m2;
            result.M13 = m3;
            result.M14 = m4;
            result.M21 = m5;
            result.M22 = m6;
            result.M23 = m7;
            result.M24 = m8;
            result.M31 = m9;
            result.M32 = m10;
            result.M33 = m11;
            result.M34 = m12;
            result.M41 = m13;
            result.M42 = m14;
            result.M43 = m15;
            result.M44 = m16;
        }

        /// <summary>
        /// Multiplies two matrices together.
        /// </summary>
        /// <param name="a">First matrix to multiply.</param>
        /// <param name="b">Second matrix to multiply.</param>
        /// <returns>Combined transformation.</returns>
        public static FixMatrix Multiply(FixMatrix a, FixMatrix b)
        {
            Multiply(ref a, ref b, out var result);
            return result;
        }

        /// <summary>
        /// Scales all components of the matrix.
        /// </summary>
        /// <param name="matrix">FixMatrix to scale.</param>
        /// <param name="scale">Amount to scale.</param>
        /// <param name="result">Scaled matrix.</param>
        public static void Multiply(ref FixMatrix matrix, FixFloat64 scale, out FixMatrix result)
        {
            result.M11 = matrix.M11 * scale;
            result.M12 = matrix.M12 * scale;
            result.M13 = matrix.M13 * scale;
            result.M14 = matrix.M14 * scale;
            result.M21 = matrix.M21 * scale;
            result.M22 = matrix.M22 * scale;
            result.M23 = matrix.M23 * scale;
            result.M24 = matrix.M24 * scale;
            result.M31 = matrix.M31 * scale;
            result.M32 = matrix.M32 * scale;
            result.M33 = matrix.M33 * scale;
            result.M34 = matrix.M34 * scale;
            result.M41 = matrix.M41 * scale;
            result.M42 = matrix.M42 * scale;
            result.M43 = matrix.M43 * scale;
            result.M44 = matrix.M44 * scale;
        }

        /// <summary>
        /// Multiplies two matrices together.
        /// </summary>
        /// <param name="a">First matrix to multiply.</param>
        /// <param name="b">Second matrix to multiply.</param>
        /// <returns>Combined transformation.</returns>
        public static FixMatrix operator *(FixMatrix a, FixMatrix b)
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
        public static FixMatrix operator *(FixMatrix m, FixFloat64 f)
        {
            Multiply(ref m, f, out var result);
            return result;
        }

        public static bool operator ==(FixMatrix lhs, FixMatrix rhs)
        {
            return lhs.GetColumn(0) == rhs.GetColumn(0) && lhs.GetColumn(1) == rhs.GetColumn(1) && lhs.GetColumn(2) == rhs.GetColumn(2) && lhs.GetColumn(3) == rhs.GetColumn(3);
        }

        public static bool operator !=(FixMatrix lhs, FixMatrix rhs)
        {
            return !(lhs == rhs);
        }

        public FixVector4 GetColumn(int index)
        {
            return index switch
            {
                0 => new FixVector4(M11, M21, M31, M41),
                1 => new FixVector4(M12, M22, M32, M42),
                2 => new FixVector4(M13, M23, M33, M43),
                3 => new FixVector4(M14, M24, M34, M44),
                _ => throw new IndexOutOfRangeException("Invalid column index!"),
            };
        }

        public FixVector3 MultiplyPoint3x4(FixVector3 point)
        {
            FixVector3 result = default(FixVector3);
            result.X = M11 * point.x + M12 * point.y + M13 * point.z + M14;
            result.Y = M21 * point.x + M22 * point.y + M23 * point.z + M24;
            result.Z = M31 * point.x + M32 * point.y + M33 * point.z + M34;
            return result;
        }

        /// <summary>
        /// Scales all components of the matrix by the given value.
        /// </summary>
        /// <param name="m">First matrix to multiply.</param>
        /// <param name="f">Scaling value to apply to all components of the matrix.</param>
        /// <returns>Product of the multiplication.</returns>
        public static FixMatrix operator *(FixFloat64 f, FixMatrix m)
        {
            Multiply(ref m, f, out var result);
            return result;
        }

        /// <summary>
        /// Transforms a vector using a matrix.
        /// </summary>
        /// <param name="v">Vector to transform.</param>
        /// <param name="matrix">Transform to apply to the vector.</param>
        /// <param name="result">Transformed vector.</param>
        public static void Transform(ref FixVector4 v, ref FixMatrix matrix, out FixVector4 result)
        {
            FixFloat64 x = v.X;
            FixFloat64 y = v.Y;
            FixFloat64 z = v.Z;
            FixFloat64 w = v.W;
            result.X = x * matrix.M11 + y * matrix.M21 + z * matrix.M31 + w * matrix.M41;
            result.Y = x * matrix.M12 + y * matrix.M22 + z * matrix.M32 + w * matrix.M42;
            result.Z = x * matrix.M13 + y * matrix.M23 + z * matrix.M33 + w * matrix.M43;
            result.W = x * matrix.M14 + y * matrix.M24 + z * matrix.M34 + w * matrix.M44;
        }

        /// <summary>
        /// Transforms a vector using a matrix.
        /// </summary>
        /// <param name="v">Vector to transform.</param>
        /// <param name="matrix">Transform to apply to the vector.</param>
        /// <returns>Transformed vector.</returns>
        public static FixVector4 Transform(FixVector4 v, FixMatrix matrix)
        {
            Transform(ref v, ref matrix, out var result);
            return result;
        }

        /// <summary>
        /// Transforms a vector using the transpose of a matrix.
        /// </summary>
        /// <param name="v">Vector to transform.</param>
        /// <param name="matrix">Transform to tranpose and apply to the vector.</param>
        /// <param name="result">Transformed vector.</param>
        public static void TransformTranspose(ref FixVector4 v, ref FixMatrix matrix, out FixVector4 result)
        {
            FixFloat64 x = v.X;
            FixFloat64 y = v.Y;
            FixFloat64 z = v.Z;
            FixFloat64 w = v.W;
            result.X = x * matrix.M11 + y * matrix.M12 + z * matrix.M13 + w * matrix.M14;
            result.Y = x * matrix.M21 + y * matrix.M22 + z * matrix.M23 + w * matrix.M24;
            result.Z = x * matrix.M31 + y * matrix.M32 + z * matrix.M33 + w * matrix.M34;
            result.W = x * matrix.M41 + y * matrix.M42 + z * matrix.M43 + w * matrix.M44;
        }

        /// <summary>
        /// Transforms a vector using the transpose of a matrix.
        /// </summary>
        /// <param name="v">Vector to transform.</param>
        /// <param name="matrix">Transform to tranpose and apply to the vector.</param>
        /// <returns>Transformed vector.</returns>
        public static FixVector4 TransformTranspose(FixVector4 v, FixMatrix matrix)
        {
            TransformTranspose(ref v, ref matrix, out var result);
            return result;
        }

        /// <summary>
        /// Transforms a vector using a matrix.
        /// </summary>
        /// <param name="v">Vector to transform.</param>
        /// <param name="matrix">Transform to apply to the vector.</param>
        /// <param name="result">Transformed vector.</param>
        public static void Transform(ref FixVector3 v, ref FixMatrix matrix, out FixVector4 result)
        {
            result.X = v.X * matrix.M11 + v.Y * matrix.M21 + v.Z * matrix.M31 + matrix.M41;
            result.Y = v.X * matrix.M12 + v.Y * matrix.M22 + v.Z * matrix.M32 + matrix.M42;
            result.Z = v.X * matrix.M13 + v.Y * matrix.M23 + v.Z * matrix.M33 + matrix.M43;
            result.W = v.X * matrix.M14 + v.Y * matrix.M24 + v.Z * matrix.M34 + matrix.M44;
        }

        /// <summary>
        /// Transforms a vector using a matrix.
        /// </summary>
        /// <param name="v">Vector to transform.</param>
        /// <param name="matrix">Transform to apply to the vector.</param>
        /// <returns>Transformed vector.</returns>
        public static FixVector4 Transform(FixVector3 v, FixMatrix matrix)
        {
            Transform(ref v, ref matrix, out FixVector4 result);
            return result;
        }

        /// <summary>
        /// Transforms a vector using the transpose of a matrix.
        /// </summary>
        /// <param name="v">Vector to transform.</param>
        /// <param name="matrix">Transform to tranpose and apply to the vector.</param>
        /// <param name="result">Transformed vector.</param>
        public static void TransformTranspose(ref FixVector3 v, ref FixMatrix matrix, out FixVector4 result)
        {
            result.X = v.X * matrix.M11 + v.Y * matrix.M12 + v.Z * matrix.M13 + matrix.M14;
            result.Y = v.X * matrix.M21 + v.Y * matrix.M22 + v.Z * matrix.M23 + matrix.M24;
            result.Z = v.X * matrix.M31 + v.Y * matrix.M32 + v.Z * matrix.M33 + matrix.M34;
            result.W = v.X * matrix.M41 + v.Y * matrix.M42 + v.Z * matrix.M43 + matrix.M44;
        }

        /// <summary>
        /// Transforms a vector using the transpose of a matrix.
        /// </summary>
        /// <param name="v">Vector to transform.</param>
        /// <param name="matrix">Transform to tranpose and apply to the vector.</param>
        /// <returns>Transformed vector.</returns>
        public static FixVector4 TransformTranspose(FixVector3 v, FixMatrix matrix)
        {
            TransformTranspose(ref v, ref matrix, out FixVector4 result);
            return result;
        }

        /// <summary>
        /// Transforms a vector using a matrix.
        /// </summary>
        /// <param name="v">Vector to transform.</param>
        /// <param name="matrix">Transform to apply to the vector.</param>
        /// <param name="result">Transformed vector.</param>
        public static void Transform(ref FixVector3 v, ref FixMatrix matrix, out FixVector3 result)
        {
            FixFloat64 x = v.X;
            FixFloat64 y = v.Y;
            FixFloat64 z = v.Z;
            result.X = x * matrix.M11 + y * matrix.M21 + z * matrix.M31 + matrix.M41;
            result.Y = x * matrix.M12 + y * matrix.M22 + z * matrix.M32 + matrix.M42;
            result.Z = x * matrix.M13 + y * matrix.M23 + z * matrix.M33 + matrix.M43;
        }

        /// <summary>
        /// Transforms a vector using the transpose of a matrix.
        /// </summary>
        /// <param name="v">Vector to transform.</param>
        /// <param name="matrix">Transform to tranpose and apply to the vector.</param>
        /// <param name="result">Transformed vector.</param>
        public static void TransformTranspose(ref FixVector3 v, ref FixMatrix matrix, out FixVector3 result)
        {
            FixFloat64 x = v.X;
            FixFloat64 y = v.Y;
            FixFloat64 z = v.Z;
            result.X = x * matrix.M11 + y * matrix.M12 + z * matrix.M13 + matrix.M14;
            result.Y = x * matrix.M21 + y * matrix.M22 + z * matrix.M23 + matrix.M24;
            result.Z = x * matrix.M31 + y * matrix.M32 + z * matrix.M33 + matrix.M34;
        }

        /// <summary>
        /// Transforms a vector using a matrix.
        /// </summary>
        /// <param name="v">Vector to transform.</param>
        /// <param name="matrix">Transform to apply to the vector.</param>
        /// <param name="result">Transformed vector.</param>
        public static void TransformNormal(ref FixVector3 v, ref FixMatrix matrix, out FixVector3 result)
        {
            FixFloat64 x = v.X;
            FixFloat64 y = v.Y;
            FixFloat64 z = v.Z;
            result.X = x * matrix.M11 + y * matrix.M21 + z * matrix.M31;
            result.Y = x * matrix.M12 + y * matrix.M22 + z * matrix.M32;
            result.Z = x * matrix.M13 + y * matrix.M23 + z * matrix.M33;
        }

        /// <summary>
        /// Transforms a vector using a matrix.
        /// </summary>
        /// <param name="v">Vector to transform.</param>
        /// <param name="matrix">Transform to apply to the vector.</param>
        /// <returns>Transformed vector.</returns>
        public static FixVector3 TransformNormal(FixVector3 v, FixMatrix matrix)
        {
            TransformNormal(ref v, ref matrix, out var result);
            return result;
        }

        /// <summary>
        /// Transforms a vector using the transpose of a matrix.
        /// </summary>
        /// <param name="v">Vector to transform.</param>
        /// <param name="matrix">Transform to tranpose and apply to the vector.</param>
        /// <param name="result">Transformed vector.</param>
        public static void TransformNormalTranspose(ref FixVector3 v, ref FixMatrix matrix, out FixVector3 result)
        {
            FixFloat64 x = v.X;
            FixFloat64 y = v.Y;
            FixFloat64 z = v.Z;
            result.X = x * matrix.M11 + y * matrix.M12 + z * matrix.M13;
            result.Y = x * matrix.M21 + y * matrix.M22 + z * matrix.M23;
            result.Z = x * matrix.M31 + y * matrix.M32 + z * matrix.M33;
        }

        /// <summary>
        /// Transforms a vector using the transpose of a matrix.
        /// </summary>
        /// <param name="v">Vector to transform.</param>
        /// <param name="matrix">Transform to tranpose and apply to the vector.</param>
        /// <returns>Transformed vector.</returns>
        public static FixVector3 TransformNormalTranspose(FixVector3 v, FixMatrix matrix)
        {
            TransformNormalTranspose(ref v, ref matrix, out var result);
            return result;
        }

        /// <summary>
        /// Transposes the matrix.
        /// </summary>
        /// <param name="m">FixMatrix to transpose.</param>
        /// <param name="transposed">FixMatrix to transpose.</param>
        public static void Transpose(ref FixMatrix m, out FixMatrix transposed)
        {
            FixFloat64 m2 = m.M12;
            transposed.M12 = m.M21;
            transposed.M21 = m2;
            m2 = m.M13;
            transposed.M13 = m.M31;
            transposed.M31 = m2;
            m2 = m.M14;
            transposed.M14 = m.M41;
            transposed.M41 = m2;
            m2 = m.M23;
            transposed.M23 = m.M32;
            transposed.M32 = m2;
            m2 = m.M24;
            transposed.M24 = m.M42;
            transposed.M42 = m2;
            m2 = m.M34;
            transposed.M34 = m.M43;
            transposed.M43 = m2;
            transposed.M11 = m.M11;
            transposed.M22 = m.M22;
            transposed.M33 = m.M33;
            transposed.M44 = m.M44;
        }

        /// <summary>
        /// Inverts the matrix.
        /// </summary>
        /// <param name="m">FixMatrix to invert.</param>
        /// <param name="inverted">Inverted version of the matrix.</param>
        public static void Invert(ref FixMatrix m, out FixMatrix inverted)
        {
            FixMatrix4x8.Invert(ref m, out inverted);
        }

        /// <summary>
        /// Inverts the matrix.
        /// </summary>
        /// <param name="m">FixMatrix to invert.</param>
        /// <returns>Inverted version of the matrix.</returns>
        public static FixMatrix Invert(FixMatrix m)
        {
            Invert(ref m, out var inverted);
            return inverted;
        }

        /// <summary>
        /// Inverts the matrix using a process that only works for rigid transforms.
        /// </summary>
        /// <param name="m">FixMatrix to invert.</param>
        /// <param name="inverted">Inverted version of the matrix.</param>
        public static void InvertRigid(ref FixMatrix m, out FixMatrix inverted)
        {
            FixFloat64 m2 = m.M12;
            inverted.M12 = m.M21;
            inverted.M21 = m2;
            m2 = m.M13;
            inverted.M13 = m.M31;
            inverted.M31 = m2;
            m2 = m.M23;
            inverted.M23 = m.M32;
            inverted.M32 = m2;
            inverted.M11 = m.M11;
            inverted.M22 = m.M22;
            inverted.M33 = m.M33;
            FixFloat64 m3 = m.M41;
            FixFloat64 m4 = m.M42;
            FixFloat64 m5 = m.M43;
            inverted.M41 = -(m3 * inverted.M11 + m4 * inverted.M21 + m5 * inverted.M31);
            inverted.M42 = -(m3 * inverted.M12 + m4 * inverted.M22 + m5 * inverted.M32);
            inverted.M43 = -(m3 * inverted.M13 + m4 * inverted.M23 + m5 * inverted.M33);
            inverted.M14 = FixFloat64.C0;
            inverted.M24 = FixFloat64.C0;
            inverted.M34 = FixFloat64.C0;
            inverted.M44 = FixFloat64.C1;
        }

        /// <summary>
        /// Inverts the matrix using a process that only works for rigid transforms.
        /// </summary>
        /// <param name="m">FixMatrix to invert.</param>
        /// <returns>Inverted version of the matrix.</returns>
        public static FixMatrix InvertRigid(FixMatrix m)
        {
            InvertRigid(ref m, out var inverted);
            return inverted;
        }

        /// <summary>
        /// Creates a right handed orthographic projection.
        /// </summary>
        /// <param name="left">Leftmost coordinate of the projected area.</param>
        /// <param name="right">Rightmost coordinate of the projected area.</param>
        /// <param name="bottom">Bottom coordinate of the projected area.</param>
        /// <param name="top">Top coordinate of the projected area.</param>
        /// <param name="zNear">Near plane of the projection.</param>
        /// <param name="zFar">Far plane of the projection.</param>
        /// <param name="projection">The resulting orthographic projection matrix.</param>
        public static void CreateOrthographicRH(FixFloat64 left, FixFloat64 right, FixFloat64 bottom, FixFloat64 top, FixFloat64 zNear, FixFloat64 zFar, out FixMatrix projection)
        {
            FixFloat64 fix = right - left;
            FixFloat64 fix2 = top - bottom;
            FixFloat64 fix3 = zFar - zNear;
            projection.M11 = FixFloat64.C2 / fix;
            projection.M12 = FixFloat64.C0;
            projection.M13 = FixFloat64.C0;
            projection.M14 = FixFloat64.C0;
            projection.M21 = FixFloat64.C0;
            projection.M22 = FixFloat64.C2 / fix2;
            projection.M23 = FixFloat64.C0;
            projection.M24 = FixFloat64.C0;
            projection.M31 = FixFloat64.C0;
            projection.M32 = FixFloat64.C0;
            projection.M33 = (FixFloat64)(-1) / fix3;
            projection.M34 = FixFloat64.C0;
            projection.M41 = (left + right) / -fix;
            projection.M42 = (top + bottom) / -fix2;
            projection.M43 = zNear / -fix3;
            projection.M44 = FixFloat64.C1;
        }

        /// <summary>
        /// Creates a right-handed perspective matrix.
        /// </summary>
        /// <param name="fieldOfView">Field of view of the perspective in radians.</param>
        /// <param name="aspectRatio">Width of the viewport over the height of the viewport.</param>
        /// <param name="nearClip">Near clip plane of the perspective.</param>
        /// <param name="farClip">Far clip plane of the perspective.</param>
        /// <param name="perspective">Resulting perspective matrix.</param>
        public static void CreatePerspectiveFieldOfViewRH(FixFloat64 fieldOfView, FixFloat64 aspectRatio, FixFloat64 nearClip, FixFloat64 farClip, out FixMatrix perspective)
        {
            FixFloat64 fix = FixFloat64.C1 / FixFloat64.Tan(fieldOfView / FixFloat64.C2);
            FixFloat64 fix2 = (perspective.M11 = fix / aspectRatio);
            perspective.M12 = FixFloat64.C0;
            perspective.M13 = FixFloat64.C0;
            perspective.M14 = FixFloat64.C0;
            perspective.M21 = FixFloat64.C0;
            perspective.M22 = fix;
            perspective.M23 = FixFloat64.C0;
            perspective.M24 = FixFloat64.C0;
            perspective.M31 = FixFloat64.C0;
            perspective.M32 = FixFloat64.C0;
            perspective.M33 = farClip / (nearClip - farClip);
            perspective.M34 = (FixFloat64)(-1);
            perspective.M41 = FixFloat64.C0;
            perspective.M42 = FixFloat64.C0;
            perspective.M44 = FixFloat64.C0;
            perspective.M43 = nearClip * perspective.M33;
        }

        /// <summary>
        /// Creates a right-handed perspective matrix.
        /// </summary>
        /// <param name="fieldOfView">Field of view of the perspective in radians.</param>
        /// <param name="aspectRatio">Width of the viewport over the height of the viewport.</param>
        /// <param name="nearClip">Near clip plane of the perspective.</param>
        /// <param name="farClip">Far clip plane of the perspective.</param>
        /// <returns>Resulting perspective matrix.</returns>
        public static FixMatrix CreatePerspectiveFieldOfViewRH(FixFloat64 fieldOfView, FixFloat64 aspectRatio, FixFloat64 nearClip, FixFloat64 farClip)
        {
            CreatePerspectiveFieldOfViewRH(fieldOfView, aspectRatio, nearClip, farClip, out var perspective);
            return perspective;
        }

        /// <summary>
        /// Creates a view matrix pointing from a position to a target with the given up vector.
        /// </summary>
        /// <param name="position">Position of the camera.</param>
        /// <param name="target">Target of the camera.</param>
        /// <param name="upVector">Up vector of the camera.</param>
        /// <param name="viewMatrix">Look at matrix.</param>
        public static void CreateLookAtRH(ref FixVector3 position, ref FixVector3 target, ref FixVector3 upVector, out FixMatrix viewMatrix)
        {
            FixVector3.Subtract(ref target, ref position, out var difference);
            CreateViewRH(ref position, ref difference, ref upVector, out viewMatrix);
        }

        /// <summary>
        /// Creates a view matrix pointing from a position to a target with the given up vector.
        /// </summary>
        /// <param name="position">Position of the camera.</param>
        /// <param name="target">Target of the camera.</param>
        /// <param name="upVector">Up vector of the camera.</param>
        /// <returns>Look at matrix.</returns>
        public static FixMatrix CreateLookAtRH(FixVector3 position, FixVector3 target, FixVector3 upVector)
        {
            FixVector3.Subtract(ref target, ref position, out var difference);
            CreateViewRH(ref position, ref difference, ref upVector, out var viewMatrix);
            return viewMatrix;
        }

        /// <summary>
        /// Creates a view matrix pointing in a direction with a given up vector.
        /// </summary>
        /// <param name="position">Position of the camera.</param>
        /// <param name="forward">Forward direction of the camera.</param>
        /// <param name="upVector">Up vector of the camera.</param>
        /// <param name="viewMatrix">Look at matrix.</param>
        public static void CreateViewRH(ref FixVector3 position, ref FixVector3 forward, ref FixVector3 upVector, out FixMatrix viewMatrix)
        {
            FixFloat64 fix = forward.Length();
            FixVector3.Divide(ref forward, -fix, out var result);
            FixVector3.Cross(ref upVector, ref result, out var result2);
            result2.Normalize();
            FixVector3.Cross(ref result, ref result2, out var result3);
            viewMatrix.M11 = result2.X;
            viewMatrix.M12 = result3.X;
            viewMatrix.M13 = result.X;
            viewMatrix.M14 = FixFloat64.C0;
            viewMatrix.M21 = result2.Y;
            viewMatrix.M22 = result3.Y;
            viewMatrix.M23 = result.Y;
            viewMatrix.M24 = FixFloat64.C0;
            viewMatrix.M31 = result2.Z;
            viewMatrix.M32 = result3.Z;
            viewMatrix.M33 = result.Z;
            viewMatrix.M34 = FixFloat64.C0;
            FixVector3.Dot(ref result2, ref position, out viewMatrix.M41);
            FixVector3.Dot(ref result3, ref position, out viewMatrix.M42);
            FixVector3.Dot(ref result, ref position, out viewMatrix.M43);
            viewMatrix.M41 = -viewMatrix.M41;
            viewMatrix.M42 = -viewMatrix.M42;
            viewMatrix.M43 = -viewMatrix.M43;
            viewMatrix.M44 = FixFloat64.C1;
        }

        /// <summary>
        /// Creates a view matrix pointing looking in a direction with a given up vector.
        /// </summary>
        /// <param name="position">Position of the camera.</param>
        /// <param name="forward">Forward direction of the camera.</param>
        /// <param name="upVector">Up vector of the camera.</param>
        /// <returns>Look at matrix.</returns>
        public static FixMatrix CreateViewRH(FixVector3 position, FixVector3 forward, FixVector3 upVector)
        {
            CreateViewRH(ref position, ref forward, ref upVector, out var viewMatrix);
            return viewMatrix;
        }

        /// <summary>
        /// Creates a world matrix pointing from a position to a target with the given up vector.
        /// </summary>
        /// <param name="position">Position of the transform.</param>
        /// <param name="forward">Forward direction of the transformation.</param>
        /// <param name="upVector">Up vector which is crossed against the forward vector to compute the transform's basis.</param>
        /// <param name="worldMatrix">World matrix.</param>
        public static void CreateWorldRH(ref FixVector3 position, ref FixVector3 forward, ref FixVector3 upVector, out FixMatrix worldMatrix)
        {
            FixFloat64 fix = forward.Length();
            FixVector3.Divide(ref forward, -fix, out var result);
            FixVector3.Cross(ref upVector, ref result, out var result2);
            result2.Normalize();
            FixVector3.Cross(ref result, ref result2, out var result3);
            worldMatrix.M11 = result2.X;
            worldMatrix.M12 = result2.Y;
            worldMatrix.M13 = result2.Z;
            worldMatrix.M14 = FixFloat64.C0;
            worldMatrix.M21 = result3.X;
            worldMatrix.M22 = result3.Y;
            worldMatrix.M23 = result3.Z;
            worldMatrix.M24 = FixFloat64.C0;
            worldMatrix.M31 = result.X;
            worldMatrix.M32 = result.Y;
            worldMatrix.M33 = result.Z;
            worldMatrix.M34 = FixFloat64.C0;
            worldMatrix.M41 = position.X;
            worldMatrix.M42 = position.Y;
            worldMatrix.M43 = position.Z;
            worldMatrix.M44 = FixFloat64.C1;
        }

        /// <summary>
        /// Creates a world matrix pointing from a position to a target with the given up vector.
        /// </summary>
        /// <param name="position">Position of the transform.</param>
        /// <param name="forward">Forward direction of the transformation.</param>
        /// <param name="upVector">Up vector which is crossed against the forward vector to compute the transform's basis.</param>
        /// <returns>World matrix.</returns>
        public static FixMatrix CreateWorldRH(FixVector3 position, FixVector3 forward, FixVector3 upVector)
        {
            CreateWorldRH(ref position, ref forward, ref upVector, out var worldMatrix);
            return worldMatrix;
        }

        /// <summary>
        /// Creates a matrix representing a translation.
        /// </summary>
        /// <param name="translation">Translation to be represented by the matrix.</param>
        /// <param name="translationMatrix">FixMatrix representing the given translation.</param>
        public static void CreateTranslation(ref FixVector3 translation, out FixMatrix translationMatrix)
        {
            translationMatrix = new FixMatrix
            {
                M11 = FixFloat64.C1,
                M22 = FixFloat64.C1,
                M33 = FixFloat64.C1,
                M44 = FixFloat64.C1,
                M41 = translation.X,
                M42 = translation.Y,
                M43 = translation.Z
            };
        }

        /// <summary>
        /// Creates a matrix representing a translation.
        /// </summary>
        /// <param name="translation">Translation to be represented by the matrix.</param>
        /// <returns>FixMatrix representing the given translation.</returns>
        public static FixMatrix CreateTranslation(FixVector3 translation)
        {
            CreateTranslation(ref translation, out var translationMatrix);
            return translationMatrix;
        }

        /// <summary>
        /// Creates a matrix representing the given axis aligned scale.
        /// </summary>
        /// <param name="scale">Scale to be represented by the matrix.</param>
        /// <param name="scaleMatrix">FixMatrix representing the given scale.</param>
        public static void CreateScale(ref FixVector3 scale, out FixMatrix scaleMatrix)
        {
            scaleMatrix = new FixMatrix
            {
                M11 = scale.X,
                M22 = scale.Y,
                M33 = scale.Z,
                M44 = FixFloat64.C1
            };
        }

        /// <summary>
        /// Creates a matrix representing the given axis aligned scale.
        /// </summary>
        /// <param name="scale">Scale to be represented by the matrix.</param>
        /// <returns>FixMatrix representing the given scale.</returns>
        public static FixMatrix CreateScale(FixVector3 scale)
        {
            CreateScale(ref scale, out var scaleMatrix);
            return scaleMatrix;
        }

        /// <summary>
        /// Creates a matrix representing the given axis aligned scale.
        /// </summary>
        /// <param name="x">Scale along the x axis.</param>
        /// <param name="y">Scale along the y axis.</param>
        /// <param name="z">Scale along the z axis.</param>
        /// <param name="scaleMatrix">FixMatrix representing the given scale.</param>
        public static void CreateScale(FixFloat64 x, FixFloat64 y, FixFloat64 z, out FixMatrix scaleMatrix)
        {
            scaleMatrix = new FixMatrix
            {
                M11 = x,
                M22 = y,
                M33 = z,
                M44 = FixFloat64.C1
            };
        }

        /// <summary>
        /// Creates a matrix representing the given axis aligned scale.
        /// </summary>
        /// <param name="x">Scale along the x axis.</param>
        /// <param name="y">Scale along the y axis.</param>
        /// <param name="z">Scale along the z axis.</param>
        /// <returns>FixMatrix representing the given scale.</returns>
        public static FixMatrix CreateScale(FixFloat64 x, FixFloat64 y, FixFloat64 z)
        {
            CreateScale(x, y, z, out var scaleMatrix);
            return scaleMatrix;
        }

        /// <summary>
        /// Creates a string representation of the matrix.
        /// </summary>
        /// <returns>A string representation of the matrix.</returns>
        public override string ToString()
        {
            return string.Concat("{", M11, ", ", M12, ", ", M13, ", ", M14, "} {", M21, ", ", M22, ", ", M23, ", ", M24, "} {", M31, ", ", M32, ", ", M33, ", ", M34, "} {", M41, ", ", M42, ", ", M43, ", ", M44, "}");
        }
    }
}