namespace AC2E.Def {

    public class Eff_FlagAccountCanCreateDrudges : Effect {

        public override PackageType packageType => PackageType.Eff_FlagAccountCanCreateDrudges;

        public StringInfo drudgeMessageText; // m_siDrudgeMessage

        public Eff_FlagAccountCanCreateDrudges(AC2Reader data) : base(data) {
            data.ReadPkg<StringInfo>(v => drudgeMessageText = v);
        }
    }
}
