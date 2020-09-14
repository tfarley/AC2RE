namespace AC2E.Def {

    public struct PackageTypeId {

        public uint id;

        public PackageTypeId(uint id) {
            this.id = id;
        }

        public override string ToString() {
            return $"0x{id:X8}";
        }
    }
}
