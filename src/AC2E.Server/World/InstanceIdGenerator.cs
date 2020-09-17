using AC2E.Def;

namespace AC2E.Server {

    internal class InstanceIdGenerator : IIdGenerator<InstanceId> {

        private ulong idCounter;

        public InstanceId next() {
            InstanceId id = new InstanceId(idCounter);
            idCounter++;
            return id;
        }
    }
}
