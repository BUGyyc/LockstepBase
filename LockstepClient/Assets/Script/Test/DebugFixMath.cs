/*
 * @Author: delevin.ying 
 * @Date: 2022-11-28 10:05:37 
 * @Last Modified by: delevin.ying
 * @Last Modified time: 2022-11-28 15:05:47
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using FixMath;
// using System;


/// <summary>
/// 测试定点数学库
/// </summary>
public class DebugFixMath : MonoBehaviour
{
    void Start()
    {
        #region 常用加减乘除

        Test1();

        Addition();
        Substraction();
        BasicMultiplication();


        MultiplicationTestCases();

        #endregion


        #region 向量相关操作

        #endregion



        #region 三角函数


        #endregion
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
        // var sw = new Stopwatch();
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
                // sw.Start();
                var actual = x * y;
                // sw.Stop();
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
                else
                {
                    var val = actualM - expected;
                    UnityEngine.Debug.LogFormat(" 实际误差值：{0}  最大容忍度 maxDelta {1}  ", val, maxDelta);
                }



            }
        }

        UnityEngine.Debug.LogFormat("误差数量：{0}", failures);

        // Console.WriteLine("{0} total, {1} per multiplication", sw.ElapsedMilliseconds, (double)sw.Elapsed.Milliseconds / (m_testCases.Length * m_testCases.Length));
        // Assert.True(failures < 1);
    }



    private void Test1()
    {
        /*
          int.MaxValue   = 2147483647;
          uint.MaxValue  = 4294967295;
          FixFloat64.One = 4294967296L
      */
        float stepFloat = 0.3f;
        float baseFloat = 1f;


        FixFloat64 stepFix = (FixFloat64)stepFloat; //(FixFloat64)stepFloat;
        FixFloat64 baseFix = (FixFloat64)baseFloat;

        // FixFloat64 compareFix = FixFloat64.Two;



        // long lastValue = 0;

        for (var i = 0; i < 10; i++)
        {
            baseFloat += stepFloat;
            baseFix += stepFix;

            // Debug.Log($"i={i}  float result  = {baseFloat}");
            // Debug.Log($"i={i}  定点数 result = {baseFix}  raw = {baseFix.RawValue}  des {baseFix.RawValue - lastValue}  ");
            // lastValue = baseFix.RawValue;
        }

        FixFloat64 compareFix = (FixFloat64)4f;

        Debug.Log($" 定点数 result = {baseFix.RawValue}  compareFix = {compareFix.RawValue}  des {compareFix - baseFix}  ");


    }

    private void FixMathTest(FixFloat64 a, float fa)
    {
        for (int i = 0; i < 1000; i++)
        {
            a += FixFloat64.One;
            fa += 1f;
        }
        Debug.Log($"  fixA 累加 1000次后  fixA =  {a}  fixA.RawValue   {a.RawValue} ");
        Debug.Log($"  floatA 累加 1000次后  floatA =  {fa}  ");

        for (int i = 0; i < 1000; i++)
        {
            a -= FixFloat64.One;
            fa -= 1f;
        }
        Debug.Log($"  fixA 累减 1000次后  fixA =  {a}  fixA.RawValue   {a.RawValue} ");
        Debug.Log($"  floatA 累减 1000次后  floatA =  {fa}  ");

        for (int i = 0; i < 10; i++)
        {
            // a *= (i + 1);
            fa *= (i + 1);
        }
        Debug.Log($"  fixA 累乘 10次后  fixA =  {a}  fixA.RawValue   {a.RawValue} ");
        Debug.Log($"  floatA 累乘 1000次后  floatA =  {fa}  ");

        for (int i = 0; i < 10; i++)
        {
            // a /= (i + 1);
            fa /= (i + 1);
        }
        Debug.Log($"  fixA 累除 10次后  fixA =  {a}  fixA.RawValue   {a.RawValue} ");
        Debug.Log($"  floatA 累除 1000次后  floatA =  {fa}  ");

        for (int i = 0; i < 10; i++)
        {
            a = a * a;
            fa = fa * fa;
        }
        Debug.Log($"  fixA*fixA 10次后  fixA =  {a}  fixA.RawValue   {a.RawValue} ");
        Debug.Log($"  floatA*floatA 10次后  floatA =  {fa}  ");
    }

    void Update()
    {
        #region  定点数压测


        #endregion
    }

    private void DebugInfo(FixFloat64 a, FixFloat64 b)
    {
        Debug.Log($"  a =  {a}  b = {a}     compare {a == b} ");
    }

    private void DebugInfo(long a, long b)
    {
        Debug.Log($"  a =  {a}  b = {a}     compare {a == b} ");
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
