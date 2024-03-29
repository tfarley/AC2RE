﻿using System;

namespace AC2RE.Server;

internal readonly struct ClientId : IEquatable<ClientId> {

    public static readonly ClientId NULL = new(0);

    public readonly ushort id;

    public ClientId(ushort id) {
        this.id = id;
    }

    public static bool operator ==(ClientId lhs, ClientId rhs) => lhs.id == rhs.id;
    public static bool operator !=(ClientId lhs, ClientId rhs) => lhs.id != rhs.id;
    public bool Equals(ClientId other) => id == other.id;
    public override bool Equals(object? obj) => obj is ClientId castObj && id == castObj.id;
    public override int GetHashCode() => id.GetHashCode();

    public override string ToString() => id.ToString();
}
