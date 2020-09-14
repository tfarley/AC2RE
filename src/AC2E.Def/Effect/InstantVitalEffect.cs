namespace AC2E.Def {

    public class InstantVitalEffect : Effect {

        public override PackageType packageType => PackageType.InstantVitalEffect;

        public RArray<FloatScaleDuple> lowBounds; // m_LowBounds
        public SingletonPkg<Effect> hateComboEffect; // m_effHateCombo
        public RArray<FloatScaleDuple> changeData; // m_changeData
        public float initialChangeVar; // m_fInitialChangeVar
        public RArray<FloatScaleDuple> highBounds; // m_HighBounds
        public SingletonPkg<Effect> hateLinkerEffect; // m_effHateLinker

        public InstantVitalEffect(AC2Reader data) : base(data) {
            data.ReadPkg<RArray<IPackage>>(v => lowBounds = v.to<FloatScaleDuple>());
            data.ReadSingletonPkg<Effect>(v => hateComboEffect = v);
            data.ReadPkg<RArray<IPackage>>(v => changeData = v.to<FloatScaleDuple>());
            initialChangeVar = data.ReadSingle();
            data.ReadPkg<RArray<IPackage>>(v => highBounds = v.to<FloatScaleDuple>());
            data.ReadSingletonPkg<Effect>(v => hateLinkerEffect = v);
        }
    }
}
