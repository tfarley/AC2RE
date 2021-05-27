using System;

namespace AC2RE.Definitions {

    // WLib
    [Flags]
    public enum TextEffectFlag : uint {
        NONE = 0,
        ALL = uint.MaxValue,

        CHAT_SPEW = 1 << 0, // 0x00000001, TextEffect::IsChatSpew
        LOCAL_BROADCAST = 1 << 1, // 0x00000002, TextEffect::IsLocalBroadcast
        GLOBAL_BROADCAST = 1 << 2, // 0x00000004, TextEffect::IsGlobalBroadcast
        POPUP = 1 << 3, // 0x00000008, TextEffect::IsPopup
    }
}
