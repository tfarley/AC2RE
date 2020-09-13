namespace AC2E.Def {

    public class InstantVitalEffect : Effect {

        public override PackageType packageType => PackageType.InstantVitalEffect;

        public RArray<IPackage> lowBounds; // m_LowBounds
        public SingletonPkg<Effect> hateComboEffect; // m_effHateCombo
        public RArray<IPackage> changeData; // m_changeData
        public float initialChangeVar; // m_fInitialChangeVar
        public RArray<IPackage> highBounds; // m_HighBounds
        public SingletonPkg<Effect> hateLinkerEffect; // m_effHateLinker

        public InstantVitalEffect(AC2Reader data) : base(data) {
            data.ReadPkg<RArray<IPackage>>(v => lowBounds = v);
            data.ReadSingletonPkg(v => hateComboEffect = v.to<Effect>());
            data.ReadPkg<RArray<IPackage>>(v => changeData = v);
            initialChangeVar = data.ReadSingle();
            data.ReadPkg<RArray<IPackage>>(v => highBounds = v);
            data.ReadSingletonPkg(v => hateLinkerEffect = v.to<Effect>());
        }
    }
}
