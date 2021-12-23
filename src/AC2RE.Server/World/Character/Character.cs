using AC2RE.Definitions;

namespace AC2RE.Server;

internal class Character {

    public readonly CharacterId id;
    public bool deleted;
    public uint sequence;
    public AccountId accountId;
    public InstanceId objectId;

    public Character(CharacterId id, uint sequence, AccountId accountId, InstanceId objectId) {
        this.id = id;
        this.sequence = sequence;
        this.accountId = accountId;
        this.objectId = objectId;
    }
}
