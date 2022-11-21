/*
 * @Author: delevin.ying 
 * @Date: 2022-11-18 18:21:37 
 * @Last Modified by: delevin.ying
 * @Last Modified time: 2022-11-18 18:24:29
 */

using System.Collections.Generic;
using Lockstep.Core.Logic.Interfaces;
using UnityEngine;
public class RecordManager
{
    public static RecordManager Instance;

    public Queue<ICommand> cacheQueue;

    public void AddInputCommand(ICommand command)
    {
        Debug.Log($"保存指令 --- {command}");
        cacheQueue.Enqueue(command);
    }


    public void Save2File()
    {

    }

    public void LoadInputCommandByFile()
    {

    }
}