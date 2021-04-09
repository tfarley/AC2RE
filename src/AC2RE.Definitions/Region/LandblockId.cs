using System;

namespace AC2RE.Definitions {

    public readonly struct LandblockId : IEquatable<LandblockId> {

        public readonly ushort id;

        public byte x => (byte)((id >> 8) & 0xFF);
        public byte y => (byte)(id & 0xFF);

        public LandblockId(ushort id) {
            this.id = id;
        }

        public LandblockId(byte x, byte y) {
            id = (ushort)(((uint)x << 8) | y);
        }

        public static bool operator ==(LandblockId lhs, LandblockId rhs) => lhs.id == rhs.id;
        public static bool operator !=(LandblockId lhs, LandblockId rhs) => lhs.id != rhs.id;
        public bool Equals(LandblockId other) => id == other.id;
        public override bool Equals(object obj) => obj is LandblockId castObj && id == castObj.id;
        public override int GetHashCode() => id.GetHashCode();

        public override string ToString() {
            return $"<{x:X2} {y:X2}>";
        }
    }
}
