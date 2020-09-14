namespace AC2E.Def {

    public class CustomFailureRecipeAction : IPackage {

        public PackageType packageType => PackageType.CustomFailureRecipeAction;

        public uint ordinal; // m_uiOrdinal
        public float param; // m_fParam

        public CustomFailureRecipeAction(AC2Reader data) {
            ordinal = data.ReadUInt32();
            param = data.ReadSingle();
        }
    }
}
