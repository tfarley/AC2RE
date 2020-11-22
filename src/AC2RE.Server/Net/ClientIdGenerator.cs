namespace AC2RE.Server {

    internal class ClientIdGenerator : IIdGenerator<ClientId> {

        private ushort idCounter = 1;

        public ClientId next() {
            ClientId id = new(idCounter);
            idCounter++;
            return id;
        }
    }
}
