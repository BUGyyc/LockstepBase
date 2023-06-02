using Lockstep.Game.Commands;
using UnityEngine;
using Lockstep;
//using Vector2 = BEPUutilities.Vector2;

public class EntitySpawner : MonoBehaviour
{
    public int Count;
    public GameObject Prefab;


    public RTSEntityDatabase EntityDatabase;

    private void Start()
    {
        if (EntityDatabase.Entities.IndexOf(Prefab) < 0)
        {
            Debug.LogError("Prefabs have to be added to the database in order to be spawnable");
        }
    }

    public void Spawn(LVector3 position)
    {
        for (int j = 0; j < Count; j++)
        {
            ActionWorld.Instance.Execute(new SpawnCommand
            {
                EntityConfigId = EntityDatabase.Entities.IndexOf(Prefab),
                Position = position
            });
        }

    }
}
