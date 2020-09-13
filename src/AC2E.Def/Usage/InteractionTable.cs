namespace AC2E.Def {

    public class InteractionTable : IPackage {

        public PackageType packageType => PackageType.InteractionTable;

        public RList<TargetInteraction> targetInteractions; // m_listTargetInteractions

        public InteractionTable(AC2Reader data) {
            data.ReadPkg<RList<IPackage>>(v => targetInteractions = v.to<TargetInteraction>());
        }
    }
}
