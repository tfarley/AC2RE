namespace AC2E.Def {

    public struct CellId {

        public uint id;

        public uint x => ((id >> 24) & 0xFF);
        public uint y => ((id >> 16) & 0xFF);
        public uint indoorCellId => ((id >> 8) & 0xFF);
        public uint outdoorCellId => (id & 0xFF);

        public CellId(uint id) {
            this.id = id;
        }

        public CellId(uint x, uint y, uint indoorCellId, uint outdoorCellId) {
            id = ((x & 0xFF) << 24) | ((y & 0xFF) << 16) | ((indoorCellId & 0xFF) << 8) | (outdoorCellId & 0xFF);
        }

        public override string ToString() {
            return $"<{x:X2}, {y:X2}> [{indoorCellId:X2} {outdoorCellId:X2}]";
        }
    }
}
