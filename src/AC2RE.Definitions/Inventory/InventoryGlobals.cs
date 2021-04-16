namespace AC2RE.Definitions {

    public class InventoryGlobals : IPackage {

        public PackageType packageType => PackageType.InventoryGlobals;

        public uint pad1;
        public DataId animDropAllFxDid; // m_didAnimDropAllFX
        public int defaultInscriptionLength; // m_iDefaultInscriptionLength
        public uint pad2;
        public DataId animPickupFxDid; // m_didAnimPickupFX
        public uint defaultMaxStackSize; // m_defaultMaxStackSize
        public DataId animGetFxDid; // m_didAnimGetFX
        public DataId animDropSplitStackFxDid; // m_didAnimDropSplitStackFX
        public DataId animPutFxDid; // m_didAnimPutFX
        public uint numInventorySlots; // m_uiNumInventorySlots

        public InventoryGlobals(AC2Reader data) {
            pad1 = data.ReadUInt32();
            animDropAllFxDid = data.ReadDataId();
            defaultInscriptionLength = data.ReadInt32();
            pad2 = data.ReadUInt32();
            animPickupFxDid = data.ReadDataId();
            defaultMaxStackSize = data.ReadUInt32();
            animGetFxDid = data.ReadDataId();
            animDropSplitStackFxDid = data.ReadDataId();
            animPutFxDid = data.ReadDataId();
            numInventorySlots = data.ReadUInt32();
        }
    }
}
