using System;

namespace AC2RE.Definitions {

    public struct LocalCellId : IEquatable<LocalCellId> {

        public ushort id;

        public byte indoorCellId => (byte)((id >> 8) & 0xFF);
        public byte outdoorCellId => (byte)(id & 0xFF);

        public LocalCellId(ushort id) {
            this.id = id;
        }

        public LocalCellId(byte indoorCellId, byte outdoorCellId) {
            id = (ushort)(((uint)indoorCellId << 8) | outdoorCellId);
        }

        public static bool operator ==(LocalCellId lhs, LocalCellId rhs) => lhs.id == rhs.id;
        public static bool operator !=(LocalCellId lhs, LocalCellId rhs) => lhs.id != rhs.id;
        public bool Equals(LocalCellId other) => id == other.id;
        public override bool Equals(object obj) => obj is LocalCellId castObj && id == castObj.id;
        public override int GetHashCode() => id.GetHashCode();

        public override string ToString() {
            return $"[{indoorCellId:X2} {outdoorCellId:X2}]";
        }
    }
}
