using AC2RE.Definitions;
using System.Collections.Generic;

namespace AC2RE.Server;

internal class Character {

    public readonly CharacterId id;
    public bool deleted;
    public uint sequence;
    public AccountId accountId;
    public InstanceId objectId;
    public ShortcutInfo?[] shortcuts;

    public Character(CharacterId id, uint sequence, AccountId accountId, InstanceId objectId, ShortcutInfo?[] shortcuts) {
        this.id = id;
        this.sequence = sequence;
        this.accountId = accountId;
        this.objectId = objectId;
        this.shortcuts = shortcuts;
    }
}
