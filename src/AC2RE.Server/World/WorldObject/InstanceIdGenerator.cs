using AC2RE.Definitions;

namespace AC2RE.Server;

internal class InstanceIdGenerator {

    public readonly string type;
    public ulong idCounter { get; private set; }

    public InstanceIdGenerator(string type, ulong idCounter = 1) {
        this.type = type;
        this.idCounter = idCounter;
    }

    public InstanceId next(InstanceId.IdType type) {
        InstanceId id = new(type, idCounter);
        idCounter++;
        return id;
    }
}
