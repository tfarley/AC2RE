using System.Collections.Generic;

namespace AC2RE.Definitions {

    public class VitalTransferEffect : Effect {

        public override PackageType packageType => PackageType.VitalTransferEffect;

        public List<FloatScaleDuple> casterChangeData; // m_casterChangeData
        public float casterChangeVar; // m_fCasterChangeVar
        public List<FloatScaleDuple> targetChangeData; // m_targetChangeData
        public float targetChangeVar; // m_fTargetChangeVar

        public VitalTransferEffect(AC2Reader data) : base(data) {
            data.ReadPkg<RArray>(v => casterChangeData = v.to<FloatScaleDuple>());
            casterChangeVar = data.ReadSingle();
            data.ReadPkg<RArray>(v => targetChangeData = v.to<FloatScaleDuple>());
            targetChangeVar = data.ReadSingle();
        }
    }
}
