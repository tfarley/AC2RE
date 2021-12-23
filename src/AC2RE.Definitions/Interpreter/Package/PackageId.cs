using System;

namespace AC2RE.Definitions;

public readonly struct PackageId : IEquatable<PackageId> {

    public static readonly PackageId NULL = new(0xFFFFFFFF);

    public readonly uint id;

    public PackageId(uint id) {
        this.id = id;
    }

    public static bool operator ==(PackageId lhs, PackageId rhs) => lhs.id == rhs.id;
    public static bool operator !=(PackageId lhs, PackageId rhs) => lhs.id != rhs.id;
    public bool Equals(PackageId other) => id == other.id;
    public override bool Equals(object obj) => obj is PackageId castObj && id == castObj.id;
    public override int GetHashCode() => id.GetHashCode();

    public override string ToString() => $"0x{id:X8}";
}
