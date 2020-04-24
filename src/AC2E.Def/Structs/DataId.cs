namespace AC2E.Def.Structs {

    public struct DataId {

        public uint id;

        public DataId(uint id) {
            this.id = id;
        }

        public static implicit operator uint(DataId o) => o.id;
        public static implicit operator DataId(uint o) => new DataId(o);
    }
}
