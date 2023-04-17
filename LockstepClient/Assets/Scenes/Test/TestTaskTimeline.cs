using System.Collections;
using System.Collections.Generic;
using TaskCore;
using UnityEngine;

public class TestTaskTimeline : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        //空任务
        // this.TaskTimeline().Start();

        //串行执行
        // this.TaskTimeline().Do(A).Do(B).Do(C).Start();

        //并行执行，单线程下的假并行
        // this.TaskTimeline().WithDo(A).WithDo(B).WithDo(C).Start();

        //并行与串行同时存在
        // this.TaskTimeline().WithDo(A).Do(B).Do(C).WithDo(D).WithDo(E).Start();



        // //条件驱动Timeline，这种写法太鸡肋了，除非是写在循环结构中
        // this.TaskTimeline().If(Condition1).Do(A).Do(B).Start();
        // //循环指定次数
        // this.TaskTimeline().LoopDo(5).Do(A).Do(B).Do(C).Start();
        // //循环，不指定次数
        // this.TaskTimeline().Loop().Do(A).Do(B).BreakLoop(Condition1).Start();
        // //多层循环 TODO:需要LoopEnd
        // this.TaskTimeline().LoopDo(3).Do(A).LoopDo(2).Do(B).Do(C).Start();
        // //等待帧数
        // this.TaskTimeline().WaitFrame(10).Do(A).Do(B).Start();
        // //等待时间
        // this.TaskTimeline().WaitTime(10).Do(A).Do(B).Start();

    }

    private bool Condition1()
    {
        return false;
    }

    private void A()
    {
        Debug.Log("AAAAAA");
    }

    private void B()
    {
        Debug.Log("BBBBBBB");
    }

    private void C()
    {
        Debug.Log("CCCCCCCC");
    }

    private void D()
    {
        Debug.Log("DDDDDDD");
    }

    private void E()
    {
        Debug.Log("EEEEEEEE");
    }


    // Update is called once per frame
    void Update()
    {
        Singleton<DelevinTask>.instance.Execute(Time.deltaTime);
    }

    void FixedUpdate()
    {

    }
}
