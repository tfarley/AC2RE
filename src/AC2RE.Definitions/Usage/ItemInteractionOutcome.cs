namespace AC2RE.Definitions {

    public class ItemInteractionOutcome : IPackage {

        public PackageType packageType => PackageType.ItemInteractionOutcome;

        public StringInfo messageText; // m_siMessage
        public uint flags; // m_uiFlags

        public ItemInteractionOutcome(AC2Reader data) {
            data.ReadPkg<StringInfo>(v => messageText = v);
            flags = data.ReadUInt32();
        }
    }
}
