using System.Collections.Generic;

namespace AC2RE.Definitions;

public class StrictAliasControl : IPackage {

    public PackageType packageType => PackageType.StrictAliasControl;

    public Dictionary<IPackage, IPackage> strictAliasTable; // m_strictAliasTable

    public StrictAliasControl(AC2Reader data) {
        data.ReadPkg<NRHash>(v => strictAliasTable = v);
    }
}
