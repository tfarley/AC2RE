using System.Collections.Generic;

namespace AC2E.Def {

    public class InteractionTable : IPackage {

        public PackageType packageType => PackageType.InteractionTable;

        public List<TargetInteraction> targetInteractions; // m_listTargetInteractions

        public InteractionTable(AC2Reader data) {
            data.ReadPkg<RList>(v => targetInteractions = v.to<TargetInteraction>());
        }
    }
}
