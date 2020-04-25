using System.IO;

namespace AC2E.Protocol.Event.ClientEvents {

    public class GenericCEvt : INetClientEvent {

        public ClientEventFunctionId funcId { get; set; }

        public byte[] payload;

        public void write(BinaryWriter data) {
            data.Write(payload);
        }
    }
}
