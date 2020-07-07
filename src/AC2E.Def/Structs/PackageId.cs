namespace AC2E.Def {

    public struct PackageId {

        public static readonly PackageId NULL = new PackageId(0xFFFFFFFF);

        public uint id;

        public PackageId(uint id) {
            this.id = id;
        }

        public override string ToString() {
            return $"0x{id:X8}";
        }
    }
}
