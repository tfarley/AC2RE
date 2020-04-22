using AC2E.Interp;
using System.IO;

namespace AC2E.Protocol.Messages {

    public class GenericEvt : INetEvent {

        public FunctionId funcId { get; set; }

        public byte[] payload;

        public void write(BinaryWriter data) {
            data.Write(payload);
        }
    }
}
