using Entitas;
using Lockstep.Core.Logic.Systems.Actor;
using Lockstep.Core.Logic.Systems.GameState;

namespace Lockstep.Core.Logic.Systems
{
    public sealed class WorldSystems : Feature
    {
        public WorldSystems(Contexts contexts, params Feature[] features)
        {
            Add(new InitializeEntityCount(contexts));
            Add(new OnNewPredictionCreateSnapshot(contexts));
            foreach (Feature feature in features)
            {
                Add(feature);
            }
            Add(new GameEventSystems(contexts));
            Add(new CalculateHashCode(contexts));
            Add(new DestroyDestroyedGameSystem(contexts));
            Add(new IncrementTick(contexts));
        }
    }
}

