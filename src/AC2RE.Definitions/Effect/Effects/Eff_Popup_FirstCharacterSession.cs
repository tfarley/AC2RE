namespace AC2RE.Definitions;

public class Eff_Popup_FirstCharacterSession : TextEffect {

    public override PackageType packageType => PackageType.Eff_Popup_FirstCharacterSession;

    public StringInfo empyreanAlternateText; // m_EmpyreanAlternateText
    public StringInfo drudgeAlternateText; // m_DrudgeAlternateText

    public Eff_Popup_FirstCharacterSession(AC2Reader data) : base(data) {
        data.ReadHO<StringInfo>(v => empyreanAlternateText = v);
        data.ReadHO<StringInfo>(v => drudgeAlternateText = v);
    }
}
