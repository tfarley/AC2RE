namespace AC2E.Def {

    public class TextRecipeAction : RecipeAction {

        public override PackageType packageType => PackageType.TextRecipeAction;

        public StringInfo text; // m_siText
        public TextType textType; // m_textType

        public TextRecipeAction(AC2Reader data) : base(data) {
            data.ReadPkg<StringInfo>(v => text = v);
            textType = (TextType)data.ReadUInt32();
        }
    }
}
