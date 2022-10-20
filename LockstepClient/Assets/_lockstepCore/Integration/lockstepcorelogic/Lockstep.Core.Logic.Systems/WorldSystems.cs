using Entitas;
using Lockstep.Core.Logic.Systems.Actor;
using Lockstep.Core.Logic.Systems.GameState;

namespace Lockstep.Core.Logic.Systems
{
    public sealed class WorldSystems : Feature
    {
        public WorldSystems(Contexts contexts, params Feature[] features)
        {
            ((Systems)this).Add((ISystem)(object)new InitializeEntityCount(contexts));
            ((Systems)this).Add((ISystem)(object)new OnNewPredictionCreateSnapshot(contexts));
            foreach (Feature feature in features)
            {
                ((Systems)this).Add((ISystem)(object)feature);
            }
            ((Systems)this).Add((ISystem)(object)new GameEventSystems(contexts));
            ((Systems)this).Add((ISystem)(object)new CalculateHashCode(contexts));
            ((Systems)this).Add((ISystem)(object)new DestroyDestroyedGameSystem(contexts));
            ((Systems)this).Add((ISystem)(object)new IncrementTick(contexts));
        }
    }
}

