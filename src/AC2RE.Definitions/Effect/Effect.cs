using System.Collections.Generic;

namespace AC2RE.Definitions {

    public class Effect : IPackage {

        public virtual PackageType packageType => PackageType.Effect;

        public List<FloatScaleDuple> durationData; // m_durationData
        public FxId fxId; // m_fxID
        public float appValue; // m_fAprValue
        public uint externalFlags; // m_uiExternalFlags
        public bool tracked; // m_tracked
        public uint enumVal; // m_enum
        public float minTsysSpellcraft; // m_fMinTsysSpellcraft
        public ulong internalFlags; // m_uiInternalFlags
        public float probVariance; // m_fProbVariance
        public StringInfo tsysItemName; // m_strTsysItemName
        public StringInfo description; // m_strDescription
        public AppearanceKey appKey; // m_aprKey
        public List<FloatScaleDuple> forceData; // m_forceData
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
        public float tsysAppValue; // m_tsysAprValue
        public uint endingFxId; // m_endingFxID
        public bool clearOnUse; // m_clearOnUse
        public List<FloatScaleDuple> priData; // m_priData
        public StringInfo name; // m_strName
        public List<uint> statList; // m_statList
        public List<FloatScaleDuple> probData; // m_probData
        public AppearanceKey tsysAppKey; // m_tsysAprKey
        public float selfTargetedSpellcraftCap; // m_fSelfTargetedSpellcraftCap
        public uint eqClass; // m_eqClass

        public Effect(AC2Reader data) {
            data.ReadPkg<RArray>(v => durationData = v.to<FloatScaleDuple>());
            fxId = (FxId)data.ReadUInt32();
            appValue = data.ReadSingle();
            externalFlags = data.ReadUInt32();
            tracked = data.ReadBoolean();
            enumVal = data.ReadUInt32();
            minTsysSpellcraft = data.ReadSingle();
            internalFlags = data.ReadUInt64();
            probVariance = data.ReadSingle();
            data.ReadPkg<StringInfo>(v => tsysItemName = v);
            data.ReadPkg<StringInfo>(v => description = v);
            appKey = (AppearanceKey)data.ReadUInt32();
            data.ReadPkg<RArray>(v => forceData = v.to<FloatScaleDuple>());
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
            tsysAppValue = data.ReadSingle();
            endingFxId = data.ReadUInt32();
            clearOnUse = data.ReadBoolean();
            data.ReadPkg<RArray>(v => priData = v.to<FloatScaleDuple>());
            data.ReadPkg<StringInfo>(v => name = v);
            data.ReadPkg<AList>(v => statList = v);
            data.ReadPkg<RArray>(v => probData = v.to<FloatScaleDuple>());
            tsysAppKey = (AppearanceKey)data.ReadUInt32();
            selfTargetedSpellcraftCap = data.ReadSingle();
            eqClass = data.ReadUInt32();
        }
    }
}
