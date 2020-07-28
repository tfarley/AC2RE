namespace AC2E.Def {

    public interface IPackage : IWritable {

        NativeType nativeType => NativeType.UNDEF;
        PackageType packageType => PackageType.UNDEF;
    }
}
