using Lockstep.Core.Logic.Serialization;
using Lockstep.Core.Logic.Serialization.Utils;

namespace Lockstep.Network.Messages
{

    public class HashCode : ISerializable
    {
        public uint FrameNumber { get; set; }

        public long hashCode { get; set; }


        //public string hashCodeStr;

        public void Serialize(Serializer writer)
        {
            writer.Put(FrameNumber);
            writer.Put(hashCode);
            ////writer.PutArray(hashList);
            //writer.FromString(hashCodeStr);
        }

        public void Deserialize(Deserializer reader)
        {
            FrameNumber = reader.GetUInt();
            hashCode = reader.GetLong();
        }
    }
}
