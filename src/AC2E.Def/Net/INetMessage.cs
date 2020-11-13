using System;

namespace AC2E.Def {

    public interface INetMessage : IWritable {

        NetBlobId.Flag blobFlags { get; }
        NetQueue queueId { get; }
        MessageOpcode opcode { get; }

        public static INetMessage read(MessageOpcode opcode, AC2Reader data, bool isClientToServer) {
            return opcode switch {
                MessageOpcode.Evt_Admin__DisplayStringInfo_ID => new DisplayStringInfoMsg(data),
                MessageOpcode.Evt_Admin__WorldName_ID => new WorldNameMsg(data),
                MessageOpcode.CLIDAT_END_DDD_EVENT => new CliDatEndDDDMsg(data),
                MessageOpcode.CLIDAT_ERROR_EVENT => new CliDatErrorMsg(data),
                MessageOpcode.CLIDAT_INTERROGATION_EVENT => new CliDatInterrogationMsg(data),
                MessageOpcode.CLIDAT_INTERROGATION_RESPONSE_EVENT => new CliDatInterrogationResponseMsg(data),
                MessageOpcode.CLIDAT_REQUEST_DATA_EVENT => new CliDatRequestDataMsg(data),
                MessageOpcode.Evt_Interp__InterpCEvent_Cell_ID => new InterpCEventCellMsg(data),
                MessageOpcode.Evt_Interp__InterpCEvent_Private_ID => new InterpCEventPrivateMsg(data),
                MessageOpcode.Evt_Interp__InterpCEvent_Visual_ID => new InterpCEventVisualMsg(data),
                MessageOpcode.Evt_Interp__InterpSEvent_ID => new InterpSEventMsg(data),
                MessageOpcode.CHARACTER_CREATE_EVENT => new CharacterCreateMsg(data),
                MessageOpcode.Evt_Login__CharacterDeletion_ID => isClientToServer ? (INetMessage)new CharacterDeletionSMsg(data) : new CharacterDeletionCMsg(data),
                MessageOpcode.CHARACTER_ENTER_GAME_EVENT => new CharacterEnterGameMsg(data),
                MessageOpcode.Evt_Login__CharacterError_ID => new CharacterErrorMsg(data),
                MessageOpcode.Evt_Login__CharExitGame_ID => isClientToServer ? (INetMessage)new CharacterExitGameSMsg(data) : new CharacterExitGameCMsg(data),
                MessageOpcode.Evt_Login__CharacterSet_ID => new CharacterSetMsg(data),
                MessageOpcode.Evt_Login__CharGenVerification_ID => new CharGenVerificationMsg(data),
                MessageOpcode.Evt_Login__ChatServerData_ID => new GenericMsg {
                    payload = data.ReadBytes((int)(data.BaseStream.Length - data.BaseStream.Position)),
                },
                MessageOpcode.Evt_Login__ClientSceneRenderingComplete_ID => new ClientSceneRenderingCompleteMsg(),
                MessageOpcode.Evt_Login__MinCharSet_ID => new MinCharSetMsg(data),
                MessageOpcode.Evt_Login__PlayerDesc_ID => new PlayerDescMsg(data),
                MessageOpcode.Evt_Physics__CForceCreate_ID => new CForceCreateMsg(data),
                MessageOpcode.Evt_Physics__CLookAtDir_ID => new CLookAtDirMsg(data),
                MessageOpcode.Evt_Physics__CLookAt_ID => new CLookAtMsg(data),
                MessageOpcode.Evt_Physics__Contain_ID => new ContainMsg(data),
                MessageOpcode.Evt_Physics__CPosition_ID => new CPositionMsg(data),
                MessageOpcode.Evt_Physics__CreateObject_ID => new CreateObjectMsg(data),
                MessageOpcode.Evt_Physics__CreatePlayer_ID => new CreatePlayerMsg(data),
                MessageOpcode.Evt_Physics__DeParent_ID => new DeParentMsg(data),
                MessageOpcode.Evt_Physics__DestroyObject_ID => new DestroyObjectMsg(data),
                MessageOpcode.Evt_Physics__DoBehavior_ID => new DoBehaviorMsg(data),
                MessageOpcode.Evt_Physics__DoFX_ID => new DoFxMsg(data),
                MessageOpcode.Evt_Physics__DoFX_Private_ID => new DoFxPrivateMsg(data),
                MessageOpcode.Evt_Physics__DoMode_ID => new DoModeMsg(data),
                MessageOpcode.Evt_Physics__DoSlider_ID => new DoSliderMsg(data),
                MessageOpcode.Evt_Physics__DoStory_ID => new DoStoryMsg(data),
                MessageOpcode.Evt_Physics__LeaveWorld_ID => new LeaveWorldMsg(data),
                MessageOpcode.Evt_Physics__LookAtDir_ID => new LookAtDirMsg(data),
                MessageOpcode.Evt_Physics__LookAt_ID => new LookAtMsg(data),
                MessageOpcode.Evt_Physics__MoveTo_ID => new MoveToMsg(data),
                MessageOpcode.Evt_Physics__Parent_ID => new ParentMsg(data),
                MessageOpcode.Evt_Physics__PositionCell_ID => new PositionCellMsg(data),
                MessageOpcode.Evt_Physics__Position_ID => new PositionMsg(data),
                MessageOpcode.Evt_Physics__ReleaseBehavior_ID => new ReleaseBehaviorMsg(data),
                MessageOpcode.Evt_Physics__SetAccelerationScale_ID => new SetAccelerationScaleMsg(data),
                MessageOpcode.Evt_Physics__SetJumpScale_ID => new SetJumpScaleMsg(data),
                MessageOpcode.Evt_Physics__SetMode_ID => new SetModeMsg(data),
                MessageOpcode.Evt_Physics__SetVelocityScale_ID => new SetVelocityScaleMsg(data),
                MessageOpcode.Evt_Physics__StopBehavior_ID => new StopBehaviorMsg(data),
                MessageOpcode.Evt_Physics__StopFX_ID => new StopFxMsg(data),
                MessageOpcode.Evt_Physics__UpdateVisualDesc_ID => new UpdateVisualDescMsg(data),
                MessageOpcode.Evt_Qualities__UpdateBool_Private_ID => new QualUpdateBoolPrivateMsg(data),
                MessageOpcode.Evt_Qualities__UpdateBool_Visual_ID => new QualUpdateBoolVisualMsg(data),
                MessageOpcode.Evt_Qualities__UpdateDataID_Private_ID => new QualUpdateDataIdPrivateMsg(data),
                MessageOpcode.Evt_Qualities__UpdateDataID_Visual_ID => new QualUpdateDataIdVisualMsg(data),
                MessageOpcode.Evt_Qualities__UpdateFloat_Private_ID => new QualUpdateFloatPrivateMsg(data),
                MessageOpcode.Evt_Qualities__UpdateFloat_Visual_ID => new QualUpdateFloatVisualMsg(data),
                MessageOpcode.Evt_Qualities__UpdateInstanceID_Private_ID => new QualUpdateInstanceIdPrivateMsg(data),
                MessageOpcode.Evt_Qualities__UpdateInstanceID_Visual_ID => new QualUpdateInstanceIdVisualMsg(data),
                MessageOpcode.Evt_Qualities__UpdateInt_Private_ID => new QualUpdateIntPrivateMsg(data),
                MessageOpcode.Evt_Qualities__UpdateInt_Visual_ID => new QualUpdateIntVisualMsg(data),
                MessageOpcode.Evt_Qualities__UpdateLongInt_Private_ID => new QualUpdateLongIntPrivateMsg(data),
                MessageOpcode.Evt_Qualities__UpdateLongInt_Visual_ID => new QualUpdateLongIntVisualMsg(data),
                MessageOpcode.Evt_Qualities__UpdatePosition_Private_ID => new QualUpdatePositionPrivateMsg(data),
                MessageOpcode.Evt_Qualities__UpdatePosition_Visual_ID => new QualUpdatePositionVisualMsg(data),
                MessageOpcode.Evt_Qualities__UpdateStringInfo_Private_ID => new QualUpdateStringInfoPrivateMsg(data),
                MessageOpcode.Evt_Qualities__UpdateStringInfo_Visual_ID => new QualUpdateStringInfoVisualMsg(data),
                MessageOpcode.Evt_Qualities__UpdateTimestamp_Private_ID => new QualUpdateTimestampPrivateMsg(data),
                MessageOpcode.Evt_Qualities__UpdateTimestamp_Visual_ID => new QualUpdateTimestampVisualMsg(data),
                _ => throw new NotImplementedException($"Unhandled opcode: {opcode}."),
            };
        }
    }
}
