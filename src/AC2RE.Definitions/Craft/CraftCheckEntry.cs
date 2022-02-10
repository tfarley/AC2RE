using System.Collections.Generic;

namespace AC2RE.Definitions;

public class CraftCheckEntry : IHeapObject {

    public PackageType packageType => PackageType.CraftCheckEntry;

    public List<uint> randomThresholds; // m_listRandThresh
    public float threshold; // m_fThresh
    public Dictionary<uint, CraftRandomEntry> randEntries; // m_hashRandEntries

    public CraftCheckEntry(AC2Reader data) {
        data.ReadHO<AList>(v => randomThresholds = v);
        threshold = data.ReadSingle();
        data.ReadHO<ARHash>(v => randEntries = v.to<uint, CraftRandomEntry>());
    }
}
