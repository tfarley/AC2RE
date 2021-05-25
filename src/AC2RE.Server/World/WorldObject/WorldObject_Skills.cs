using AC2RE.Definitions;
using System.Collections.Generic;

namespace AC2RE.Server {

    internal partial class WorldObject {

        public SkillRepository skillRepo;

        private void initSkills() {
            skillRepo = new() {
                perkTypes = new(),
                categories = new(),
                skills = new(),
            };
        }

        public ErrorType trainSkill(SkillId skillId) {
            Skill skill = world.contentManager.getSkill(skillId);
            if (skillRepo.skillCredits < skill.cost) {
                return ErrorType.SKILL_NOTENOUGHCREDITS;
            }

            if (skill.allowedSpecies != SpeciesType.UNDEF && !skill.allowedSpecies.HasFlag(species)) {
                return ErrorType.SKILL_WRONGSPECIES;
            }

            if (skill.allowedFactions != FactionType.UNDEF && !skill.allowedFactions.HasFlag(faction)) {
                return ErrorType.SKILL_WRONGSPECIES;
            }

            HashSet<SkillId> trainedSkills = new();
            foreach (SkillInfo ownedSkill in skillRepo.skills.Values) {
                if (ownedSkill.mask.HasFlag(SkillInfoMask.TRAINED)) {
                    if (ownedSkill.skillId == skillId) {
                        return ErrorType.SKILL_ALREADYTRAINED;
                    }
                    if (skill.barringSkillIds != null && skill.barringSkillIds.ContainsKey(ownedSkill.skillId)) {
                        return ErrorType.SKILL_HASBARRINGSKILLS;
                    }
                    trainedSkills.Add(ownedSkill.skillId);
                }
            }

            if (skill.prereqSkillIds != null) {
                foreach (SkillId prereqSkillId in skill.prereqSkillIds.Keys) {
                    if (!trainedSkills.Contains(prereqSkillId)) {
                        return ErrorType.SKILL_MISSINGPREREQSKILLS;
                    }
                }
            }

            if (skill.parentSkillIds != null) {
                foreach (SkillId parentSkillId in skill.parentSkillIds.Keys) {
                    if (!trainedSkills.Contains(parentSkillId)) {
                        return ErrorType.SKILL_MISSINGPARENTS;
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

            skillInfo.mask |= SkillInfoMask.TRAINED | SkillInfoMask.PERSONAL_UNTRAINABLE;

            skillRepo.skillCredits -= skill.cost;

            return ErrorType.NONE;
        }

        public ErrorType raiseSkill(SkillId skillId) {
            if (skillRepo.skillIdUntraining == skillId) {
                return ErrorType.SKILL_BEINGUNTRAINED;
            }

            if (!skillRepo.skills.TryGetValue(skillId, out SkillInfo? skillInfo) || !skillInfo.mask.HasFlag(SkillInfoMask.TRAINED)) {
                return ErrorType.SKILL_NOTTRAINED;
            }

            Skill skill = world.contentManager.getSkill(skillId);
            AdvancementTable advancementTable = world.contentManager.getAdvancementTable(skill.advTableDid);

            int nextSkillLevel;
            for (nextSkillLevel = 2; nextSkillLevel < advancementTable.maxLevel && nextSkillLevel < advancementTable.map.Count && skillInfo.xpAllocated >= advancementTable.map[nextSkillLevel]; nextSkillLevel++);

            if (nextSkillLevel > advancementTable.maxLevel) {
                return ErrorType.SKILL_CANNOTRAISE;
            }

            skillInfo.levelCached = (uint)nextSkillLevel;
            skillInfo.timeCached = world.serverTime.time;
            skillInfo.xpAllocated = advancementTable.map[nextSkillLevel];

            return ErrorType.NONE;
        }
    }
}
