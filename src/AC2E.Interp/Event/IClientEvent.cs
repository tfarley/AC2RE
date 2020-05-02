using System.IO;

namespace AC2E.Interp.Event {

    public interface IClientEvent {

        ClientEventFunctionId funcId { get; } // _fid

        void write(BinaryWriter data);
    }
}
