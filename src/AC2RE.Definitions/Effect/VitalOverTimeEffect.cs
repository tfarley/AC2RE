using System.Collections.Generic;

namespace AC2RE.Definitions {

    public class VitalOverTimeEffect : Effect {

        public override PackageType packageType => PackageType.VitalOverTimeEffect;

        public float changePerSecVar; // m_fChangePerSecVar
        public List<FloatScaleDuple> lowBounds; // m_LowBounds
        public List<FloatScaleDuple> changeData; // m_changeData
        public List<FloatScaleDuple> highBounds; // m_HighBounds
        public List<uint> tickFx; // m_TickFX
        public VitalOverTimeEffectFlag vitalOverTimeFlags => (VitalOverTimeEffectFlag)flags;

        public VitalOverTimeEffect(AC2Reader data) : base(data) {
            changePerSecVar = data.ReadSingle();
            data.ReadPkg<RArray>(v => lowBounds = v.to<FloatScaleDuple>());
            data.ReadPkg<RArray>(v => changeData = v.to<FloatScaleDuple>());
            data.ReadPkg<RArray>(v => highBounds = v.to<FloatScaleDuple>());
            data.ReadPkg<AArray>(v => tickFx = v);
        }
    }
}
