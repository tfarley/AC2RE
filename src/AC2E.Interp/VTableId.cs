namespace AC2E.Interp {

    public struct VTableId {

        private static readonly uint PACKAGE_NUM_MASK = 0xFFFF0000;
        private static readonly uint FUNC_NUM_MASK = 0x0000FFFF;

        public uint id;

        public VTableId(uint id) {
            this.id = id;
        }

        public VTableId(ushort packageNum, ushort funcNum) {
            id = (((uint)packageNum << 16) & PACKAGE_NUM_MASK) | (funcNum & FUNC_NUM_MASK);
        }

        public static implicit operator uint(VTableId o) => o.id;
        public static implicit operator VTableId(uint o) => new VTableId(o);

        public ushort packageNum {
            get {
                return (ushort)((id & PACKAGE_NUM_MASK) >> 16);
            }
            set {
                id = (id & ~PACKAGE_NUM_MASK) | (((uint)value << 16) & PACKAGE_NUM_MASK);
            }
        }

        public ushort funcNum {
            get {
                return (ushort)(id & FUNC_NUM_MASK);
            }
            set {
                id = (id & ~FUNC_NUM_MASK) | (value & FUNC_NUM_MASK);
            }
        }
    }
}
