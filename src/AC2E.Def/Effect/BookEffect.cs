namespace AC2E.Def {

    public class BookEffect : ParameterizedNumericEffect {

        public override PackageType packageType => PackageType.BookEffect;

        public StringInfo bookSource; // m_siBookSource
        public bool controls; // m_bControls
        public DataId imageDid; // m_didImage

        public BookEffect(AC2Reader data) : base(data) {
            data.ReadPkg<StringInfo>(v => bookSource = v);
            controls = data.ReadBoolean();
            imageDid = data.ReadDataId();
        }
    }
}
