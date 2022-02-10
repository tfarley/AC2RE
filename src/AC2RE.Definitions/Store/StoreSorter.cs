namespace AC2RE.Definitions;

public class StoreSorter : IHeapObject {

    public virtual PackageType packageType => PackageType.StoreSorter;

    public StringInfo name; // m_siName

    public StoreSorter(AC2Reader data) {
        data.ReadHO<StringInfo>(v => name = v);
    }
}
