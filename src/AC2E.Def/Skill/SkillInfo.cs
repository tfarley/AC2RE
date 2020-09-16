﻿namespace AC2E.Def {

    public class SkillInfo : IPackage {

        public PackageType packageType => PackageType.SkillInfo;

        public double lastUsedTime; // m_timeLastUsed
        public uint levelCached; // m_levelCached
        public double timeCached; // m_timeCached
        public ulong xpAllocated; // m_nXPAllocated
        public uint mask; // m_mask
        public double grantedTime; // m_timeGranted
        public uint trainedChildren; // m_nTrainedChildren
        public uint trainedDependents; // m_nTrainedDependents
        public uint costWhenLearned; // m_nCostWhenLearned
        public uint skillOverride; // m_nSkillOverride
        public SkillId typeSkill; // m_typeSkill

        public SkillInfo() {

        }

        public SkillInfo(AC2Reader data) {
            lastUsedTime = data.ReadDouble();
            levelCached = data.ReadUInt32();
            timeCached = data.ReadDouble();
            xpAllocated = data.ReadUInt64();
            mask = data.ReadUInt32();
            grantedTime = data.ReadDouble();
            trainedChildren = data.ReadUInt32();
            trainedDependents = data.ReadUInt32();
            costWhenLearned = data.ReadUInt32();
            skillOverride = data.ReadUInt32();
            typeSkill = (SkillId)data.ReadUInt32();
        }

        public void write(AC2Writer data) {
            data.Write(lastUsedTime);
            data.Write(levelCached);
            data.Write(timeCached);
            data.Write(xpAllocated);
            data.Write(mask);
            data.Write(grantedTime);
            data.Write(trainedChildren);
            data.Write(trainedDependents);
            data.Write(costWhenLearned);
            data.Write(skillOverride);
            data.Write((uint)typeSkill);
        }
    }
}
