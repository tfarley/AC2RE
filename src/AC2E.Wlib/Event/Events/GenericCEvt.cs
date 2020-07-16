using System.IO;

namespace AC2E.WLib {

    public class GenericCEvt : IClientEvent {

        public ClientEventFunctionId funcId { get; set; }

        public byte[] payload;

        public void write(BinaryWriter data) {
            data.Write(payload);
        }
    }
}
