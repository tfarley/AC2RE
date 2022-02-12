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
        regionId = data.ReadEnum<RegionID>();
        nameRuleLanguage = data.ReadEnum<Language>();
        supportedLanguages = data.ReadList(data.ReadEnum<Language>);
    }

    public void write(AC2Writer data) {
        data.WriteEnum(regionId);
        data.WriteEnum(nameRuleLanguage);
        data.Write(supportedLanguages, data.WriteEnum);
    }
}
