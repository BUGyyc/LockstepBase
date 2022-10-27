using FixMath.NET;
using UnityEngine;

public class UnityInput : MonoBehaviour
{
    public static BEPUutilities.Vector2 GetWorldPos(Vector2 screenPos)
    {
        var ray = Camera.main.ScreenPointToRay(screenPos);
        if (Physics.Raycast(ray, out var hit))
        {
            return new BEPUutilities.Vector2((Fix64)hit.point.x, (Fix64)hit.point.z);
        }
        var hitPoint = ray.origin - ray.direction * (ray.origin.y / ray.direction.y);
        return new BEPUutilities.Vector2((Fix64)hitPoint.x, (Fix64)hitPoint.z);
    }

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            var pos = GetWorldPos(Input.mousePosition);
            //FindObjectOfType<RTSEntitySpawner>()?.Spawn(pos);

            FindObjectOfType<EntitySpawner>()?.Spawn(pos);
        }
#if UNITY_EDITOR


        //注意这种模式的追帧需要限制输入，让追帧的操作是纯净的
        if (Input.GetKeyDown(KeyCode.R))
        {
            var tick = Contexts.sharedInstance.gameState.tick.value;

            uint offset = 30;

            if (tick <= offset) return;

            var target = tick - offset;

            Debug.LogFormat($"  手动触发回滚  from {tick}  to {target}    ");

            TPSWorld.Instance.Simulation.GetWorld().RevertToTick(target);

        }
#endif
        //if (Input.GetKeyDown(KeyCode.X))
        //{
        //    var e = Contexts.sharedInstance.game
        //        .GetEntities(GameMatcher.LocalId)
        //        .Where(entity => entity.actorId.value == TPSWorld.Instance?.Simulation.LocalActorId)
        //        .Select(entity => entity.id.value).ToArray();

        //    TPSWorld.Instance?.Execute(new NavigateCommand
        //    {
        //        Destination = GetWorldPos(Input.mousePosition),
        //        Selection = e
        //    });
        //}
    }
}
