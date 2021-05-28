using System;
using System.Collections.Generic;

namespace AC2RE.Definitions {

    public class VitalTransferEffect : Effect {

        public override PackageType packageType => PackageType.VitalTransferEffect;

        // WLib
        [Flags]
        public new enum Flag : uint {
            NONE = 0,
            ALL = uint.MaxValue,

            TARGET_HEALTH = 1 << 0, // 0x00000001, VitalTransferEffect::IsTargetHealth
            TARGET_VIGOR = 1 << 1, // 0x00000002, VitalTransferEffect::IsTargetVigor
            CASTER_HEALTH = 1 << 2, // 0x00000004, VitalTransferEffect::IsCasterHealth
            CASTER_VIGOR = 1 << 3, // 0x00000008, VitalTransferEffect::IsCasterVigor
            TARGET_CHANGE_CONSTANT = 1 << 4, // 0x00000010, VitalTransferEffect::TargetChangeConstant
            TARGET_CHANGE_RANDOM = 1 << 5, // 0x00000020, VitalTransferEffect::TargetChangeRandom
            TARGET_CHANGE_VARIABLE = 1 << 6, // 0x00000040, VitalTransferEffect::TargetChangeVariable
            TARGET_CHANGE_MULTIPLICATIVE = 1 << 7, // 0x00000080, VitalTransferEffect::IsTargetChangeMultiplicative
            CASTER_CHANGE_CONSTANT = 1 << 8, // 0x00000100, VitalTransferEffect::CasterChangeConstant
            CASTER_CHANGE_RANDOM = 1 << 9, // 0x00000200, VitalTransferEffect::CasterChangeRandom
            CASTER_CHANGE_VARIABLE = 1 << 10, // 0x00000400, VitalTransferEffect::CasterChangeVariable
        }

        public List<FloatScaleDuple> casterChangeData; // m_casterChangeData
        public float casterChangeVar; // m_fCasterChangeVar
        public List<FloatScaleDuple> targetChangeData; // m_targetChangeData
        public float targetChangeVar; // m_fTargetChangeVar
        public Flag vitalTransferFlags => (Flag)flags;

        public VitalTransferEffect(AC2Reader data) : base(data) {
            data.ReadPkg<RArray>(v => casterChangeData = v.to<FloatScaleDuple>());
            casterChangeVar = data.ReadSingle();
            data.ReadPkg<RArray>(v => targetChangeData = v.to<FloatScaleDuple>());
            targetChangeVar = data.ReadSingle();
        }
    }
}
