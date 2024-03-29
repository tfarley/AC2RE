﻿using System.Collections.Generic;

namespace AC2RE.Definitions;

public class Eff_SetOstethResetTimers : Effect {

    public override PackageType packageType => PackageType.Eff_SetOstethResetTimers;

    public List<uint> recipes; // m_recipeList

    public Eff_SetOstethResetTimers(AC2Reader data) : base(data) {
        data.ReadHO<AList>(v => recipes = v);
    }
}
