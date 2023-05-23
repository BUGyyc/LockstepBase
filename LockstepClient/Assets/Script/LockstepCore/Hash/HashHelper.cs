/*
 * @Author: delevin.ying
 * @Date: 2023-05-23 17:23:21
 * @Last Modified by: delevin.ying
 * @Last Modified time: 2023-05-23 17:34:28
 */


using System.Collections.Generic;

namespace Lockstep.Client
{
    public class HashHelper
    {
        public static HashHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new HashHelper();
                }
                return _instance;
            }
        }

        private static HashHelper _instance;

        private List<long> _hashCode = new List<long>();

        private Dictionary<uint, long> _hashDic = new Dictionary<uint, long>();

        private uint _firstHashTick = 0;

        public void CheckAndSendHashCode() { }

        public void AddHashCode(uint tick, long hash)
        {
            if (tick < _firstHashTick)
                return;

            int num = (int)(tick - _firstHashTick);

            if (_hashCode.Count <= num)
            {
                for (int i = 0; i < num + 1; i++)
                {
                    _hashCode.Add(0L);
                }
            }
            _hashCode[num] = hash;
            uint worldTick = Contexts.sharedInstance.gameState.tick.value;
            _hashDic[worldTick] = hash;
        }
    }
}
