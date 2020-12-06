using AC2RE.Definitions;
using AC2RE.Server.Database;

namespace AC2RE.Server {

    internal class InstanceIdGenerator : IIdGenerator<InstanceId> {

        [DbId]
        public readonly string type;

        [DbPersist]
        public ulong idCounter { get; private set; }

        [DbConstructor]
        public InstanceIdGenerator(string type, ulong idCounter = 1) {
            this.type = type;
            this.idCounter = idCounter;
        }

        public InstanceId next() {
            InstanceId id = new(idCounter);
            idCounter++;
            return id;
        }
    }
}
