/*
 * @Author: delevin.ying 
 * @Date: 2022-11-18 16:02:47 
 * @Last Modified by:   delevin.ying 
 * @Last Modified time: 2022-11-18 16:02:47 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using Lockstep;

//TODO: 这个是测试代码，后面干掉
public class AnimatorBindEntity : MonoBehaviour
{
    private GameEntity myEntity;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        this.animator = this.GetComponent<Animator>();
    }

    public void SetGameEntity(GameEntity entity)
    {
        myEntity = entity;
    }



    void FixedUpdate()
    {
        if (this.myEntity == null) return;

        if (this.animator == null) return;

        var deltaPos = this.animator.deltaPosition;
        var deltaRot = this.animator.deltaRotation;

        if (this.myEntity.hasPosition == false) return;

        this.myEntity.position.value += (deltaPos.ToLVector3());

        this.myEntity.position.rotate *= (deltaRot.ToLQuaternion());

        this.transform.localPosition = Vector3.zero;
        this.transform.localRotation = Quaternion.Euler(Vector3.zero);


        Debug.LogFormat($"deltaPos {deltaPos}  ");
    }
}
