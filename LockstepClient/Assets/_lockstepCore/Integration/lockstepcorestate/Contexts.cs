using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Entitas;
using Entitas.CodeGeneration.Attributes;
using Lockstep.Core.State.Actor;
using Lockstep.Core.State.Game;
using Lockstep.Core.State.Snapshot;

public class Contexts : IContexts
{
	private static Contexts _sharedInstance;

	public const string Id = "Id";

	public const string LocalId = "LocalId";

	public const string Tick = "Tick";

	public static Contexts sharedInstance
	{
		get
		{
			if (_sharedInstance == null)
			{
				_sharedInstance = new Contexts();
			}
			return _sharedInstance;
		}
		set
		{
			_sharedInstance = value;
		}
	}

	public ActorContext actor { get; set; }

	public ConfigContext config { get; set; }

	public DebugContext debug { get; set; }

	public GameContext game { get; set; }

	public GameStateContext gameState { get; set; }

	public InputContext input { get; set; }

	public SnapshotContext snapshot { get; set; }

	public IContext[] allContexts => (IContext[])(object)new IContext[7]
	{
		(IContext)actor,
		(IContext)config,
		(IContext)debug,
		(IContext)game,
		(IContext)gameState,
		(IContext)input,
		(IContext)snapshot
	};

	public Contexts()
	{
		actor = new ActorContext();
		config = new ConfigContext();
		debug = new DebugContext();
		game = new GameContext();
		gameState = new GameStateContext();
		input = new InputContext();
		snapshot = new SnapshotContext();
		IEnumerable<MethodInfo> enumerable = from method in GetType().GetMethods()
			where Attribute.IsDefined(method, typeof(PostConstructorAttribute))
			select method;
		foreach (MethodInfo item in enumerable)
		{
			item.Invoke(this, null);
		}
	}

	public void Reset()
	{
		IContext[] array = allContexts;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Reset();
		}
	}

	[PostConstructor]
	public void InitializeEntityIndices()
	{
		((Context<ActorEntity>)actor).AddEntityIndex((IEntityIndex)(object)new PrimaryEntityIndex<ActorEntity, byte>("Id", ((Context<ActorEntity>)actor).GetGroup(ActorMatcher.Id), (Func<ActorEntity, IComponent, byte>)((ActorEntity e, IComponent c) => ((Lockstep.Core.State.Actor.IdComponent)(object)c).value)));
		((Context<GameEntity>)game).AddEntityIndex((IEntityIndex)(object)new PrimaryEntityIndex<GameEntity, uint>("LocalId", ((Context<GameEntity>)game).GetGroup(GameMatcher.LocalId), (Func<GameEntity, IComponent, uint>)((GameEntity e, IComponent c) => ((LocalIdComponent)(object)c).value)));
		((Context<SnapshotEntity>)snapshot).AddEntityIndex((IEntityIndex)(object)new PrimaryEntityIndex<SnapshotEntity, uint>("Tick", ((Context<SnapshotEntity>)snapshot).GetGroup(SnapshotMatcher.Tick), (Func<SnapshotEntity, IComponent, uint>)((SnapshotEntity e, IComponent c) => ((TickComponent)(object)c).value)));
	}
}
