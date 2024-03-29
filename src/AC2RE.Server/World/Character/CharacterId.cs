﻿using System;

namespace AC2RE.Server;

internal readonly struct CharacterId : IEquatable<CharacterId> {

    public readonly Guid id;

    public CharacterId(Guid id) {
        this.id = id;
    }

    public static bool operator ==(CharacterId lhs, CharacterId rhs) => lhs.id == rhs.id;
    public static bool operator !=(CharacterId lhs, CharacterId rhs) => lhs.id != rhs.id;
    public bool Equals(CharacterId other) => id == other.id;
    public override bool Equals(object? obj) => obj is CharacterId castObj && id == castObj.id;
    public override int GetHashCode() => id.GetHashCode();

    public override string ToString() => id.ToString();
}
