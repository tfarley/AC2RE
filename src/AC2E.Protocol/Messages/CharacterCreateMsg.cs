using AC2E.Def.Enums;
using AC2E.Protocol.NetBlob;
using AC2E.Utils.Extensions;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AC2E.Protocol.Messages {

    public class CharacterCreateMsg : INetMessage {

        public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;

        public NetQueue queueId => NetQueue.NET_QUEUE_LOGON;

        public MessageOpcode opcode => MessageOpcode.CHARACTER_CREATE_EVENT;

        public string accountName;
        public string characterName;
        public uint entityDid;
        public uint unk1;
        public SpeciesType species;
        public SexType sex;
        public uint unk2;
        public uint unk3;
        public Dictionary<PhysiqueType, float> physiqueTypeValues;

        public CharacterCreateMsg(BinaryReader data) {
            accountName = data.ReadEncryptedString();
            characterName = data.ReadEncryptedString(Encoding.Unicode);
            entityDid = data.ReadUInt32();
            unk1 = data.ReadUInt32();
            species = (SpeciesType)data.ReadUInt32();
            sex = (SexType)data.ReadUInt32();
            unk2 = data.ReadUInt32();
            unk3 = data.ReadUInt32();
            physiqueTypeValues = data.ReadDictionary(data => (PhysiqueType)data.ReadUInt32(), data => data.ReadSingle());
        }
    }
}
