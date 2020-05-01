using AC2E.Def.Extensions;
using AC2E.Def.Structs;
using AC2E.Protocol.NetBlob;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AC2E.Protocol.Message.Messages {

    public class LoginMinCharSetMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.OUT_OF_WORLD;

        public NetQueue queueId => NetQueue.NET_QUEUE_EVENT;

        public MessageOpcode opcode => MessageOpcode.Evt_Login__MinCharSet_ID;

        public uint unk1;
        public string accountName;
        public List<string> characterNames;
        public List<InstanceId> characterIds;

        public LoginMinCharSetMsg() {

        }

        public LoginMinCharSetMsg(BinaryReader data) {
            unk1 = data.ReadUInt32();
            accountName = data.ReadEncryptedString();
            characterNames = data.ReadList(() => data.ReadEncryptedString(Encoding.Unicode));
            characterIds = data.ReadList(data.ReadInstanceId);
        }

        public void write(BinaryWriter data) {
            data.Write(unk1);
            data.WriteEncryptedString(accountName);
            data.Write(characterNames, v => data.WriteEncryptedString(v, Encoding.Unicode));
            data.Write(characterIds, data.Write);
        }
    }
}
