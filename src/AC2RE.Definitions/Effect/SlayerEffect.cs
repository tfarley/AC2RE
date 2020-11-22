using System.Collections.Generic;

namespace AC2RE.Definitions {

    public class SlayerEffect : Effect {

        public override PackageType packageType => PackageType.SlayerEffect;

        public Dictionary<uint, uint> slayerHash; // m_SlayerHash
        public float slayerVariance; // m_fVariance

        public SlayerEffect(AC2Reader data) : base(data) {
            data.ReadPkg<AAHash>(v => slayerHash = v);
            slayerVariance = data.ReadSingle();
        }
    }
}
