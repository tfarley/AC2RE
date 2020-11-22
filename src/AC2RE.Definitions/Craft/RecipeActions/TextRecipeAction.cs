namespace AC2RE.Definitions {

    public class TextRecipeAction : IPackage {

        public PackageType packageType => PackageType.TextRecipeAction;

        public StringInfo text; // m_siText
        public TextType textType; // m_textType

        public TextRecipeAction(AC2Reader data) {
            data.ReadPkg<StringInfo>(v => text = v);
            textType = (TextType)data.ReadUInt32();
        }
    }
}
