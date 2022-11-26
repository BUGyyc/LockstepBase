/*
 * @Author: delevin.ying 
 * @Date: 2022-11-26 15:38:14 
 * @Last Modified by: delevin.ying
 * @Last Modified time: 2022-11-26 15:46:43
 */


using System;

namespace FixMath
{
    internal static class FixMatrix3x6
    {
        [ThreadStatic]
        private static FixFloat64[,] Matrix;

        public static bool Gauss(FixFloat64[,] M, int m, int n)
        {
            for (int i = 0; i < m; i++)
            {
                FixFloat64 fix = FixFloat64.Abs(M[i, i]);
                int num = i;
                for (int j = i + 1; j < m; j++)
                {
                    FixFloat64 fix2 = FixFloat64.Abs(M[j, i]);
                    if (fix2 >= fix)
                    {
                        fix = fix2;
                        num = j;
                    }
                }
                if (fix == FixFloat64Util.C0)
                {
                    return false;
                }
                if (i != num)
                {
                    for (int k = 0; k < n; k++)
                    {
                        FixFloat64 fix3 = M[i, k];
                        M[i, k] = M[num, k];
                        M[num, k] = fix3;
                    }
                }
                FixFloat64 fix4 = FixFloat64Util.C1 / M[i, i];
                M[i, i] = FixFloat64Util.C1;
                for (int l = i + 1; l < n; l++)
                {
                    M[i, l] *= fix4;
                }
                for (int num2 = 0; num2 < m; num2++)
                {
                    if (num2 != i)
                    {
                        FixFloat64 fix5 = M[num2, i];
                        for (int num3 = i + 1; num3 < n; num3++)
                        {
                            M[num2, num3] -= M[i, num3] * fix5;
                        }
                        M[num2, i] = FixFloat64Util.C0;
                    }
                }
            }
            return true;
        }

        public static bool Invert(ref FixMatrix3x3 m, out FixMatrix3x3 r)
        {
            if (Matrix == null)
            {
                Matrix = new FixFloat64[3, 6];
            }
            FixFloat64[,] matrix = Matrix;
            matrix[0, 0] = m.M11;
            matrix[0, 1] = m.M12;
            matrix[0, 2] = m.M13;
            matrix[1, 0] = m.M21;
            matrix[1, 1] = m.M22;
            matrix[1, 2] = m.M23;
            matrix[2, 0] = m.M31;
            matrix[2, 1] = m.M32;
            matrix[2, 2] = m.M33;
            matrix[0, 3] = FixFloat64.One;
            matrix[0, 4] = FixFloat64.Zero;
            matrix[0, 5] = FixFloat64.Zero;
            matrix[1, 3] = FixFloat64.Zero;
            matrix[1, 4] = FixFloat64.One;
            matrix[1, 5] = FixFloat64.Zero;
            matrix[2, 3] = FixFloat64.Zero;
            matrix[2, 4] = FixFloat64.Zero;
            matrix[2, 5] = FixFloat64.One;
            if (!Gauss(matrix, 3, 6))
            {
                r = default(FixMatrix3x3);
                return false;
            }
            r = new FixMatrix3x3(matrix[0, 3], matrix[0, 4], matrix[0, 5], matrix[1, 3], matrix[1, 4], matrix[1, 5], matrix[2, 3], matrix[2, 4], matrix[2, 5]);
            return true;
        }
    }
}