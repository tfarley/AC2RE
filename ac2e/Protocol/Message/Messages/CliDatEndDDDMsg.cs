using System.IO;

public class CliDatEndDDDMsg : INetMessage {

    public NetQueue queue => NetQueue.NET_QUEUE_DATABASE;

    public MessageOpcode opcode => MessageOpcode.CLIDAT_END_DDD_EVENT;

    public void write(BinaryWriter data) {

    }
}
