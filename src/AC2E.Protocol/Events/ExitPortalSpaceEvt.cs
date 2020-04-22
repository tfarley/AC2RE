using AC2E.Def.Extensions;
using AC2E.Interp;
using System.IO;

namespace AC2E.Protocol.Messages {

    public class ExitPortalSpaceEvt : INetEvent {

        public FunctionId funcId => (uint)EventFunctionId.ExitPortalSpace;

        public double delay;

        public void write(BinaryWriter data) {
            data.Pack(delay);
        }
    }
}
