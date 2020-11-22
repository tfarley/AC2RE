using System.Collections.Generic;

namespace AC2RE.Definitions {

    public class SecretRecipe : Recipe {

        public override PackageType packageType => PackageType.SecretRecipe;

        public uint nextProduct; // m_nextProduct
        public Dictionary<uint, List<uint>> ingredientHash; // m_ingredientHash
        public Dictionary<uint, IPackage> productHash; // m_productHash

        public SecretRecipe(AC2Reader data) : base(data) {
            nextProduct = data.ReadUInt32();
            data.ReadPkg<AAMultiHash>(v => ingredientHash = v);
            data.ReadPkg<ARHash>(v => productHash = v);
        }
    }
}
