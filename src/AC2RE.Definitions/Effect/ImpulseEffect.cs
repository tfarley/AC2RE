using System;

namespace AC2RE.Definitions;

public class ImpulseEffect : ParameterizedNumericEffect {

    public override PackageType packageType => PackageType.ImpulseEffect;

    // WLib ImpulseEffect
    [Flags]
    public new enum Flag : uint {
        None = 0,

        HasForward = 1 << 8, // AddForward 0x00000100
        HasBackward = 1 << 9, // AddBackward 0x00000200
        HasUp = 1 << 10, // AddUp 0x00000400
        FromCaster = 1 << 11, // SetFromCaster 0x00000800
        HasUpAndBack = 1 << 12, // AddUpAndBack 0x00001000
        HasUpAndForward = 1 << 13, // AddUpAndForward 0x00002000
    }

    public Flag impulseFlags => (Flag)flags;

    public ImpulseEffect(AC2Reader data) : base(data) {

    }
}
