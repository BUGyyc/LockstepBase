/*
 * @Author: delevin.ying 
 * @Date: 2022-11-28 10:05:37 
 * @Last Modified by: delevin.ying
 * @Last Modified time: 2022-11-29 14:30:32
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using FixMath;
using Lockstep;
// using System;


/// <summary>
/// 测试定点数学库
/// </summary>
public class DebugFixMath : MonoBehaviour
{
    void Start()
    {

        //int a = 45 * 45;


        //LFloat ab = new LFloat(false, a * 1000);

        //var val = LMath.Sqrt(ab);

        //Debug.Log($"{val}  {val._val}  {Mathf.Sqrt(a)} ");


        //long lg = 1;

        //float fv1 = 0.021f;
        //float fv2 = 0.019f;

        //float currFv = lg * (fv1 + fv2);

        //Debug.Log(currFv);


        //LFloat lFloat = new LFloat(fv1) + new LFloat(fv2);
        //LFloat rF = lFloat * lg;

        //Debug.Log("rF   " + rF + "   " + rF._val);


        //float a = 2;

        //long b = 10;

        //long c = 2;
        //float d = 2;

        //Debug.Log($"{a * b / c * d}");


        Debug.Log("----------------------------------------------------------");


        float f1 = 1f;
        LFloat lf1 = new LFloat(1f);

        sfloat sf1 = (sfloat)1f;//new sfloat(1f);

        int i = 0;
        while (i < 100)
        {
            i++;
            lf1 = lf1 + 0.1f;
            f1 = f1 + 0.1f;
            sf1 = sf1 + (sfloat)0.1f;
        }

        Debug.Log($"f1 {f1}  lf1 {lf1}  sf1 {sf1}  ");

        f1 = f1 * 2.1f;

        lf1 = lf1 * 2.1f;
        sf1 = sf1 * (sfloat)2.1f;

        Debug.Log($"f1 {f1}  lf1 {lf1}  sf1 {sf1} ");


        f1 = f1 / 0.3f;
        lf1 = lf1 / 0.3f;
        sf1 = sf1 / (sfloat)0.3f;

        Debug.Log($"f1 {f1}  lf1 {lf1}  sf1 {sf1} ");

        f1 = f1 + 0.1f + 0.3f + 1.1f;
        lf1 = lf1 + 0.1f + 0.3f + 1.1f;
        sf1 = sf1 + (sfloat)0.1f + (sfloat)0.3f + (sfloat)1.1f;

        Debug.Log($"f1 {f1}  lf1 {lf1}  sf1 {sf1} ");

        f1 = f1 - 3.11f;
        lf1 = lf1 - 3.11f;
        sf1 = sf1 - (sfloat)3.11f;

        Debug.Log($"f1 {f1}  lf1 {lf1}  sf1 {sf1} ");

        f1 = f1 + 0.77f;
        lf1 = lf1 + 0.77f;
        sf1 = sf1 + (sfloat)0.77f;

        Debug.Log($"f1 {f1}  lf1 {lf1}  sf1 {sf1} ");

        f1 = f1 * 100f;
        lf1 = lf1 * 100f;
        sf1 = sf1 * (sfloat)100f;

        Debug.Log($"f1 {f1}  lf1 {lf1}  sf1 {sf1} ");

        f1 = f1 * 0.0001f;
        lf1 = lf1 * 0.0001f;
        sf1 = sf1 * (sfloat)0.0001f;

        Debug.Log($"f1 {f1}  lf1 {lf1}  sf1 {sf1} ");

        long lg = 10000;

        f1 = f1 + lg;
        sf1 = sf1 + (sfloat)lg;

        lg = 1234556;
        f1 = f1 + lg;
        sf1 = sf1 + (sfloat)lg;

        lg = 632823;
        f1 = f1 + lg;
        sf1 = sf1 + (sfloat)lg;

        lg = 123;
        f1 = f1 / lg;
        sf1 = sf1 / (sfloat)lg;

        Debug.Log($"f1 {f1}   sf1 {sf1} ");

        double db = 2.7776f;

        f1 = f1 + (float)db;
        sf1 = sf1 + (sfloat)db;

        Debug.Log($"f1 {f1}   sf1 {sf1} ");

        return;
        #region 常用加减乘除

        // Addition();
        // Substraction();
        // BasicMultiplication();


        // MultiplicationTestCases();

        // DivisionTestCases();

        // Sign();

        // Abs();

        // FastAbs();

        // Floor();

        // Ceiling();

        // Round();

        // Sqrt();


        // Log2();

        // Ln();

        // Pow();

        // Modulus();
        // Pow2();
        #endregion


        #region 向量相关操作

        TestFixVector3();

        #endregion


    }

    public void TestFixVector3()
    {
        Vector3 origin = Vector3.zero;
        Vector3 v3 = new Vector3(0, 0, 0);
        Vector3 a = Vector3.right;
        Vector3 b = Vector3.forward;
        Vector3 c = Vector3.up;

        FixVector3 originLv3 = origin.ToFixVector3();
        FixVector3 lv3 = v3.ToFixVector3();

        FixVector3 la = a.ToFixVector3();
        FixVector3 lb = b.ToFixVector3();
        FixVector3 lc = c.ToFixVector3();


        var val = la * lb;

        var dotVal = FixVector3.Dot(la, lb);
        var crossVal = FixVector3.Cross(la, lb);

        var distance = FixVector3.Distance(la, lb);

        #region 带有误差的计算实例

        //这里是因为角度转弧度，或者弧度转角度是存在精度上的取值，所以一定会有一定偏差，
        //然后把偏差的值继续给到三角函数，导致和预期的有一定的差别

        //角度转弧度
        var rad90 = MathHelper.ToRadians((FixFloat64)90L);

        Debug.Log($" rad90 {rad90}      {FixFloat64.PiOver2}   ");

        //弧度转角度
        var angle = MathHelper.ToDegrees(FixFloat64.PiOver2);

        var val90 = (FixFloat64)90;
        Debug.Log($" angle {angle}   {angle.RawValue}   val90 {val90}    {val90.RawValue}     ");

        var cos90Val = FixFloat64.Cos(rad90);

        var rad60 = MathHelper.ToRadians((FixFloat64)60L);

        var cos60Val = FixFloat64.Cos(rad60);

        Debug.Log($" cos90   {cos90Val}  ");

        Debug.Log($" cos60   {cos60Val}  ");
        #endregion

        //如下，用精准的弧度 求 三角函数
        var sin90Val = FixFloat64.Sin(FixFloat64.PiOver2);
        Debug.Log($" sin90Val   {sin90Val}  ");
        var cos180Val = FixFloat64.Cos(FixFloat64.Pi);
        Debug.Log($" cos180Val   {cos180Val}  ");


    }









    public void Addition()
    {
        UnityEngine.Debug.Log("Addition ----");
        var terms1 = new[] { FixFloat64.MinValue, (FixFloat64)(-1), FixFloat64.Zero, FixFloat64.One, FixFloat64.MaxValue };
        var terms2 = new[] { (FixFloat64)(-1), (FixFloat64)2, (FixFloat64)(-1.5m), (FixFloat64)(-2), FixFloat64.One };
        var expecteds = new[] { FixFloat64.MinValue, FixFloat64.One, (FixFloat64)(-1.5m), (FixFloat64)(-1), FixFloat64.MaxValue };
        for (int i = 0; i < terms1.Length; ++i)
        {
            var actual = terms1[i] + terms2[i];
            var expected = expecteds[i];
            DebugInfo(expected, actual);
        }
    }

    public void Substraction()
    {
        UnityEngine.Debug.Log("Substraction ----");
        var terms1 = new[] { FixFloat64.MinValue, (FixFloat64)(-1), FixFloat64.Zero, FixFloat64.One, FixFloat64.MaxValue };
        var terms2 = new[] { FixFloat64.One, (FixFloat64)(-2), (FixFloat64)(1.5m), (FixFloat64)(2), (FixFloat64)(-1) };
        var expecteds = new[] { FixFloat64.MinValue, FixFloat64.One, (FixFloat64)(-1.5m), (FixFloat64)(-1), FixFloat64.MaxValue };
        for (int i = 0; i < terms1.Length; ++i)
        {
            var actual = terms1[i] - terms2[i];
            var expected = expecteds[i];
            DebugInfo(expected, actual);
        }
    }

    public void BasicMultiplication()
    {
        UnityEngine.Debug.Log("BasicMultiplication ----");
        var term1s = new[] { 0m, 1m, -1m, 5m, -5m, 0.5m, -0.5m, -1.0m };
        var term2s = new[] { 16m, 16m, 16m, 16m, 16m, 16m, 16m, -1.0m };
        var expecteds = new[] { 0L, 16, -16, 80, -80, 8, -8, 1 };
        for (int i = 0; i < term1s.Length; ++i)
        {
            var expected = expecteds[i];
            var actual = (long)((FixFloat64)term1s[i] * (FixFloat64)term2s[i]);
            DebugInfo(expected, actual);
        }
    }

    public void MultiplicationTestCases()
    {
        UnityEngine.Debug.Log("MultiplicationTestCases ----");
        int failures = 0;
        for (int i = 0; i < m_testCases.Length; ++i)
        {
            for (int j = 0; j < m_testCases.Length; ++j)
            {
                var x = FixFloat64.FromRaw(m_testCases[i]);
                var y = FixFloat64.FromRaw(m_testCases[j]);
                var xM = (decimal)x;
                var yM = (decimal)y;
                var expected = xM * yM;
                expected =
                    expected > (decimal)FixFloat64.MaxValue
                        ? (decimal)FixFloat64.MaxValue
                        : expected < (decimal)FixFloat64.MinValue
                              ? (decimal)FixFloat64.MinValue
                              : expected;
                var actual = x * y;
                var actualM = (decimal)actual;
                var maxDelta = (decimal)FixFloat64.FromRaw(1);
                if (actualM - expected > maxDelta || expected - actualM > maxDelta)
                {
                    UnityEngine.Debug.LogErrorFormat("Failed for FromRaw({0}) * FromRaw({1}): expected {2} but got {3}",
                                                        m_testCases[i],
                                                        m_testCases[j],
                                                        (FixFloat64)expected,
                                                        actualM);
                    ++failures;
                }
                // else
                // {
                //     var val = actualM - expected;
                //     UnityEngine.Debug.LogFormat(" 实际误差值：{0}  最大容忍度 maxDelta {1}  ", val, maxDelta);
                // }
            }
        }

        UnityEngine.Debug.LogFormat("误差数量：{0}", failures);
    }



    public void DivisionTestCases()
    {
        UnityEngine.Debug.Log("DivisionTestCases ----");
        int failures = 0;
        for (int i = 0; i < m_testCases.Length; ++i)
        {
            for (int j = 0; j < m_testCases.Length; ++j)
            {
                var x = FixFloat64.FromRaw(m_testCases[i]);
                var y = FixFloat64.FromRaw(m_testCases[j]);
                var xM = (decimal)x;
                var yM = (decimal)y;

                if (m_testCases[j] == 0)
                {
                    throw new System.Exception("不能为 0 ");
                }
                else
                {
                    var expected = xM / yM;
                    expected =
                        expected > (decimal)FixFloat64.MaxValue
                            ? (decimal)FixFloat64.MaxValue
                            : expected < (decimal)FixFloat64.MinValue
                                  ? (decimal)FixFloat64.MinValue
                                  : expected;
                    var actual = x / y;
                    var actualM = (decimal)actual;
                    var maxDelta = (decimal)FixFloat64.FromRaw(1);
                    if (actualM - expected > maxDelta || expected - actualM > maxDelta)
                    {
                        UnityEngine.Debug.LogErrorFormat("Failed for FromRaw({0}) * FromRaw({1}): expected {2} but got {3}",
                                                            m_testCases[i],
                                                            m_testCases[j],
                                                            (FixFloat64)expected,
                                                            actualM);
                        ++failures;
                    }
                }
            }
        }
        UnityEngine.Debug.LogFormat("误差数量：{0}", failures);
    }


    public void Sign()
    {
        var sources = new[] { FixFloat64.MinValue, (FixFloat64)(-1), FixFloat64.Zero, FixFloat64.One, FixFloat64.MaxValue };
        var expecteds = new[] { -1, -1, 0, 1, 1 };
        for (int i = 0; i < sources.Length; ++i)
        {
            var actual = FixFloat64.Sign(sources[i]);
            var expected = expecteds[i];
            DebugInfo(expected, actual);
        }
    }


    public void Abs()
    {
        DebugInfo(FixFloat64.MaxValue, FixFloat64.Abs(FixFloat64.MinValue));
        var sources = new[] { -1, 0, 1, int.MaxValue };
        var expecteds = new[] { 1, 0, 1, int.MaxValue };
        for (int i = 0; i < sources.Length; ++i)
        {
            var actual = FixFloat64.Abs((FixFloat64)sources[i]);
            var expected = (FixFloat64)expecteds[i];
            DebugInfo(expected, actual);
        }
    }


    public void FastAbs()
    {
        DebugInfo(FixFloat64.MinValue, FixFloat64.FastAbs(FixFloat64.MinValue));
        var sources = new[] { -1, 0, 1, int.MaxValue };
        var expecteds = new[] { 1, 0, 1, int.MaxValue };
        for (int i = 0; i < sources.Length; ++i)
        {
            var actual = FixFloat64.FastAbs((FixFloat64)sources[i]);
            var expected = (FixFloat64)expecteds[i];
            DebugInfo(expected, actual);
        }
    }


    public void Floor()
    {
        var sources = new[] { -5.1m, -1, 0, 1, 5.1m };
        var expecteds = new[] { -6m, -1, 0, 1, 5m };
        for (int i = 0; i < sources.Length; ++i)
        {
            var actual = (decimal)FixFloat64.Floor((FixFloat64)sources[i]);
            var expected = expecteds[i];
            DebugInfo(expected, actual);
        }
    }


    public void Ceiling()
    {
        var sources = new[] { -5.1m, -1, 0, 1, 5.1m };
        var expecteds = new[] { -5m, -1, 0, 1, 6m };
        for (int i = 0; i < sources.Length; ++i)
        {
            var actual = (decimal)FixFloat64.Ceiling((FixFloat64)sources[i]);
            var expected = expecteds[i];
            DebugInfo(expected, actual);
        }

        DebugInfo(FixFloat64.MaxValue, FixFloat64.Ceiling(FixFloat64.MaxValue));
    }


    public void Round()
    {
        var sources = new[] { -5.5m, -5.1m, -4.5m, -4.4m, -1, 0, 1, 4.5m, 4.6m, 5.4m, 5.5m };
        var expecteds = new[] { -6m, -5m, -4m, -4m, -1, 0, 1, 4m, 5m, 5m, 6m };
        for (int i = 0; i < sources.Length; ++i)
        {
            var actual = (decimal)FixFloat64.Round((FixFloat64)sources[i]);
            var expected = expecteds[i];
            DebugInfo(expected, actual);
        }
        DebugInfo(FixFloat64.MaxValue, FixFloat64.Round(FixFloat64.MaxValue));
    }



    public void Sqrt()
    {
        for (int i = 0; i < m_testCases.Length; ++i)
        {
            var f = FixFloat64.FromRaw(m_testCases[i]);
            if (FixFloat64.Sign(f) < 0)
            {
                throw new System.Exception("根号内不能小于 0");
            }
            else
            {
                var expected = System.Math.Sqrt((double)f);
                var actual = (double)FixFloat64.Sqrt(f);
                var delta = (decimal)System.Math.Abs(expected - actual);
                DebugInfo(delta <= FixFloat64.Precision, "Sqrt");
            }
        }
    }


    public void Log2()
    {
        double maxDelta = (double)(FixFloat64.Precision * 4);

        for (int j = 0; j < m_testCases.Length; ++j)
        {
            var b = FixFloat64.FromRaw(m_testCases[j]);

            if (b <= FixFloat64.Zero)
            {
                throw new System.Exception("不能小于等于 0");
            }
            else
            {
                var expected = System.Math.Log((double)b) / System.Math.Log(2);
                var actual = (double)FixFloat64.Log2(b);
                var delta = System.Math.Abs(expected - actual);

                DebugInfo(delta <= maxDelta, string.Format("Ln({0}) = expected {1} but got {2}", b, expected, actual));
            }
        }
    }


    public void Ln()
    {
        double maxDelta = 0.00000001;

        for (int j = 0; j < m_testCases.Length; ++j)
        {
            var b = FixFloat64.FromRaw(m_testCases[j]);

            if (b <= FixFloat64.Zero)
            {
                throw new System.Exception("不能小于等于 0");
            }
            else
            {
                var expected = System.Math.Log((double)b);
                var actual = (double)FixFloat64.Ln(b);
                var delta = System.Math.Abs(expected - actual);

                DebugInfo(delta <= maxDelta, string.Format("Ln({0}) = expected {1} but got {2}", b, expected, actual));
            }
        }
    }


    public void Pow2()
    {
        double maxDelta = 0.0000001;
        for (int i = 0; i < m_testCases.Length; ++i)
        {
            var e = FixFloat64.FromRaw(m_testCases[i]);

            var expected = System.Math.Min(System.Math.Pow(2, (double)e), (double)FixFloat64.MaxValue);
            var actual = (double)FixFloat64.Pow2(e);
            var delta = System.Math.Abs(expected - actual);

            DebugInfo(delta <= maxDelta, string.Format("Pow2({0}) = expected {1} but got {2}", e, expected, actual));
        }
    }


    public void Pow()
    {
        for (int i = 0; i < m_testCases.Length; ++i)
        {
            var b = FixFloat64.FromRaw(m_testCases[i]);

            for (int j = 0; j < m_testCases.Length; ++j)
            {
                var e = FixFloat64.FromRaw(m_testCases[j]);

                if (b == FixFloat64.Zero && e < FixFloat64.Zero)
                {
                    throw new System.Exception("b == FixFloat64.Zero && e < FixFloat64.Zero");
                }
                else if (b < FixFloat64.Zero && e != FixFloat64.Zero)
                {
                    // Assert.Throws<ArgumentOutOfRangeException>(() => FixFloat64.Pow(b, e));
                    throw new System.Exception("b < FixFloat64.Zero && e != FixFloat64.Zero");
                }
                else
                {
                    var expected = e == FixFloat64.Zero ? 1 : b == FixFloat64.Zero ? 0 : System.Math.Min(System.Math.Pow((double)b, (double)e), (double)FixFloat64.MaxValue);

                    // Absolute precision deteriorates with large result values, take this into account
                    // Similarly, large exponents reduce precision, even if result is small.
                    double maxDelta = System.Math.Abs((double)e) > 100000000 ? 0.5 : expected > 100000000 ? 10 : expected > 1000 ? 0.5 : 0.00001;

                    var actual = (double)FixFloat64.Pow(b, e);
                    var delta = System.Math.Abs(expected - actual);

                    DebugInfo(delta <= maxDelta, string.Format("Pow({0}, {1}) = expected {2} but got {3}", b, e, expected, actual));
                }
            }
        }
    }


    public void Modulus()
    {
        var deltas = new List<decimal>();
        foreach (var operand1 in m_testCases)
        {
            foreach (var operand2 in m_testCases)
            {
                var f1 = FixFloat64.FromRaw(operand1);
                var f2 = FixFloat64.FromRaw(operand2);

                if (operand2 == 0)
                {
                    // Assert.Throws<DivideByZeroException>(() => Ignore(f1 / f2));
                    throw new System.Exception("operand2 == 0");
                }
                else
                {
                    var d1 = (decimal)f1;
                    var d2 = (decimal)f2;
                    var actual = (decimal)(f1 % f2);
                    var expected = d1 % d2;
                    var delta = System.Math.Abs(expected - actual);
                    deltas.Add(delta);
                    DebugInfo(delta <= 60 * FixFloat64.Precision, string.Format("{0} % {1} = expected {2} but got {3}", f1, f2, expected, actual));
                }
            }
        }
        Debug.LogFormat("Max error: {0} ({1} times precision)", deltas.Max(), deltas.Max() / FixFloat64.Precision);
        Debug.LogFormat("Average precision: {0} ({1} times precision)", deltas.Average(), deltas.Average() / FixFloat64.Precision);
        Debug.LogFormat("failed: {0}%", deltas.Count(d => d > FixFloat64.Precision) * 100.0 / deltas.Count);
    }

    void Update()
    {
        #region  定点数压测


        #endregion
    }

    private void DebugInfo(FixFloat64 a, FixFloat64 b)
    {
        if (a != b)
        {
            Debug.LogError($"  a =  {a}  b = {a}     compare {a == b} ");
            return;
        }
        Debug.Log($"  a =  {a}  b = {a}     compare {a == b} ");
    }

    private void DebugInfo(long a, long b)
    {
        if (a != b)
        {
            Debug.LogError($"  a =  {a}  b = {a}     compare {a == b} ");
            return;
        }
        Debug.Log($"  a =  {a}  b = {a}     compare {a == b} ");
    }

    private void DebugInfo(decimal a, decimal b)
    {
        if (a != b)
        {
            Debug.LogError($"  a =  {a}  b = {a}     compare {a == b} ");
            return;
        }
        Debug.Log($"  a =  {a}  b = {a}     compare {a == b} ");
    }

    private void DebugInfo(bool val, string str = "测试")
    {
        if (val)
        {
            Debug.Log(str + "  测试通过 ");
        }
        else
        {
            Debug.LogError(str + "  测试失败 ");
        }
    }


    long[] m_testCases = new[] {
            // Small numbers
            0L, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10,
            -1, -2, -3, -4, -5, -6, -7, -8, -9, -10,
  
            // Integer numbers
            0x100000000, -0x100000000, 0x200000000, -0x200000000, 0x300000000, -0x300000000,
            0x400000000, -0x400000000, 0x500000000, -0x500000000, 0x600000000, -0x600000000,
  
            // Fractions (1/2, 1/4, 1/8)
            0x80000000, -0x80000000, 0x40000000, -0x40000000, 0x20000000, -0x20000000,
  
            // Problematic carry
            0xFFFFFFFF, -0xFFFFFFFF, 0x1FFFFFFFF, -0x1FFFFFFFF, 0x3FFFFFFFF, -0x3FFFFFFFF,
  
            // Smallest and largest values
            long.MaxValue, long.MinValue,
  
            // Large random numbers
            6791302811978701836, -8192141831180282065, 6222617001063736300, -7871200276881732034,
            8249382838880205112, -7679310892959748444, 7708113189940799513, -5281862979887936768,
            8220231180772321456, -5204203381295869580, 6860614387764479339, -9080626825133349457,
            6658610233456189347, -6558014273345705245, 6700571222183426493,
  
            // Small random numbers
            -436730658, -2259913246, 329347474, 2565801981, 3398143698, 137497017, 1060347500,
            -3457686027, 1923669753, 2891618613, 2418874813, 2899594950, 2265950765, -1962365447,
            3077934393

            // Tiny random numbers
            - 171,
            -359, 491, 844, 158, -413, -422, -737, -575, -330,
            -376, 435, -311, 116, 715, -1024, -487, 59, 724, 993
        };
}
