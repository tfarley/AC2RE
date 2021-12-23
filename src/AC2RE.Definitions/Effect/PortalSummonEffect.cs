using System;

namespace AC2RE.Definitions;

public class PortalSummonEffect : GenesisEffect {

    public override PackageType packageType => PackageType.PortalSummonEffect;

    // WLib PortalSummonEffect
    [Flags]
    public new enum Flag : uint {
        None = 0,

        IsIgnorePortalFlags = 1 << 9, // IsIgnorePortalFlags 0x00000200
    }

    public uint portalLink; // m_link
    public Flag portalSummonFlags => (Flag)flags;

    public PortalSummonEffect(AC2Reader data) : base(data) {
        portalLink = data.ReadUInt32();
    }
}
