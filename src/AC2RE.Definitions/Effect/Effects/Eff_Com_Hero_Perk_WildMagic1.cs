﻿namespace AC2RE.Definitions;

public class Eff_Com_Hero_Perk_WildMagic1 : Effect {

    public override PackageType packageType => PackageType.Eff_Com_Hero_Perk_WildMagic1;

    public RandomSelectionTable randomEffects; // m_randomEffects

    public Eff_Com_Hero_Perk_WildMagic1(AC2Reader data) : base(data) {
        data.ReadHO<RandomSelectionTable>(v => randomEffects = v);
    }
}
