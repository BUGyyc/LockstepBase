
using System;

namespace FixMath
{
    internal static class FixMatrix4x8
    {
        [ThreadStatic]
        private static FixFloat64[,] FixMatrix;

        public static bool Invert(ref FixMatrix m, out FixMatrix r)
        {
            if (FixMatrix == null)
            {
                FixMatrix = new FixFloat64[4, 8];
            }
            FixFloat64[,] matrix = FixMatrix;
            matrix[0, 0] = m.M11;
            matrix[0, 1] = m.M12;
            matrix[0, 2] = m.M13;
            matrix[0, 3] = m.M14;
            matrix[1, 0] = m.M21;
            matrix[1, 1] = m.M22;
            matrix[1, 2] = m.M23;
            matrix[1, 3] = m.M24;
            matrix[2, 0] = m.M31;
            matrix[2, 1] = m.M32;
            matrix[2, 2] = m.M33;
            matrix[2, 3] = m.M34;
            matrix[3, 0] = m.M41;
            matrix[3, 1] = m.M42;
            matrix[3, 2] = m.M43;
            matrix[3, 3] = m.M44;
            matrix[0, 4] = FixFloat64.One;
            matrix[0, 5] = FixFloat64.Zero;
            matrix[0, 6] = FixFloat64.Zero;
            matrix[0, 7] = FixFloat64.Zero;
            matrix[1, 4] = FixFloat64.Zero;
            matrix[1, 5] = FixFloat64.One;
            matrix[1, 6] = FixFloat64.Zero;
            matrix[1, 7] = FixFloat64.Zero;
            matrix[2, 4] = FixFloat64.Zero;
            matrix[2, 5] = FixFloat64.Zero;
            matrix[2, 6] = FixFloat64.One;
            matrix[2, 7] = FixFloat64.Zero;
            matrix[3, 4] = FixFloat64.Zero;
            matrix[3, 5] = FixFloat64.Zero;
            matrix[3, 6] = FixFloat64.Zero;
            matrix[3, 7] = FixFloat64.One;
            if (!FixMatrix3x6.Gauss(matrix, 4, 8))
            {
                r = default(FixMatrix);
                return false;
            }
            r = new FixMatrix(matrix[0, 4], matrix[0, 5], matrix[0, 6], matrix[0, 7], matrix[1, 4], matrix[1, 5], matrix[1, 6], matrix[1, 7], matrix[2, 4], matrix[2, 5], matrix[2, 6], matrix[2, 7], matrix[3, 4], matrix[3, 5], matrix[3, 6], matrix[3, 7]);
            return true;
        }
    }
}