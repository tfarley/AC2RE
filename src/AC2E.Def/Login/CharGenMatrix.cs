namespace AC2E.Def {

    public class CharGenMatrix : IPackage {

        public PackageType packageType => PackageType.CharGenMatrix;

        public ARHash<ARHash<IPackage>> physiqueTypeModifierTable; // m_PhysiqueTypeModifierTable
        public ARHash<WPString> physiqueTypeLabelTable; // m_PhysiqueTypeLabelTable
        public ARHash<AList> raceSexTable; // m_RaceSexTable
        public ARHash<ARHash<IPackage>> startingInventoryTable; // m_StartingInventoryTable
        public ARHash<GMRaceSexInfo> raceSexInfoTable; // m_RaceSexInfoTable
        public ARHash<ARHash<IPackage>> startingAttributesTable; // m_StartingAttributesTable
        public ARHash<StartArea> startAreaHash; // m_StartAreaHash
        public ARHash<RList<IPackage>> startingSkillsTable; // m_StartingSkillsTable
        public ARHash<ARHash<IPackage>> startingLocationTable; // m_StartingLocationTable
        public AAHash physiqueTypeAppearanceKeyMap; // m_PhysiqueTypeAppearanceKeyMap

        public CharGenMatrix(AC2Reader data) {
            data.ReadPkg<ARHash<IPackage>>(v => physiqueTypeModifierTable = v.to<ARHash<IPackage>>());
            data.ReadPkg<ARHash<IPackage>>(v => physiqueTypeLabelTable = v.to<WPString>());
            data.ReadPkg<ARHash<IPackage>>(v => raceSexTable = v.to<AList>());
            data.ReadPkg<ARHash<IPackage>>(v => startingInventoryTable = v.to<ARHash<IPackage>>());
            data.ReadPkg<ARHash<IPackage>>(v => raceSexInfoTable = v.to<GMRaceSexInfo>());
            data.ReadPkg<ARHash<IPackage>>(v => startingAttributesTable = v.to<ARHash<IPackage>>());
            data.ReadPkg<ARHash<IPackage>>(v => startAreaHash = v.to<StartArea>());
            data.ReadPkg<ARHash<IPackage>>(v => startingSkillsTable = v.to<RList<IPackage>>());
            data.ReadPkg<ARHash<IPackage>>(v => startingLocationTable = v.to<ARHash<IPackage>>());
            data.ReadPkg<AAHash>(v => physiqueTypeAppearanceKeyMap = v);
        }
    }
}
