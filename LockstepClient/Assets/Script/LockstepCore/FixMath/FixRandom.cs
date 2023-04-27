/*
 * @Author: delevin.ying 
 * @Date: 2022-11-29 17:25:57 
 * @Last Modified by: delevin.ying
 * @Last Modified time: 2022-11-29 17:32:05
 */




namespace FixMath
{

    public class Random
    {
        private static readonly Random _fixRandom;

        public long seed = 129389898L;

        public static Random FixRandom
        {
            get
            {
                return _fixRandom;
            }
        }

        static Random()
        {
            _fixRandom = new Random();
        }

        private Random()
        {

        }

        public void InitSeed(long _seed)
        {
            seed = _seed;
        }

        public void InitSeed(int _seed)
        {
            seed = _seed;
        }

        public long GetRandom()
        {
            return 0;
        }
    }
}