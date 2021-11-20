using System;

namespace AC2RE.Definitions {

    public class TextEffect : Effect {

        public override PackageType packageType => PackageType.TextEffect;

        // WLib TextEffect
        [Flags]
        public new enum Flag : uint {
            None = 0,
            IsChatSpew = 1 << 0, // IsChatSpew 0x00000001
            IsLocalBroadcast = 1 << 1, // IsLocalBroadcast 0x00000002
            IsGlobalBroadcast = 1 << 2, // IsGlobalBroadcast 0x00000004
            IsPopup = 1 << 3, // IsPopup 0x00000008
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
