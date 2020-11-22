using System;

namespace AC2RE.Server {

    internal struct ClientId : IEquatable<ClientId> {

        public ushort id;

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
}
