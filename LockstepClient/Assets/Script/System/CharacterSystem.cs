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

    BEPUutilities.Vector2 baseV2;
    BEPUutilities.Vector2 bx2;
    BEPUutilities.Vector2 by2;
    public CharacterSystem()
    {
        baseV2 = new BEPUutilities.Vector2(1, 1);
        bx2 = new BEPUutilities.Vector2(baseV2.x, 0);
        by2 = new BEPUutilities.Vector2(0, baseV2.y);
    }


    public void Execute()
    {
        var game = Contexts.sharedInstance.game.GetEntities();

        //Debug.LogFormat($"game  Count = {game.Length} ");

        foreach (GameEntity item in game)
        {
            //Debug.LogFormat($"I am Character  speed = {item.character.speed} ");

            var pos = item.position;

            var id = item.id.value;

            int val = (int)id;
            int res = val % 4;
            switch (res)
            {
                case 0:
                    pos.value += bx2;
                    break;
                case 1:
                    pos.value -= bx2;
                    break;
                case 2:
                    pos.value += by2;
                    break;
                case 3:
                    pos.value -= by2;
                    break;
            }

            //var xFlag = Lockstep.Random.NextCompare(345);
            //var offset = 1;
            //var yFlag = Lockstep.Random.NextCompare(456);


            //BEPUutilities.Vector2 bx2 = new BEPUutilities.Vector2(baseV2.x, 0);
            //BEPUutilities.Vector2 by2 = new BEPUutilities.Vector2(0, baseV2.y);
            //pos.value += (xFlag) ? offset * bx2 : (offset * bx2) * -1;
            //pos.value += (yFlag) ? offset * by2 : (offset * by2) * -1;

        }

    }

    //protected override void Execute(List<CharacterEntity> entities)
    //{
    //    //throw new System.NotImplementedException();
    //    foreach (var item in entities) Debug.LogFormat("I am Character  {0} ", item.character.speed);
    //}

}
