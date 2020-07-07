using System;
using System.IO;

namespace AC2E.WLib {

    public interface IClientEvent {

        ClientEventFunctionId funcId { get; } // _fid

        void write(BinaryWriter data);

        public static IClientEvent read(ClientEventFunctionId funcId, BinaryReader data) {
            switch (funcId) {
                case ClientEventFunctionId.Effect__ClientAddEffect:
                    return new ClientAddEffectCEvt(data);
                case ClientEventFunctionId.Effect__ClientRemoveEffect:
                    return new ClientRemoveEffectCEvt(data);
                case ClientEventFunctionId.Communication__CDoSay:
                    return new DoSayCEvt(data);
                case ClientEventFunctionId.Player__EnterPortalSpace:
                    return new EnterPortalSpaceCEvt(data);
                case ClientEventFunctionId.Player__ExitPortalSpace:
                    return new ExitPortalSpaceCEvt(data);
                case ClientEventFunctionId.Player__HandleCharacterSessionStart:
                    return new HandleCharacterSessionStartCEvt(data);
                case ClientEventFunctionId.Communication__CHearTell:
                    return new HearTellCEvt(data);
                case ClientEventFunctionId.Player__SetMovementFrozen:
                    return new SetMovementFrozenCEvt(data);
                case ClientEventFunctionId.Fellowship__UpdateFellowCellID:
                    return new UpdateFellowCellIdCEvt(data);
                case ClientEventFunctionId.Fellowship__UpdateFellowHealth:
                    return new UpdateFellowHealthCEvt(data);
                case ClientEventFunctionId.Fellowship__UpdateFellowLevel:
                    return new UpdateFellowLevelCEvt(data);
                case ClientEventFunctionId.Fellowship__UpdateFellowMaxVigor:
                    return new UpdateFellowMaxVigorCEvt(data);
                case ClientEventFunctionId.Fellowship__UpdateFellowMaxHealth:
                    return new UpdateFellowMaxHealthCEvt(data);
                case ClientEventFunctionId.Fellowship__UpdateFellowVigor:
                    return new UpdateFellowVigorCEvt(data);
                default:
                    throw new NotImplementedException($"Unhandled client event: {funcId}.");
            }
        }
    }
}
