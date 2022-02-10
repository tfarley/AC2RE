namespace AC2RE.Definitions;

public class BookEffect : ParameterizedNumericEffect {

    public override PackageType packageType => PackageType.BookEffect;

    public StringInfo bookSource; // m_siBookSource
    public bool controls; // m_bControls
    public DataId imageDid; // m_didImage

    public BookEffect(AC2Reader data) : base(data) {
        data.ReadHO<StringInfo>(v => bookSource = v);
        controls = data.ReadBoolean();
        imageDid = data.ReadDataId();
    }
}
