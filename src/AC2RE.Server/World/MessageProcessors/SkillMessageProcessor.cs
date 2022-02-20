using AC2RE.Definitions;

namespace AC2RE.Server;

internal class SkillMessageProcessor : BaseMessageProcessor {

    public SkillMessageProcessor(World world) : base(world) {

    }

    public override bool processMessage(ClientConnection client, Player player, INetMessage genericMsg) {
        switch (genericMsg.opcode) {
            case MessageOpcode.Interp__InterpSEvent: {
                    InterpSEventMsg msg = (InterpSEventMsg)genericMsg;
                    if (msg.netEvent.funcId == ServerEventFunctionId.Skill__RequestTrainSkill) {
                        RequestTrainSkillSEvt sEvent = (RequestTrainSkillSEvt)msg.netEvent;
                        if (tryGetCharacter(player, out WorldObject? character)) {
                            ErrorType trainSkillError = character.trainSkill(sEvent.skillId);
                            if (trainSkillError != ErrorType.None) {
                                sendText(player, trainSkillError.ToString(), TextType.Error);
                                return true;
                            }

                            Skill skill = world.contentManager.getSkill(sEvent.skillId);
                            sendText(player, new StringInfo(new(0x250004AA), new(0x5B661385), new() {
                                { StringVariable.SkillName, new(skill.name) },
                            }));
                        }
                    } else if (msg.netEvent.funcId == ServerEventFunctionId.Skill__RequestRaiseSkill) {
                        RequestRaiseSkillSEvt sEvent = (RequestRaiseSkillSEvt)msg.netEvent;
                        if (tryGetCharacter(player, out WorldObject? character)) {
                            ErrorType raiseSkillError = character.raiseSkill(sEvent.skillId);
                            if (raiseSkillError != ErrorType.None) {
                                sendText(player, raiseSkillError.ToString(), TextType.Error);
                                return true;
                            }

                            Skill skill = world.contentManager.getSkill(sEvent.skillId);
                            sendText(player, new StringInfo(new(0x250004AA), new(0x903DFFF9), new() {
                                { StringVariable.SkillName, new(skill.name) },
                                { StringVariable.SkillLevel, new((int)character.skillRepo.skills[sEvent.skillId].levelCached) },
                            }));
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
}
