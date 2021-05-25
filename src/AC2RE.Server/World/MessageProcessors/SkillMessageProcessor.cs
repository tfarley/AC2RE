using AC2RE.Definitions;

namespace AC2RE.Server {

    internal class SkillMessageProcessor : BaseMessageProcessor {

        public SkillMessageProcessor(World world) : base(world) {

        }

        public override bool processMessage(ClientConnection client, Player player, INetMessage genericMsg) {
            switch (genericMsg.opcode) {
                case MessageOpcode.Evt_Interp__InterpSEvent_ID: {
                        InterpSEventMsg msg = (InterpSEventMsg)genericMsg;
                        if (msg.netEvent.funcId == ServerEventFunctionId.Skill__RequestTrainSkill) {
                            RequestTrainSkillSEvt sEvent = (RequestTrainSkillSEvt)msg.netEvent;
                            if (tryGetCharacter(player, out WorldObject? character)) {
                                ErrorType trainSkillError = character.trainSkill(sEvent.skillId);
                                if (trainSkillError == ErrorType.NONE) {
                                    sendUpdateSkillRepo(player, character);
                                    sendUpdateSkillInfo(player, character, sEvent.skillId);
                                    Skill skill = world.contentManager.getSkill(sEvent.skillId);
                                    sendMessage(player, new StringInfo(new(0x250004AA), 1533416325, new() {
                                        { StringVariable.SKILL_NAME, new(skill.name) },
                                    }));
                                } else {
                                    sendMessage(player, trainSkillError.ToString(), TextType.ERROR);
                                }
                            }
                        } else if (msg.netEvent.funcId == ServerEventFunctionId.Skill__RequestRaiseSkill) {
                            RequestRaiseSkillSEvt sEvent = (RequestRaiseSkillSEvt)msg.netEvent;
                            if (tryGetCharacter(player, out WorldObject? character)) {
                                ErrorType raiseSkillError = character.raiseSkill(sEvent.skillId);
                                if (raiseSkillError == ErrorType.NONE) {
                                    sendUpdateSkillRepo(player, character);
                                    sendUpdateSkillInfo(player, character, sEvent.skillId);
                                    Skill skill = world.contentManager.getSkill(sEvent.skillId);
                                    sendMessage(player, new StringInfo(new(0x250004AA), 2419982329, new() {
                                        { StringVariable.SKILL_NAME, new(skill.name) },
                                        { StringVariable.SKILL_LEVEL, new((int)character.skillRepo.skills[sEvent.skillId].levelCached) },
                                    }));
                                } else {
                                    sendMessage(player, raiseSkillError.ToString(), TextType.ERROR);
                                }
                            }
                        } else {
                            return false;
                        }
                        break;
                    }
                default:
                    return false;
            }
            return true;
        }

        private void sendUpdateSkillRepo(Player player, WorldObject character) {
            send(player, new InterpCEventPrivateMsg {
                netEvent = new UpdateSkillRepositoryCEvt {
                    skillRepository = new() {
                        skillCredits = character.skillRepo.skillCredits,
                        untrainingXp = character.skillRepo.untrainingXp,
                        heroSkillCredits = character.skillRepo.heroSkillCredits,
                        skillIdUntraining = character.skillRepo.skillIdUntraining,
                    },
                }
            });
        }

        private void sendUpdateSkillInfo(Player player, WorldObject character, SkillId skillId) {
            send(player, new InterpCEventPrivateMsg {
                netEvent = new UpdateSkillInfoCEvt {
                    skillInfo = character.skillRepo.skills[skillId],
                }
            });
        }
    }
}
