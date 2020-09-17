namespace AC2E.Server {

    internal class ClientIdGenerator : IIdGenerator<ClientId> {

        private ushort idCounter = 1;

        public ClientId next() {
            ClientId id = new ClientId(idCounter);
            idCounter++;
            return id;
        }
    }
}
