using AC2E.Protocol.Message;
using AC2E.Protocol.Message.Messages;
using Serilog;
using System.IO;

namespace AC2E.PacketTool.Reader {

    public class MessageReader {

        public static INetMessage read(BinaryReader data) {
            MessageOpcode opcode = (MessageOpcode)data.ReadUInt32();
            switch (opcode) {
                case MessageOpcode.CHARACTER_CREATE_EVENT:
                    return new CharacterCreateMsg(data);
                case MessageOpcode.CHARACTER_ENTER_GAME_EVENT:
                    return new CharacterEnterGameMsg(data);
                case MessageOpcode.CLIDAT_END_DDD_EVENT:
                    return new CliDatEndDDDMsg(data);
                case MessageOpcode.CLIDAT_ERROR_EVENT:
                    return new CliDatErrorMsg(data);
                case MessageOpcode.CLIDAT_INTERROGATION_EVENT:
                    return new CliDatInterrogationMsg(data);
                case MessageOpcode.CLIDAT_INTERROGATION_RESPONSE_EVENT:
                    return new CliDatInterrogationResponseMsg(data);
                case MessageOpcode.CLIDAT_REQUEST_DATA_EVENT:
                    return new CliDatRequestDataMsg(data);
                case MessageOpcode.Evt_Physics__CreatePlayer_ID:
                    return new CreatePlayerMsg(data);
                case MessageOpcode.Evt_Interp__InterpCEvent_Private_ID:
                    return new InterpCEventPrivateMsg(data);
                case MessageOpcode.Evt_Interp__InterpSEvent_ID:
                    return new InterpSEventMsg(data);
                case MessageOpcode.Evt_Login__CharacterSet_ID:
                    return new LoginCharacterSetMsg(data);
                case MessageOpcode.Evt_Login__MinCharSet_ID:
                    return new LoginMinCharSetMsg(data);
                case MessageOpcode.Evt_Admin__WorldName_ID:
                    return new WorldNameMsg(data);
                default:
                    Log.Error($"Unhandled opcode: {opcode}.");
                    return null;
            }
        }
    }
}
