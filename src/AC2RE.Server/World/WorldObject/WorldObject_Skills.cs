using AC2RE.Definitions;
using System.Collections.Generic;

namespace AC2RE.Server;

internal partial class WorldObject {

    public SkillRepository skillRepo = new() {
        perkTypes = new(),
        categories = new(),
        skills = new(),
    };

    private void initSkills() {

    }

    public ErrorType trainSkill(SkillId skillId) {
        Skill skill = world.contentManager.getSkill(skillId);
        if (skillRepo.skillCredits < skill.cost) {
            return ErrorType.Skill_NotEnoughCredits;
        }

        if (skill.allowedSpecies != SpeciesType.Undef && !skill.allowedSpecies.HasFlag(species)) {
            return ErrorType.Skill_WrongSpecies;
        }

        if (skill.allowedFactions != FactionType.Undef && !skill.allowedFactions.HasFlag(faction)) {
            return ErrorType.Skill_WrongFaction;
        }

        HashSet<SkillId> trainedSkills = new();
        foreach (SkillInfo ownedSkill in skillRepo.skills.Values) {
            if (ownedSkill.flags.HasFlag(SkillInfo.Flag.IsTrained)) {
                if (ownedSkill.skillId == skillId) {
                    return ErrorType.Skill_AlreadyTrained;
                }
                if (skill.barringSkillIds != null && skill.barringSkillIds.ContainsKey(ownedSkill.skillId)) {
                    return ErrorType.Skill_HasBarringSkills;
                }
                trainedSkills.Add(ownedSkill.skillId);
            }
        }

        if (skill.prereqSkillIds != null) {
            foreach (SkillId prereqSkillId in skill.prereqSkillIds.Keys) {
                if (!trainedSkills.Contains(prereqSkillId)) {
                    return ErrorType.Skill_MissingPrereqSkills;
                }
            }
        }

        if (skill.parentSkillIds != null) {
            foreach (SkillId parentSkillId in skill.parentSkillIds.Keys) {
                if (!trainedSkills.Contains(parentSkillId)) {
                    return ErrorType.Skill_MissingParents;
                }
            }
        }

        if (!skillRepo.skills.TryGetValue(skillId, out SkillInfo? skillInfo)) {
            skillInfo = new() {
                levelCached = (uint)skill.levelWhenTrained,
                timeCached = world.serverTime.time,
                xpAllocated = world.contentManager.getAdvancementTable(skill.advTableDid).map[skill.levelWhenTrained],
                skillId = skillId,
            };
            skillRepo.skills[skillId] = skillInfo;
        }

        skillInfo.flags |= SkillInfo.Flag.IsTrained | SkillInfo.Flag.IsPersonalUntrainable;

        skillRepo.skillCredits -= skill.cost;

        sendUpdateSkillRepo();
        sendUpdateSkillInfo(skillId);

        return ErrorType.None;
    }

    public ErrorType raiseSkill(SkillId skillId) {
        if (skillRepo.skillIdUntraining == skillId) {
            return ErrorType.Skill_BeingUntrained;
        }

        if (!skillRepo.skills.TryGetValue(skillId, out SkillInfo? skillInfo) || !skillInfo.flags.HasFlag(SkillInfo.Flag.IsTrained)) {
            return ErrorType.Skill_NotTrained;
        }

        Skill skill = world.contentManager.getSkill(skillId);
        AdvancementTable advancementTable = world.contentManager.getAdvancementTable(skill.advTableDid);

        int nextSkillLevel = 2;
        while (nextSkillLevel < advancementTable.maxLevel && nextSkillLevel < advancementTable.map.Count && skillInfo.xpAllocated >= advancementTable.map[nextSkillLevel]) {
            nextSkillLevel++;
        }

        if (nextSkillLevel > advancementTable.maxLevel) {
            return ErrorType.Skill_CannotRaise;
        }

        ulong xpCost = advancementTable.map[nextSkillLevel] - skillInfo.xpAllocated;

        if (xpAvailable < (long)xpCost) {
            return ErrorType.Skill_NotEnoughExperience;
        }

        skillInfo.levelCached = (uint)nextSkillLevel;
        skillInfo.timeCached = world.serverTime.time;
        skillInfo.xpAllocated += xpCost;

        xpAvailable -= (long)xpCost;

        sendUpdateSkillRepo();
        sendUpdateSkillInfo(skillId);

        return ErrorType.None;
    }

    public int getSkillLevel(SkillId skillId) {
        if (!skillRepo.skills.TryGetValue(skillId, out SkillInfo? skillInfo)) {
            return 0;
        }

        Skill skill = world.contentManager.getSkill(skillId);
        AdvancementTable advancementTable = world.contentManager.getAdvancementTable(skill.advTableDid);

        int level = 1;
        while (level < advancementTable.maxLevel && level < advancementTable.map.Count && skillInfo.xpAllocated >= advancementTable.map[level + 1]) {
            level++;
        }
        return level;
    }

    private void sendUpdateSkillRepo() {
        if (player != null) {
            world.playerManager.send(player, new UpdateSkillRepositoryCEvt {
                skillRepository = new() {
                    skillCredits = skillRepo.skillCredits,
                    untrainingXp = skillRepo.untrainingXp,
                    heroSkillCredits = skillRepo.heroSkillCredits,
                    skillIdUntraining = skillRepo.skillIdUntraining,
                },
            });
        }
    }

    private void sendUpdateSkillInfo(SkillId skillId) {
        if (player != null) {
            world.playerManager.send(player, new UpdateSkillInfoCEvt {
                skillInfo = skillRepo.skills[skillId],
            });
        }
    }
}
