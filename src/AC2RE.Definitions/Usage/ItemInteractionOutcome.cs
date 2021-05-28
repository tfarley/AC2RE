using System;

namespace AC2RE.Definitions {

    public class ItemInteractionOutcome : IPackage {

        public PackageType packageType => PackageType.ItemInteractionOutcome;

        // WLib
        [Flags]
        public enum Flag : uint {
            NONE = 0,
            ALL = uint.MaxValue,

            DESTROY_SOURCE_ITEM = 1 << 0, // 0x00000001, ItemInteractionOutcome::DestroySourceItem
            DESTROY_TARGET_ITEM = 1 << 1, // 0x00000002, ItemInteractionOutcome::DestroyTargetItem
        }

        public StringInfo messageText; // m_siMessage
        public Flag flags; // m_uiFlags

        public ItemInteractionOutcome(AC2Reader data) {
            data.ReadPkg<StringInfo>(v => messageText = v);
            flags = (Flag)data.ReadUInt32();
        }
    }
}
