using System.Runtime.InteropServices;
using BEPUutilities;
using FixMath.NET;

namespace Lockstep.Game.Features.Navigation.RVO.Algorithm
{

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct RVOMath
    {
        internal static readonly Fix64 RVO_EPSILON = 0.00001m;

        internal static Fix64 det(Vector2 vector1, Vector2 vector2)
        {
            return vector1.X * vector2.Y - vector1.Y * vector2.X;
        }

        internal static Fix64 DistSqPointLineSegment(Vector2 vector1, Vector2 vector2, Vector2 vector3)
        {
            Fix64 fix = Vector2.Dot(vector3 - vector1, vector2 - vector1) / (vector2 - vector1).LengthSquared();
            if (fix < Fix64.Zero)
            {
                return (vector3 - vector1).LengthSquared();
            }
            if (fix > Fix64.One)
            {
                return (vector3 - vector2).LengthSquared();
            }
            return (vector3 - (vector1 + fix * (vector2 - vector1))).LengthSquared();
        }

        internal static Fix64 leftOf(Vector2 a, Vector2 b, Vector2 c)
        {
            return det(a - c, b - a);
        }

        internal static Fix64 sqr(Fix64 scalar)
        {
            return scalar * scalar;
        }

        internal static Fix64 Min(Fix64 a, Fix64 b)
        {
            return (a < b) ? a : b;
        }

        internal static Fix64 Max(Fix64 a, Fix64 b)
        {
            return (a > b) ? a : b;
        }
    }
}