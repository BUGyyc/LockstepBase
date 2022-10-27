/*
 * @Author: delevin.ying 
 * @Date: 2022-07-15 09:45:00 
 * @Last Modified by: delevin.ying
 * @Last Modified time: 2022-07-15 09:55:52
 */

using Lockstep;
using UnityEngine;

namespace AI
{
    public static class LockStepMath
    {

        public static void Test()
        {
            float a = 0.1239999945f;
            float b = 0.35672123999999f;
            float c = 0.999999999123f;
            float d = 1.430000512672f;
            float e = 1.00999992345f;

            LFloat lfA = GetLFloat(a);
            LFloat lfB = GetLFloat(b);
            LFloat lfC = GetLFloat(c);
            LFloat lfD = GetLFloat(d);
            LFloat lfE = GetLFloat(e);

            Debug.LogFormat("[LockStepMath]  (a+b-e)*c={0}    (lfA+lfB-lfE)*lfC={1} ", (a + b) * c, ((lfA + lfB) * lfC).GetValue());
            Debug.LogFormat("[LockStepMath]  (a+b+c)*c={0}    (lfA+lfB+lfC)*lfC={1} ", (a + b + c) * c, ((lfA + lfB + lfC) * lfC).GetValue());
            Debug.LogFormat("[LockStepMath]  (a+b+d)*c={0}    (lfA+lfB+lfD)*lfC={1} ", (a + b + d) * c, ((lfA + lfB + lfD) * lfC).GetValue());
            Debug.LogFormat("[LockStepMath]  d / (e-a)={0}    lfD  /  (lfE-lfD)={1} ", d / e, (lfD / lfE).GetValue());
            Debug.LogFormat("[LockStepMath]  c - d + e={0}    lfC  - lfD  + lfE={1} ", c - d + e, (lfC - lfD + lfE).GetValue());


            Quaternion q = Quaternion.AngleAxis(30, Vector3.up) * Quaternion.AngleAxis(10, Vector3.forward);

        }

        public static void TestVector()
        {
            var a = new Vector3(0.11f, 0.33342f, 0.8963222f);
            var b = new Vector3(1.23345621f, 1113.1199f, 6.7778211f);
            var c = new Vector3(9.352111334f, 3.22222111f, 3.111111f);
            var d = 1.1133445622233f;
            var lfA3 = GetLVector3(a);
            var lfB3 = GetLVector3(b);
            var lfC3 = GetLVector3(c);
            var lfD = GetLFloat(d);
            Debug.LogFormat("[LockStepMath]  a+b={0}    lfA3 + lfB3 = {1} ", (a + b), lfA3 + lfB3);
            Debug.LogFormat("[LockStepMath]  c.normalized={0}    lfC3.normalized = {1} ", c.normalized, lfC3.normalized);
            Debug.LogFormat("[LockStepMath]  a - c={0}    lfA3 - lfC3 = {1} ", (a - c), lfA3 * lfC3);
            Debug.LogFormat("[LockStepMath]  Dot(a,c) = {0}    Doc(lfA3,lfC3) = {1} ", Vector3.Dot(a, c), LVector3.Dot(lfA3, lfC3).GetValue());
            Debug.LogFormat("[LockStepMath]  Dot(b,c) = {0}    Doc(lfB3,lfC3) = {1} ", Vector3.Cross(b, c), LVector3.Cross(lfB3, lfC3));
        }

        public static LQuaternion ToLQuaternion(this Quaternion self)
        {
            LFloat x = self.x.ToLFloat();
            LFloat y = self.y.ToLFloat();
            LFloat z = self.z.ToLFloat();
            LFloat w = self.w.ToLFloat();
            LQuaternion q = new LQuaternion(x, y, z, w);
            return q;
        }

        public static LVector3 GetLVector3(float x, float y, float z)
        {
            var lfX = GetLFloat(x);
            var lfY = GetLFloat(y);
            var lfZ = GetLFloat(z);
            return new LVector3(lfX, lfY, lfZ);
        }

        public static LVector3 GetLVector3(Vector3 v3)
        {
            return GetLVector3(v3.x, v3.y, v3.z);
        }

        public static LFloat GetLFloat(float val)
        {
            //这里防止溢出，最好用long去存
            long mVal = (int)(val * LFloat.MAX_BASE);
            long resVal = mVal / LFloat.LOCK_NUMBER_MUL;
            return new LFloat(true, (long)resVal);
        }

        public static LFloat GetLFloatDefault(float val)
        {
            return new LFloat(val);
        }

        /// <summary>
        /// 弧度Cos
        /// </summary>
        /// <param name="rad"></param>
        /// <returns></returns>
        public static float LCos(float rad)
        {
            var lfRad = new LFloat((long)rad);
            return LMath.Cos(lfRad).ToFloat();
        }

        /// <summary>
        /// 弧度Sin
        /// </summary>
        /// <param name="rad"></param>
        /// <returns></returns>
        public static float LSin(float rad)
        {
            var lfRad = new LFloat((long)rad);
            return LMath.Sin(lfRad).ToFloat();
        }

        public static float Add(float a, float b)
        {
            var lfA = new LFloat((long)a);
            var lfB = new LFloat((long)b);
            return (lfA + lfB).ToFloat();
        }

        public static float Des(float a, float b)
        {
            //TODO:
            return 0;
        }

        //public static float CalculateAngle(Quaternion a, Quaternion b)
        //{
        //    //TODO: 暂时用官方Angle ,目前Math 下 三角函数计算有问题，后续重写
        //    var angle = Quaternion.Angle(a, b);
        //    return angle;
        //    // LFloat lf = new LFloat(angle);
        //    // return lf.ToFloat();
        //}
    }
}