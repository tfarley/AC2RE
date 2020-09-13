namespace AC2E.Def {

    public class MountEffect : VisualDescEffect {

        public override PackageType packageType => PackageType.MountEffect;

        public uint mountType; // m_mountType

        public MountEffect(AC2Reader data) : base(data) {
            mountType = data.ReadUInt32();
        }
    }
}
