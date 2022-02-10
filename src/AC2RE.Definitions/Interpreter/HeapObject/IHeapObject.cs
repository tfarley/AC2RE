namespace AC2RE.Definitions;

public interface IHeapObject : IWritable {

    // HeapObject
    NativeType nativeType => NativeType.Undef; // m_ntype
    PackageType packageType => PackageType.Undef; // m_pkgid
}
