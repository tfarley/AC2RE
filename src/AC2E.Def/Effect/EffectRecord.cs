namespace AC2E.Def {

    public class EffectRecord : IPackage {

        public PackageType packageType => PackageType.EffectRecord;

        public double m_timeDemotedFromTopLevel;
        public double m_timeCast;
        public InstanceId m_iidCaster;
        public float m_ttTimeout;
        public float m_fApp;
        public float m_fSpellcraft;
        public int m_iApp;
        public bool m_bPK;
        public IPackage m_rApp;
        public double m_timePromotedToTopLevel;
        public SingletonPkg m_effect;
        public InstanceId m_iidActingForWhom;
        public DataId m_didSkill;
        public InstanceId m_iidFromItem;
        public uint m_flags;
        public uint m_uiDurabilityLevel;
        public uint m_relatedEID;
        public uint m_effectID;
        public uint m_categories;
        public uint m_uiMaxDurabilityLevel;

        public EffectRecord() {

        }

        public EffectRecord(AC2Reader data) {
            m_timeDemotedFromTopLevel = data.ReadDouble();
            m_timeCast = data.ReadDouble();
            m_iidCaster = data.ReadInstanceId();
            m_ttTimeout = data.ReadSingle();
            m_fApp = data.ReadSingle();
            m_fSpellcraft = data.ReadSingle();
            m_iApp = data.ReadInt32();
            m_bPK = data.ReadBoolean();
            data.ReadPkg<IPackage>(v => m_rApp = v);
            m_timePromotedToTopLevel = data.ReadDouble();
            data.ReadPkg<SingletonPkg>(v => m_effect = v);
            m_iidActingForWhom = data.ReadInstanceId();
            m_didSkill = data.ReadDataId();
            m_iidFromItem = data.ReadInstanceId();
            m_flags = data.ReadUInt32();
            m_uiDurabilityLevel = data.ReadUInt32();
            m_relatedEID = data.ReadUInt32();
            m_effectID = data.ReadUInt32();
            m_categories = data.ReadUInt32();
            m_uiMaxDurabilityLevel = data.ReadUInt32();
        }

        public void write(AC2Writer data) {
            data.Write(m_timeDemotedFromTopLevel);
            data.Write(m_timeCast);
            data.Write(m_iidCaster);
            data.Write(m_ttTimeout);
            data.Write(m_fApp);
            data.Write(m_fSpellcraft);
            data.Write(m_iApp);
            data.Write(m_bPK);
            data.WritePkg(m_rApp);
            data.Write(m_timePromotedToTopLevel);
            data.WritePkg(m_effect);
            data.Write(m_iidActingForWhom);
            data.Write(m_didSkill);
            data.Write(m_iidFromItem);
            data.Write(m_flags);
            data.Write(m_uiDurabilityLevel);
            data.Write(m_relatedEID);
            data.Write(m_effectID);
            data.Write(m_categories);
            data.Write(m_uiMaxDurabilityLevel);
        }
    }
}
