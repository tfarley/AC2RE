namespace AC2E.Def {

    public class gmEntity : Entity {

        public override PackageType packageType => PackageType.gmEntity;

        public VisualDescInfo pileVisualDesc; // m_PileAppearanceInfo
        public AAHash appearanceMutationHash; // m_appearanceMutationHash
        public SingletonPkg<IPackage> usagePermission; // m_usage_usagePermission
        public SingletonPkg<BiasProfile> biasProfile; // m_mf
        public SingletonPkg<IPackage> usageAction; // m_usage_usageAction
        public EffectRegistry effectRegistry; // m_regEffect
        public AList thresholdList; // m_thresholdList
        public SingletonPkg<IPackage> usageTakePermission; // m_usage_takePermission

        public gmEntity(AC2Reader data) : base(data) {
            data.ReadPkg<VisualDescInfo>(v => pileVisualDesc = v);
            data.ReadPkg<AAHash>(v => appearanceMutationHash = v);
            data.ReadSingletonPkg<IPackage>(v => usagePermission = v);
            data.ReadSingletonPkg<BiasProfile>(v => biasProfile = v);
            data.ReadSingletonPkg<IPackage>(v => usageAction = v);
            data.ReadPkg<EffectRegistry>(v => effectRegistry = v);
            data.ReadPkg<AList>(v => thresholdList = v);
            data.ReadSingletonPkg<IPackage>(v => usageTakePermission = v);
        }
    }
}
