namespace AC2E.Def {

    public class LinearAttackHook : AttackHook {

        public override PackageType packageType => PackageType.LinearAttackHook;

        public RArray<FloatScaleDuple> addDmgData; // m_addDmgData
        public RArray<FloatScaleDuple> multDmgData; // m_multDmgData

        public LinearAttackHook(AC2Reader data) : base(data) {
            data.ReadPkg<RArray<IPackage>>(v => addDmgData = v.to<FloatScaleDuple>());
            data.ReadPkg<RArray<IPackage>>(v => multDmgData = v.to<FloatScaleDuple>());
        }
    }
}
