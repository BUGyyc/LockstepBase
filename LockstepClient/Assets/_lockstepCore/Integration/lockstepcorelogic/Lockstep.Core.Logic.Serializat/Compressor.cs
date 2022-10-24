using System.IO;
using System.IO.Compression;
using Lockstep.Core.Logic.Serialization.Utils;

namespace Lockstep.Core.Logic.Serialization
{

    public static class Compressor
    {
        public static byte[] Compress(byte[] data)
        {
            using MemoryStream memoryStream = new MemoryStream();
            using (DeflateStream deflateStream = new DeflateStream(memoryStream, CompressionLevel.Optimal))
            {
                deflateStream.Write(data, 0, data.Length);
            }
            return memoryStream.ToArray();
        }

        public static byte[] Compress(Serializer serializer)
        {
            using MemoryStream memoryStream = new MemoryStream();
            using (DeflateStream deflateStream = new DeflateStream(memoryStream, CompressionLevel.Optimal))
            {
                deflateStream.Write(serializer.Data, 0, serializer.Length);
            }
            return memoryStream.ToArray();
        }

        public static byte[] Decompress(byte[] data)
        {
            using MemoryStream stream = new MemoryStream(data);
            using MemoryStream memoryStream = new MemoryStream();
            using (DeflateStream deflateStream = new DeflateStream(stream, CompressionMode.Decompress))
            {
                deflateStream.CopyTo(memoryStream);
            }
            return memoryStream.ToArray();
        }
    }
}