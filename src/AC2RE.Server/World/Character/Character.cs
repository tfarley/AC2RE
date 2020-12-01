using AC2RE.Definitions;

namespace AC2RE.Server {

    internal class Character {

        public readonly CharacterId id;
        public bool deleted;

        public uint order;
        public AccountId ownerAccountId;
        public InstanceId worldObjectId;

        // For deserialization
        private Character(CharacterId id) {
            this.id = id;
        }

        public Character(CharacterId id, uint order, AccountId ownerAccountId, InstanceId worldObjectId) : this(id) {
            this.id = id;
            this.order = order;
            this.ownerAccountId = ownerAccountId;
            this.worldObjectId = worldObjectId;
        }
    }
}
