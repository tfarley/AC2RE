namespace AC2E.Def.Structs {

    public struct InstanceId {

        public ulong id;

        public InstanceId(ulong id) {
            this.id = id;
        }

        public static implicit operator ulong(InstanceId o) => o.id;
        public static implicit operator InstanceId(ulong o) => new InstanceId(o);
    }
}
