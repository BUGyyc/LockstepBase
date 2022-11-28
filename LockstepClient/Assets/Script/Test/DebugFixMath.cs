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
using System;
// using Xunit;

// using System.Diagnostics;
using System.Linq;

// using Fix64 = FixMath.Fix64;

/// <summary>
/// 测试定点数学库
/// </summary>
public class DebugFixMath : MonoBehaviour
{
    void Start()
    {
        #region 常用加减乘除

        // Test1();


        var terms1 = new[] { Fix64.MinValue, (Fix64)(-1), Fix64.Zero, Fix64.One, Fix64.MaxValue };
        var terms2 = new[] { (Fix64)(-1), (Fix64)2, (Fix64)(-1.5m), (Fix64)(-2), Fix64.One };
        var expecteds = new[] { Fix64.MinValue, Fix64.One, (Fix64)(-1.5m), (Fix64)(-1), Fix64.MaxValue };
        for (int i = 0; i < terms1.Length; ++i)
        {
            var actual = terms1[i] + terms2[i];
            var expected = expecteds[i];
            // DebugInfo(expected, actual);
            DebugInfo(expected, actual);
        }

        #endregion


        #region 向量相关操作

        #endregion



        #region 三角函数


        #endregion
    }


    private void Test1()
    {
        int realValue = 1;
        Fix64 a = new Fix64(realValue);
        float fa = realValue;
        Debug.Log($"  整数 {realValue}  转化为定点数  fixA =  {a}  fixA.RawValue   {a.RawValue} ");
        Debug.Log($"  整数 {realValue}  转化为浮点数  floatA =  {fa}");
        FixMathTest(a, fa);

        Debug.Log($"<color=green>------------------------------------------------------------------------------</color>");

        float b = 1.33f;
        Fix64 fixB = (Fix64)1.33f;
        Debug.Log($"  浮点数 {b}  转化为定点数  fixA =  {fixB}  fixA.RawValue   {fixB.RawValue} ");
        Debug.Log($"  浮点数 fa= {b}  ");
        FixMathTest(fixB, b);

        Debug.Log($"<color=green>------------------------------------------------------------------------------</color>");

        float c = 2.66f;
        Fix64 fixC = (Fix64)c;
        Debug.Log($"  浮点数 {c}  转化为定点数  fixA =  {fixC}  fixA.RawValue   {fixC.RawValue} ");
        Debug.Log($"  浮点数 fa= {c}  ");
        FixMathTest(fixC, c);

        Debug.Log($"<color=green>------------------------------------------------------------------------------</color>");

        float d = 3.66f;
        Fix64 fixD = (Fix64)d;
        Debug.Log($"  b = {b}  c = {c}   b/c = {b / c}  c/b= {c / b}  ");
        Debug.Log($"  fixB = {fixB}  fixC = {fixC}   fixB/fixC = {fixB / fixC}  fixC/fixB= {fixC / fixB}  ");

        Debug.Log($"<color=green>------------------------------------------------------------------------------</color>");

        float e = 3.66f;
        Fix64 fixE = (Fix64)d;
        Debug.Log($"  b = {b}  c = {c}   b/c = {b / c}  c/b= {c / b}  ");
        Debug.Log($"  fixB = {fixB}  fixC = {fixC}   fixB/fixC = {fixB / fixC}  fixC/fixB= {fixC / fixB}  ");

        Debug.Log($"<color=green>------------------------------------------------------------------------------</color>");

        float stepFloat = 0.1f;
        float baseFloat = 1f;



        /*
            int.MaxValue   = 2147483647;
            uint.MaxValue  = 4294967295;
            Fix64.One = 4294967296L
        */

        // Fix64 stepFix = new Fix64(4294967295L); //(Fix64)stepFloat;
        // Fix64 baseFix = Fix64.One;

        // Fix64 compareFix = Fix64.Two;



        // long lastValue = 0;

        // for (var i = 0; i < 10; i++)
        // {
        //     baseFloat += stepFloat;
        //     baseFix += stepFix;

        //     Debug.Log($"i={i}  float result  = {baseFloat}");
        //     Debug.Log($"i={i}  定点数 result = {baseFix}  raw = {baseFix.RawValue}  des {baseFix.RawValue - lastValue}  ");
        //     lastValue = baseFix.RawValue;
        // }


        // Debug.Log($" 定点数 result = {baseFix}  compareFix = {compareFix}  des {compareFix - baseFix}  ");


    }

    private void FixMathTest(Fix64 a, float fa)
    {
        for (int i = 0; i < 1000; i++)
        {
            a += Fix64.One;
            fa += 1f;
        }
        Debug.Log($"  fixA 累加 1000次后  fixA =  {a}  fixA.RawValue   {a.RawValue} ");
        Debug.Log($"  floatA 累加 1000次后  floatA =  {fa}  ");

        for (int i = 0; i < 1000; i++)
        {
            a -= Fix64.One;
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

    private void DebugInfo(Fix64 a, Fix64 b)
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
