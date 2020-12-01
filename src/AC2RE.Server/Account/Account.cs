namespace AC2RE.Server {

    internal class Account {

        public readonly AccountId id;
        public bool deleted;

        public string userName;
        public string password;

        // For deserialization
        private Account(AccountId id) {
            this.id = id;
        }

        public Account(AccountId id, string userName, string password) : this(id) {
            this.userName = userName;
            this.password = password;
        }
    }
}
