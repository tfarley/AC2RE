namespace AC2E.Def {

    public class NPCTemplate : NPC {

        public override PackageType packageType => PackageType.NPCTemplate;

        public NPCTemplate(AC2Reader data) : base(data) {

        }
    }
}
