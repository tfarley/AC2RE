using System.Collections.Generic;

namespace AC2RE.Definitions;

public class PhaseInfo : IHeapObject {

    public PackageType packageType => PackageType.PhaseInfo;

    public List<QuestId> subquests; // m_listSubquests
    public uint phaseRange; // m_uiPhaseRange
    public bool subquestAny; // m_bSubquestAny
    public StringInfo journalEntry; // m_siJournalEntry

    public PhaseInfo(AC2Reader data) {
        data.ReadHO<AList>(v => subquests = v.to<QuestId>());
        phaseRange = data.ReadUInt32();
        subquestAny = data.ReadBoolean();
        data.ReadHO<StringInfo>(v => journalEntry = v);
    }
}
