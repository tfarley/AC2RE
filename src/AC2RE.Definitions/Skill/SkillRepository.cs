﻿using System.Collections.Generic;

namespace AC2RE.Definitions;

public class SkillRepository : IHeapObject {

    public PackageType packageType => PackageType.SkillRepository;

    public uint skillCredits; // m_nSkillCredits
    public ulong untrainingXp; // m_nUntrainXP
    public uint heroSkillCredits; // m_nHeroSkillCredits
    public Dictionary<uint, uint> perkTypes; // m_hashPerkTypes
    public SkillId skillIdUntraining; // m_typeUntrained
    public Dictionary<uint, uint> categories; // m_hashCategories
    public Dictionary<SkillId, SkillInfo> skills; // m_hashSkills

    public SkillRepository() {

    }

    public SkillRepository(AC2Reader data) {
        skillCredits = data.ReadUInt32();
        untrainingXp = data.ReadUInt64();
        heroSkillCredits = data.ReadUInt32();
        data.ReadHO<AAHash>(v => perkTypes = v);
        skillIdUntraining = data.ReadEnum<SkillId>();
        data.ReadHO<AAHash>(v => categories = v);
        data.ReadHO<ARHash>(v => skills = v.to<SkillId, SkillInfo>());
    }

    public void write(AC2Writer data) {
        data.Write(skillCredits);
        data.Write(untrainingXp);
        data.Write(heroSkillCredits);
        data.WriteHO(AAHash.from(perkTypes));
        data.WriteEnum(skillIdUntraining);
        data.WriteHO(AAHash.from(categories));
        data.WriteHO(ARHash.from(skills));
    }
}
