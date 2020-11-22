namespace AC2RE.Definitions {

    public class InventoryGlobals : IPackage {

        public PackageType packageType => PackageType.InventoryGlobals;

        public uint unk1;
        public DataId animDropAllFxDid; // m_didAnimDropAllFX
        public int defaultInscriptionLength; // m_iDefaultInscriptionLength
        public DataId animPickupFxDid; // m_didAnimPickupFX
        public uint defaultMaxStackSize; // m_defaultMaxStackSize
        public DataId animGetFxDid; // m_didAnimGetFX
        public DataId animDropSplitStackFxDid; // m_didAnimDropSplitStackFX
        public DataId animPutFxDid; // m_didAnimPutFX
        public uint numInventorySlots; // m_uiNumInventorySlots
        public uint unk2;

        public InventoryGlobals(AC2Reader data) {
            unk1 = data.ReadUInt32();
            animDropAllFxDid = data.ReadDataId();
            defaultInscriptionLength = data.ReadInt32();
            animPickupFxDid = data.ReadDataId();
            defaultMaxStackSize = data.ReadUInt32();
            animGetFxDid = data.ReadDataId();
            animDropSplitStackFxDid = data.ReadDataId();
            animPutFxDid = data.ReadDataId();
            numInventorySlots = data.ReadUInt32();
            unk2 = data.ReadUInt32();
        }
    }
}
