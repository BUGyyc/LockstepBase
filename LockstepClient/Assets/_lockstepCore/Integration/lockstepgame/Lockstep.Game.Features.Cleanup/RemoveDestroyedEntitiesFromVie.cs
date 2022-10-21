using System.Collections.Generic;
using Entitas;
using Lockstep.Game.Interfaces;

namespace Lockstep.Game.Features.Cleanup
{

    public class RemoveDestroyedEntitiesFromView : ICleanupSystem, ISystem
    {
        private readonly IGroup<GameEntity> _group;

        private readonly List<GameEntity> _buffer = new List<GameEntity>();

        private readonly IViewService _viewService;

        public RemoveDestroyedEntitiesFromView(Contexts contexts, ServiceContainer services)
        {
            _group = ((Context<GameEntity>)contexts.game).GetGroup(GameMatcher.Destroyed);
            _viewService = services.Get<IViewService>();
        }

        public void Cleanup()
        {
            foreach (GameEntity entity in _group.GetEntities(_buffer))
            {
                _viewService.DeleteView(entity.localId.value);
            }
        }
    }
}