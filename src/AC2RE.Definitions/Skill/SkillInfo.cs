using System;

namespace AC2RE.Definitions {

    public class SkillInfo : IPackage {

        public PackageType packageType => PackageType.SkillInfo;

        // WLib
        [Flags]
        public enum Flag : uint {
            NONE = 0,
            ALL = uint.MaxValue,

            TRAINED = 1 << 0, // 0x00000001, SkillInfo::IsTrained
            PERSONAL_UNTRAINABLE = 1 << 1, // 0x00000002, SkillInfo::IsPersonalUntrainable

            BASE_MANEUVER = 1 << 3, // 0x00000008, SkillInfo::IsBaseManeuver
            CANNOT_RAISE = 1 << 4, // 0x00000010, SkillInfo::IsCannotRaise
            TIME_LAST_USED = 1 << 5, // 0x00000020, SkillInfo::HasTimeLastUsed
            TIME_GRANTED = 1 << 6, // 0x00000040, SkillInfo::HasTimeGranted
            TOGGLED = 1 << 7, // 0x00000080, SkillInfo::IsToggled
        }

        public double lastUsedTime; // m_timeLastUsed
        public uint levelCached; // m_levelCached
        public double timeCached; // m_timeCached
        public ulong xpAllocated; // m_nXPAllocated
        public Flag flags; // m_mask
        public double grantedTime; // m_timeGranted
        public uint trainedChildren; // m_nTrainedChildren
        public uint trainedDependents; // m_nTrainedDependents
        public uint costWhenLearned; // m_nCostWhenLearned
        public uint skillOverride; // m_nSkillOverride
        public SkillId skillId; // m_typeSkill

        public SkillInfo() {

        }

        public SkillInfo(AC2Reader data) {
            lastUsedTime = data.ReadDouble();
            levelCached = data.ReadUInt32();
            timeCached = data.ReadDouble();
            xpAllocated = data.ReadUInt64();
            flags = (Flag)data.ReadUInt32();
            grantedTime = data.ReadDouble();
            trainedChildren = data.ReadUInt32();
            trainedDependents = data.ReadUInt32();
            costWhenLearned = data.ReadUInt32();
            skillOverride = data.ReadUInt32();
            skillId = (SkillId)data.ReadUInt32();
        }

        public void write(AC2Writer data) {
            data.Write(lastUsedTime);
            data.Write(levelCached);
            data.Write(timeCached);
            data.Write(xpAllocated);
            data.Write((uint)flags);
            data.Write(grantedTime);
            data.Write(trainedChildren);
            data.Write(trainedDependents);
            data.Write(costWhenLearned);
            data.Write(skillOverride);
            data.Write((uint)skillId);
        }
    }
}
