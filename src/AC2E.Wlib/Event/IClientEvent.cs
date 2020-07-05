using System;
using System.IO;

namespace AC2E.WLib {

    public interface IClientEvent {

        ClientEventFunctionId funcId { get; } // _fid

        void write(BinaryWriter data);

        public static IClientEvent read(ClientEventFunctionId funcId, BinaryReader data) {
            switch (funcId) {
                case ClientEventFunctionId.Effect__ClientRemoveEffect:
                    return new ClientRemoveEffectCEvt(data);
                case ClientEventFunctionId.Player__EnterPortalSpace:
                    return new EnterPortalSpaceCEvt(data);
                case ClientEventFunctionId.Player__ExitPortalSpace:
                    return new ExitPortalSpaceCEvt(data);
                default:
                    throw new NotImplementedException($"Unhandled client event: {funcId}.");
            }
        }
    }
}
