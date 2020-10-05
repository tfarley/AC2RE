﻿using System.Collections.Generic;

namespace AC2E.Def {

    public class CharGenMatrix : IPackage {

        public PackageType packageType => PackageType.CharGenMatrix;

        public Dictionary<SpeciesType, Dictionary<SexType, Dictionary<PhysiqueType, List<AppearanceProfile>>>> physiqueTypeModifierTable; // m_PhysiqueTypeModifierTable
        public Dictionary<uint, WPString> physiqueTypeLabelTable; // m_PhysiqueTypeLabelTable
        public Dictionary<uint, List<uint>> raceSexTable; // m_RaceSexTable
        public Dictionary<SpeciesType, Dictionary<uint, Dictionary<uint, List<StartInvData>>>> startingInventoryTable; // m_StartingInventoryTable
        public Dictionary<uint, GMRaceSexInfo> raceSexInfoTable; // m_RaceSexInfoTable
        public Dictionary<uint, Dictionary<uint, IPackage>> startingAttributesTable; // m_StartingAttributesTable
        public Dictionary<uint, StartArea> startAreaHash; // m_StartAreaHash
        public Dictionary<uint, List<IPackage>> startingSkillsTable; // m_StartingSkillsTable
        public Dictionary<uint, Dictionary<uint, IPackage>> startingLocationTable; // m_StartingLocationTable
        public Dictionary<uint, uint> physiqueTypeAppearanceKeyMap; // m_PhysiqueTypeAppearanceKeyMap

        public CharGenMatrix(AC2Reader data) {
            data.ReadPkg<ARHash>(v => physiqueTypeModifierTable = v.to<SpeciesType, Dictionary<SexType, Dictionary<PhysiqueType, List<AppearanceProfile>>>>(
                v => (v as ARHash).to<SexType, Dictionary<PhysiqueType, List<AppearanceProfile>>>(
                    v => (v as ARHash).to<PhysiqueType, List<AppearanceProfile>>(
                        v => (v as RList).to<AppearanceProfile>()))));
            data.ReadPkg<ARHash>(v => physiqueTypeLabelTable = v.to<uint, WPString>());
            data.ReadPkg<ARHash>(v => raceSexTable = v.to<uint, List<uint>>(v => (v as AList).to<uint>()));
            data.ReadPkg<ARHash>(v => startingInventoryTable = v.to<SpeciesType, Dictionary<uint, Dictionary<uint, List<StartInvData>>>>(
                v => (v as ARHash).to<uint, Dictionary<uint, List<StartInvData>>>(
                    v => (v as ARHash).to<uint, List<StartInvData>>(
                        v => (v as RList).to<StartInvData>()))));
            data.ReadPkg<ARHash>(v => raceSexInfoTable = v.to<uint, GMRaceSexInfo>());
            data.ReadPkg<ARHash>(v => startingAttributesTable = v.to<uint, Dictionary<uint, IPackage>>());
            data.ReadPkg<ARHash>(v => startAreaHash = v.to<uint, StartArea>());
            data.ReadPkg<ARHash>(v => startingSkillsTable = v.to<uint, List<IPackage>>());
            data.ReadPkg<ARHash>(v => startingLocationTable = v.to<uint, Dictionary<uint, IPackage>>());
            data.ReadPkg<AAHash>(v => physiqueTypeAppearanceKeyMap = v);
        }
    }
}
