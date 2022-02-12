using System;

namespace AC2RE.Definitions;

public class TravelTieEffect : Effect {

    public override PackageType packageType => PackageType.TravelTieEffect;

    // WLib TravelTieEffectFlag
    [Flags]
    public new enum Flag : uint {
        NONE = 0,

        IsIgnorePortalFlags = 1 << 9, // IsIgnorePortalFlags 0x00000200
    }

    public WeenieType weenieType; // m_wtype
    public uint portalLink; // m_link
    public Flag travelTieFlags => (Flag)flags;

    public TravelTieEffect(AC2Reader data) : base(data) {
        weenieType = data.ReadEnum<WeenieType>();
        portalLink = data.ReadUInt32();
    }
}
