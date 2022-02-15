﻿using System;

namespace AC2RE.Definitions;

public interface INetMessage : IWritable {

    public NetBlobId.Flag blobFlags { get; }
    public NetQueue queueId { get; }
    public MessageOpcode opcode { get; }
    public OrderingType orderingType => OrderingType.PRIVATE_ORDERED;

    public static INetMessage read(MessageOpcode opcode, AC2Reader data, bool isClientToServer) {
        return opcode switch {
            MessageOpcode.Admin__DisplayStringInfo => new DisplayStringInfoMsg(data),
            MessageOpcode.Admin__WorldName => new WorldNameMsg(data),
            MessageOpcode.CLIDAT_END_DDD_EVENT => new CliDatEndDDDMsg(data),
            MessageOpcode.CLIDAT_ERROR_EVENT => new CliDatErrorMsg(data),
            MessageOpcode.CLIDAT_INTERROGATION_EVENT => new CliDatInterrogationMsg(data),
            MessageOpcode.CLIDAT_INTERROGATION_RESPONSE_EVENT => new CliDatInterrogationResponseMsg(data),
            MessageOpcode.CLIDAT_REQUEST_DATA_EVENT => new CliDatRequestDataMsg(data),
            MessageOpcode.Interp__InterpCEvent_Cell => new InterpCEventCellMsg(data),
            MessageOpcode.Interp__InterpCEvent_Private => new InterpCEventPrivateMsg(data),
            MessageOpcode.Interp__InterpCEvent_Visual => new InterpCEventVisualMsg(data),
            MessageOpcode.Interp__InterpSEvent => new InterpSEventMsg(data),
            MessageOpcode.CHARACTER_CREATE_EVENT => new CharacterCreateMsg(data),
            MessageOpcode.Login__CharacterDeletion => isClientToServer ? new CharacterDeletionSMsg(data) : new CharacterDeletionCMsg(data),
            MessageOpcode.CHARACTER_ENTER_GAME_EVENT => new CharacterEnterGameMsg(data),
            MessageOpcode.Login__CharacterError => new CharacterErrorMsg(data),
            MessageOpcode.Login__CharExitGame => isClientToServer ? new CharacterExitGameSMsg(data) : new CharacterExitGameCMsg(data),
            MessageOpcode.Login__CharacterSet => new CharacterSetMsg(data),
            MessageOpcode.Login__CharGenVerification => new CharGenVerificationMsg(data),
            MessageOpcode.Login__ChatServerData => new GenericMsg {
                payload = data.ReadBytes((int)(data.BaseStream.Length - data.BaseStream.Position)),
            },
            MessageOpcode.Login__ClientSceneRenderingComplete => new ClientSceneRenderingCompleteMsg(),
            MessageOpcode.Login__MinCharSet => new MinCharSetMsg(data),
            MessageOpcode.Login__PlayerDesc => new PlayerDescMsg(data),
            MessageOpcode.Physics__CForceCreate => new CForceCreateMsg(data),
            MessageOpcode.Physics__CLookAtDir => new CLookAtDirMsg(data),
            MessageOpcode.Physics__CLookAt => new CLookAtMsg(data),
            MessageOpcode.Physics__Contain => new ContainMsg(data),
            MessageOpcode.Physics__CPosition => new CPositionMsg(data),
            MessageOpcode.Physics__CreateObject => new CreateObjectMsg(data),
            MessageOpcode.Physics__CreatePlayer => new CreatePlayerMsg(data),
            MessageOpcode.Physics__DeParent => new DeParentMsg(data),
            MessageOpcode.Physics__DestroyObject => new DestroyObjectMsg(data),
            MessageOpcode.Physics__DoBehavior => new DoBehaviorMsg(data),
            MessageOpcode.Physics__DoFX => new DoFxMsg(data),
            MessageOpcode.Physics__DoFX_Private => new DoFxPrivateMsg(data),
            MessageOpcode.Physics__DoMode => new DoModeMsg(data),
            MessageOpcode.Physics__DoSlider => new DoSliderMsg(data),
            MessageOpcode.Physics__DoStory => new DoStoryMsg(data),
            MessageOpcode.Physics__LeaveWorld => new LeaveWorldMsg(data),
            MessageOpcode.Physics__LookAtDir => new LookAtDirMsg(data),
            MessageOpcode.Physics__LookAt => new LookAtMsg(data),
            MessageOpcode.Physics__MoveTo => new MoveToMsg(data),
            MessageOpcode.Physics__Parent => new ParentMsg(data),
            MessageOpcode.Physics__PositionCell => new PositionCellMsg(data),
            MessageOpcode.Physics__Position => new PositionMsg(data),
            MessageOpcode.Physics__ReleaseBehavior => new ReleaseBehaviorMsg(data),
            MessageOpcode.Physics__SetAccelerationScale => new SetAccelerationScaleMsg(data),
            MessageOpcode.Physics__SetJumpScale => new SetJumpScaleMsg(data),
            MessageOpcode.Physics__SetMode => new SetModeMsg(data),
            MessageOpcode.Physics__SetVelocityScale => new SetVelocityScaleMsg(data),
            MessageOpcode.Physics__StopBehavior => new StopBehaviorMsg(data),
            MessageOpcode.Physics__StopFX => new StopFxMsg(data),
            MessageOpcode.Physics__UpdateVisualDesc => new UpdateVisualDescMsg(data),
            MessageOpcode.Qualities__UpdateBool_Private => new QualUpdateBoolPrivateMsg(data),
            MessageOpcode.Qualities__UpdateBool_Visual => new QualUpdateBoolVisualMsg(data),
            MessageOpcode.Qualities__UpdateDataID_Private => new QualUpdateDataIdPrivateMsg(data),
            MessageOpcode.Qualities__UpdateDataID_Visual => new QualUpdateDataIdVisualMsg(data),
            MessageOpcode.Qualities__UpdateFloat_Private => new QualUpdateFloatPrivateMsg(data),
            MessageOpcode.Qualities__UpdateFloat_Visual => new QualUpdateFloatVisualMsg(data),
            MessageOpcode.Qualities__UpdateInstanceID_Private => new QualUpdateInstanceIdPrivateMsg(data),
            MessageOpcode.Qualities__UpdateInstanceID_Visual => new QualUpdateInstanceIdVisualMsg(data),
            MessageOpcode.Qualities__UpdateInt_Private => new QualUpdateIntPrivateMsg(data),
            MessageOpcode.Qualities__UpdateInt_Visual => new QualUpdateIntVisualMsg(data),
            MessageOpcode.Qualities__UpdateLongInt_Private => new QualUpdateLongIntPrivateMsg(data),
            MessageOpcode.Qualities__UpdateLongInt_Visual => new QualUpdateLongIntVisualMsg(data),
            MessageOpcode.Qualities__UpdatePosition_Private => new QualUpdatePositionPrivateMsg(data),
            MessageOpcode.Qualities__UpdatePosition_Visual => new QualUpdatePositionVisualMsg(data),
            MessageOpcode.Qualities__UpdateStringInfo_Private => new QualUpdateStringInfoPrivateMsg(data),
            MessageOpcode.Qualities__UpdateStringInfo_Visual => new QualUpdateStringInfoVisualMsg(data),
            MessageOpcode.Qualities__UpdateTimestamp_Private => new QualUpdateTimestampPrivateMsg(data),
            MessageOpcode.Qualities__UpdateTimestamp_Visual => new QualUpdateTimestampVisualMsg(data),
            _ => throw new NotImplementedException($"Unhandled opcode: {opcode}."),
        };
    }
}
