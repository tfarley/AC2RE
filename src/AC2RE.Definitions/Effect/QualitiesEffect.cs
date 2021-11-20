using System;

namespace AC2RE.Definitions {

    public class QualitiesEffect : ParameterizedNumericEffect {

        public override PackageType packageType => PackageType.QualitiesEffect;

        // WLib QualitiesEffect
        [Flags]
        public new enum Flag : uint {
            None = 0,

            IsPositiveOnly = 1 << 5, // SetPositiveOnly 0x00000020
            IsNegativeOnly = 1 << 6, // SetNegativeOnly 0x00000040
            IsPositiveNonZeroOnly = 1 << 7, // SetPositiveNonZeroOnly 0x00000080
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
