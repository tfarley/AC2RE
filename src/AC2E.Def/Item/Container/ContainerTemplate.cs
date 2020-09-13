namespace AC2E.Def {

    public class ContainerTemplate : GameplayContainer {

        public override PackageType packageType => PackageType.ContainerTemplate;

        public ContainerTemplate(AC2Reader data) : base(data) {

        }
    }
}
