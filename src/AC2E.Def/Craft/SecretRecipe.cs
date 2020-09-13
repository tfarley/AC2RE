namespace AC2E.Def {

    public class SecretRecipe : Recipe {

        public override PackageType packageType => PackageType.SecretRecipe;

        public uint nextProduct; // m_nextProduct
        public AAMultiHash ingredientHash; // m_ingredientHash
        public ARHash<IPackage> productHash; // m_productHash

        public SecretRecipe(AC2Reader data) : base(data) {
            nextProduct = data.ReadUInt32();
            data.ReadPkg<AAMultiHash>(v => ingredientHash = v);
            data.ReadPkg<ARHash<IPackage>>(v => productHash = v);
        }
    }
}
