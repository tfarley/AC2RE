using System;

namespace AC2E.Def {

    public struct PackageTypeId : IEquatable<PackageTypeId> {

        public uint id;

        public PackageTypeId(uint id) {
            this.id = id;
        }

        public static bool operator ==(PackageTypeId lhs, PackageTypeId rhs) => lhs.id == rhs.id;
        public static bool operator !=(PackageTypeId lhs, PackageTypeId rhs) => lhs.id != rhs.id;
        public bool Equals(PackageTypeId other) => id == other.id;
        public override bool Equals(object obj) => obj is PackageTypeId castObj && id == castObj.id;
        public override int GetHashCode() => id.GetHashCode();

        public override string ToString() => $"0x{id:X8}";
    }
}
