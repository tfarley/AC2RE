using System.Collections.Generic;

namespace AC2RE.Definitions;

public class CliDatInterrogationResponseMsg : INetMessage {

    public NetBlobId.Flag blobFlags => NetBlobId.Flag.OUT_OF_WORLD;
    public NetQueue queueId => NetQueue.DATABASE;
    public MessageOpcode opcode => MessageOpcode.CLIDAT_INTERROGATION_RESPONSE_EVENT;
    public OrderingType orderingType => OrderingType.UNORDERED;

    // CCliDatInterrogationResponseEvent::CDataFormat
    public Language language; // LanguageID
    public List<TaggedIterationList> iterationListData; // IterationListData

    public CliDatInterrogationResponseMsg(AC2Reader data) {
        language = data.ReadEnum<Language>();
        iterationListData = data.ReadList(() => new TaggedIterationList(data));
    }
}
