﻿namespace AC2E.Def {

    public class FactionChangeEffect : Effect {

        public override PackageType packageType => PackageType.FactionChangeEffect;

        public uint newFactionType; // m_newFactionType
        public RList<FactionEffectEntry> factionEffects; // m_listFactionEffects

        public FactionChangeEffect(AC2Reader data) : base(data) {
            newFactionType = data.ReadUInt32();
            data.ReadPkg<RList<IPackage>>(v => factionEffects = v.to<FactionEffectEntry>());
        }
    }
}
