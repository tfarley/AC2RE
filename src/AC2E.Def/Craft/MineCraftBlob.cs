namespace AC2E.Def {

    public class MineCraftBlob : CraftBlob {

        public override PackageType packageType => PackageType.MineCraftBlob;

        public DataId objectDid; // m_didObject
        public float quantityMod; // m_quantityMod

        public MineCraftBlob(AC2Reader data) : base(data) {
            objectDid = data.ReadDataId();
            quantityMod = data.ReadSingle();
        }
    }
}
