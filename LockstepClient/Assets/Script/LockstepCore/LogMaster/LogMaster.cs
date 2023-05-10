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
        uint tickValue = 0;
        //#if UNITY_EDITOR
        if (ActionWorld.Instance != null) tickValue = ActionWorld.Instance.Simulation.GetWorld().Contexts.gameState.tick.value;
        //#endif

        Debug.LogFormat($"<color=yellow> [Tick:{tickValue}]  info:  {string.Join(",", str)}   </color>");
    }


    public static void E(params string[] str)
    {
        uint tickValue = 0;
#if UNITY_EDITOR
        if (ActionWorld.Instance != null) tickValue = ActionWorld.Instance.Simulation.GetWorld().Contexts.gameState.tick.value;
#endif
        Debug.LogErrorFormat($"<color=red> tick:{tickValue}  err:    {string.Join(",", str)}   </color>");
    }


    public static void L(this Entity self, params string[] str)
    {
        Debug.LogFormat($"{self} info:  ", str);
    }

}