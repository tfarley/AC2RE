namespace AC2E.Def {

    public class VitalTransferEffect : Effect {

        public override PackageType packageType => PackageType.VitalTransferEffect;

        public RArray<FloatScaleDuple> casterChangeData; // m_casterChangeData
        public float casterChangeVar; // m_fCasterChangeVar
        public RArray<FloatScaleDuple> targetChangeData; // m_targetChangeData
        public float targetChangeVar; // m_fTargetChangeVar

        public VitalTransferEffect(AC2Reader data) : base(data) {
            data.ReadPkg<RArray<IPackage>>(v => casterChangeData = v.to<FloatScaleDuple>());
            casterChangeVar = data.ReadSingle();
            data.ReadPkg<RArray<IPackage>>(v => targetChangeData = v.to<FloatScaleDuple>());
            targetChangeVar = data.ReadSingle();
        }
    }
}
