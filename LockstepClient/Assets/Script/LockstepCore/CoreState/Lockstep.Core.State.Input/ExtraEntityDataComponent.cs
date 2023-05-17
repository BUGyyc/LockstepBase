/*
 * @Author: delevin.ying 
 * @Date: 2023-05-15 16:40:46 
 * @Last Modified by: delevin.ying
 * @Last Modified time: 2023-05-15 16:41:07
 */
using Entitas;

namespace Lockstep.Core.State.Input
{
    [Input]
    public class ExtraEntityDataComponent : IComponent
    {
        public byte[] dataBs;
    }
}

