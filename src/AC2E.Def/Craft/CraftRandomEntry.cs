namespace AC2E.Def {

    public class CraftRandomEntry : IPackage {

        public PackageType packageType => PackageType.CraftRandomEntry;

        public RList<IPackage> outcomeActions; // m_outcomeActions
        public float craftXpMod; // m_fCraftXPMod
        public float threshold; // m_fThresh
        public uint inventorySlots; // m_uiInvSlots

        public CraftRandomEntry(AC2Reader data) {
            data.ReadPkg<RList<IPackage>>(v => outcomeActions = v);
            craftXpMod = data.ReadSingle();
            threshold = data.ReadSingle();
            inventorySlots = data.ReadUInt32();
        }
    }
}
