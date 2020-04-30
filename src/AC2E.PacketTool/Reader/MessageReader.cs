﻿using AC2E.Protocol.Message;
using AC2E.Protocol.Message.Messages;
using Serilog;
using System.IO;

namespace AC2E.PacketTool.Reader {

    public class MessageReader {

        public static INetMessage read(BinaryReader data) {
            MessageOpcode opcode = (MessageOpcode)data.ReadUInt32();
            switch (opcode) {
                case MessageOpcode.CLIDAT_INTERROGATION_RESPONSE_EVENT:
                    return new CliDatInterrogationResponseMsg(data);
                case MessageOpcode.CHARACTER_CREATE_EVENT:
                    return new CharacterCreateMsg(data);
                case MessageOpcode.CHARACTER_ENTER_GAME_EVENT:
                    return new CharacterEnterGameMsg(data);
                case MessageOpcode.CLIDAT_REQUEST_DATA_EVENT:
                    return new CliDatRequestDataMsg(data);
                case MessageOpcode.Evt_Interp__InterpSEvent_ID:
                    return new InterpSEventMsg(data);
                default:
                    Log.Error($"Unhandled opcode: {opcode}.");
                    return null;
            }
        }
    }
}
