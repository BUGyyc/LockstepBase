/*
 * @Author: delevin.ying 
 * @Date: 2022-11-14 10:24:41 
 * @Last Modified by: delevin.ying
 * @Last Modified time: 2022-11-14 11:17:35
 */
using UnityEngine;
using Entitas;
using System.Collections.Generic;
using UnityEngine.UI;
using Lockstep;
using System.Collections;
public class EntityDebugUIManager : MonoBehaviour
{
    public static bool ShowDebugUI;

    private IGroup<GameEntity> _characterGroup;
    private Dictionary<uint, Transform> uiDic;

    private Transform cameraTf;
    void Start()
    {
        uiDic = new Dictionary<uint, Transform>(8);
        cameraTf = Camera.main.transform;
    }

    void Update()
    {
        if (Contexts.sharedInstance == null) return;

        if (Contexts.sharedInstance.game == null) return;

        if (_characterGroup == null)
        {
            _characterGroup = Contexts.sharedInstance.game.GetGroup(GameMatcher.AllOf(GameMatcher.Animation));
        }

        for (int i = 0; i < _characterGroup.GetEntities().Length; i++)
        {
            var entity = _characterGroup.GetEntities()[i];
            if (entity == null) continue;

            if (uiDic.ContainsKey(entity.actorId.value) == false)
            {
                GameObject uiObj = new GameObject();
                var text = uiObj.AddComponent<TextMesh>();
                text.text = ((uint)(entity.actorId.value)).ToString();
                uiObj.transform.localScale = 0.1f * Vector3.one;
                uiDic.Add(entity.actorId.value, uiObj.transform);
            }
        }

        StartCoroutine(SetUIPosition());
    }

    void LateUpdate()
    {

    }

    IEnumerator SetUIPosition()
    {
        yield return new WaitForEndOfFrame();

        foreach (var item in uiDic)
        {
            GameEntity entity = Contexts.sharedInstance.game.GetEntityWithLocalId(item.Key + EntityUtil.BaseCharacterEntityID);
            if (entity == null) continue;
            var dataPos = entity.position.value;

            var tf = item.Value;

            var targetPos = dataPos.ToVector3() + 2f * Vector3.up;

            var distance = Vector3.Distance(tf.position, targetPos);

            var val = Mathf.Clamp01(distance / 0.5f);

            tf.position = val > 0.1f ? Vector3.Lerp(tf.position, targetPos, val) : targetPos;

            // tf.position = targetPos;

            tf.rotation = Quaternion.LookRotation(-cameraTf.transform.transform.forward);
        }
    }
}