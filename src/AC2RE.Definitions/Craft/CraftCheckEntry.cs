using System.Collections.Generic;

namespace AC2RE.Definitions;

public class CraftCheckEntry : IPackage {

    public PackageType packageType => PackageType.CraftCheckEntry;

    public List<uint> randomThresholds; // m_listRandThresh
    public float threshold; // m_fThresh
    public Dictionary<uint, CraftRandomEntry> randEntries; // m_hashRandEntries

    public CraftCheckEntry(AC2Reader data) {
        data.ReadPkg<AList>(v => randomThresholds = v);
        threshold = data.ReadSingle();
        data.ReadPkg<ARHash>(v => randEntries = v.to<uint, CraftRandomEntry>());
    }
}
