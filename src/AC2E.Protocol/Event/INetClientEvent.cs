using System.IO;

namespace AC2E.Protocol.Event {

    public interface INetClientEvent {

        ClientEventFunctionId funcId { get; }

        void write(BinaryWriter data);
    }
}
