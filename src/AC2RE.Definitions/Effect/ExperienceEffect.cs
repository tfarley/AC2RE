using System;

namespace AC2RE.Definitions {

    public class ExperienceEffect : ParameterizedNumericEffect {

        public override PackageType packageType => PackageType.ExperienceEffect;

        // WLib
        [Flags]
        public new enum Flag : uint {
            NONE = 0,
            ALL = uint.MaxValue,

            DONT_DISTRIBUTE_THROUGH_SOCIAL_SYSTEMS = 1 << 30, // 0x40000000, ExperienceEffect::SetDontDistributeThroughSocialSystems
            CHALLENGE_LEVEL = 1u << 31, // 0x80000000, ExperienceEffect::SetChallengeLevel
        }

        public int challengeLevel; // m_chalLvl
        public Flag experienceFlags => (Flag)flags;

        public ExperienceEffect(AC2Reader data) : base(data) {
            challengeLevel = data.ReadInt32();
        }
    }
}
