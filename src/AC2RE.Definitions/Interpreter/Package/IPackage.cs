namespace AC2RE.Definitions;

public interface IPackage : IWritable {

    NativeType nativeType => NativeType.Undef;
    PackageType packageType => PackageType.Undef;
}
