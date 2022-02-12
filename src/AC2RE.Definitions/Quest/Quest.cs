using System.Collections.Generic;

namespace AC2RE.Definitions;

public class Quest : MasterDIDListMember {

    public override PackageType packageType => PackageType.Quest;

    public StringInfo longName; // m_siLongName
    public StringInfo name; // m_siName
    public DataId iconDid; // m_didIcon
    public Dictionary<uint, PhaseInfo> questPhaseInfo; // m_questPhaseInfo
    public StringInfo description; // m_siDescription
    public bool playFxOnUpdate; // m_bPlayFXOnUpdate
    public Dictionary<QuestUpdateType, List<EffectRecord>> questUpdateEffects; // m_questUpdateEffects

    public Quest(AC2Reader data) : base(data) {
        data.ReadHO<StringInfo>(v => longName = v);
        data.ReadHO<StringInfo>(v => name = v);
        iconDid = data.ReadDataId();
        data.ReadHO<ARHash>(v => questPhaseInfo = v.to<uint, PhaseInfo>());
        data.ReadHO<StringInfo>(v => description = v);
        playFxOnUpdate = data.ReadBoolean();
        data.ReadHO<ARHash>(v => questUpdateEffects = v.to<QuestUpdateType, List<EffectRecord>>(
            v => (v as RList).to<EffectRecord>()));
    }
}
