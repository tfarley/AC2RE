using System.Collections.Generic;

namespace AC2E.Def {

    public class Eff_SetOmishanResetTimers : Effect {

        public override PackageType packageType => PackageType.Eff_SetOmishanResetTimers;

        public List<uint> recipes; // m_recipeList

        public Eff_SetOmishanResetTimers(AC2Reader data) : base(data) {
            data.ReadPkg<AList>(v => recipes = v);
        }
    }
}
