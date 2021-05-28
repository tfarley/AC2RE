using System;

namespace AC2RE.Definitions {

    public class QualitiesEffect : ParameterizedNumericEffect {

        public override PackageType packageType => PackageType.QualitiesEffect;

        // WLib
        [Flags]
        public new enum Flag : uint {
            NONE = 0,
            ALL = uint.MaxValue,

            POSITIVE_ONLY = 1 << 5, // 0x00000020, QualitiesEffect::SetPositiveOnly
            NEGATIVE_ONLY = 1 << 6, // 0x00000040, QualitiesEffect::SetNegativeOnly
            NON_ZERO_ONLY = 1 << 7, // 0x00000080, QualitiesEffect::SetPositiveNonZeroOnly
        }

        public float minChange; // m_fMinChange
        public float maxChange; // m_fMaxChange
        public Flag qualitiesFlags => (Flag)flags;

        public QualitiesEffect(AC2Reader data) : base(data) {
            minChange = data.ReadSingle();
            maxChange = data.ReadSingle();
        }
    }
}
