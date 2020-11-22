namespace AC2RE.Definitions {

    public class BallUsageBlob : UsageBlob {

        public override PackageType packageType => PackageType.BallUsageBlob;

        public Position userPos; // m_userPos

        public BallUsageBlob() : base() {

        }

        public BallUsageBlob(AC2Reader data) : base(data) {
            data.ReadPkg<Position>(v => userPos = v);
        }

        public override void write(AC2Writer data) {
            base.write(data);
            data.WritePkg(userPos);
        }
    }
}
