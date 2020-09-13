namespace AC2E.Def {

    public class GenericPackage : IPackage {

        public NativeType nativeType { get; private set; }
        public PackageType packageType { get; private set; }

        public GenericPackage(NativeType nativeType) {
            this.nativeType = nativeType;
        }

        public GenericPackage(PackageType packageType) {
            this.packageType = packageType;
        }
    }
}
