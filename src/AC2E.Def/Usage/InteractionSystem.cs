namespace AC2E.Def {

    public class InteractionSystem : IPackage {

        public PackageType packageType => PackageType.InteractionSystem;

        public AAHash errorPriorityHash; // m_errorPriorityHash

        public InteractionSystem(AC2Reader data) {
            data.ReadPkg<AAHash>(v => errorPriorityHash = v);
        }
    }
}
