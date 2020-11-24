using System;

namespace AC2RE.Definitions {

    public struct CellId : IEquatable<CellId> {

        public uint id;

        public byte x => (byte)((id >> 24) & 0xFF);
        public byte y => (byte)((id >> 16) & 0xFF);
        public byte indoorCellId => (byte)((id >> 8) & 0xFF);
        public byte outdoorCellId => (byte)(id & 0xFF);
        public LocalCellId localCellId => new LocalCellId((ushort)(id & 0xFFFF));

        public CellId(uint id) {
            this.id = id;
        }

        public CellId(byte x, byte y, byte indoorCellId, byte outdoorCellId) {
            id = ((uint)x << 24) | ((uint)y << 16) | ((uint)indoorCellId << 8) | outdoorCellId;
        }

        public CellId(byte x, byte y, LocalCellId localCellId) {
            id = ((uint)x << 24) | ((uint)y << 16) | localCellId.id;
        }

        public static bool operator ==(CellId lhs, CellId rhs) => lhs.id == rhs.id;
        public static bool operator !=(CellId lhs, CellId rhs) => lhs.id != rhs.id;
        public bool Equals(CellId other) => id == other.id;
        public override bool Equals(object obj) => obj is CellId castObj && id == castObj.id;
        public override int GetHashCode() => id.GetHashCode();

        public override string ToString() {
            return $"<{x:X2}, {y:X2}> [{indoorCellId:X2} {outdoorCellId:X2}]";
        }
    }
}
