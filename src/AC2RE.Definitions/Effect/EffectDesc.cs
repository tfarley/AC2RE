using System;
using System.Collections.Generic;

namespace AC2RE.Definitions {

    public class EffectDesc : IPackage {

        public PackageType packageType => PackageType.EffectDesc;

        // WLib
        [Flags]
        public enum Flag : uint {
            NONE = 0,
            ALL = uint.MaxValue,

            CONSIDERING = 1 << 0, // 0x00000001, EffectDesc::IsConsidering
            IGNORE_PROBABILITY = 1 << 1, // 0x00000002, EffectDesc::IsIgnoreProbability
            IGNORE_CONSIDERATION = 1 << 2, // 0x00000004, EffectDesc::IsIgnoreConsideration
            INFINITE_TIMEOUT = 1 << 3, // 0x00000008, EffectDesc::IsInfiniteTimeout
            NORMAL_TIMEOUT = 1 << 4, // 0x00000010, EffectDesc::IsNormalTimeout
            SPECIFIED_TIMEOUT = 1 << 5, // 0x00000020, EffectDesc::IsSpecifiedTimeout
            REMOVE_ON_LOGOUT = 1 << 6, // 0x00000040, EffectDesc::IsRemoveOnLogout
            EQUIPPER = 1 << 7, // 0x00000080, EffectDesc::IsEquipperEffect
            TOGGLED = 1 << 8, // 0x00000100, EffectDesc::IsToggled
            PULSED = 1 << 9, // 0x00000200, EffectDesc::IsPulsed
            CLIENT_NO_UI = 1 << 10, // 0x00000400, EffectDesc::IsClientNoUI
        }

        public Dictionary<uint, IPackage> considerationHash; // m_hashConsideration
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
        public Flag flags; // m_flags
        public uint durabilityLevel; // m_uiDurabilityLevel
        public List<IPackage> sourceEffects; // m_listSourceEffect
        public int targetFocus; // m_iTargetFocus
        public InstanceId targetId; // m_iidTarget
        public uint result; // m_result
        public InstanceId sourceId; // m_iidSource
        public int sourceFocusChange; // m_iSourceFocusChange
        public bool hasProjectedHealth; // m_bHasProjectedHealth
        public int targetVigorChange; // m_iTargetVigorChange
        public DataId skillFromDid; // m_didSkillFrom

        public EffectDesc(AC2Reader data) {
            data.ReadPkg<ARHash>(v => considerationHash = v);
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
            data.ReadPkg<Effect>(v => effect = v);
            targetVigor = data.ReadInt32();
            flags = (Flag)data.ReadUInt32();
            durabilityLevel = data.ReadUInt32();
            data.ReadPkg<RList>(v => sourceEffects = v);
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
