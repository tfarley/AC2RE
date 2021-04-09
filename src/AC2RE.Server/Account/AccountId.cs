using System;

namespace AC2RE.Server {

    internal readonly struct AccountId : IEquatable<AccountId> {

        public readonly Guid id;

        public AccountId(Guid id) {
            this.id = id;
        }

        public static bool operator ==(AccountId lhs, AccountId rhs) => lhs.id == rhs.id;
        public static bool operator !=(AccountId lhs, AccountId rhs) => lhs.id != rhs.id;
        public bool Equals(AccountId other) => id == other.id;
        public override bool Equals(object? obj) => obj is AccountId castObj && id == castObj.id;
        public override int GetHashCode() => id.GetHashCode();

        public override string ToString() => id.ToString();
    }
}
