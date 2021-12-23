using System.Collections.Generic;

namespace AC2RE.Definitions;

public class Clothing : gmCEntity {

    public override PackageType packageType => PackageType.Clothing;

    public Dictionary<PhysiqueSpeciesSexId, DataId> wornAppearanceDidHash; // m_hashWornAppearanceDID

    public Clothing(AC2Reader data) : base(data) {
        data.ReadPkg<AAHash>(v => wornAppearanceDidHash = v.to<PhysiqueSpeciesSexId, DataId>());
    }
}
