namespace AC2RE.Definitions {

    public class EffectRecord : IPackage {

        public PackageType packageType => PackageType.EffectRecord;

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
        public uint flags; // m_flags
        public uint durabilityLevel; // m_uiDurabilityLevel
        public uint relatedEffectId; // m_relatedEID
        public uint effectId; // m_effectID
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
            data.ReadSingletonPkg<Effect>(v => effect = v);
            actingForWhomId = data.ReadInstanceId();
            skillDid = data.ReadDataId();
            fromItemId = data.ReadInstanceId();
            flags = data.ReadUInt32();
            durabilityLevel = data.ReadUInt32();
            relatedEffectId = data.ReadUInt32();
            effectId = data.ReadUInt32();
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
            data.Write(flags);
            data.Write(durabilityLevel);
            data.Write(relatedEffectId);
            data.Write(effectId);
            data.Write(categories);
            data.Write(maxDurabilityLevel);
        }
    }
}
