﻿using System;

namespace AC2RE.Definitions;

public class SkillInfo : IHeapObject {

    public PackageType packageType => PackageType.SkillInfo;

    // WLib SkillInfo
    [Flags]
    public enum Flag : uint {
        None = 0,
        IsTrained = 1 << 0, // IsTrained 0x00000001
        IsPersonalUntrainable = 1 << 1, // IsPersonalUntrainable 0x00000002

        IsBaseManeuver = 1 << 3, // IsBaseManeuver 0x00000008
        CannotRaise = 1 << 4, // IsCannotRaise 0x00000010
        HasTimeLastUsed = 1 << 5, // HasTimeLastUsed 0x00000020
        HasTimeGranted = 1 << 6, // HasTimeGranted 0x00000040
        IsToggled = 1 << 7, // IsToggled 0x00000080
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
        flags = data.ReadEnum<Flag>();
        grantedTime = data.ReadDouble();
        trainedChildren = data.ReadUInt32();
        trainedDependents = data.ReadUInt32();
        costWhenLearned = data.ReadUInt32();
        skillOverride = data.ReadUInt32();
        skillId = data.ReadEnum<SkillId>();
    }

    public void write(AC2Writer data) {
        data.Write(lastUsedTime);
        data.Write(levelCached);
        data.Write(timeCached);
        data.Write(xpAllocated);
        data.WriteEnum(flags);
        data.Write(grantedTime);
        data.Write(trainedChildren);
        data.Write(trainedDependents);
        data.Write(costWhenLearned);
        data.Write(skillOverride);
        data.WriteEnum(skillId);
    }
}
