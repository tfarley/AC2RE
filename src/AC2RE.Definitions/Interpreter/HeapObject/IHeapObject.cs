namespace AC2RE.Definitions;

public interface IHeapObject : IWritable {

    // HeapObject
    public NativeType nativeType => NativeType.Undef; // m_ntype
    public PackageType packageType => PackageType.Undef; // m_pkgid
}
