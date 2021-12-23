namespace AC2RE.Definitions;

public class Eff_RemoveMonsterWeaponsFromPlayersMay05 : Effect {

    public override PackageType packageType => PackageType.Eff_RemoveMonsterWeaponsFromPlayersMay05;

    public StringInfo popupMessageText; // m_siPopupMessage

    public Eff_RemoveMonsterWeaponsFromPlayersMay05(AC2Reader data) : base(data) {
        data.ReadPkg<StringInfo>(v => popupMessageText = v);
    }
}
