using Lockstep.Game.Commands;
using UnityEngine;
using Vector2 = BEPUutilities.Vector2;

public class EntitySpawner : MonoBehaviour
{
    //public int Count;
    public RTSEntityDatabase EntityDatabase;

    public void Spawn(Vector2 position, GameObject obj)
    {
        //for (int j = 0; j < Count; j++)
        //{
        TPSWorld.Instance.Execute(new SpawnCommand
        {
            EntityConfigId = EntityDatabase.Entities.IndexOf(obj),
            Position = position
        });
        //}
    }
}
