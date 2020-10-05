﻿using System.Collections.Generic;

namespace AC2E.Def {

    public class ChainedInstantEffect : Effect {

        public override PackageType packageType => PackageType.ChainedInstantEffect;

        public List<Effect> effects; // m_listEffect

        public ChainedInstantEffect(AC2Reader data) : base(data) {
            data.ReadPkg<RList>(v => effects = v.to<Effect>());
        }
    }
}
