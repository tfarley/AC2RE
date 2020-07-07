using System;
using System.IO;

namespace AC2E.WLib {

    public interface IServerEvent {

        ServerEventFunctionId funcId { get; } // _fid

        public static IServerEvent read(ServerEventFunctionId funcId, BinaryReader data) {
            switch (funcId) {
                case ServerEventFunctionId.Combat__DoAttack:
                    return new DoAttackSEvt(data);
                case ServerEventFunctionId.Allegiance__QueryAllegianceProfile:
                    return new QueryAllegianceProfileSEvt(data);
                case ServerEventFunctionId.Allegiance__QueryAllegiance:
                    return new QueryAllegianceSEvt(data);
                case ServerEventFunctionId.Combat__SetNextTarget:
                    return new SetNextTargetSEvt(data);
                case ServerEventFunctionId.Player__SetSelection:
                    return new SetSelectionSEvt(data);
                case ServerEventFunctionId.Combat__StartAttack:
                    return new StartAttackSEvt(data);
                case ServerEventFunctionId.Combat__StopAttacking:
                    return new StopAttackingSEvt(data);
                case ServerEventFunctionId.Combat__StopAttack:
                    return new StopAttackSEvt(data);
                default:
                    throw new NotImplementedException($"Unhandled server event: {funcId}.");
            }
        }
    }
}
