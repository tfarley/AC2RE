using System.Collections.Generic;

namespace AC2E.Def {

    public class SecretProduct : IPackage {

        public PackageType packageType => PackageType.SecretProduct;

        public List<uint> ingredients; // ingredients
        public DataId productDid; // productDID
        public uint quantity; // productQty

        public SecretProduct(AC2Reader data) {
            data.ReadPkg<AList>(v => ingredients = v);
            productDid = data.ReadDataId();
            quantity = data.ReadUInt32();
        }
    }
}
