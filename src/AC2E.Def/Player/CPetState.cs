namespace AC2E.Def {

    public class CPetState : IPackage {

        public PackageType packageType => PackageType.CPetState;

        public LRHash<IPackage> pets; // m_hashPets

        public CPetState(AC2Reader data) {
            data.ReadPkg<LRHash<IPackage>>(v => pets = v);
        }
    }
}
