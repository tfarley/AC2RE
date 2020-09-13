namespace AC2E.Def {

    public class Door : CItem {

        public override PackageType packageType => PackageType.Door;

        public bool rollingBackAlready; // m_bRollingBackAlready

        public Door(AC2Reader data) : base(data) {
            rollingBackAlready = data.ReadBoolean();
        }
    }
}
