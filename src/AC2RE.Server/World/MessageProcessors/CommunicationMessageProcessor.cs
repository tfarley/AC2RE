using AC2RE.Definitions;
using System.Globalization;
using System.Numerics;

namespace AC2RE.Server;

internal class CommunicationMessageProcessor : BaseMessageProcessor {

    public CommunicationMessageProcessor(World world) : base(world) {

    }

    public override bool processMessage(ClientConnection client, Player player, INetMessage genericMsg) {
        switch (genericMsg.opcode) {
            case MessageOpcode.Interp__InterpSEvent: {
                    InterpSEventMsg msg = (InterpSEventMsg)genericMsg;
                    if (msg.netEvent.funcId == ServerEventFunctionId.Communication__Say) {
                        SaySEvt sEvent = (SaySEvt)msg.netEvent;
                        if (sEvent.text.literalValue != null) {
                            if (sEvent.text.literalValue.StartsWith('.')) {
                                string[] splitText = sEvent.text.literalValue.Split(' ');
                                switch (splitText[0]) {
                                    case ".vel": {
                                            if (splitText.Length < 2) {
                                                sendText(player, "Too few arguments", TextType.Error);
                                                return true;
                                            }
                                            if (!float.TryParse(splitText[1], out float value)) {
                                                sendText(player, "Cannot parse argument", TextType.Error);
                                                return true;
                                            }
                                            if (tryGetCharacter(player, out WorldObject? character)) {
                                                character.setVelScale(value);
                                                character.doFx(FxId.Portal_Use, 1.0f);
                                            }
                                            break;
                                        }
                                    case ".pos": {
                                            if (tryGetCharacter(player, out WorldObject? character)) {
                                                sendText(player, $"{character.pos.cell.id:X8} {character.pos.frame.pos.X} {character.pos.frame.pos.Y} {character.pos.frame.pos.Z}");
                                            }
                                            break;
                                        }
                                    case ".tp": {
                                            if (splitText.Length < 2) {
                                                sendText(player, "Too few arguments", TextType.Error);
                                                return true;
                                            }
                                            if (!uint.TryParse(splitText[1], NumberStyles.HexNumber, null, out uint cellId)) {
                                                sendText(player, "Cannot parse argument", TextType.Error);
                                                return true;
                                            }
                                            Vector3 offset = new();
                                            if (splitText.Length >= 5) {
                                                if (!float.TryParse(splitText[2], out offset.X)) {
                                                    sendText(player, "Cannot parse argument", TextType.Error);
                                                    return true;
                                                }
                                                if (!float.TryParse(splitText[3], out offset.Y)) {
                                                    sendText(player, "Cannot parse argument", TextType.Error);
                                                    return true;
                                                }
                                                if (!float.TryParse(splitText[4], out offset.Z)) {
                                                    sendText(player, "Cannot parse argument", TextType.Error);
                                                    return true;
                                                }
                                            }
                                            teleport(player, new(new(cellId), offset));
                                            break;
                                        }
                                    default:
                                        sendText(player, "Invalid command", TextType.Error);
                                        break;
                                }
                            } else if (tryGetCharacter(player, out WorldObject? character)) {
                                world.playerManager.sendAllVisible(character.id, new InterpCEventCellMsg {
                                    senderIdWithStamp = character.getInstanceIdWithStamp(),
                                    netEvent = new DoSayCEvt {
                                        text = sEvent.text,
                                    },
                                });
                            }
                        }
                    } else {
                        return false;
                    }
                    break;
                }
            case MessageOpcode.Login__ChatServerData: {
                    ChatServerDataMsg msg = (ChatServerDataMsg)genericMsg;
                    if (tryGetCharacter(player, out WorldObject? character)) {
                        if (msg.method is SendToRoomByIdChatRequestAsyncMethod method) {
                            world.playerManager.sendAll(new ChatServerDataMsg {
                                header = new() {
                                    blobType = ChatNetworkBlobType.EVENT_BINARY,
                                    blobDispatchType = ChatAsyncMethodId.SEND_TO_ROOM_BY_NAME,
                                    targetType = 1,
                                    targetId = 15794206,
                                    transportType = 1,
                                    transportId = 15794206,
                                },
                                method = new SendToRoomChatEvent {
                                    roomId = method.roomId,
                                    sourceDisplayName = StringUtil.removeMetaTags(character.name!.literalValue),
                                    text = method.text,
                                    remoteBlob = method.remoteBlob,
                                }
                            });
                            world.playerManager.send(player, new ChatServerDataMsg {
                                header = new() {
                                    blobType = ChatNetworkBlobType.RESPONSE_BINARY,
                                    blobDispatchType = ChatAsyncMethodId.SEND_TO_ROOM_BY_ID,
                                    targetType = 1,
                                    targetId = 15794206,
                                    transportType = 1,
                                    transportId = 15794206,
                                },
                                method = new SendToRoomByIdChatResponseAsyncMethod {
                                    contextId = method.contextId,
                                    requestId = method.requestId,
                                    methodId = ChatAsyncMethodId.SEND_TO_ROOM_BY_ID,
                                    result = 0,
                                }
                            });
                            break;
                        }
                    }
                    break;
                }
            default:
                return false;
        }
        return true;
    }
}
