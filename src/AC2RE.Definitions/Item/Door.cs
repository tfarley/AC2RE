namespace AC2RE.Definitions {

    public class Door : gmCEntity {

        public override PackageType packageType => PackageType.Door;

        public bool rollingBackAlready; // m_bRollingBackAlready

        public Door(AC2Reader data) : base(data) {
            rollingBackAlready = data.ReadBoolean();
        }
    }
}
