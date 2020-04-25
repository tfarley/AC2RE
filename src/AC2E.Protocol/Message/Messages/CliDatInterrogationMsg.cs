using AC2E.Def.Enums;
using AC2E.Protocol.NetBlob;
using System.IO;

namespace AC2E.Protocol.Message.Messages {

    public class CliDatInterrogationMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.OUT_OF_WORLD;

        public NetQueue queueId => NetQueue.NET_QUEUE_DATABASE;

        public MessageOpcode opcode => MessageOpcode.CLIDAT_INTERROGATION_EVENT;

        public uint regionId;
        public Language nameRuleLanguage;
        public Language[] supportedLanguages;

        public void write(BinaryWriter data) {
            data.Write(regionId);
            data.Write((uint)nameRuleLanguage);
            data.Write((uint)supportedLanguages.Length);
            foreach (Language supportedLanguages in supportedLanguages) {
                data.Write((uint)supportedLanguages);
            }
        }
    }
}
