using System.Collections.Generic;
using System.Text;

namespace AC2RE.Definitions;

public class CharacterCreateMsg : INetMessage {

    public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
    public NetQueue queueId => NetQueue.LOGON;
    public MessageOpcode opcode => MessageOpcode.CHARACTER_CREATE_EVENT;

    public string accountName;
    public string characterName;
    public DataId entityDid;
    public uint unk1;
    public SpeciesType species;
    public SexType sex;
    public uint unk2;
    public uint unk3;
    public Dictionary<PhysiqueType, float> physiqueTypeValues;

    public CharacterCreateMsg(AC2Reader data) {
        accountName = data.ReadString();
        characterName = data.ReadString(Encoding.Unicode);
        entityDid = data.ReadDataId();
        unk1 = data.ReadUInt32();
        species = (SpeciesType)data.ReadUInt32();
        sex = (SexType)data.ReadUInt32();
        unk2 = data.ReadUInt32();
        unk3 = data.ReadUInt32();
        physiqueTypeValues = data.ReadDictionary(() => (PhysiqueType)data.ReadUInt32(), data.ReadSingle);
    }
}
