namespace AC2E.Def {

    public class CustomCorpseTemplate : Corpse {

        public override PackageType packageType => PackageType.CustomCorpseTemplate;

        public CustomCorpseTemplate(AC2Reader data) : base(data) {

        }
    }
}
