/*
 * @Author: delevin.ying 
 * @Date: 2023-04-25 16:02:53 
 * @Last Modified by: delevin.ying
 * @Last Modified time: 2023-04-25 16:23:16
 */


using UnityEngine;
using ET;
using System.Threading.Tasks;
public class ETTaskExample : MonoBehaviour
{

    void Start()
    {
        Debug.Log("1");

        Func().Coroutine();

        Debug.Log("2");

    }

    private async ETVoid Func()
    {
        Debug.Log("3");

        await Func2();

        Debug.Log("4");

        int res = await FuncRes();

        Debug.Log("5");

    }

    private async ETTask Func2()
    {
        await Task.Delay(4000);
    }

    private async ETTask<int> FuncRes()
    {
        await Task.Delay(2000);

        return 100;
    }
}