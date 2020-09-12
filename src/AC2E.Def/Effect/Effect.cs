namespace AC2E.Def {

    public class Effect : IPackage {

        public virtual PackageType packageType => PackageType.Effect;

        public RArray<IPackage> durationData; // m_durationData
        public uint fxId; // m_fxID
        public float aprValue; // m_fAprValue
        public uint externalFlags; // m_uiExternalFlags
        public bool tracked; // m_tracked
        public uint enumVal; // m_enum
        public float minTsysSpellcraft; // m_fMinTsysSpellcraft
        public ulong internalFlags; // m_uiInternalFlags
        public float probVariance; // m_fProbVariance
        public StringInfo tsysItemName; // m_strTsysItemName
        public StringInfo description; // m_strDescription
        public uint aprKey; // m_aprKey
        public RArray<IPackage> forceData; // m_forceData
        public uint examinationFlags; // m_ExaminationFlags
        public float variance; // m_fVariance
        public DefaultPermissionBlob usagePermissions; // m_usagePermissions
        public bool removeOnLogout; // m_removeOnLogout
        public bool removeOnTeleport; // m_removeOnTeleport
        public DataId iconDid; // m_didIcon
        public int tsysValue; // m_iTsysValue
        public uint forceModifyVital; // m_forceModifyVital
        public uint durabilityLevel; // m_uiDurabilityLevel
        public float relativeCasterLevelSpellcraftCap; // m_fRelativeCasterLevelSpellcraftCap
        public float maxTsysSpellcraft; // m_fMaxTsysSpellcraft
        public float tsysAprValue; // m_tsysAprValue
        public uint endingFxId; // m_endingFxID
        public bool clearOnUse; // m_clearOnUse
        public RArray<FloatScaleDuple> priData; // m_priData
        public StringInfo name; // m_strName
        public AList statList; // m_statList
        public RArray<FloatScaleDuple> probData; // m_probData
        public uint tsysAprKey; // m_tsysAprKey
        public float selfTargetedSpellcraftCap; // m_fSelfTargetedSpellcraftCap
        public uint eqClass; // m_eqClass

        public Effect(AC2Reader data) {
            data.ReadPkg<RArray<IPackage>>(v => durationData = v);
            fxId = data.ReadUInt32();
            aprValue = data.ReadSingle();
            externalFlags = data.ReadUInt32();
            tracked = data.ReadBoolean();
            enumVal = data.ReadUInt32();
            minTsysSpellcraft = data.ReadSingle();
            internalFlags = data.ReadUInt64();
            probVariance = data.ReadSingle();
            data.ReadPkg<StringInfo>(v => tsysItemName = v);
            data.ReadPkg<StringInfo>(v => description = v);
            aprKey = data.ReadUInt32();
            data.ReadPkg<RArray<IPackage>>(v => forceData = v);
            examinationFlags = data.ReadUInt32();
            variance = data.ReadSingle();
            data.ReadPkg<DefaultPermissionBlob>(v => usagePermissions = v);
            removeOnLogout = data.ReadBoolean();
            removeOnTeleport = data.ReadBoolean();
            iconDid = data.ReadDataId();
            tsysValue = data.ReadInt32();
            forceModifyVital = data.ReadUInt32();
            durabilityLevel = data.ReadUInt32();
            relativeCasterLevelSpellcraftCap = data.ReadSingle();
            maxTsysSpellcraft = data.ReadSingle();
            tsysAprValue = data.ReadSingle();
            endingFxId = data.ReadUInt32();
            clearOnUse = data.ReadBoolean();
            data.ReadPkg<RArray<IPackage>>(v => priData = v.to<FloatScaleDuple>());
            data.ReadPkg<StringInfo>(v => name = v);
            data.ReadPkg<AList>(v => statList = v);
            data.ReadPkg<RArray<IPackage>>(v => probData = v.to<FloatScaleDuple>());
            tsysAprKey = data.ReadUInt32();
            selfTargetedSpellcraftCap = data.ReadSingle();
            eqClass = data.ReadUInt32();
        }
    }
}
