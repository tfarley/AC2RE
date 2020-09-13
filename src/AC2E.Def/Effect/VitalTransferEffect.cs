namespace AC2E.Def {

    public class VitalTransferEffect : Effect {

        public override PackageType packageType => PackageType.VitalTransferEffect;

        public RArray<IPackage> casterChangeData; // m_casterChangeData
        public float casterChangeVar; // m_fCasterChangeVar
        public RArray<IPackage> targetChangeData; // m_targetChangeData
        public float targetChangeVar; // m_fTargetChangeVar

        public VitalTransferEffect(AC2Reader data) : base(data) {
            data.ReadPkg<RArray<IPackage>>(v => casterChangeData = v);
            casterChangeVar = data.ReadSingle();
            data.ReadPkg<RArray<IPackage>>(v => targetChangeData = v);
            targetChangeVar = data.ReadSingle();
        }
    }
}
