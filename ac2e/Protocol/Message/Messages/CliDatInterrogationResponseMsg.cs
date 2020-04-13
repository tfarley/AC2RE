using System.IO;

public class CliDatInterrogationResponseMsg : INetMessage {

    public NetQueue queue => NetQueue.NET_QUEUE_DATABASE;

    public MessageOpcode opcode => MessageOpcode.CLIDAT_INTERROGATION_RESPONSE_EVENT;

    public Language language;

    public CliDatInterrogationResponseMsg(BinaryReader data) {
        language = (Language)data.ReadUInt32();
        // TODO: Read IterationListData, 88 bytes worth - always?
    }
}
