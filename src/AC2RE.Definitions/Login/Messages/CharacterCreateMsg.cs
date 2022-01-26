using System.Collections.Generic;
using System.Text;

namespace AC2RE.Definitions;

public class CharacterCreateMsg : INetMessage {

    public NetBlobId.Flag blobFlags => NetBlobId.Flag.NONE;
    public NetQueue queueId => NetQueue.LOGON;
    public MessageOpcode opcode => MessageOpcode.CHARACTER_CREATE_EVENT;

    // accountID
    public string accountName; // account
    // gmCharGenResult
    public string characterName; // m_name
    public DataId entityDid; // m_entityDID
    public uint startArea; // m_startArea
    public SpeciesType species; // m_race
    public SexType sex; // m_sex
    public ClassType classType; // m_class
    public ImplementType implement; // m_implement
    public Dictionary<PhysiqueType, float> physiqueTypeValues; // m_ptValues

    public CharacterCreateMsg(AC2Reader data) {
        accountName = data.ReadString();
        characterName = data.ReadString(Encoding.Unicode);
        entityDid = data.ReadDataId();
        startArea = data.ReadUInt32();
        species = (SpeciesType)data.ReadUInt32();
        sex = (SexType)data.ReadUInt32();
        classType = (ClassType)data.ReadUInt32();
        implement = (ImplementType)data.ReadUInt32();
        physiqueTypeValues = data.ReadDictionary(() => (PhysiqueType)data.ReadUInt32(), data.ReadSingle);
    }
}
