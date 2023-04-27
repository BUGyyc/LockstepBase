/*
 * @Author: delevin.ying 
 * @Date: 2023-04-26 20:30:59 
 * @Last Modified by: delevin.ying
 * @Last Modified time: 2023-04-26 20:32:30
 */
using UnityEngine;
using Entitas;
using System.Linq;

public static class LogMaster
{
    public static void L(params string[] str)
    {
        Debug.LogFormat($"<color=yellow>  info:  {string.Join(",", str)}   </color>");
    }


    public static void E(params string[] str)
    {
        Debug.LogErrorFormat($"<color=red>  err:    {string.Join(",", str)}   </color>");
    }


    public static void L(this Entity self, params string[] str)
    {
        Debug.LogFormat($"{self} info:  ", str);
    }

}