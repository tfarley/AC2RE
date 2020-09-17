namespace AC2E.Server {

    internal struct ClientId {

        public ushort id;

        public ClientId(ushort id) {
            this.id = id;
        }

        public override string ToString() {
            return id.ToString();
        }
    }
}
