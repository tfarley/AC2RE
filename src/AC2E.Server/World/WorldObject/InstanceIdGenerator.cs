using AC2E.Def;

namespace AC2E.Server {

    internal class InstanceIdGenerator : IIdGenerator<InstanceId> {

        public readonly string type;
        public ulong idCounter { get; private set; }

        public InstanceIdGenerator(string type, ulong initialId = 1) {
            this.type = type;
            idCounter = initialId;
        }

        public InstanceId next() {
            InstanceId id = new(idCounter);
            idCounter++;
            return id;
        }
    }
}
