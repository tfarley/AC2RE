using System.Collections.Generic;

namespace AC2RE.Definitions {

    public class CharGenMatrix : IPackage {

        public PackageType packageType => PackageType.CharGenMatrix;

        public Dictionary<SpeciesType, Dictionary<SexType, Dictionary<PhysiqueType, List<AppearanceProfile>>>> physiqueTypeModifierTable; // m_PhysiqueTypeModifierTable
        public Dictionary<PhysiqueSpeciesSexId, WPString> physiqueTypeLabelTable; // m_PhysiqueTypeLabelTable
        public Dictionary<SpeciesType, List<SexType>> raceSexTable; // m_RaceSexTable
        public Dictionary<SpeciesType, Dictionary<uint, Dictionary<uint, List<StartInvData>>>> startingInventoryTable; // m_StartingInventoryTable
        public Dictionary<PhysiqueSpeciesSexId, GMRaceSexInfo> raceSexInfoTable; // m_RaceSexInfoTable
        public Dictionary<SpeciesType, Dictionary<SexType, Dictionary<uint, AttributeProfile>>> startingAttributesTable; // m_StartingAttributesTable
        public Dictionary<uint, StartArea> startAreaHash; // m_StartAreaHash
        public Dictionary<uint, List<SkillProfile>> startingSkillsTable; // m_StartingSkillsTable
        public Dictionary<SpeciesType, Dictionary<SpeciesType, List<StartArea>>> startingLocationTable; // m_StartingLocationTable
        public Dictionary<PhysiqueType, AppearanceKey> physiqueTypeAppearanceKeyMap; // m_PhysiqueTypeAppearanceKeyMap

        public CharGenMatrix(AC2Reader data) {
            data.ReadPkg<ARHash>(v => physiqueTypeModifierTable = v.to<SpeciesType, Dictionary<SexType, Dictionary<PhysiqueType, List<AppearanceProfile>>>>(
                v => (v as ARHash).to<SexType, Dictionary<PhysiqueType, List<AppearanceProfile>>>(
                    v => (v as ARHash).to<PhysiqueType, List<AppearanceProfile>>(
                        v => (v as RList).to<AppearanceProfile>()))));
            data.ReadPkg<ARHash>(v => physiqueTypeLabelTable = v.to<PhysiqueSpeciesSexId, WPString>());
            data.ReadPkg<ARHash>(v => raceSexTable = v.to<SpeciesType, List<SexType>>(v => (v as AList).to<SexType>()));
            data.ReadPkg<ARHash>(v => startingInventoryTable = v.to<SpeciesType, Dictionary<uint, Dictionary<uint, List<StartInvData>>>>(
                v => (v as ARHash).to<uint, Dictionary<uint, List<StartInvData>>>(
                    v => (v as ARHash).to<uint, List<StartInvData>>(
                        v => (v as RList).to<StartInvData>()))));
            data.ReadPkg<ARHash>(v => raceSexInfoTable = v.to<PhysiqueSpeciesSexId, GMRaceSexInfo>());
            data.ReadPkg<ARHash>(v => startingAttributesTable = v.to<SpeciesType, Dictionary<SexType, Dictionary<uint, AttributeProfile>>>(
                v => (v as ARHash).to<SexType, Dictionary<uint, AttributeProfile>>(
                    v => (v as ARHash).to<uint, AttributeProfile>())));
            data.ReadPkg<ARHash>(v => startAreaHash = v.to<uint, StartArea>());
            data.ReadPkg<ARHash>(v => startingSkillsTable = v.to<uint, List<SkillProfile>>(v => (v as RList).to<SkillProfile>()));
            data.ReadPkg<ARHash>(v => startingLocationTable = v.to<SpeciesType, Dictionary<SpeciesType, List<StartArea>>>(
                v => (v as ARHash).to<SpeciesType, List<StartArea>>(
                    v => (v as RList).to<StartArea>())));
            data.ReadPkg<AAHash>(v => physiqueTypeAppearanceKeyMap = v.to<PhysiqueType, AppearanceKey>());
        }
    }
}
