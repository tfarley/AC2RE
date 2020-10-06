using System;

namespace AC2E.Def {

    public struct EnumId : IEquatable<EnumId> {

        public uint id;

        public EnumId(uint id) {
            this.id = id;
        }

        public static bool operator ==(EnumId lhs, EnumId rhs) => lhs.id == rhs.id;
        public static bool operator !=(EnumId lhs, EnumId rhs) => lhs.id != rhs.id;
        public bool Equals(EnumId other) => id == other.id;
        public override bool Equals(object obj) => obj is EnumId castObj && id == castObj.id;
        public override int GetHashCode() => id.GetHashCode();

        public override string ToString() => $"0x{id:X8}";
    }
}
