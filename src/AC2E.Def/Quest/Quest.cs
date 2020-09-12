namespace AC2E.Def {

    public class Quest : MasterDIDListMember {

        public override PackageType packageType => PackageType.Quest;

        public StringInfo longName; // m_siLongName
        public StringInfo name; // m_siName
        public DataId iconDid; // m_didIcon
        public ARHash<PhaseInfo> questPhaseInfo; // m_questPhaseInfo
        public StringInfo description; // m_siDescription
        public bool playFxOnUpdate; // m_bPlayFXOnUpdate
        public ARHash<IPackage> questUpdateEffects; // m_questUpdateEffects

        public Quest(AC2Reader data) : base(data) {
            data.ReadPkg<StringInfo>(v => longName = v);
            data.ReadPkg<StringInfo>(v => name = v);
            iconDid = data.ReadDataId();
            data.ReadPkg<ARHash<IPackage>>(v => questPhaseInfo = v.to<PhaseInfo>());
            data.ReadPkg<StringInfo>(v => description = v);
            playFxOnUpdate = data.ReadBoolean();
            data.ReadPkg<ARHash<IPackage>>(v => questUpdateEffects = v);
        }
    }
}
