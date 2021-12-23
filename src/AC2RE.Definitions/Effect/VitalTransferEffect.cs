using System;
using System.Collections.Generic;

namespace AC2RE.Definitions;

public class VitalTransferEffect : Effect {

    public override PackageType packageType => PackageType.VitalTransferEffect;

    // WLib VitalTransferEffect
    [Flags]
    public new enum Flag : uint {
        None = 0,
        IsTargetHealth = 1 << 0, // IsTargetHealth 0x00000001
        IsTargetVigor = 1 << 1, // IsTargetVigor 0x00000002
        IsCasterHealth = 1 << 2, // IsCasterHealth 0x00000004
        IsCasterVigor = 1 << 3, // IsCasterVigor 0x00000008
        IsTargetChangeConstant = 1 << 4, // TargetChangeConstant 0x00000010
        IsTargetChangeRandom = 1 << 5, // TargetChangeRandom 0x00000020
        IsTargetChangeVariable = 1 << 6, // TargetChangeVariable 0x00000040
        IsTargetChangeMultiplicative = 1 << 7, // IsTargetChangeMultiplicative 0x00000080
        IsCasterChangeConstant = 1 << 8, // CasterChangeConstant 0x00000100
        IsCasterChangeRandom = 1 << 9, // CasterChangeRandom 0x00000200
        IsCasterChangeVariable = 1 << 10, // CasterChangeVariable 0x00000400
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
