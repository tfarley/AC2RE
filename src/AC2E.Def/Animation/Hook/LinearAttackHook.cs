namespace AC2E.Def {

    public class LinearAttackHook : AttackHook {

        public override PackageType packageType => PackageType.LinearAttackHook;

        public RArray<IPackage> addDmgData; // m_addDmgData
        public RArray<IPackage> multDmgData; // m_multDmgData

        public LinearAttackHook(AC2Reader data) : base(data) {
            data.ReadPkg<RArray<IPackage>>(v => addDmgData = v);
            data.ReadPkg<RArray<IPackage>>(v => multDmgData = v);
        }
    }
}
