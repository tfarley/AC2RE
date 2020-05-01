using AC2E.Def.Enums;
using AC2E.Def.Extensions;
using AC2E.Protocol.NetBlob;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Protocol.Message.Messages {

    public class CliDatInterrogationMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.OUT_OF_WORLD;

        public NetQueue queueId => NetQueue.NET_QUEUE_DATABASE;

        public MessageOpcode opcode => MessageOpcode.CLIDAT_INTERROGATION_EVENT;

        public uint regionId;
        public Language nameRuleLanguage;
        public List<Language> supportedLanguages;

        public CliDatInterrogationMsg() {

        }

        public CliDatInterrogationMsg(BinaryReader data) {
            regionId = data.ReadUInt32();
            nameRuleLanguage = (Language)data.ReadUInt32();
            supportedLanguages = data.ReadList(() => (Language)data.ReadUInt32());
        }

        public void write(BinaryWriter data) {
            data.Write(regionId);
            data.Write((uint)nameRuleLanguage);
            data.Write(supportedLanguages, v => data.Write((uint)v));
        }
    }
}
