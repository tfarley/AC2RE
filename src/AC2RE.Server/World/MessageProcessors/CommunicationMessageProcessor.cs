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
                        if (sEvent.text.literalValue != null && sEvent.text.literalValue.StartsWith('.')) {
                            string[] splitText = sEvent.text.literalValue.Split(' ');
                            switch (splitText[0]) {
                                case ".vel": {
                                        if (splitText.Length < 2) {
                                            sendMessage(player, "Too few arguments", TextType.Error);
                                            return true;
                                        }
                                        if (!float.TryParse(splitText[1], out float value)) {
                                            sendMessage(player, "Cannot parse argument", TextType.Error);
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
                                            sendMessage(player, $"{character.pos.cell.id:X8} {character.pos.frame.pos.X} {character.pos.frame.pos.Y} {character.pos.frame.pos.Z}");
                                        }
                                        break;
                                    }
                                case ".tp": {
                                        if (splitText.Length < 2) {
                                            sendMessage(player, "Too few arguments", TextType.Error);
                                            return true;
                                        }
                                        if (!uint.TryParse(splitText[1], NumberStyles.HexNumber, null, out uint cellId)) {
                                            sendMessage(player, "Cannot parse argument", TextType.Error);
                                            return true;
                                        }
                                        Vector3 offset = new();
                                        if (splitText.Length >= 5) {
                                            if (!float.TryParse(splitText[2], out offset.X)) {
                                                sendMessage(player, "Cannot parse argument", TextType.Error);
                                                return true;
                                            }
                                            if (!float.TryParse(splitText[3], out offset.Y)) {
                                                sendMessage(player, "Cannot parse argument", TextType.Error);
                                                return true;
                                            }
                                            if (!float.TryParse(splitText[4], out offset.Z)) {
                                                sendMessage(player, "Cannot parse argument", TextType.Error);
                                                return true;
                                            }
                                        }
                                        teleport(player, new(new(cellId), offset));
                                        break;
                                    }
                                default:
                                    sendMessage(player, "Invalid command", TextType.Error);
                                    return true;
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
}
