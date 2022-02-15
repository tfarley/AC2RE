using AC2RE.Definitions;

namespace AC2RE.Server;

internal class UIMessageProcessor : BaseMessageProcessor {

    public UIMessageProcessor(World world) : base(world) {

    }

    public override bool processMessage(ClientConnection client, Player player, INetMessage genericMsg) {
        switch (genericMsg.opcode) {
            case MessageOpcode.Interp__InterpSEvent: {
                    InterpSEventMsg msg = (InterpSEventMsg)genericMsg;
                    if (msg.netEvent.funcId == ServerEventFunctionId.Player__SetShortcut) {
                        SetShortcutSEvt sEvent = (SetShortcutSEvt)msg.netEvent;
                        if (sEvent.index >= player.dbCharacter.shortcuts.Length) {
                            sendMessage(player, "Shortcut index out of range", TextType.Error);
                            return true;
                        }

                        if ((sEvent.shortcut.valString?.Length ?? 0) > 255) {
                            sendMessage(player, "Shortcut alias too long", TextType.Error);
                            return true;
                        }

                        player.dbCharacter.shortcuts[(int)sEvent.index] = sEvent.shortcut;
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
