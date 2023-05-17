using Protocol;
using LiteNetLib.Utils;
using System.IO;
using UnityEngine;
using System.Text;
// using Google.Protobuf;
using ProtoBuf.Serializers;
using CustomDataStruct;
using System;

public class PbNoGC : MonoBehaviour
{
    protected NetDataWriter _dataWriter;
    ABC test;

    // Start is called before the first frame update
    void Start()
    {
        _dataWriter = new NetDataWriter();
        //ProtocolHelper.Instance = new ProtocolHelper();
        test = new ABC();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.frameCount % 5 == 0 && ProtocolHelper.Instance != null)
        {
            test.IVal = Time.frameCount;
            test.UVal = (uint)Time.frameCount;
            test.FVal = Time.realtimeSinceStartup;

            ProtocolHelper.Instance.Obj2BitArrayPreview2(test);
        }
    }
}

public class ProtocolHelper
{
    MemoryStream msSend;
    MemoryStream msReceive;

    const int DATA_BYTE_LENGTH = 40; //假设一个字段4个字节，共10个字段，已经远远超过游戏实际情况了
    const int SEND_BUFFER_LEN = 64 * 1024;
    const int REVIVE_BUFFER_LEN = 128 * 1024;

    byte[] sendBuffer = new byte[SEND_BUFFER_LEN];
    byte[] receiveBuffer = new byte[REVIVE_BUFFER_LEN];

    BinaryWriter bwWriter;
    BinaryReader bwReader;

    static ProtocolHelper _instance;

    public static ProtocolHelper Instance {
        get
        {
            if(_instance == null)
            {
                _instance = new ProtocolHelper();
            }

            return _instance;
        }
    }

    public ProtocolHelper()
    {
        //按照字节数组，创建内存流数据对象
        msSend = new MemoryStream(sendBuffer, 0, SEND_BUFFER_LEN, true, true);
        msReceive = new MemoryStream(receiveBuffer, 0, REVIVE_BUFFER_LEN, true, true);

        //把内存流传递下去
        bwWriter = new BinaryWriter(msSend);
        bwReader = new BinaryReader(msReceive);
    }

    public byte[] SerializableData(EntityData data)
    {
        //设置一个足够大的内存流长度
        msSend.SetLength(SEND_BUFFER_LEN);
        //把内存流操作位置调到头部
        msSend.Seek(0, SeekOrigin.Begin);
        ////创建一个临时数据
        //EntityData tmp = ProtoFactory.Get<EntityData>();
        ////深拷贝数据给临时对象
        //DeepCopyData(msg, tmp);
        //把临时对象的内容序列化到内存流中
        ProtoBufSerializer.Serialize(msSend, data);
        ////回收临时对象
        //ProtoFactory.Recycle(tmp);
        //给内存流设置一个新的长度,之所以可以直接用Position当作长度，是因为 Seek 调到了头部。
        msSend.SetLength(msSend.Position);
        //然后再调到头部
        msSend.Seek(0, SeekOrigin.Begin);

        ////接收者内存流也调到头部
        //msReceive.Seek(0, SeekOrigin.Begin);
        ////将发送者指定长度的内存内存流拷贝到接收者的内存流
        //Buffer.BlockCopy(msSend.GetBuffer(), 0, msReceive.GetBuffer(), 0, (int)msSend.Length);
        ////反序列化指定长度的内存流
        //tmp = ProtoBufSerializer.Deserialize(msReceive, typeof(EntityData), (int)msSend.Length) as ABC;

        ////NOTE：测试时，可以打印看看
        ////Debug.LogFormat($"反序列化  {tmp.IVal}  {tmp.FVal} {tmp.UVal} ");

        ////因为又使用了一次，所以再回收
        //ProtoFactory.Recycle(tmp);

        return msSend.GetBuffer();
    }

    public object Deserialize<T>(byte[] data) 
    {
        //接收者内存流也调到头部
        msReceive.Seek(0, SeekOrigin.Begin);
        //将发送者指定长度的内存内存流拷贝到接收者的内存流
        Buffer.BlockCopy(msSend.GetBuffer(), 0, msReceive.GetBuffer(), 0, (int)msSend.Length);
        //反序列化指定长度的内存流
        var tmp = ProtoBufSerializer.Deserialize(msReceive, typeof(T), (int)msSend.Length);

        return tmp;
    }

    public void Obj2BitArrayPreview2(ABC msg)
    {
        //设置一个足够大的内存流长度
        msSend.SetLength(SEND_BUFFER_LEN);
        //把内存流操作位置调到头部
        msSend.Seek(0, SeekOrigin.Begin);
        //创建一个临时数据
        ABC tmp = ProtoFactory.Get<ABC>();
        //深拷贝数据给临时对象
        DeepCopyData(msg, tmp);
        //把临时对象的内容序列化到内存流中
        ProtoBufSerializer.Serialize(msSend, tmp);
        //回收临时对象
        ProtoFactory.Recycle(tmp);
        //给内存流设置一个新的长度,之所以可以直接用Position当作长度，是因为 Seek 调到了头部。
        msSend.SetLength(msSend.Position);
        //然后再调到头部
        msSend.Seek(0, SeekOrigin.Begin);

        //接收者内存流也调到头部
        msReceive.Seek(0, SeekOrigin.Begin);
        //将发送者指定长度的内存内存流拷贝到接收者的内存流
        Buffer.BlockCopy(msSend.GetBuffer(), 0, msReceive.GetBuffer(), 0, (int)msSend.Length);
        //反序列化指定长度的内存流
        tmp = ProtoBufSerializer.Deserialize(msReceive, typeof(ABC), (int)msSend.Length) as ABC;

        //NOTE：测试时，可以打印看看
        //Debug.LogFormat($"反序列化  {tmp.IVal}  {tmp.FVal} {tmp.UVal} ");

        //因为又使用了一次，所以再回收
        ProtoFactory.Recycle(tmp);
    }

    private void CleanData(ABC temp)
    {
        temp.IVal = 0;
        temp.FVal = 0f;
        temp.UVal = 0;
    }

    void DeepCopyData(ABC source, ABC dest)
    {
        dest.IVal = source.IVal;
        dest.FVal = source.FVal;
        dest.UVal = source.UVal;
    }
}
