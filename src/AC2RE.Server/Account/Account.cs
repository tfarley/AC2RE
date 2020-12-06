using AC2RE.Server.Database;

namespace AC2RE.Server {

    internal class Account {

        [DbId]
        public readonly AccountId id;

        [DbPersist]
        public bool deleted;

        [DbPersist]
        public string userName;

        [DbPersist]
        public string password;

        [DbConstructor]
        private Account(AccountId id) {
            this.id = id;
        }

        public Account(AccountId id, string userName, string password) : this(id) {
            this.userName = userName;
            this.password = password;
        }
    }
}
