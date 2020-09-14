namespace AC2E.Def {

    public class Act : MasterListMember {

        public override PackageType packageType => PackageType.Act;

        public StringInfo longName; // m_siLongName
        public bool isCompletable; // m_bIsCompletable
        public RList<IPackage> completionEffects; // m_actCompletionEffects
        public bool isVisible; // m_bIsVisible
        public DataId iconDid; // m_didIcon
        public StringInfo description; // m_siDescription
        public ARHash<IPackage> sceneExaminationInfo; // m_sceneExaminationInfo
        public uint numToComplete; // m_uiNumToComplete
        public ARHash<GMSceneInfo> sceneTable; // m_sceneTable
        public bool isActive; // m_bIsActive
        public uint actNum; // m_uiActNum
        public StringInfo name; // m_strName

        public Act(AC2Reader data) : base(data) {
            data.ReadPkg<StringInfo>(v => longName = v);
            isCompletable = data.ReadBoolean();
            data.ReadPkg<RList<IPackage>>(v => completionEffects = v);
            isVisible = data.ReadBoolean();
            iconDid = data.ReadDataId();
            data.ReadPkg<StringInfo>(v => description = v);
            data.ReadPkg<ARHash<IPackage>>(v => sceneExaminationInfo = v);
            numToComplete = data.ReadUInt32();
            data.ReadPkg<ARHash<IPackage>>(v => sceneTable = v.to<GMSceneInfo>());
            isActive = data.ReadBoolean();
            actNum = data.ReadUInt32();
            data.ReadPkg<StringInfo>(v => name = v);
        }
    }
}
