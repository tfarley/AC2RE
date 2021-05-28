using System;

namespace AC2RE.Definitions {

    public class ImpulseEffect : ParameterizedNumericEffect {

        public override PackageType packageType => PackageType.ImpulseEffect;

        // WLib
        [Flags]
        public new enum Flag : uint {
            NONE = 0,
            ALL = uint.MaxValue,

            FORWARD = 1 << 8, // 0x00000100, ImpulseEffect::AddForward
            BACKWARD = 1 << 9, // 0x00000200, ImpulseEffect::AddBackward
            UP = 1 << 10, // 0x00000400, ImpulseEffect::AddUp
            FROM_CASTER = 1 << 11, // 0x00000800, ImpulseEffect::SetFromCaster
            UP_AND_BACK = 1 << 12, // 0x00001000, ImpulseEffect::AddUpAndBack
            UP_AND_FORWARD = 1 << 13, // 0x00002000, ImpulseEffect::AddUpAndForward
        }

        public Flag impulseFlags => (Flag)flags;

        public ImpulseEffect(AC2Reader data) : base(data) {

        }
    }
}
