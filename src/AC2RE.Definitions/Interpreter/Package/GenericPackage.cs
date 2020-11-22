namespace AC2RE.Definitions {

    public class GenericPackage : IPackage {

        public NativeType nativeType { get; init; }
        public PackageType packageType { get; init; }

        public GenericPackage(NativeType nativeType) {
            this.nativeType = nativeType;
        }

        public GenericPackage(PackageType packageType) {
            this.packageType = packageType;
        }
    }
}
