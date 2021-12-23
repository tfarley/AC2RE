﻿using System.Threading;

namespace AC2RE.Server;

internal class ClientIdGenerator : IIdGenerator<ClientId> {

    private int idCounter = 0;

    public ClientId next() {
        return new((ushort)Interlocked.Increment(ref idCounter));
    }
}
