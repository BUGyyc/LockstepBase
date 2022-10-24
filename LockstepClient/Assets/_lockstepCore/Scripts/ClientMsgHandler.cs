using LiteNetLib;

public static class ClientMsgHandler
{
    public static void MsgRespone(NetPeer peer, NetPacketReader reader, byte channel, DeliveryMethod deliveryMethod)
    {
        var eid = reader.GetInt();

        switch (eid)
        {
            default:
                break;
        }


    }
}

