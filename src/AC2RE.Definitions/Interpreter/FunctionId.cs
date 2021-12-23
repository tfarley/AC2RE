namespace AC2RE.Definitions;

public struct FunctionId {

    private static readonly uint ABS_FLAG_MASK = 0x80000000;
    private static readonly uint PACKAGE_NUM_MASK = 0x7FFF0000;
    private static readonly uint FUNC_NUM_MASK = 0x0000FFFF;

    public uint id;

    public FunctionId(uint id) {
        this.id = id;
    }

    public FunctionId(bool isAbs, ushort packageNum, ushort funcNum) {
        id = (isAbs ? ABS_FLAG_MASK : 0) | (((uint)packageNum << 16) & PACKAGE_NUM_MASK) | (funcNum & FUNC_NUM_MASK);
    }

    public bool isAbs {
        get => (id & ABS_FLAG_MASK) != 0;
        set {
            if (value) {
                id |= ABS_FLAG_MASK;
            } else {
                id &= ~ABS_FLAG_MASK;
            }
        }
    }

    public ushort packageNum {
        get => (ushort)((id & PACKAGE_NUM_MASK) >> 16);
        set => id = (id & ~PACKAGE_NUM_MASK) | (((uint)value << 16) & PACKAGE_NUM_MASK);
    }

    public ushort funcNum {
        get => (ushort)(id & FUNC_NUM_MASK);
        set => id = (id & ~FUNC_NUM_MASK) | (value & FUNC_NUM_MASK);
    }

    public uint funcAddr {
        get => (id & ~ABS_FLAG_MASK);
        set => id = (id & ABS_FLAG_MASK) | (value & ~ABS_FLAG_MASK);
    }
}
