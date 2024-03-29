﻿using System.Collections.Generic;

namespace AC2RE.Definitions;

public class Eff_SetOmishanResetTimers : Effect {

    public override PackageType packageType => PackageType.Eff_SetOmishanResetTimers;

    public List<uint> recipes; // m_recipeList

    public Eff_SetOmishanResetTimers(AC2Reader data) : base(data) {
        data.ReadHO<AList>(v => recipes = v);
    }
}
