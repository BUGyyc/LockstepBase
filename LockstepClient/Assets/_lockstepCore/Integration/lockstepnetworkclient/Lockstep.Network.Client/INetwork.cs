using System;

namespace Lockstep.Network.Client
{
    public interface INetwork
    {
        event Action<byte[]> DataReceived;

        void Send(byte[] data);
    }
}


