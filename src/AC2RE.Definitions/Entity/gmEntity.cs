using System.Collections.Generic;

namespace AC2RE.Definitions {

    public class gmEntity : Entity {

        public override PackageType packageType => PackageType.gmEntity;

        public VisualDescInfo pileVisualDesc; // m_PileAppearanceInfo
        public Dictionary<uint, uint> appearanceMutationHash; // m_appearanceMutationHash
        public SingletonPkg<MasterListMember> usagePermission; // m_usage_usagePermission
        public SingletonPkg<BiasProfile> biasProfile; // m_mf
        public SingletonPkg<MasterListMember> usageAction; // m_usage_usageAction
        public EffectRegistry effectRegistry; // m_regEffect
        public List<uint> thresholdList; // m_thresholdList
        public SingletonPkg<IPackage> usageTakePermission; // m_usage_takePermission

        public gmEntity(AC2Reader data) : base(data) {
            data.ReadPkg<VisualDescInfo>(v => pileVisualDesc = v);
            data.ReadPkg<AAHash>(v => appearanceMutationHash = v);
            data.ReadPkg<MasterListMember>(v => usagePermission = v);
            data.ReadPkg<BiasProfile>(v => biasProfile = v);
            data.ReadPkg<MasterListMember>(v => usageAction = v);
            data.ReadPkg<EffectRegistry>(v => effectRegistry = v);
            data.ReadPkg<AList>(v => thresholdList = v);
            data.ReadPkg<IPackage>(v => usageTakePermission = v);
        }
    }
}
