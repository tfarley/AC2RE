using System;

namespace AC2RE.Definitions;

public class ItemInteractionOutcome : IHeapObject {

    public PackageType packageType => PackageType.ItemInteractionOutcome;

    // WLib ItemInteractionOutcome
    [Flags]
    public enum Flag : uint {
        None = 0,
        DestroySourceItem = 1 << 0, // DestroySourceItem 0x00000001
        DestroyTargetItem = 1 << 1, // DestroyTargetItem 0x00000002
    }

    public StringInfo messageText; // m_siMessage
    public Flag flags; // m_uiFlags

    public ItemInteractionOutcome(AC2Reader data) {
        data.ReadHO<StringInfo>(v => messageText = v);
        flags = (Flag)data.ReadUInt32();
    }
}
