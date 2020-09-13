namespace AC2E.Def {

    public class ShardTemplate : Shard {

        public override PackageType packageType => PackageType.ShardTemplate;

        public ShardTemplate(AC2Reader data) : base(data) {

        }
    }
}
