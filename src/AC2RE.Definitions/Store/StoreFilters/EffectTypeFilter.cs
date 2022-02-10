using System.Collections.Generic;

namespace AC2RE.Definitions;

public class EffectTypeFilter : IHeapObject {

    public PackageType packageType => PackageType.EntityFilter;

    public List<uint> effectTypes; // m_effectTypes

    public EffectTypeFilter(AC2Reader data) {
        data.ReadHO<AList>(v => effectTypes = v);
    }
}
