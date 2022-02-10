using System.Collections.Generic;

namespace AC2RE.Definitions;

public class SecretProduct : IHeapObject {

    public PackageType packageType => PackageType.SecretProduct;

    public List<uint> ingredients; // ingredients
    public DataId productDid; // productDID
    public uint quantity; // productQty

    public SecretProduct(AC2Reader data) {
        data.ReadHO<AList>(v => ingredients = v);
        productDid = data.ReadDataId();
        quantity = data.ReadUInt32();
    }
}
