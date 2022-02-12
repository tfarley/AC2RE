using System.Collections.Generic;

namespace AC2RE.Definitions;

public class TargetInteraction : IHeapObject {

    public PackageType packageType => PackageType.TargetInteraction;

    public List<ItemInteractionOutcome> successOutcomes; // m_listSuccessOutcomes
    public bool validTargetExternal; // m_bValidTargetExternal
    public int iidModRequiredResult; // m_nIIDModRequiredResult
    public HashSet<uint> validTargetClasses; // m_hashValidTargetClasses
    public List<ItemInteractionOutcome> failureOutcomes; // m_listFailureOutcomes
    public uint userAnimRepeatCount; // m_fUserAnimRepeatCount
    public DataId craftSkillDid; // m_didCraftSkill
    public BehaviorId anim; // m_anim
    public float externalTargetMaxDistance; // m_fExternalTargetMaxDistance
    public bool validTargetPet; // m_bValidTargetPet
    public float userAnimTimeScale; // m_fUserAnimTimeScale
    public HashSet<DataId> validTargets; // m_hashValidTargets
    public int iidMod; // m_nIIDMod
    public uint difficulty; // m_uiDifficulty
    public bool userAnimFadeChildren; // m_bUserAnimFadeChildren
    public bool validTargetUsable; // m_bValidTargetUsable

    public TargetInteraction(AC2Reader data) {
        data.ReadHO<RList>(v => successOutcomes = v.to<ItemInteractionOutcome>());
        validTargetExternal = data.ReadBoolean();
        iidModRequiredResult = data.ReadInt32();
        data.ReadHO<AHashSet>(v => validTargetClasses = v);
        data.ReadHO<RList>(v => failureOutcomes = v.to<ItemInteractionOutcome>());
        userAnimRepeatCount = data.ReadUInt32();
        craftSkillDid = data.ReadDataId();
        anim = data.ReadEnum<BehaviorId>();
        externalTargetMaxDistance = data.ReadSingle();
        validTargetPet = data.ReadBoolean();
        userAnimTimeScale = data.ReadSingle();
        data.ReadHO<AHashSet>(v => validTargets = v.to<DataId>());
        iidMod = data.ReadInt32();
        difficulty = data.ReadUInt32();
        userAnimFadeChildren = data.ReadBoolean();
        validTargetUsable = data.ReadBoolean();
    }
}
