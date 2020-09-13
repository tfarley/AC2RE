namespace AC2E.Def {

    public class TargetInteraction : IPackage {

        public PackageType packageType => PackageType.TargetInteraction;

        public RList<ItemInteractionOutcome> successOutcomes; // m_listSuccessOutcomes
        public bool validTargetExternal; // m_bValidTargetExternal
        public int iidModRequiredResult; // m_nIIDModRequiredResult
        public AHashSet validTargetClasses; // m_hashValidTargetClasses
        public RList<ItemInteractionOutcome> failureOutcomes; // m_listFailureOutcomes
        public uint userAnimRepeatCount; // m_fUserAnimRepeatCount
        public DataId craftSkillDid; // m_didCraftSkill
        public uint anim; // m_anim
        public float externalTargetMaxDistance; // m_fExternalTargetMaxDistance
        public bool validTargetPet; // m_bValidTargetPet
        public float userAnimTimeScale; // m_fUserAnimTimeScale
        public AHashSet validTargets; // m_hashValidTargets
        public int iidMod; // m_nIIDMod
        public uint difficulty; // m_uiDifficulty
        public bool userAnimFadeChildren; // m_bUserAnimFadeChildren
        public bool validTargetUsable; // m_bValidTargetUsable

        public TargetInteraction(AC2Reader data) {
            data.ReadPkg<RList<IPackage>>(v => successOutcomes = v.to<ItemInteractionOutcome>());
            validTargetExternal = data.ReadBoolean();
            iidModRequiredResult = data.ReadInt32();
            data.ReadPkg<AHashSet>(v => validTargetClasses = v);
            data.ReadPkg<RList<IPackage>>(v => failureOutcomes = v.to<ItemInteractionOutcome>());
            userAnimRepeatCount = data.ReadUInt32();
            craftSkillDid = data.ReadDataId();
            anim = data.ReadUInt32();
            externalTargetMaxDistance = data.ReadSingle();
            validTargetPet = data.ReadBoolean();
            userAnimTimeScale = data.ReadSingle();
            data.ReadPkg<AHashSet>(v => validTargets = v);
            iidMod = data.ReadInt32();
            difficulty = data.ReadUInt32();
            userAnimFadeChildren = data.ReadBoolean();
            validTargetUsable = data.ReadBoolean();
        }
    }
}
