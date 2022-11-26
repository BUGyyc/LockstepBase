/*
 * @Author: delevin.ying 
 * @Date: 2022-11-26 15:31:38 
 * @Last Modified by: delevin.ying
 * @Last Modified time: 2022-11-26 16:04:20
 */

using System;


namespace FixMath
{

    /// <summary>
    /// Provides XNA-like quaternion support.
    /// </summary>
    public struct FixQuaternion : IEquatable<FixQuaternion>
    {
        /// <summary>
        /// X component of the quaternion.
        /// </summary>
        public FixFloat64 X;

        /// <summary>
        /// Y component of the quaternion.
        /// </summary>
        public FixFloat64 Y;

        /// <summary>
        /// Z component of the quaternion.
        /// </summary>
        public FixFloat64 Z;

        /// <summary>
        /// W component of the quaternion.
        /// </summary>
        public FixFloat64 W;

        /// <summary>
        /// FixQuaternion representing the identity transform.
        /// </summary>
        public static FixQuaternion Identity => new FixQuaternion(FixFloat64Util.C0, FixFloat64Util.C0, FixFloat64Util.C0, FixFloat64Util.C1);

        /// <summary>
        /// Constructs a new FixQuaternion.
        /// </summary>
        /// <param name="x">X component of the quaternion.</param>
        /// <param name="y">Y component of the quaternion.</param>
        /// <param name="z">Z component of the quaternion.</param>
        /// <param name="w">W component of the quaternion.</param>
        public FixQuaternion(FixFloat64 x, FixFloat64 y, FixFloat64 z, FixFloat64 w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        /// <summary>
        /// Adds two quaternions together.
        /// </summary>
        /// <param name="a">First quaternion to add.</param>
        /// <param name="b">Second quaternion to add.</param>
        /// <param name="result">Sum of the addition.</param>
        public static void Add(ref FixQuaternion a, ref FixQuaternion b, out FixQuaternion result)
        {
            result.X = a.X + b.X;
            result.Y = a.Y + b.Y;
            result.Z = a.Z + b.Z;
            result.W = a.W + b.W;
        }

        /// <summary>
        /// Multiplies two quaternions.
        /// </summary>
        /// <param name="a">First quaternion to multiply.</param>
        /// <param name="b">Second quaternion to multiply.</param>
        /// <param name="result">Product of the multiplication.</param>
        public static void Multiply(ref FixQuaternion a, ref FixQuaternion b, out FixQuaternion result)
        {
            FixFloat64 x = a.X;
            FixFloat64 y = a.Y;
            FixFloat64 z = a.Z;
            FixFloat64 w = a.W;
            FixFloat64 x2 = b.X;
            FixFloat64 y2 = b.Y;
            FixFloat64 z2 = b.Z;
            FixFloat64 w2 = b.W;
            result.X = x * w2 + x2 * w + y * z2 - z * y2;
            result.Y = y * w2 + y2 * w + z * x2 - x * z2;
            result.Z = z * w2 + z2 * w + x * y2 - y * x2;
            result.W = w * w2 - x * x2 - y * y2 - z * z2;
        }

        /// <summary>
        /// Scales a quaternion.
        /// </summary>
        /// <param name="q">FixQuaternion to multiply.</param>
        /// <param name="scale">Amount to multiply each component of the quaternion by.</param>
        /// <param name="result">Scaled quaternion.</param>
        public static void Multiply(ref FixQuaternion q, FixFloat64 scale, out FixQuaternion result)
        {
            result.X = q.X * scale;
            result.Y = q.Y * scale;
            result.Z = q.Z * scale;
            result.W = q.W * scale;
        }

        /// <summary>
        /// Multiplies two quaternions together in opposite order.
        /// </summary>
        /// <param name="a">First quaternion to multiply.</param>
        /// <param name="b">Second quaternion to multiply.</param>
        /// <param name="result">Product of the multiplication.</param>
        public static void Concatenate(ref FixQuaternion a, ref FixQuaternion b, out FixQuaternion result)
        {
            FixFloat64 x = a.X;
            FixFloat64 y = a.Y;
            FixFloat64 z = a.Z;
            FixFloat64 w = a.W;
            FixFloat64 x2 = b.X;
            FixFloat64 y2 = b.Y;
            FixFloat64 z2 = b.Z;
            FixFloat64 w2 = b.W;
            result.X = w * x2 + x * w2 + z * y2 - y * z2;
            result.Y = w * y2 + y * w2 + x * z2 - z * x2;
            result.Z = w * z2 + z * w2 + y * x2 - x * y2;
            result.W = w * w2 - x * x2 - y * y2 - z * z2;
        }

        /// <summary>
        /// Multiplies two quaternions together in opposite order.
        /// </summary>
        /// <param name="a">First quaternion to multiply.</param>
        /// <param name="b">Second quaternion to multiply.</param>
        /// <returns>Product of the multiplication.</returns>
        public static FixQuaternion Concatenate(FixQuaternion a, FixQuaternion b)
        {
            Concatenate(ref a, ref b, out var result);
            return result;
        }

        /// <summary>
        /// Constructs a quaternion from a rotation matrix.
        /// </summary>
        /// <param name="r">Rotation matrix to create the quaternion from.</param>
        /// <param name="q">FixQuaternion based on the rotation matrix.</param>
        public static void CreateFromRotationMatrix(ref FixMatrix3x3 r, out FixQuaternion q)
        {
            FixFloat64 fix = r.M11 + r.M22 + r.M33;
            if (fix >= FixFloat64Util.C0)
            {
                FixFloat64 fix2 = FixFloat64.Sqrt(fix + FixFloat64Util.C1) * FixFloat64Util.C2;
                FixFloat64 fix3 = FixFloat64Util.C1 / fix2;
                q.W = FixFloat64Util.C0p25 * fix2;
                q.X = (r.M23 - r.M32) * fix3;
                q.Y = (r.M31 - r.M13) * fix3;
                q.Z = (r.M12 - r.M21) * fix3;
            }
            else if ((r.M11 > r.M22) & (r.M11 > r.M33))
            {
                FixFloat64 fix4 = FixFloat64.Sqrt(FixFloat64Util.C1 + r.M11 - r.M22 - r.M33) * FixFloat64Util.C2;
                FixFloat64 fix5 = FixFloat64Util.C1 / fix4;
                q.W = (r.M23 - r.M32) * fix5;
                q.X = FixFloat64Util.C0p25 * fix4;
                q.Y = (r.M21 + r.M12) * fix5;
                q.Z = (r.M31 + r.M13) * fix5;
            }
            else if (r.M22 > r.M33)
            {
                FixFloat64 fix6 = FixFloat64.Sqrt(FixFloat64Util.C1 + r.M22 - r.M11 - r.M33) * FixFloat64Util.C2;
                FixFloat64 fix7 = FixFloat64Util.C1 / fix6;
                q.W = (r.M31 - r.M13) * fix7;
                q.X = (r.M21 + r.M12) * fix7;
                q.Y = FixFloat64Util.C0p25 * fix6;
                q.Z = (r.M32 + r.M23) * fix7;
            }
            else
            {
                FixFloat64 fix8 = FixFloat64.Sqrt(FixFloat64Util.C1 + r.M33 - r.M11 - r.M22) * FixFloat64Util.C2;
                FixFloat64 fix9 = FixFloat64Util.C1 / fix8;
                q.W = (r.M12 - r.M21) * fix9;
                q.X = (r.M31 + r.M13) * fix9;
                q.Y = (r.M32 + r.M23) * fix9;
                q.Z = FixFloat64Util.C0p25 * fix8;
            }
        }

        /// <summary>
        /// Creates a quaternion from a rotation matrix.
        /// </summary>
        /// <param name="r">Rotation matrix used to create a new quaternion.</param>
        /// <returns>FixQuaternion representing the same rotation as the matrix.</returns>
        public static FixQuaternion CreateFromRotationMatrix(FixMatrix3x3 r)
        {
            CreateFromRotationMatrix(ref r, out var q);
            return q;
        }

        /// <summary>
        /// Constructs a quaternion from a rotation matrix.
        /// </summary>
        /// <param name="r">Rotation matrix to create the quaternion from.</param>
        /// <param name="q">FixQuaternion based on the rotation matrix.</param>
        public static void CreateFromRotationMatrix(ref FixMatrix r, out FixQuaternion q)
        {
            FixMatrix3x3.CreateFromMatrix(ref r, out var matrix3X);
            CreateFromRotationMatrix(ref matrix3X, out q);
        }

        /// <summary>
        /// Creates a quaternion from a rotation matrix.
        /// </summary>
        /// <param name="r">Rotation matrix used to create a new quaternion.</param>
        /// <returns>FixQuaternion representing the same rotation as the matrix.</returns>
        public static FixQuaternion CreateFromRotationMatrix(FixMatrix r)
        {
            CreateFromRotationMatrix(ref r, out var q);
            return q;
        }

        /// <summary>
        /// Ensures the quaternion has unit length.
        /// </summary>
        /// <param name="quaternion">FixQuaternion to normalize.</param>
        /// <returns>Normalized quaternion.</returns>
        public static FixQuaternion Normalize(FixQuaternion quaternion)
        {
            Normalize(ref quaternion, out var toReturn);
            return toReturn;
        }

        /// <summary>
        /// Ensures the quaternion has unit length.
        /// </summary>
        /// <param name="quaternion">FixQuaternion to normalize.</param>
        /// <param name="toReturn">Normalized quaternion.</param>
        public static void Normalize(ref FixQuaternion quaternion, out FixQuaternion toReturn)
        {
            FixFloat64 fix = FixFloat64Util.C1 / FixFloat64.Sqrt(quaternion.X * quaternion.X + quaternion.Y * quaternion.Y + quaternion.Z * quaternion.Z + quaternion.W * quaternion.W);
            toReturn.X = quaternion.X * fix;
            toReturn.Y = quaternion.Y * fix;
            toReturn.Z = quaternion.Z * fix;
            toReturn.W = quaternion.W * fix;
        }

        /// <summary>
        /// Scales the quaternion such that it has unit length.
        /// </summary>
        public void Normalize()
        {
            FixFloat64 fix = FixFloat64Util.C1 / FixFloat64.Sqrt(X * X + Y * Y + Z * Z + W * W);
            X *= fix;
            Y *= fix;
            Z *= fix;
            W *= fix;
        }

        /// <summary>
        /// Computes the squared length of the quaternion.
        /// </summary>
        /// <returns>Squared length of the quaternion.</returns>
        public FixFloat64 LengthSquared()
        {
            return X * X + Y * Y + Z * Z + W * W;
        }

        /// <summary>
        /// Computes the length of the quaternion.
        /// </summary>
        /// <returns>Length of the quaternion.</returns>
        public FixFloat64 Length()
        {
            return FixFloat64.Sqrt(X * X + Y * Y + Z * Z + W * W);
        }

        /// <summary>
        /// Blends two quaternions together to get an intermediate state.
        /// </summary>
        /// <param name="start">Starting point of the interpolation.</param>
        /// <param name="end">Ending point of the interpolation.</param>
        /// <param name="interpolationAmount">Amount of the end point to use.</param>
        /// <param name="result">Interpolated intermediate quaternion.</param>
        public static void Slerp(ref FixQuaternion start, ref FixQuaternion end, FixFloat64 interpolationAmount, out FixQuaternion result)
        {
            FixFloat64 fix = start.W * end.W + start.X * end.X + start.Y * end.Y + start.Z * end.Z;
            if (fix < FixFloat64Util.C0)
            {
                end.X = -end.X;
                end.Y = -end.Y;
                end.Z = -end.Z;
                end.W = -end.W;
                fix = -fix;
            }
            if (fix > FixFloat64Util.C1m1em12)
            {
                result.W = start.W;
                result.X = start.X;
                result.Y = start.Y;
                result.Z = start.Z;
                return;
            }
            FixFloat64 fix2 = FixFloat64.Acos(fix);
            FixFloat64 fix3 = FixFloat64.Sqrt(FixFloat64Util.C1 - fix * fix);
            FixFloat64 fix4 = FixFloat64.Sin((FixFloat64Util.C1 - interpolationAmount) * fix2) / fix3;
            FixFloat64 fix5 = FixFloat64.Sin(interpolationAmount * fix2) / fix3;
            result.X = start.X * fix4 + end.X * fix5;
            result.Y = start.Y * fix4 + end.Y * fix5;
            result.Z = start.Z * fix4 + end.Z * fix5;
            result.W = start.W * fix4 + end.W * fix5;
        }

        /// <summary>
        /// Blends two quaternions together to get an intermediate state.
        /// </summary>
        /// <param name="start">Starting point of the interpolation.</param>
        /// <param name="end">Ending point of the interpolation.</param>
        /// <param name="interpolationAmount">Amount of the end point to use.</param>
        /// <returns>Interpolated intermediate quaternion.</returns>
        public static FixQuaternion Slerp(FixQuaternion start, FixQuaternion end, FixFloat64 interpolationAmount)
        {
            Slerp(ref start, ref end, interpolationAmount, out var result);
            return result;
        }

        /// <summary>
        /// Computes the conjugate of the quaternion.
        /// </summary>
        /// <param name="quaternion">FixQuaternion to conjugate.</param>
        /// <param name="result">Conjugated quaternion.</param>
        public static void Conjugate(ref FixQuaternion quaternion, out FixQuaternion result)
        {
            result.X = -quaternion.X;
            result.Y = -quaternion.Y;
            result.Z = -quaternion.Z;
            result.W = quaternion.W;
        }

        /// <summary>
        /// Computes the conjugate of the quaternion.
        /// </summary>
        /// <param name="quaternion">FixQuaternion to conjugate.</param>
        /// <returns>Conjugated quaternion.</returns>
        public static FixQuaternion Conjugate(FixQuaternion quaternion)
        {
            Conjugate(ref quaternion, out var result);
            return result;
        }

        /// <summary>
        /// Computes the inverse of the quaternion.
        /// </summary>
        /// <param name="quaternion">FixQuaternion to invert.</param>
        /// <param name="result">Result of the inversion.</param>
        public static void Inverse(ref FixQuaternion quaternion, out FixQuaternion result)
        {
            FixFloat64 fix = quaternion.X * quaternion.X + quaternion.Y * quaternion.Y + quaternion.Z * quaternion.Z + quaternion.W * quaternion.W;
            result.X = -quaternion.X * fix;
            result.Y = -quaternion.Y * fix;
            result.Z = -quaternion.Z * fix;
            result.W = quaternion.W * fix;
        }

        /// <summary>
        /// Computes the inverse of the quaternion.
        /// </summary>
        /// <param name="quaternion">FixQuaternion to invert.</param>
        /// <returns>Result of the inversion.</returns>
        public static FixQuaternion Inverse(FixQuaternion quaternion)
        {
            Inverse(ref quaternion, out var result);
            return result;
        }

        /// <summary>
        /// Tests components for equality.
        /// </summary>
        /// <param name="a">First quaternion to test for equivalence.</param>
        /// <param name="b">Second quaternion to test for equivalence.</param>
        /// <returns>Whether or not the quaternions' components were equal.</returns>
        public static bool operator ==(FixQuaternion a, FixQuaternion b)
        {
            return a.X == b.X && a.Y == b.Y && a.Z == b.Z && a.W == b.W;
        }

        /// <summary>
        /// Tests components for inequality.
        /// </summary>
        /// <param name="a">First quaternion to test for equivalence.</param>
        /// <param name="b">Second quaternion to test for equivalence.</param>
        /// <returns>Whether the quaternions' components were not equal.</returns>
        public static bool operator !=(FixQuaternion a, FixQuaternion b)
        {
            return a.X != b.X || a.Y != b.Y || a.Z != b.Z || a.W != b.W;
        }

        /// <summary>
        /// Negates the components of a quaternion.
        /// </summary>
        /// <param name="a">FixQuaternion to negate.</param>
        /// <param name="b">Negated result.</param>
        public static void Negate(ref FixQuaternion a, out FixQuaternion b)
        {
            b.X = -a.X;
            b.Y = -a.Y;
            b.Z = -a.Z;
            b.W = -a.W;
        }

        /// <summary>
        /// Negates the components of a quaternion.
        /// </summary>
        /// <param name="q">FixQuaternion to negate.</param>
        /// <returns>Negated result.</returns>
        public static FixQuaternion Negate(FixQuaternion q)
        {
            Negate(ref q, out var b);
            return b;
        }

        /// <summary>
        /// Negates the components of a quaternion.
        /// </summary>
        /// <param name="q">FixQuaternion to negate.</param>
        /// <returns>Negated result.</returns>
        public static FixQuaternion operator -(FixQuaternion q)
        {
            Negate(ref q, out var b);
            return b;
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals(FixQuaternion other)
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
            if (obj is FixQuaternion)
            {
                return Equals((FixQuaternion)obj);
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
        /// Transforms the vector using a quaternion.
        /// </summary>
        /// <param name="v">Vector to transform.</param>
        /// <param name="rotation">Rotation to apply to the vector.</param>
        /// <param name="result">Transformed vector.</param>
        public static void Transform(ref FixVector3 v, ref FixQuaternion rotation, out FixVector3 result)
        {
            FixFloat64 fix = rotation.X + rotation.X;
            FixFloat64 fix2 = rotation.Y + rotation.Y;
            FixFloat64 fix3 = rotation.Z + rotation.Z;
            FixFloat64 fix4 = rotation.X * fix;
            FixFloat64 fix5 = rotation.X * fix2;
            FixFloat64 fix6 = rotation.X * fix3;
            FixFloat64 fix7 = rotation.Y * fix2;
            FixFloat64 fix8 = rotation.Y * fix3;
            FixFloat64 fix9 = rotation.Z * fix3;
            FixFloat64 fix10 = rotation.W * fix;
            FixFloat64 fix11 = rotation.W * fix2;
            FixFloat64 fix12 = rotation.W * fix3;
            FixFloat64 x = v.X * (FixFloat64Util.C1 - fix7 - fix9) + v.Y * (fix5 - fix12) + v.Z * (fix6 + fix11);
            FixFloat64 y = v.X * (fix5 + fix12) + v.Y * (FixFloat64Util.C1 - fix4 - fix9) + v.Z * (fix8 - fix10);
            FixFloat64 z = v.X * (fix6 - fix11) + v.Y * (fix8 + fix10) + v.Z * (FixFloat64Util.C1 - fix4 - fix7);
            result.X = x;
            result.Y = y;
            result.Z = z;
        }

        /// <summary>
        /// Transforms the vector using a quaternion.
        /// </summary>
        /// <param name="v">Vector to transform.</param>
        /// <param name="rotation">Rotation to apply to the vector.</param>
        /// <returns>Transformed vector.</returns>
        public static FixVector3 Transform(FixVector3 v, FixQuaternion rotation)
        {
            Transform(ref v, ref rotation, out var result);
            return result;
        }

        /// <summary>
        /// Transforms a vector using a quaternion. Specialized for x,0,0 vectors.
        /// </summary>
        /// <param name="x">X component of the vector to transform.</param>
        /// <param name="rotation">Rotation to apply to the vector.</param>
        /// <param name="result">Transformed vector.</param>
        public static void TransformX(FixFloat64 x, ref FixQuaternion rotation, out FixVector3 result)
        {
            FixFloat64 fix = rotation.Y + rotation.Y;
            FixFloat64 fix2 = rotation.Z + rotation.Z;
            FixFloat64 fix3 = rotation.X * fix;
            FixFloat64 fix4 = rotation.X * fix2;
            FixFloat64 fix5 = rotation.Y * fix;
            FixFloat64 fix6 = rotation.Z * fix2;
            FixFloat64 fix7 = rotation.W * fix;
            FixFloat64 fix8 = rotation.W * fix2;
            FixFloat64 x2 = x * (FixFloat64Util.C1 - fix5 - fix6);
            FixFloat64 y = x * (fix3 + fix8);
            FixFloat64 z = x * (fix4 - fix7);
            result.X = x2;
            result.Y = y;
            result.Z = z;
        }

        /// <summary>
        /// Transforms a vector using a quaternion. Specialized for 0,y,0 vectors.
        /// </summary>
        /// <param name="y">Y component of the vector to transform.</param>
        /// <param name="rotation">Rotation to apply to the vector.</param>
        /// <param name="result">Transformed vector.</param>
        public static void TransformY(FixFloat64 y, ref FixQuaternion rotation, out FixVector3 result)
        {
            FixFloat64 fix = rotation.X + rotation.X;
            FixFloat64 fix2 = rotation.Y + rotation.Y;
            FixFloat64 fix3 = rotation.Z + rotation.Z;
            FixFloat64 fix4 = rotation.X * fix;
            FixFloat64 fix5 = rotation.X * fix2;
            FixFloat64 fix6 = rotation.Y * fix3;
            FixFloat64 fix7 = rotation.Z * fix3;
            FixFloat64 fix8 = rotation.W * fix;
            FixFloat64 fix9 = rotation.W * fix3;
            FixFloat64 x = y * (fix5 - fix9);
            FixFloat64 y2 = y * (FixFloat64Util.C1 - fix4 - fix7);
            FixFloat64 z = y * (fix6 + fix8);
            result.X = x;
            result.Y = y2;
            result.Z = z;
        }

        /// <summary>
        /// Transforms a vector using a quaternion. Specialized for 0,0,z vectors.
        /// </summary>
        /// <param name="z">Z component of the vector to transform.</param>
        /// <param name="rotation">Rotation to apply to the vector.</param>
        /// <param name="result">Transformed vector.</param>
        public static void TransformZ(FixFloat64 z, ref FixQuaternion rotation, out FixVector3 result)
        {
            FixFloat64 fix = rotation.X + rotation.X;
            FixFloat64 fix2 = rotation.Y + rotation.Y;
            FixFloat64 fix3 = rotation.Z + rotation.Z;
            FixFloat64 fix4 = rotation.X * fix;
            FixFloat64 fix5 = rotation.X * fix3;
            FixFloat64 fix6 = rotation.Y * fix2;
            FixFloat64 fix7 = rotation.Y * fix3;
            FixFloat64 fix8 = rotation.W * fix;
            FixFloat64 fix9 = rotation.W * fix2;
            FixFloat64 x = z * (fix5 + fix9);
            FixFloat64 y = z * (fix7 - fix8);
            FixFloat64 z2 = z * (FixFloat64Util.C1 - fix4 - fix6);
            result.X = x;
            result.Y = y;
            result.Z = z2;
        }

        /// <summary>
        /// Multiplies two quaternions.
        /// </summary>
        /// <param name="a">First quaternion to multiply.</param>
        /// <param name="b">Second quaternion to multiply.</param>
        /// <returns>Product of the multiplication.</returns>
        public static FixQuaternion operator *(FixQuaternion a, FixQuaternion b)
        {
            Multiply(ref a, ref b, out var result);
            return result;
        }

        /// <summary>
        /// Creates a quaternion from an axis and angle.
        /// </summary>
        /// <param name="axis">Axis of rotation.</param>
        /// <param name="angle">Angle to rotate around the axis.</param>
        /// <returns>FixQuaternion representing the axis and angle rotation.</returns>
        public static FixQuaternion CreateFromAxisAngle(FixVector3 axis, FixFloat64 angle)
        {
            FixFloat64 x = angle * FixFloat64Util.C0p5;
            FixFloat64 fix = FixFloat64.Sin(x);
            FixQuaternion result = default(FixQuaternion);
            result.X = axis.X * fix;
            result.Y = axis.Y * fix;
            result.Z = axis.Z * fix;
            result.W = FixFloat64.Cos(x);
            return result;
        }

        /// <summary>
        /// Creates a quaternion from an axis and angle.
        /// </summary>
        /// <param name="axis">Axis of rotation.</param>
        /// <param name="angle">Angle to rotate around the axis.</param>
        /// <param name="q">FixQuaternion representing the axis and angle rotation.</param>
        public static void CreateFromAxisAngle(ref FixVector3 axis, FixFloat64 angle, out FixQuaternion q)
        {
            FixFloat64 x = angle * FixFloat64Util.C0p5;
            FixFloat64 fix = FixFloat64.Sin(x);
            q.X = axis.X * fix;
            q.Y = axis.Y * fix;
            q.Z = axis.Z * fix;
            q.W = FixFloat64.Cos(x);
        }

        /// <summary>
        /// Constructs a quaternion from yaw, pitch, and roll.
        /// </summary>
        /// <param name="yaw">Yaw of the rotation.</param>
        /// <param name="pitch">Pitch of the rotation.</param>
        /// <param name="roll">Roll of the rotation.</param>
        /// <returns>FixQuaternion representing the yaw, pitch, and roll.</returns>
        public static FixQuaternion CreateFromYawPitchRoll(FixFloat64 yaw, FixFloat64 pitch, FixFloat64 roll)
        {
            CreateFromYawPitchRoll(yaw, pitch, roll, out var q);
            return q;
        }

        /// <summary>
        /// Constructs a quaternion from yaw, pitch, and roll.
        /// </summary>
        /// <param name="yaw">Yaw of the rotation.</param>
        /// <param name="pitch">Pitch of the rotation.</param>
        /// <param name="roll">Roll of the rotation.</param>
        /// <param name="q">FixQuaternion representing the yaw, pitch, and roll.</param>
        public static void CreateFromYawPitchRoll(FixFloat64 yaw, FixFloat64 pitch, FixFloat64 roll, out FixQuaternion q)
        {
            FixFloat64 x = roll * FixFloat64Util.C0p5;
            FixFloat64 x2 = pitch * FixFloat64Util.C0p5;
            FixFloat64 x3 = yaw * FixFloat64Util.C0p5;
            FixFloat64 fix = FixFloat64.Sin(x);
            FixFloat64 fix2 = FixFloat64.Sin(x2);
            FixFloat64 fix3 = FixFloat64.Sin(x3);
            FixFloat64 fix4 = FixFloat64.Cos(x);
            FixFloat64 fix5 = FixFloat64.Cos(x2);
            FixFloat64 fix6 = FixFloat64.Cos(x3);
            FixFloat64 fix7 = fix6 * fix5;
            FixFloat64 fix8 = fix6 * fix2;
            FixFloat64 fix9 = fix3 * fix5;
            FixFloat64 fix10 = fix3 * fix2;
            q.X = fix8 * fix4 + fix9 * fix;
            q.Y = fix9 * fix4 - fix8 * fix;
            q.Z = fix7 * fix - fix10 * fix4;
            q.W = fix7 * fix4 + fix10 * fix;
        }

        /// <summary>
        /// Computes the angle change represented by a normalized quaternion.
        /// </summary>
        /// <param name="q">FixQuaternion to be converted.</param>
        /// <returns>Angle around the axis represented by the quaternion.</returns>
        public static FixFloat64 GetAngleFromQuaternion(ref FixQuaternion q)
        {
            FixFloat64 fix = FixFloat64.Abs(q.W);
            if (fix > FixFloat64Util.C1)
            {
                return FixFloat64Util.C0;
            }
            return FixFloat64Util.C2 * FixFloat64.Acos(fix);
        }

        /// <summary>
        /// Computes the axis angle representation of a normalized quaternion.
        /// </summary>
        /// <param name="q">FixQuaternion to be converted.</param>
        /// <param name="axis">Axis represented by the quaternion.</param>
        /// <param name="angle">Angle around the axis represented by the quaternion.</param>
        public static void GetAxisAngleFromQuaternion(ref FixQuaternion q, out FixVector3 axis, out FixFloat64 angle)
        {
            FixFloat64 fix = q.W;
            if (fix > FixFloat64Util.C0)
            {
                axis.X = q.X;
                axis.Y = q.Y;
                axis.Z = q.Z;
            }
            else
            {
                axis.X = -q.X;
                axis.Y = -q.Y;
                axis.Z = -q.Z;
                fix = -fix;
            }
            FixFloat64 fix2 = axis.LengthSquared();
            if (fix2 > FixFloat64Util.C1em14)
            {
                FixVector3.Divide(ref axis, FixFloat64.Sqrt(fix2), out axis);
                angle = FixFloat64Util.C2 * FixFloat64.Acos(MathHelper.Clamp(fix, -1, FixFloat64Util.C1));
            }
            else
            {
                axis = FixVector3.Up;
                angle = FixFloat64Util.C0;
            }
        }

        /// <summary>
        /// Computes the quaternion rotation between two normalized vectors.
        /// </summary>
        /// <param name="v1">First unit-length vector.</param>
        /// <param name="v2">Second unit-length vector.</param>
        /// <param name="q">FixQuaternion representing the rotation from v1 to v2.</param>
        public static void GetQuaternionBetweenNormalizedVectors(ref FixVector3 v1, ref FixVector3 v2, out FixQuaternion q)
        {
            FixVector3.Dot(ref v1, ref v2, out var product);
            if (product < FixFloat64Util.Cm0p9999)
            {
                FixFloat64 fix = FixFloat64.Abs(v1.X);
                FixFloat64 fix2 = FixFloat64.Abs(v1.Y);
                FixFloat64 fix3 = FixFloat64.Abs(v1.Z);
                if (fix < fix2 && fix < fix3)
                {
                    q = new FixQuaternion(FixFloat64Util.C0, -v1.Z, v1.Y, FixFloat64Util.C0);
                }
                else if (fix2 < fix3)
                {
                    q = new FixQuaternion(-v1.Z, FixFloat64Util.C0, v1.X, FixFloat64Util.C0);
                }
                else
                {
                    q = new FixQuaternion(-v1.Y, v1.X, FixFloat64Util.C0, FixFloat64Util.C0);
                }
            }
            else
            {
                FixVector3.Cross(ref v1, ref v2, out var result);
                q = new FixQuaternion(result.X, result.Y, result.Z, product + FixFloat64Util.C1);
            }
            q.Normalize();
        }

        /// <summary>
        /// Computes the rotation from the start orientation to the end orientation such that end = FixQuaternion.Concatenate(start, relative).
        /// </summary>
        /// <param name="start">Starting orientation.</param>
        /// <param name="end">Ending orientation.</param>
        /// <param name="relative">Relative rotation from the start to the end orientation.</param>
        public static void GetRelativeRotation(ref FixQuaternion start, ref FixQuaternion end, out FixQuaternion relative)
        {
            Conjugate(ref start, out var result);
            Concatenate(ref result, ref end, out relative);
        }

        /// <summary>
        /// Transforms the rotation into the local space of the target basis such that rotation = FixQuaternion.Concatenate(localRotation, targetBasis)
        /// </summary>
        /// <param name="rotation">Rotation in the original frame of reference.</param>
        /// <param name="targetBasis">Basis in the original frame of reference to transform the rotation into.</param>
        /// <param name="localRotation">Rotation in the local space of the target basis.</param>
        public static void GetLocalRotation(ref FixQuaternion rotation, ref FixQuaternion targetBasis, out FixQuaternion localRotation)
        {
            Conjugate(ref targetBasis, out var result);
            Concatenate(ref rotation, ref result, out localRotation);
        }

        /// <summary>
        /// Gets a string representation of the quaternion.
        /// </summary>
        /// <returns>String representing the quaternion.</returns>
        public override string ToString()
        {
            return string.Concat("{ X: ", X, ", Y: ", Y, ", Z: ", Z, ", W: ", W, "}");
        }
    }
}