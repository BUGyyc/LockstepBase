using Entitas;
using Entitas.Unity;
using Lockstep.Game;
using Lockstep.Game.Interfaces;
using UnityEngine;
public class CharacterSystem : IExecuteSystem, ISystem
{
    BEPUutilities.Vector2 baseV2;
    BEPUutilities.Vector2 bx2;
    BEPUutilities.Vector2 by2;
    private readonly IViewService _viewService;
    public CharacterSystem(Contexts contexts, ServiceContainer serviceContainer)
    {
        baseV2 = new BEPUutilities.Vector2(1, 1);
        bx2 = new BEPUutilities.Vector2(baseV2.x, 0);
        by2 = new BEPUutilities.Vector2(0, baseV2.y);

        _viewService = serviceContainer.Get<IViewService>();
    }


    public void Execute()
    {
        var game = Contexts.sharedInstance.game.GetEntities();

        //Debug.LogFormat($"game  Count = {game.Length} ");

        foreach (GameEntity item in game)
        {
            if (item.hasBackup) continue;

            //Debug.LogFormat($"I am Character  speed = {item.character.speed} ");

            //Debug.Log($"game  id {item.id.value}  ");

            //item.id.value += 1;

            if (item.hasCharacter && item.hasModel && item.model.hasLoad == false)
            {

                Debug.Log($"game  id {item.id.value}  {item.model.hasLoad} {item.model.modelId}  ");

                item.model.hasLoad = true;
                var modelId = item.model.modelId;
                _viewService.LoadView(item, modelId);
            }


            //var pos = item.position;

            //var id = item.id.value;

            //int val = (int)id;
            //int res = val % 4;
            //switch (res)
            //{
            //    case 0:
            //        pos.value += bx2;
            //        break;
            //    case 1:
            //        pos.value -= bx2;
            //        break;
            //    case 2:
            //        pos.value += by2;
            //        break;
            //    case 3:
            //        pos.value -= by2;
            //        break;
            //}
        }
    }
}
