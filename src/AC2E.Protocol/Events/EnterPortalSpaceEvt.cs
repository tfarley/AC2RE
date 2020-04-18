using AC2E.Def.Enums;
using AC2E.Interp;
using System.IO;

namespace AC2E.Protocol.Messages {

    public class EnterPortalSpaceEvt : INetEvent {

        public FunctionId funcId => (uint)EventFunctionId.EnterPortalSpace;

        public double delay;

        public void write(BinaryWriter data) {
            data.Write(12);
            data.Write((uint)PackTag.LONGFLOAT);
            data.Write(delay);
        }
    }
}
