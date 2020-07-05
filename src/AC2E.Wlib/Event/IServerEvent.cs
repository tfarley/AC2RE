using System;
using System.IO;

namespace AC2E.WLib {

    public interface IServerEvent {

        ServerEventFunctionId funcId { get; } // _fid

        public static IServerEvent read(ServerEventFunctionId funcId, BinaryReader data) {
            switch (funcId) {
                case ServerEventFunctionId.Combat__StartAttack:
                    return new StartAttackSEvt(data);
                default:
                    throw new NotImplementedException($"Unhandled server event: {funcId}.");
            }
        }
    }
}
