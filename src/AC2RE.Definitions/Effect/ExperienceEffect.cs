using System;

namespace AC2RE.Definitions {

    public class ExperienceEffect : ParameterizedNumericEffect {

        public override PackageType packageType => PackageType.ExperienceEffect;

        // WLib ExperienceEffect
        [Flags]
        public new enum Flag : uint {
            None = 0,

            DontDistributeThroughSocialSystems = 1 << 30, // SetDontDistributeThroughSocialSystems 0x40000000
            HasChallengeLevel = 1u << 31, // SetChallengeLevel 0x80000000
        }

        public int challengeLevel; // m_chalLvl
        public Flag experienceFlags => (Flag)flags;

        public ExperienceEffect(AC2Reader data) : base(data) {
            challengeLevel = data.ReadInt32();
        }
    }
}
