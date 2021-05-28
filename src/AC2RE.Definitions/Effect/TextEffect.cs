using System;

namespace AC2RE.Definitions {

    public class TextEffect : Effect {

        public override PackageType packageType => PackageType.TextEffect;

        // WLib
        [Flags]
        public new enum Flag : uint {
            NONE = 0,
            ALL = uint.MaxValue,

            CHAT_SPEW = 1 << 0, // 0x00000001, TextEffect::IsChatSpew
            LOCAL_BROADCAST = 1 << 1, // 0x00000002, TextEffect::IsLocalBroadcast
            GLOBAL_BROADCAST = 1 << 2, // 0x00000004, TextEffect::IsGlobalBroadcast
            POPUP = 1 << 3, // 0x00000008, TextEffect::IsPopup
        }

        public StringInfo broadcastText; // m_siBroadcast
        public TextType textType; // m_typeText
        public Flag textFlags => (Flag)flags;

        public TextEffect(AC2Reader data) : base(data) {
            data.ReadPkg<StringInfo>(v => broadcastText = v);
            textType = (TextType)data.ReadUInt32();
        }
    }
}
