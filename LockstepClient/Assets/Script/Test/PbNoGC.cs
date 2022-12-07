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
        ProtocolHelper.Instance = new ProtocolHelper();
        test = new ABC();

        // test.FVal = 1f;
        // test.IVal = 1;
        // test.UVal = 1;

        MemoryStream temp = new MemoryStream();
        // ProtoBufSerializer.Serialize(temp, test);

        // Debug.Log("" + temp.ToArray().Length);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.frameCount % 5 == 0)
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

    const int DATA_BYTE_LENGTH = 40;//假设一个字段4个字节，共10个字段，已经远远超过游戏实际情况了
    const int SEND_BUFFER_LEN = 64 * 1024;
    const int REVIVE_BUFFER_LEN = 128 * 1024;

    byte[] sendBuffer = new byte[SEND_BUFFER_LEN];
    byte[] receiveBuffer = new byte[REVIVE_BUFFER_LEN];

    BinaryWriter bwWriter;
    BinaryReader bwReader;

    public static ProtocolHelper Instance;

    StringBuilder stringBuilder;

    // private PbGC msg;

    // CodedInputStream cis;
    // CodedOutputStream cos;

    public ProtocolHelper()
    {
        //按照字节数组，创建内存流数据对象
        msSend = new MemoryStream(sendBuffer, 0, SEND_BUFFER_LEN, true, true);
        msReceive = new MemoryStream(receiveBuffer, 0, REVIVE_BUFFER_LEN, true, true);

        //把内存流传递下去
        bwWriter = new BinaryWriter(msSend);
        bwReader = new BinaryReader(msReceive);

        stringBuilder = new StringBuilder();
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

        // msSend.GetBuffer();

        // Debug.LogFormat($"反序列化  {temp.IVal}  {temp.FVal} {temp.UVal} ");

        //因为又使用了一次，所以再回收
        ProtoFactory.Recycle(tmp);
    }

    private void CleanData(ABC temp)
    {
        temp.IVal = 0;
        temp.FVal = 0f;
        temp.UVal = 0;
    }

    ABC temp = new ABC();
    public void Obj2BitArray(ABC msg)
    {

        // ABCDecorator.



        //设置一个足够大的内存流长度
        // msSend.SetLength(SEND_BUFFER_LEN);
        // //把内存流操作位置调到头部
        // msSend.Seek(0, SeekOrigin.Begin);
        // // //创建一个临时数据
        // // PbGC tmp = ProtoFactory.Get<PbGC>();

        // //深拷贝数据给临时对象
        // DeepCopyData(msg, temp);

        // //把字节流写入
        // temp.WriteTo(msSend);
        // //给内存流设置一个新的长度,之所以可以直接用Position当作长度，是因为 Seek 调到了头部。
        // msSend.SetLength(msSend.Position);
        // //然后再调到头部
        // msSend.Seek(0, SeekOrigin.Begin);
        // //接收者内存流也调到头部
        // msReceive.Seek(0, SeekOrigin.Begin);
        // //将发送者指定长度的内存内存流拷贝到接收者的内存流
        // Buffer.BlockCopy(msSend.GetBuffer(), 0, msReceive.GetBuffer(), 0, (int)msSend.Length);

        // PbGC.ParseFrom(msReceive.GetBuffer());

        //反序列化指定长度的内存流
        // tmp = ProtoBufSerializer.Deserialize(msReceive, typeof(PbGC), (int)msSend.Length) as PbGC;
        //因为又使用了一次，所以再回收
        // ProtoFactory.Recycle(tmp);


        // //把临时对象的内容序列化到内存流中
        // ProtoBufSerializer.Serialize(msSend, tmp);
        // //回收临时对象
        // ProtoFactory.Recycle(tmp);
        //给内存流设置一个新的长度,之所以可以直接用Position当作长度，是因为 Seek 调到了头部。
        // msSend.SetLength(msSend.Position);
        //然后再调到头部
        // msSend.Seek(0, SeekOrigin.Begin);

        //接收者内存流也调到头部
        // msReceive.Seek(0, SeekOrigin.Begin);
        // //将发送者指定长度的内存内存流拷贝到接收者的内存流
        // Buffer.BlockCopy(msSend.GetBuffer(), 0, msReceive.GetBuffer(), 0, (int)msSend.Length);
        // //反序列化指定长度的内存流
        // tmp = ProtoBufSerializer.Deserialize(msReceive, typeof(PbGC), (int)msSend.Length) as PbGC;
        // //因为又使用了一次，所以再回收
        // ProtoFactory.Recycle(tmp);
    }


    void DeepCopyData(ABC source, ABC dest)
    {
        dest.IVal = source.IVal;
        dest.FVal = source.FVal;
        dest.UVal = source.UVal;
    }

    void PrintData(ABC data)
    {
        stringBuilder.Clear();
        stringBuilder.AppendFormat(" msg:  ");
        stringBuilder.AppendFormat(" IntValue: {0} ", data.IVal);
        stringBuilder.AppendFormat(" UIntValue: {0} ", data.UVal);
        stringBuilder.AppendFormat(" FloatValue: {0} \n", data.FVal);
        Debug.Log(stringBuilder.ToString());
    }
}
