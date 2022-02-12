namespace AC2RE.Definitions;

public class TextRecipeAction : IHeapObject {

    public PackageType packageType => PackageType.TextRecipeAction;

    public StringInfo text; // m_siText
    public TextType textType; // m_textType

    public TextRecipeAction(AC2Reader data) {
        data.ReadHO<StringInfo>(v => text = v);
        textType = data.ReadEnum<TextType>();
    }
}
