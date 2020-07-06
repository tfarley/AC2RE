using AC2E.Protocol;
using Serilog;
using System.IO;

namespace AC2E.PacketTool {

    public class MessageReader {

        public static INetMessage read(BinaryReader data) {
            MessageOpcode opcode = (MessageOpcode)data.ReadUInt32();
            switch (opcode) {
                case MessageOpcode.Evt_Physics__CForceCreate_ID:
                    return new CForceCreateMsg(data);
                case MessageOpcode.CHARACTER_CREATE_EVENT:
                    return new CharacterCreateMsg(data);
                case MessageOpcode.CHARACTER_ENTER_GAME_EVENT:
                    return new CharacterEnterGameMsg(data);
                case MessageOpcode.Evt_Login__CharExitGame_ID:
                    return new CharacterExitGameMsg(data);
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
                case MessageOpcode.Evt_Login__ClientSceneRenderingComplete_ID:
                    return new ClientSceneRenderingCompleteMsg();
                case MessageOpcode.Evt_Physics__CLookAtDir_ID:
                    return new CLookAtDirMsg(data);
                case MessageOpcode.Evt_Physics__Contain_ID:
                    return new ContainMsg(data);
                case MessageOpcode.Evt_Physics__CPosition_ID:
                    return new CPositionMsg(data);
                case MessageOpcode.Evt_Physics__CreateObject_ID:
                    return new CreateObjectMsg(data);
                case MessageOpcode.Evt_Physics__CreatePlayer_ID:
                    return new CreatePlayerMsg(data);
                case MessageOpcode.Evt_Physics__DeParent_ID:
                    return new DeParentMsg(data);
                case MessageOpcode.Evt_Physics__DestroyObject_ID:
                    return new DestroyObjectMsg(data);
                case MessageOpcode.Evt_Admin__DisplayStringInfo_ID:
                    return new DisplayStringInfoMsg(data);
                case MessageOpcode.Evt_Physics__DoBehavior_ID:
                    return new DoBehaviorMsg(data);
                case MessageOpcode.Evt_Physics__DoFX_ID:
                    return new DoFxMsg(data);
                case MessageOpcode.Evt_Physics__DoFX_Private_ID:
                    return new DoFxPrivateMsg(data);
                case MessageOpcode.Evt_Physics__DoMode_ID:
                    return new DoModeMsg(data);
                case MessageOpcode.Evt_Physics__DoSlider_ID:
                    return new DoSliderMsg(data);
                case MessageOpcode.Evt_Physics__DoStory_ID:
                    return new DoStoryMsg(data);
                case MessageOpcode.Evt_Interp__InterpCEvent_Cell_ID:
                    return new InterpCEventCellMsg(data);
                case MessageOpcode.Evt_Interp__InterpCEvent_Private_ID:
                    return new InterpCEventPrivateMsg(data);
                case MessageOpcode.Evt_Interp__InterpCEvent_Visual_ID:
                    return new InterpCEventVisualMsg(data);
                case MessageOpcode.Evt_Interp__InterpSEvent_ID:
                    return new InterpSEventMsg(data);
                case MessageOpcode.Evt_Physics__LeaveWorld_ID:
                    return new LeaveWorldMsg(data);
                case MessageOpcode.Evt_Login__CharacterSet_ID:
                    return new LoginCharacterSetMsg(data);
                case MessageOpcode.Evt_Login__MinCharSet_ID:
                    return new LoginMinCharSetMsg(data);
                case MessageOpcode.Evt_Physics__LookAtDir_ID:
                    return new LookAtDirMsg(data);
                case MessageOpcode.Evt_Physics__LookAt_ID:
                    return new LookAtMsg(data);
                case MessageOpcode.Evt_Physics__Parent_ID:
                    return new ParentMsg(data);
                case MessageOpcode.Evt_Physics__PositionCell_ID:
                    return new PositionCellMsg(data);
                case MessageOpcode.Evt_Physics__Position_ID:
                    return new PositionMsg(data);
                case MessageOpcode.Evt_Physics__ReleaseBehavior_ID:
                    return new ReleaseBehaviorMsg(data);
                case MessageOpcode.Evt_Physics__SetAccelerationScale_ID:
                    return new SetAccelerationScaleMsg(data);
                case MessageOpcode.Evt_Physics__SetJumpScale_ID:
                    return new SetJumpScaleMsg(data);
                case MessageOpcode.Evt_Physics__SetMode_ID:
                    return new SetModeMsg(data);
                case MessageOpcode.Evt_Physics__SetVelocityScale_ID:
                    return new SetVelocityScaleMsg(data);
                case MessageOpcode.Evt_Physics__StopBehavior_ID:
                    return new StopBehaviorMsg(data);
                case MessageOpcode.Evt_Physics__StopFX_ID:
                    return new StopFxMsg(data);
                case MessageOpcode.Evt_Admin__WorldName_ID:
                    return new WorldNameMsg(data);
                default:
                    Log.Error($"Unhandled opcode: {opcode}.");
                    return null;
            }
        }
    }
}
