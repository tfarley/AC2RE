using AC2RE.Definitions;

namespace AC2RE.Server {

    internal class Player {

        public readonly ClientId clientId;
        public readonly Account account;
        public InstanceId characterId;

        public Player(ClientId clientId, Account account) {
            this.clientId = clientId;
            this.account = account;
        }
    }
}
