using AC2E.Def;

namespace AC2E.Server {

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
