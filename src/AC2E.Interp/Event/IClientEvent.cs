using System.IO;

namespace AC2E.Interp.Event {

    public interface IClientEvent {

        ClientEventFunctionId funcId { get; }

        void write(BinaryWriter data);
    }
}
