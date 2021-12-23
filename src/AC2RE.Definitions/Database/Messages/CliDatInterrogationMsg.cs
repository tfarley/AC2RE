using System.Collections.Generic;

namespace AC2RE.Definitions;

public class CliDatInterrogationMsg : INetMessage {

    public NetBlobId.Flag blobFlags => NetBlobId.Flag.OUT_OF_WORLD;
    public NetQueue queueId => NetQueue.DATABASE;
    public MessageOpcode opcode => MessageOpcode.CLIDAT_INTERROGATION_EVENT;

    // CCliDatInterrogationEvent::CDataFormat
    public RegionID regionId; // dwServersRegion
    public Language nameRuleLanguage; // NameRuleLanguage
    public List<Language> supportedLanguages; // nSupportedLanguages + SupportedLanguages

    public CliDatInterrogationMsg() {

    }

    public CliDatInterrogationMsg(AC2Reader data) {
        regionId = (RegionID)data.ReadUInt32();
        nameRuleLanguage = (Language)data.ReadUInt32();
        supportedLanguages = data.ReadList(() => (Language)data.ReadUInt32());
    }

    public void write(AC2Writer data) {
        data.Write((uint)regionId);
        data.Write((uint)nameRuleLanguage);
        data.Write(supportedLanguages, v => data.Write((uint)v));
    }
}
