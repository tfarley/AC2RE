namespace AC2E.Def {

    public class gmEntity : IPackage {

        public virtual PackageType packageType => PackageType.gmEntity;

        public LAMultiHash hashLockTakenOnOther; // m_hashLockTakenOnOther
        public int movementFrozenCounter; // m_movementFrozenCounter
        public LogInfo logInfo; // m_logInfo
        public int undetectableCounter; // m_undetectableCounter
        public int animationFrozenCounter; // m_animationFrozenCounter
        public AAHash hashLock; // m_hashLock
        public VisualDescInfo pileVisualDesc; // m_PileAppearanceInfo
        public AAHash appearanceMutationHash; // m_appearanceMutationHash
        public SingletonPkg<UsagePermission> usagePermission; // m_usage_usagePermission
        public SingletonPkg<BiasProfile> biasProfile; // m_mf
        public SingletonPkg<UsageAction> usageAction; // m_usage_usageAction
        public EffectRegistry effectRegistry; // m_regEffect
        public AList thresholdList; // m_thresholdList
        public SingletonPkg<IPackage> usageTakePermission; // m_usage_takePermission

        public gmEntity(AC2Reader data) {
            data.ReadPkg<LAMultiHash>(v => hashLockTakenOnOther = v);
            movementFrozenCounter = data.ReadInt32();
            data.ReadPkg<LogInfo>(v => logInfo = v);
            undetectableCounter = data.ReadInt32();
            animationFrozenCounter = data.ReadInt32();
            data.ReadPkg<AAHash>(v => hashLock = v);
            data.ReadPkg<VisualDescInfo>(v => pileVisualDesc = v);
            data.ReadPkg<AAHash>(v => appearanceMutationHash = v);
            data.ReadPkg<SingletonPkg<IPackage>>(v => usagePermission = v.to<UsagePermission>());
            data.ReadPkg<SingletonPkg<IPackage>>(v => biasProfile = v.to<BiasProfile>());
            data.ReadPkg<SingletonPkg<IPackage>>(v => usageAction = v.to<UsageAction>());
            data.ReadPkg<EffectRegistry>(v => effectRegistry = v);
            data.ReadPkg<AList>(v => thresholdList = v);
            data.ReadPkg<SingletonPkg<IPackage>>(v => usageTakePermission = v);
        }
    }
}
