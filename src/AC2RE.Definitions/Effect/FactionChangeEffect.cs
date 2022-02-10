using System.Collections.Generic;

namespace AC2RE.Definitions;

public class FactionChangeEffect : Effect {

    public override PackageType packageType => PackageType.FactionChangeEffect;

    public FactionType newFactionType; // m_newFactionType
    public List<FactionEffectEntry> factionEffects; // m_listFactionEffects

    public FactionChangeEffect(AC2Reader data) : base(data) {
        newFactionType = (FactionType)data.ReadUInt32();
        data.ReadHO<RList>(v => factionEffects = v.to<FactionEffectEntry>());
    }
}
