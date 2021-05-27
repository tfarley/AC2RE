using System.Collections.Generic;

namespace AC2RE.Definitions {

    public class InstantVitalEffect : Effect {

        public override PackageType packageType => PackageType.InstantVitalEffect;

        public List<FloatScaleDuple> lowBounds; // m_LowBounds
        public SingletonPkg<Effect> hateComboEffect; // m_effHateCombo
        public List<FloatScaleDuple> changeData; // m_changeData
        public float initialChangeVar; // m_fInitialChangeVar
        public List<FloatScaleDuple> highBounds; // m_HighBounds
        public SingletonPkg<Effect> hateLinkerEffect; // m_effHateLinker
        public InstantVitalEffectFlag instantVitalFlags => (InstantVitalEffectFlag)flags;

        public InstantVitalEffect(AC2Reader data) : base(data) {
            data.ReadPkg<RArray>(v => lowBounds = v.to<FloatScaleDuple>());
            data.ReadPkg<Effect>(v => hateComboEffect = v);
            data.ReadPkg<RArray>(v => changeData = v.to<FloatScaleDuple>());
            initialChangeVar = data.ReadSingle();
            data.ReadPkg<RArray>(v => highBounds = v.to<FloatScaleDuple>());
            data.ReadPkg<Effect>(v => hateLinkerEffect = v);
        }
    }
}
