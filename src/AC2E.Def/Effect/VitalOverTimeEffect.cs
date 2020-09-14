namespace AC2E.Def {

    public class VitalOverTimeEffect : Effect {

        public override PackageType packageType => PackageType.VitalOverTimeEffect;

        public float changePerSecVar; // m_fChangePerSecVar
        public RArray<FloatScaleDuple> lowBounds; // m_LowBounds
        public RArray<FloatScaleDuple> changeData; // m_changeData
        public RArray<FloatScaleDuple> highBounds; // m_HighBounds
        public AArray tickFx; // m_TickFX

        public VitalOverTimeEffect(AC2Reader data) : base(data) {
            changePerSecVar = data.ReadSingle();
            data.ReadPkg<RArray<IPackage>>(v => lowBounds = v.to<FloatScaleDuple>());
            data.ReadPkg<RArray<IPackage>>(v => changeData = v.to<FloatScaleDuple>());
            data.ReadPkg<RArray<IPackage>>(v => highBounds = v.to<FloatScaleDuple>());
            data.ReadPkg<AArray>(v => tickFx = v);
        }
    }
}
