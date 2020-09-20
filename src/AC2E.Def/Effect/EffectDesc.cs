﻿namespace AC2E.Def {

    public class EffectDesc : IPackage {

        public PackageType packageType => PackageType.EffectDesc;

        public ARHash<IPackage> considerationHash; // m_hashConsideration
        public uint eid; // m_eid
        public int projectedTargetHealthChange; // m_iProjectedTargetHealthChange
        public int sourceVigorChange; // m_iSourceVigorChange
        public float timeout; // m_ttTimeout
        public InstanceId itemId; // m_iidItem
        public int targetFocusChange; // m_iTargetFocusChange
        public InstanceId actingForId; // m_iidActingFor
        public float spellcraft; // m_fSpellcraft
        public bool pk; // m_bPK
        public int targetHealthChange; // m_iTargetHealthChange
        public int casterLevel; // m_casterLevel
        public int targetHealth; // m_iTargetHealth
        public int sourceHealthChange; // m_iSourceHealthChange
        public SingletonPkg<Effect> effect; // m_effect
        public int targetVigor; // m_iTargetVigor
        public uint flags; // m_flags
        public uint durabilityLevel; // m_uiDurabilityLevel
        public RList<IPackage> sourceEffects; // m_listSourceEffect
        public int targetFocus; // m_iTargetFocus
        public InstanceId targetId; // m_iidTarget
        public uint result; // m_result
        public InstanceId sourceId; // m_iidSource
        public int sourceFocusChange; // m_iSourceFocusChange
        public bool hasProjectedHealth; // m_bHasProjectedHealth
        public int targetVigorChange; // m_iTargetVigorChange
        public DataId skillFromDid; // m_didSkillFrom

        public EffectDesc(AC2Reader data) {
            data.ReadPkg<ARHash<IPackage>>(v => considerationHash = v);
            eid = data.ReadUInt32();
            projectedTargetHealthChange = data.ReadInt32();
            sourceVigorChange = data.ReadInt32();
            timeout = data.ReadSingle();
            itemId = data.ReadInstanceId();
            targetFocusChange = data.ReadInt32();
            actingForId = data.ReadInstanceId();
            spellcraft = data.ReadSingle();
            pk = data.ReadBoolean();
            targetHealthChange = data.ReadInt32();
            casterLevel = data.ReadInt32();
            targetHealth = data.ReadInt32();
            sourceHealthChange = data.ReadInt32();
            data.ReadSingletonPkg<Effect>(v => effect = v);
            targetVigor = data.ReadInt32();
            flags = data.ReadUInt32();
            durabilityLevel = data.ReadUInt32();
            data.ReadPkg<RList<IPackage>>(v => sourceEffects = v);
            targetFocus = data.ReadInt32();
            targetId = data.ReadInstanceId();
            result = data.ReadUInt32();
            sourceId = data.ReadInstanceId();
            sourceFocusChange = data.ReadInt32();
            hasProjectedHealth = data.ReadBoolean();
            targetVigorChange = data.ReadInt32();
            skillFromDid = data.ReadDataId();
        }
    }
}