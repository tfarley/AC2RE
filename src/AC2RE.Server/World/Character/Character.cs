using AC2RE.Definitions;

namespace AC2RE.Server {

    internal class Character {

        public readonly CharacterId id;
        public bool deleted;

        public uint order;
        public AccountId ownerAccountId;
        public InstanceId worldObjectId;

        public Character(CharacterId id) {
            this.id = id;
        }
    }
}
