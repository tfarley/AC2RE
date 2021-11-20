using System;

namespace AC2RE.Definitions {

    public class EffectRecord : IPackage {

        public PackageType packageType => PackageType.EffectRecord;

        // WLib EffectRecord
        [Flags]
        public enum Flag : uint {
            None = 0,
            IsInfiniteTimeout = 1 << 0, // IsInfiniteTimeout 0x00000001
            IsNormalTimeout = 1 << 1, // IsNormalTimeout 0x00000002
            IsSpecifiedTimeout = 1 << 2, // IsSpecifiedTimeout 0x00000004

            IsRemoveOnLogout = 1 << 4, // IsRemoveOnLogout 0x00000010
            IsPermanentEffect = 1 << 5, // IsPermanentEffect 0x00000020
            IsTransientEffect = 1 << 6, // IsTransientEffect 0x00000040
            IsEquipperEffect = 1 << 7, // IsEquipperEffect 0x00000080
            IsExtractable = 1 << 8, // IsExtractable 0x00000100
            IsToggled = 1 << 9, // IsToggled 0x00000200
            IsPulsed = 1 << 10, // IsPulsed 0x00000400
            IsClientNoUI = 1 << 11, // IsClientNoUI 0x00000800
        }

        public double timeDemotedFromTopLevel; // m_timeDemotedFromTopLevel
        public double timeCast; // m_timeCast
        public InstanceId casterId; // m_iidCaster
        public float timeout; // m_ttTimeout
        public float appFloat; // m_fApp
        public float spellcraft; // m_fSpellcraft
        public int appInt; // m_iApp
        public bool pk; // m_bPK
        public IPackage appPackage; // m_rApp
        public double timePromotedToTopLevel; // m_timePromotedToTopLevel
        public SingletonPkg<Effect> effect; // m_effect
        public InstanceId actingForWhomId; // m_iidActingForWhom
        public DataId skillDid; // m_didSkill
        public InstanceId fromItemId; // m_iidFromItem
        public Flag flags; // m_flags
        public uint durabilityLevel; // m_uiDurabilityLevel
        public EffectId relatedEffectId; // m_relatedEID
        public EffectId effectId; // m_effectID
        public uint categories; // m_categories
        public uint maxDurabilityLevel; // m_uiMaxDurabilityLevel

        public EffectRecord() {

        }

        public EffectRecord(AC2Reader data) {
            timeDemotedFromTopLevel = data.ReadDouble();
            timeCast = data.ReadDouble();
            casterId = data.ReadInstanceId();
            timeout = data.ReadSingle();
            appFloat = data.ReadSingle();
            spellcraft = data.ReadSingle();
            appInt = data.ReadInt32();
            pk = data.ReadBoolean();
            data.ReadPkg<IPackage>(v => appPackage = v);
            timePromotedToTopLevel = data.ReadDouble();
            data.ReadPkg<Effect>(v => effect = v);
            actingForWhomId = data.ReadInstanceId();
            skillDid = data.ReadDataId();
            fromItemId = data.ReadInstanceId();
            flags = (Flag)data.ReadUInt32();
            durabilityLevel = data.ReadUInt32();
            relatedEffectId = data.ReadEffectId();
            effectId = data.ReadEffectId();
            categories = data.ReadUInt32();
            maxDurabilityLevel = data.ReadUInt32();
        }

        public void write(AC2Writer data) {
            data.Write(timeDemotedFromTopLevel);
            data.Write(timeCast);
            data.Write(casterId);
            data.Write(timeout);
            data.Write(appFloat);
            data.Write(spellcraft);
            data.Write(appInt);
            data.Write(pk);
            data.WritePkg(appPackage);
            data.Write(timePromotedToTopLevel);
            data.WritePkg(effect);
            data.Write(actingForWhomId);
            data.Write(skillDid);
            data.Write(fromItemId);
            data.Write((uint)flags);
            data.Write(durabilityLevel);
            data.Write(relatedEffectId);
            data.Write(effectId);
            data.Write(categories);
            data.Write(maxDurabilityLevel);
        }
    }
}
