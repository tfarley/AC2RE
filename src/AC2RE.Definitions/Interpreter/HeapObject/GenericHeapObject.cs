namespace AC2RE.Definitions;

public class GenericHeapObject : IHeapObject {

    public NativeType nativeType { get; init; }
    public PackageType packageType { get; init; }

    public GenericHeapObject(NativeType nativeType) {
        this.nativeType = nativeType;
    }

    public GenericHeapObject(PackageType packageType) {
        this.packageType = packageType;
    }
}
