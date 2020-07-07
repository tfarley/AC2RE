namespace AC2E.Def {

    public struct PackageId {

        public ulong id;

        public PackageId(ulong id) {
            this.id = id;
        }

        public override string ToString() {
            return $"0x{id:X8}";
        }
    }
}
