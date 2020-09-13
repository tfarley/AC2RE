namespace AC2E.Def {

    public class NPCNonUsableTemplate : NPC {

        public override PackageType packageType => PackageType.NPCNonUsableTemplate;

        public NPCNonUsableTemplate(AC2Reader data) : base(data) {

        }
    }
}
