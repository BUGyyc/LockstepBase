using Entitas;
using System.Collections.Generic;
using System.Linq;
using BEPUutilities;
using FixMath.NET;
using Lockstep.Common.Logging;
using Lockstep.Game.Interfaces;
using Lockstep.Game;
using UnityEngine;

public class CharacterSystem : IExecuteSystem, ISystem
{
    //Contexts
    //private readonly IGroup<CharacterEntity> _characters;

    //private uint _localIdCounter;

    //private readonly ActorContext _actorContext;

    //public CharacterSystem(Contexts contexts)
    //{
    //    //_viewService = serviceContainer.Get<IViewService>();
    //    //_characterContext = contexts.character;
    //    //_gameStateContext = contexts.gameState;
    //    //_actorContext = contexts.actor;
    //    //_characters = ((Context<CharacterEntity>)contexts.character).GetGroup((IMatcher<CharacterEntity>)(object)CharacterMatcher.AllOf(CharacterMatcher.Character));



    //    //_spawnInputs = ((Context<InputEntity>)contexts.input).GetGroup((IMatcher<InputEntity>)(object)InputMatcher.AllOf(InputMatcher.EntityConfigId, InputMatcher.ActorId, InputMatcher.Coordinate, InputMatcher.Tick));
    //}


    public void Execute()
    {
        var _characters = Contexts.sharedInstance.character.GetEntities();

        Debug.LogFormat($"Character  Count = {_characters.Length} ");

        foreach (CharacterEntity item in _characters)
        {
            //Debug.LogFormat($"I am Character  speed = {item.character.speed} ");
        }

    }

    //protected override void Execute(List<CharacterEntity> entities)
    //{
    //    //throw new System.NotImplementedException();
    //    foreach (var item in entities) Debug.LogFormat("I am Character  {0} ", item.character.speed);
    //}

}
