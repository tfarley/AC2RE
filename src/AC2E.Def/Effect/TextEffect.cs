namespace AC2E.Def {

    public class TextEffect : InstantEffect {

        public override PackageType packageType => PackageType.TextEffect;

        public StringInfo broadcastText; // m_siBroadcast
        public TextType textType; // m_typeText

        public TextEffect(AC2Reader data) : base(data) {
            data.ReadPkg<StringInfo>(v => broadcastText = v);
            textType = (TextType)data.ReadUInt32();
        }
    }
}
