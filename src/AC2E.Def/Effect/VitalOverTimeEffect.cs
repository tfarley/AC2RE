namespace AC2E.Def {

    public class VitalOverTimeEffect : Effect {

        public override PackageType packageType => PackageType.VitalOverTimeEffect;

        public float changePerSecVar; // m_fChangePerSecVar
        public RArray<IPackage> lowBounds; // m_LowBounds
        public RArray<IPackage> changeData; // m_changeData
        public RArray<IPackage> highBounds; // m_HighBounds
        public AArray tickFx; // m_TickFX

        public VitalOverTimeEffect(AC2Reader data) : base(data) {
            changePerSecVar = data.ReadSingle();
            data.ReadPkg<RArray<IPackage>>(v => lowBounds = v);
            data.ReadPkg<RArray<IPackage>>(v => changeData = v);
            data.ReadPkg<RArray<IPackage>>(v => highBounds = v);
            data.ReadPkg<AArray>(v => tickFx = v);
        }
    }
}
