using AC2RE.Definitions;
using AC2RE.Server.Database;

namespace AC2RE.Server {

    internal class Character {

        [DbId]
        public readonly CharacterId id;

        [DbPersist]
        public bool deleted;

        [DbPersist]
        public uint order;

        [DbPersist]
        public AccountId accountId;

        [DbPersist]
        public InstanceId objectId;

        [DbConstructor]
        private Character(CharacterId id) {
            this.id = id;
        }

        public Character(CharacterId id, uint order, AccountId accountId, InstanceId objectId) : this(id) {
            this.id = id;
            this.order = order;
            this.accountId = accountId;
            this.objectId = objectId;
        }
    }
}
