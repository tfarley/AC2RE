namespace AC2E.Server {

    internal class Account {

        public readonly AccountId id;
        public bool deleted;

        public string userName;
        public string password;

        public Account(AccountId id) {
            this.id = id;
        }
    }
}
