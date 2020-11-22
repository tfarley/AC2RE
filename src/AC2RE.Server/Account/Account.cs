namespace AC2RE.Server {

    internal class Account {

        public readonly AccountId id;
        public bool deleted;

        public string userName;
        public string password;

        public Account(AccountId id, string userName, string password) {
            this.id = id;
            this.userName = userName;
            this.password = password;
        }
    }
}
