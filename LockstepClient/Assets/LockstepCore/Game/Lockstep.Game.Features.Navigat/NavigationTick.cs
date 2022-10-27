using System.Collections.Generic;
using System.Linq;
using BEPUutilities;
using Entitas;
using FixMath.NET;
using Lockstep.Common;
using Lockstep.Game.Features.Navigation.RVO.Algorithm;

namespace Lockstep.Game.Features.Navigation.Simple
{
	public class NavigationTick : IExecuteSystem, ISystem
	{
		private readonly Contexts _contexts;

		public NavigationTick(Contexts contexts, ServiceContainer services)
		{
			_contexts = contexts;
		}

		public void Execute()
		{
			GameEntity[] entities = ContextExtension.GetEntities<GameEntity>((IContext<GameEntity>)(object)_contexts.game, (IMatcher<GameEntity>)(object)GameMatcher.AllOf(GameMatcher.LocalId, GameMatcher.Destination));
			foreach (GameEntity gameEntity in entities)
			{
				Vector2 vector = gameEntity.destination.value - gameEntity.position.value;
				if (vector.LengthSquared() > Fix64.One)
				{
					vector.Normalize();
				}
				if ((gameEntity.destination.value - gameEntity.position.value).LengthSquared() > 1)
				{
					gameEntity.ReplacePosition(gameEntity.position.value + vector);
				}
				if ((gameEntity.destination.value - gameEntity.position.value).LengthSquared() <= 1)
				{
					gameEntity.RemoveDestination();
				}
			}
		}
	}
}
namespace Lockstep.Game.Features.Navigation.RVO
{
	public class NavigationTick : IInitializeSystem, ISystem, IExecuteSystem
	{
		private readonly Contexts _contexts;

		public NavigationTick(Contexts contexts, ServiceContainer services)
		{
			_contexts = contexts;
		}

		public void Initialize()
		{
			Simulator.Instance.setTimeStep(0.5m);
			Simulator.Instance.setAgentDefaults(15, 10, 5, 5, 1, 1);
		}

		public void Execute()
		{
			GameEntity[] entities = ContextExtension.GetEntities<GameEntity>((IContext<GameEntity>)(object)_contexts.game, (IMatcher<GameEntity>)(object)GameMatcher.AllOf(GameMatcher.LocalId, GameMatcher.RvoAgentSettings, GameMatcher.Destination));
			Simulator.Instance.agents_.Clear();
			foreach (GameEntity item in from entity in entities
				orderby entity.actorId.value, entity.id.value
				select entity)
			{
				Simulator.Instance.addAgent(item.localId.value, item.position.value, item.destination.value);
			}
			uint key;
			Agent value;
			foreach (KeyValuePair<uint, Agent> item2 in Simulator.Instance.agents_)
			{
				KvpExtensions.Deconstruct(item2, out key, out value);
				Agent agent = value;
				agent.CalculatePrefVelocity();
			}
			Simulator.Instance.doStep();
			foreach (KeyValuePair<uint, Agent> item3 in Simulator.Instance.agents_)
			{
				KvpExtensions.Deconstruct(item3, out key, out value);
				uint value2 = key;
				Agent agent2 = value;
				GameEntity entityWithLocalId = _contexts.game.GetEntityWithLocalId(value2);
				Vector2 vector = entityWithLocalId.position.value + agent2.Velocity;
				if ((vector - entityWithLocalId.position.value).LengthSquared() < F64.C0p5)
				{
					entityWithLocalId.RemoveDestination();
				}
				entityWithLocalId.ReplacePosition(entityWithLocalId.position.value + agent2.Velocity);
			}
		}
	}
}
