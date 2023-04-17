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
        this.TaskTimeline().Start();
        //
        // this.CreateTaskTimeline().Do(A).Do(B).Do(C).Start();
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

    // Update is called once per frame
    void Update()
    {
        Singleton<DelevinTask>.instance.Execute(Time.deltaTime);
    }

    void FixedUpdate()
    {

    }
}
