using System;
using System.Net;
using System.Text;

namespace Lockstep.Core.Logic.Serialization.Utils
{
    public class Serializer
    {
        protected byte[] _data;

        protected int _position;

        private const int InitialSize = 64;

        private readonly bool _autoResize;

        public int Capacity => _data.Length;

        public byte[] Data => _data;

        public int Length => _position;

        public Serializer()
            : this(autoResize: true, 64)
        {
        }

        public Serializer(bool autoResize)
            : this(autoResize, 64)
        {
        }

        public Serializer(bool autoResize, int initialSize)
        {
            _data = new byte[initialSize];
            _autoResize = autoResize;
        }

        public static Serializer FromBytes(byte[] bytes, bool copy)
        {
            if (copy)
            {
                Serializer serializer = new Serializer(autoResize: true, bytes.Length);
                serializer.Put(bytes);
                return serializer;
            }
            return new Serializer(autoResize: true, 0)
            {
                _data = bytes
            };
        }

        public static Serializer FromBytes(byte[] bytes, int offset, int length)
        {
            Serializer serializer = new Serializer(autoResize: true, bytes.Length);
            serializer.Put(bytes, offset, length);
            return serializer;
        }

        public static Serializer FromString(string value)
        {
            Serializer serializer = new Serializer();
            serializer.Put(value);
            return serializer;
        }

        public void ResizeIfNeed(int newSize)
        {
            int num = _data.Length;
            if (num < newSize)
            {
                while (num < newSize)
                {
                    num *= 2;
                }
                Array.Resize(ref _data, num);
            }
        }

        public void Reset(int size)
        {
            ResizeIfNeed(size);
            _position = 0;
        }

        public void Reset()
        {
            _position = 0;
        }

        public byte[] CopyData()
        {
            byte[] array = new byte[_position];
            Buffer.BlockCopy(_data, 0, array, 0, _position);
            return array;
        }

        public void Put(float value)
        {
            if (_autoResize)
            {
                ResizeIfNeed(_position + 4);
            }
            FastBitConverter.GetBytes(_data, _position, value);
            _position += 4;
        }

        public void Put(double value)
        {
            if (_autoResize)
            {
                ResizeIfNeed(_position + 8);
            }
            FastBitConverter.GetBytes(_data, _position, value);
            _position += 8;
        }

        public void Put(long value)
        {
            if (_autoResize)
            {
                ResizeIfNeed(_position + 8);
            }
            FastBitConverter.GetBytes(_data, _position, value);
            _position += 8;
        }

        public void Put(ulong value)
        {
            if (_autoResize)
            {
                ResizeIfNeed(_position + 8);
            }
            FastBitConverter.GetBytes(_data, _position, value);
            _position += 8;
        }

        public void Put(int value)
        {
            if (_autoResize)
            {
                ResizeIfNeed(_position + 4);
            }
            FastBitConverter.GetBytes(_data, _position, value);
            _position += 4;
        }

        public void Put(uint value)
        {
            if (_autoResize)
            {
                ResizeIfNeed(_position + 4);
            }
            FastBitConverter.GetBytes(_data, _position, value);
            _position += 4;
        }

        public void Put(char value)
        {
            if (_autoResize)
            {
                ResizeIfNeed(_position + 2);
            }
            FastBitConverter.GetBytes(_data, _position, value);
            _position += 2;
        }

        public void Put(ushort value)
        {
            if (_autoResize)
            {
                ResizeIfNeed(_position + 2);
            }
            FastBitConverter.GetBytes(_data, _position, value);
            _position += 2;
        }

        public void Put(short value)
        {
            if (_autoResize)
            {
                ResizeIfNeed(_position + 2);
            }
            FastBitConverter.GetBytes(_data, _position, value);
            _position += 2;
        }

        public void Put(sbyte value)
        {
            if (_autoResize)
            {
                ResizeIfNeed(_position + 1);
            }
            _data[_position] = (byte)value;
            _position++;
        }

        public void Put(byte value)
        {
            if (_autoResize)
            {
                ResizeIfNeed(_position + 1);
            }
            _data[_position] = value;
            _position++;
        }

        public void Put(byte[] data, int offset, int length)
        {
            if (_autoResize)
            {
                ResizeIfNeed(_position + length);
            }
            Buffer.BlockCopy(data, offset, _data, _position, length);
            _position += length;
        }

        public void Put(byte[] data)
        {
            if (_autoResize)
            {
                ResizeIfNeed(_position + data.Length);
            }
            Buffer.BlockCopy(data, 0, _data, _position, data.Length);
            _position += data.Length;
        }

        public void PutBytesWithLength(byte[] data, int offset, int length)
        {
            if (_autoResize)
            {
                ResizeIfNeed(_position + length + 4);
            }
            FastBitConverter.GetBytes(_data, _position, length);
            Buffer.BlockCopy(data, offset, _data, _position + 4, length);
            _position += length + 4;
        }

