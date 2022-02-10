using System.Collections.Generic;

namespace AC2RE.Definitions;

public class ConsignerDesc : IHeapObject {

    public PackageType packageType => PackageType.ConsignerDesc;

    public StringInfo locationName; // m_siLocation
    public List<Consignment> consignments; // m_consignments
    public DataId catalogDid; // m_didCatalog

    public ConsignerDesc() {

    }

    public ConsignerDesc(AC2Reader data) {
        data.ReadHO<StringInfo>(v => locationName = v);
        data.ReadHO<RList>(v => consignments = v.to<Consignment>());
        catalogDid = data.ReadDataId();
    }

    public void write(AC2Writer data) {
        data.WriteHO(locationName);
        data.WriteHO(RList.from(consignments));
        data.Write(catalogDid);
    }
}
