using System;

namespace AC2E.Def {

    public struct InstanceId : IEquatable<InstanceId> {

        public static readonly InstanceId NULL = new InstanceId(0);

        // Enum IIDUtils::IDType
        public enum IdType : uint {
            UNKNOWN,
            UI,
            CAMERA,
            TEMPORARY,
            PLAYER,
            STATIC,
            DYNAMIC,
        }

        public ulong id;

        public InstanceId(ulong id) {
            this.id = id;
        }

        public static bool operator ==(InstanceId lhs, InstanceId rhs) => lhs.id == rhs.id;
        public static bool operator !=(InstanceId lhs, InstanceId rhs) => lhs.id != rhs.id;
        public bool Equals(InstanceId other) => id == other.id;
        public override bool Equals(object obj) => obj is InstanceId castObj && id == castObj.id;
        public override int GetHashCode() => id.GetHashCode();

        public override string ToString() => $"0x{id:X16}";
    }
}
