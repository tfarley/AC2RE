using System.Collections.Generic;

namespace AC2RE.Definitions;

public class gmEntity : Entity {

    public override PackageType packageType => PackageType.gmEntity;

    public VisualDescInfo pileVisualDesc; // m_PileAppearanceInfo
    public Dictionary<uint, uint> appearanceMutationHash; // m_appearanceMutationHash
    public SingletonPkg<MasterListMember> usagePermission; // m_usage_usagePermission
    public SingletonPkg<BiasProfile> biasProfile; // m_mf
    public SingletonPkg<MasterListMember> usageAction; // m_usage_usageAction
    public EffectRegistry effectRegistry; // m_regEffect
    public List<uint> thresholdList; // m_thresholdList
    public SingletonPkg<IHeapObject> usageTakePermission; // m_usage_takePermission

    public gmEntity(AC2Reader data) : base(data) {
        data.ReadHO<VisualDescInfo>(v => pileVisualDesc = v);
        data.ReadHO<AAHash>(v => appearanceMutationHash = v);
        data.ReadHO<MasterListMember>(v => usagePermission = v);
        data.ReadHO<BiasProfile>(v => biasProfile = v);
        data.ReadHO<MasterListMember>(v => usageAction = v);
        data.ReadHO<EffectRegistry>(v => effectRegistry = v);
        data.ReadHO<AList>(v => thresholdList = v);
        data.ReadHO<IHeapObject>(v => usageTakePermission = v);
    }
}
