using Entitas;
using Lockstep.Core.Logic.Systems.Actor;
using Lockstep.Core.Logic.Systems.GameState;

namespace Lockstep.Core.Logic.Systems
{
    public sealed class WorldSystems : Feature
    {
        public WorldSystems(Contexts contexts, params Feature[] features)
        {
            (this).Add((ISystem)(object)new InitializeEntityCount(contexts));
            (this).Add((ISystem)(object)new OnNewPredictionCreateSnapshot(contexts));
            foreach (Feature feature in features)
            {
                (this).Add((ISystem)(object)feature);
            }
            (this).Add((ISystem)(object)new GameEventSystems(contexts));
            (this).Add((ISystem)(object)new CalculateHashCode(contexts));
            (this).Add((ISystem)(object)new DestroyDestroyedGameSystem(contexts));
            (this).Add((ISystem)(object)new IncrementTick(contexts));
        }
    }
}

