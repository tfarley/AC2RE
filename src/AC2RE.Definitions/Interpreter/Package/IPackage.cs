namespace AC2RE.Definitions {

    public interface IPackage : IWritable {

        NativeType nativeType => NativeType.UNDEF;
        PackageType packageType => PackageType.UNDEF;
    }
}
