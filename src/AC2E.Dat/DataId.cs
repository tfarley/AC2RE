namespace AC2E.Dat {

    public struct DataId {

        public uint id;

        public DataId(uint id) {
            this.id = id;
        }

        public override string ToString() {
            return $"0x{id:X8}";
        }
    }
}
