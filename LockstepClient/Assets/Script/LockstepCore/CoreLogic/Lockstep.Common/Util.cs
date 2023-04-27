// using BEPUutilities;
// using FixMath.NET;

// namespace Lockstep.Common
// {
//     internal static class Util
//     {
//         public static Fix64 ClampOne(this Fix64 f1)
//         {
//             if (f1 > Fix64.One)
//             {
//                 return Fix64.One;
//             }
//             if (f1 < -Fix64.One)
//             {
//                 return -Fix64.One;
//             }
//             return f1;
//         }

//         public static int CeilToInt(this long f1)
//         {
//             return (int)(long)(f1 + Fix64.One - 1);
//         }

//         public static Fix64 Abs(this Fix64 f1)
//         {
//             return (f1 < 0) ? (-f1) : f1;
//         }

//         public static int RoundToInt(this Fix64 f1)
//         {
//             return (int)((f1 + Fix64.One / 2 - 1).RawValue >> 32);
//         }

//         public static Fix64 FastDistance(this Vector2 one, Vector2 other)
//         {
//             Fix64 fix = one.X - other.X;
//             fix *= fix;
//             Fix64 fix2 = one.Y - other.Y;
//             fix2 *= fix2;
//             return fix + fix2;
//         }

//         public static double ToDouble(this Fix64 f1)
//         {
//             return (double)f1.RawValue / (double)Fix64.One;
//         }

//         public static int CeilToInt(this Fix64 f1)
//         {
//             return (int)((f1 + Fix64.One - 1).RawValue >> 32);
//         }

//         public static Vector2 Lerped(this Vector2 v, Vector2 end, Fix64 interpolationAmount)
//         {
//             Fix64 fix = F64.C1 - interpolationAmount;
//             Vector2 result = v;
//             result.X = v.X * fix + end.X * interpolationAmount;
//             result.Y = v.Y * fix + end.Y * interpolationAmount;
//             return result;
//         }

//         public static Vector2 ClampMagnitude(Vector2 vector, Fix64 maxLength)
//         {
//             if (vector.LengthSquared() > maxLength * maxLength)
//             {
//                 return Vector2.Normalize(vector) * maxLength;
//             }
//             return vector;
//         }

//         public static Vector2 ToVector2(this Vector3 v)
//         {
//             return new Vector2(v.X, v.Z);
//         }

//         public static Vector3 ToVector3(this Vector2 v)
//         {
//             return new Vector3(v.X, 0, v.Y);
//         }
//     }
// }

