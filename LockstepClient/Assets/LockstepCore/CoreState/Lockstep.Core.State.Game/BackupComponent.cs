using Entitas;

namespace Lockstep.Core.State.Game
{
    /// <summary>
    /// 备份，通常用在预测向，如果预测失败，则可以回到备份再追帧
    /// </summary>
    [Game]
    public class BackupComponent : IComponent
    {
        public uint localEntityId;

        public uint tick;
    }
}


