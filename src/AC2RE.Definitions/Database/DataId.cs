﻿using System;

namespace AC2RE.Definitions;

public readonly struct DataId : IEquatable<DataId> {

    public static readonly DataId NULL = new(0);

    public readonly uint id;

    public DataId(uint id) {
        this.id = id;
    }

    public static bool operator ==(DataId lhs, DataId rhs) => lhs.id == rhs.id;
    public static bool operator !=(DataId lhs, DataId rhs) => lhs.id != rhs.id;
    public bool Equals(DataId other) => id == other.id;
    public override bool Equals(object obj) => obj is DataId castObj && id == castObj.id;
    public override int GetHashCode() => id.GetHashCode();

    public override string ToString() => $"0x{id:X8}";
}
