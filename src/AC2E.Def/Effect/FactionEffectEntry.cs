namespace AC2E.Def {

    public class FactionEffectEntry : IPackage {

        public PackageType packageType => PackageType.FactionEffectEntry;

        public SingletonPkg<Effect> effect; // m_eff
        public uint rating; // m_rating

        public FactionEffectEntry(AC2Reader data) {
            data.ReadSingletonPkg<Effect>(v => effect = v);
            rating = data.ReadUInt32();
        }
    }
}
