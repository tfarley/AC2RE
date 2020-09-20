﻿namespace AC2E.Def {

    public class PhaseInfo : IPackage {

        public PackageType packageType => PackageType.PhaseInfo;

        public AList subquests; // m_listSubquests
        public uint phaseRange; // m_uiPhaseRange
        public bool subquestAny; // m_bSubquestAny
        public StringInfo journalEntry; // m_siJournalEntry

        public PhaseInfo(AC2Reader data) {
            data.ReadPkg<AList>(v => subquests = v);
            phaseRange = data.ReadUInt32();
            subquestAny = data.ReadBoolean();
            data.ReadPkg<StringInfo>(v => journalEntry = v);
        }
    }
}