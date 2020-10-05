using System.Collections.Generic;

namespace AC2E.Def {

    public class ChainedNumericEffect : Effect {

        public override PackageType packageType => PackageType.ChainedNumericEffect;

        public List<Effect> effects; // m_listEffect

        public ChainedNumericEffect(AC2Reader data) : base(data) {
            data.ReadPkg<RList>(v => effects = v.to<Effect>());
        }
    }
}
