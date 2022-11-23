// using Protocol;
// using System.Collections.Generic;
// public static class AnimationUtil
// {
//     private static Dictionary<string, MoveMotion> motionDic = new Dictionary<string, MoveMotion>();
//     private static Dictionary<string, Blender> blenderDic = new Dictionary<string, Blender>();


//     public static MoveMotion GetMoveMotion(string name)
//     {
//         MoveControllerCfg cfg = ConfigManager.GetMoveController(GameSetting.HERO_ANIMATOR);
//         if (cfg == null) return null;

//         if (motionDic.ContainsKey(name))
//         {
//             return motionDic[name];
//         }

//         foreach (var motion in cfg.ExportMotions)
//         {
//             if (motion.MotionName == name)
//             {
//                 motionDic.Add(name, motion);
//                 return motion;
//             }
//         }

//         return null;
//     }

//     public static Blender GetBlender(string name)
//     {
//         MoveControllerCfg cfg = ConfigManager.GetMoveController(GameSetting.HERO_ANIMATOR);
//         if (cfg == null) return null;

//         if (blenderDic.ContainsKey(name))
//         {
//             return blenderDic[name];
//         }

//         foreach (var blender in cfg.ExportBlenders)
//         {
//             if (blender.Name == name)
//             {
//                 blenderDic.Add(name, blender);
//                 return blender;
//             }
//         }

//         return null;
//     }
// }
