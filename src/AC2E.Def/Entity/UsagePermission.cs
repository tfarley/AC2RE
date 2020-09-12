namespace AC2E.Def {

    public class UsagePermission : MasterListMember {

        public override PackageType packageType => PackageType.UsagePermission;

        public UsagePermission(AC2Reader data) : base(data) {

        }
    }
}
