namespace AC2E.Def.Structs {

    public struct InstanceId {

        public ulong id;

        public InstanceId(ulong id) {
            this.id = id;
        }

        public override string ToString() {
            return $"0x{id:X16}";
        }
    }
}
