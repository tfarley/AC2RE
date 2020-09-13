namespace AC2E.Def {

    public class GenericShard : Shard {

        public override PackageType packageType => PackageType.GenericShard;

        public GenericShard(AC2Reader data) : base(data) {

        }
    }
}
