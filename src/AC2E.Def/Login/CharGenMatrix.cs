namespace AC2E.Def {

    public class CharGenMatrix : IPackage {

        public PackageType packageType => PackageType.CharGenMatrix;

        public ARHash<IPackage> physiqueTypeModifierTable; // m_PhysiqueTypeModifierTable
        public ARHash<IPackage> physiqueTypeLabelTable; // m_PhysiqueTypeLabelTable
        public ARHash<IPackage> raceSexTable; // m_RaceSexTable
        public ARHash<IPackage> startingInventoryTable; // m_StartingInventoryTable
        public ARHash<IPackage> raceSexInfoTable; // m_RaceSexInfoTable
        public ARHash<IPackage> startingAttributesTable; // m_StartingAttributesTable
        public ARHash<IPackage> startAreaHash; // m_StartAreaHash
        public ARHash<IPackage> startingSkillsTable; // m_StartingSkillsTable
        public ARHash<IPackage> startingLocationTable; // m_StartingLocationTable
        public AAHash physiqueTypeAppearanceKeyMap; // m_PhysiqueTypeAppearanceKeyMap

        public CharGenMatrix(AC2Reader data) {
            data.ReadPkg<ARHash<IPackage>>(v => physiqueTypeModifierTable = v);
            data.ReadPkg<ARHash<IPackage>>(v => physiqueTypeLabelTable = v);
            data.ReadPkg<ARHash<IPackage>>(v => raceSexTable = v);
            data.ReadPkg<ARHash<IPackage>>(v => startingInventoryTable = v);
            data.ReadPkg<ARHash<IPackage>>(v => raceSexInfoTable = v);
            data.ReadPkg<ARHash<IPackage>>(v => startingAttributesTable = v);
            data.ReadPkg<ARHash<IPackage>>(v => startAreaHash = v);
            data.ReadPkg<ARHash<IPackage>>(v => startingSkillsTable = v);
            data.ReadPkg<ARHash<IPackage>>(v => startingLocationTable = v);
            data.ReadPkg<AAHash>(v => physiqueTypeAppearanceKeyMap = v);
        }
    }
}