        public void PutBytesWithLength(byte[] data)
        {
            if (_autoResize)
            {
                ResizeIfNeed(_position + data.Length + 4);
            }
            FastBitConverter.GetBytes(_data, _position, data.Length);
            Buffer.BlockCopy(data, 0, _data, _position + 4, data.Length);
            _position += data.Length + 4;
        }

        public void Put(bool value)
        {
            if (_autoResize)
            {
                ResizeIfNeed(_position + 1);
            }
            _data[_position] = (byte)(value ? 1u : 0u);
            _position++;
        }

        public void PutArray(float[] value)
        {
            ushort num = (ushort)((value != null) ? ((ushort)value.Length) : 0);
            if (_autoResize)
            {
                ResizeIfNeed(_position + num * 4 + 2);
            }
            Put(num);
            for (int i = 0; i < num; i++)
            {
                Put(value[i]);
            }
        }

        public void PutArray(double[] value)
        {
            ushort num = (ushort)((value != null) ? ((ushort)value.Length) : 0);
            if (_autoResize)
            {
                ResizeIfNeed(_position + num * 8 + 2);
            }
            Put(num);
            for (int i = 0; i < num; i++)
            {
                Put(value[i]);
            }
        }

        public void PutArray(long[] value)
        {
            ushort num = (ushort)((value != null) ? ((ushort)value.Length) : 0);
            if (_autoResize)
            {
                ResizeIfNeed(_position + num * 8 + 2);
            }
            Put(num);
            for (int i = 0; i < num; i++)
            {
                Put(value[i]);
            }
        }

        public void PutArray(ulong[] value)
        {
            ushort num = (ushort)((value != null) ? ((ushort)value.Length) : 0);
            if (_autoResize)
            {
                ResizeIfNeed(_position + num * 8 + 2);
            }
            Put(num);
            for (int i = 0; i < num; i++)
            {
                Put(value[i]);
            }
        }

        public void PutArray(int[] value)
        {
            ushort num = (ushort)((value != null) ? ((ushort)value.Length) : 0);
            if (_autoResize)
            {
                ResizeIfNeed(_position + num * 4 + 2);
            }
            Put(num);
            for (int i = 0; i < num; i++)
            {
                Put(value[i]);
            }
        }

        public void PutArray(uint[] value)
        {
            ushort num = (ushort)((value != null) ? ((ushort)value.Length) : 0);
            if (_autoResize)
            {
                ResizeIfNeed(_position + num * 4 + 2);
            }
            Put(num);
            for (int i = 0; i < num; i++)
            {
                Put(value[i]);
            }
        }

        public void PutArray(ushort[] value)
        {
            ushort num = (ushort)((value != null) ? ((ushort)value.Length) : 0);
            if (_autoResize)
            {
                ResizeIfNeed(_position + num * 2 + 2);
            }
            Put(num);
            for (int i = 0; i < num; i++)
            {
                Put(value[i]);
            }
        }

        public void PutArray(short[] value)
        {
            ushort num = (ushort)((value != null) ? ((ushort)value.Length) : 0);
            if (_autoResize)
            {
                ResizeIfNeed(_position + num * 2 + 2);
            }
            Put(num);
            for (int i = 0; i < num; i++)
            {
                Put(value[i]);
            }
        }

        public void PutArray(bool[] value)
        {
            ushort num = (ushort)((value != null) ? ((ushort)value.Length) : 0);
            if (_autoResize)
            {
                ResizeIfNeed(_position + num + 2);
            }
            Put(num);
            for (int i = 0; i < num; i++)
            {
                Put(value[i]);
            }
        }

        public void PutArray(string[] value)
        {
            ushort num = (ushort)((value != null) ? ((ushort)value.Length) : 0);
            Put(num);
            for (int i = 0; i < num; i++)
            {
                Put(value[i]);
            }
        }

        public void PutArray(string[] value, int maxLength)
        {
            ushort num = (ushort)((value != null) ? ((ushort)value.Length) : 0);
            Put(num);
            for (int i = 0; i < num; i++)
            {
                Put(value[i], maxLength);
            }
        }

        public void Put(IPEndPoint endPoint)
        {
            Put(endPoint.Address.ToString());
            Put(endPoint.Port);
        }

        public void Put(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                Put(0);
                return;
            }
            int byteCount = Encoding.UTF8.GetByteCount(value);
            if (_autoResize)
            {
                ResizeIfNeed(_position + byteCount + 4);
            }
            Put(byteCount);
            Encoding.UTF8.GetBytes(value, 0, value.Length, _data, _position);
            _position += byteCount;
        }

        public void Put(string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value))
            {
                Put(0);
                return;
            }
            int charCount = ((value.Length > maxLength) ? maxLength : value.Length);
            int byteCount = Encoding.UTF8.GetByteCount(value);
            if (_autoResize)
            {
                ResizeIfNeed(_position + byteCount + 4);
            }
            Put(byteCount);
            Encoding.UTF8.GetBytes(value, 0, charCount, _data, _position);
            _position += byteCount;
        }
    }
}
