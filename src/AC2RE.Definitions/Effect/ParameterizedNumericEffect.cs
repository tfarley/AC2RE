using System.Collections.Generic;

namespace AC2RE.Definitions {

    public class ParameterizedNumericEffect : Effect {

        public override PackageType packageType => PackageType.ParameterizedNumericEffect;

        public float numericVariance; // m_fVariance
        public List<FloatScaleDuple> magData; // m_magData

        public ParameterizedNumericEffect(AC2Reader data) : base(data) {
            numericVariance = data.ReadSingle();
            data.ReadPkg<RArray>(v => magData = v.to<FloatScaleDuple>());
        }
    }
}
