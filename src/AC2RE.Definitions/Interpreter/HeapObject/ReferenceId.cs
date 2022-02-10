using System;

namespace AC2RE.Definitions;

public readonly struct ReferenceId : IEquatable<ReferenceId> {

    public static readonly ReferenceId NULL = new(0xFFFFFFFF);

    public readonly uint id;

    public ReferenceId(uint id) {
        this.id = id;
    }

    public static bool operator ==(ReferenceId lhs, ReferenceId rhs) => lhs.id == rhs.id;
    public static bool operator !=(ReferenceId lhs, ReferenceId rhs) => lhs.id != rhs.id;
    public bool Equals(ReferenceId other) => id == other.id;
    public override bool Equals(object obj) => obj is ReferenceId castObj && id == castObj.id;
    public override int GetHashCode() => id.GetHashCode();

    public override string ToString() => $"0x{id:X8}";
}
