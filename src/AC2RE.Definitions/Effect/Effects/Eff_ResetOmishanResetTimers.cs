using System.Collections.Generic;

namespace AC2RE.Definitions;

public class Eff_ResetOmishanResetTimers : Effect {

    public override PackageType packageType => PackageType.Eff_ResetOmishanResetTimers;

    public List<uint> recipes; // m_recipeList

    public Eff_ResetOmishanResetTimers(AC2Reader data) : base(data) {
        data.ReadPkg<AList>(v => recipes = v);
    }
}
