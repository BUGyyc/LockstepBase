// using System.Diagnostics;

// namespace Lockstep.Common
// {
//     public class Timer
//     {
//         private long _lastTick;

//         private readonly Stopwatch _sw = new Stopwatch();

//         public uint TickCount { get; private set; }

//         public void Start()
//         {
//             _sw.Start();
//         }

//         public long Tick()
//         {
//             long elapsedMilliseconds = _sw.ElapsedMilliseconds;
//             long result = elapsedMilliseconds - _lastTick;
//             _lastTick = elapsedMilliseconds;
//             TickCount++;
//             return result;
//         }
//     }
// }
