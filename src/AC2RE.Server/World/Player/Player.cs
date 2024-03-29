﻿using AC2RE.Definitions;
using System.Collections.Generic;

namespace AC2RE.Server;

internal class Player {

    public readonly ClientId clientId;
    public readonly Account account;
    public readonly HashSet<InstanceId> visibleObjectIds = new();
    public Character dbCharacter;
    public InstanceId characterId;
    public uint attackNum;

    public Player(ClientId clientId, Account account) {
        this.clientId = clientId;
        this.account = account;
    }
}
