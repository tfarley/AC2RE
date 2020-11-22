using System.Collections.Generic;

namespace AC2RE.Definitions {

    public class LinearAttackHook : AttackHook {

        public override PackageType packageType => PackageType.LinearAttackHook;

        public List<FloatScaleDuple> addDmgData; // m_addDmgData
        public List<FloatScaleDuple> multDmgData; // m_multDmgData

        public LinearAttackHook(AC2Reader data) : base(data) {
            data.ReadPkg<RArray>(v => addDmgData = v.to<FloatScaleDuple>());
            data.ReadPkg<RArray>(v => multDmgData = v.to<FloatScaleDuple>());
        }
    }
}
