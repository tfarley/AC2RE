using AC2E.Interp;
using System.IO;

namespace AC2E.Protocol {

    public interface INetEvent {

        FunctionId funcId { get; }

        void write(BinaryWriter data);
    }
}
