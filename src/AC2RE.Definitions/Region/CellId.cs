using System;

namespace AC2RE.Definitions {

    public struct CellId : IEquatable<CellId> {

        public uint id;

        public byte landblockX => (byte)((id >> 24) & 0xFF);
        public byte landblockY => (byte)((id >> 16) & 0xFF);
        public LandblockId landblockId => new LandblockId((ushort)((id >> 16) & 0xFFFF));
        public byte indoorCellId => (byte)((id >> 8) & 0xFF);
        public byte outdoorCellId => (byte)(id & 0xFF);
        public LocalCellId localCellId => new LocalCellId((ushort)(id & 0xFFFF));

        public CellId(uint id) {
            this.id = id;
        }

        public CellId(byte landblockX, byte landblockY, byte indoorCellId, byte outdoorCellId) {
            id = ((uint)landblockX << 24) | ((uint)landblockY << 16) | ((uint)indoorCellId << 8) | outdoorCellId;
        }

        public CellId(LandblockId landblockId, byte indoorCellId, byte outdoorCellId) {
            id = ((uint)landblockId.id << 16) | ((uint)indoorCellId << 8) | outdoorCellId;
        }

        public CellId(byte landblockX, byte landblockY, LocalCellId localCellId) {
            id = ((uint)landblockX << 24) | ((uint)landblockY << 16) | localCellId.id;
        }

        public CellId(LandblockId landblockId, LocalCellId localCellId) {
            id = ((uint)landblockId.id << 16) | localCellId.id;
        }

        public static bool operator ==(CellId lhs, CellId rhs) => lhs.id == rhs.id;
        public static bool operator !=(CellId lhs, CellId rhs) => lhs.id != rhs.id;
        public bool Equals(CellId other) => id == other.id;
        public override bool Equals(object obj) => obj is CellId castObj && id == castObj.id;
        public override int GetHashCode() => id.GetHashCode();

        public override string ToString() {
            return $"<{landblockX:X2}, {landblockY:X2}> [{indoorCellId:X2} {outdoorCellId:X2}]";
        }
    }
}
