using System;

namespace AC2E.Interp {

    public struct InterpReferenceMeta {

        [Flags]
        public enum Flag : uint {
            RECURSE = 0x04000000,
            PROCESS = 0x08000000,
            ROOT = 0x10000000,
            SINGLETON = 0x20000000,
            LOADED = 0x40000000,
            UNK1 = 0x80000000,
        }

        private static readonly uint GC_HIGH_MASK = 0x03FFFC00;
        private static readonly uint REFERENCE_TYPE_MASK = 0x00000300;
        private static readonly uint GC_LOW_MASK = 0x000000FF;

        public uint id;

        public InterpReferenceMeta(uint id) {
            this.id = id;
        }

        public InterpReferenceMeta(Flag flags, ReferenceType referenceType) {
            id = (uint)flags | (((uint)referenceType << 8) & REFERENCE_TYPE_MASK);
        }

        public static implicit operator uint(InterpReferenceMeta o) => o.id;
        public static implicit operator InterpReferenceMeta(uint o) => new InterpReferenceMeta(o);

        public bool isRecurse {
            get {
                return getFlag(Flag.RECURSE);
            }
            set {
                setFlag(Flag.RECURSE, value);
            }
        }

        public bool isProcess {
            get {
                return getFlag(Flag.PROCESS);
            }
            set {
                setFlag(Flag.PROCESS, value);
            }
        }

        public bool isRoot {
            get {
                return getFlag(Flag.ROOT);
            }
            set {
                setFlag(Flag.ROOT, value);
            }
        }

        public bool isSingleton {
            get {
                return getFlag(Flag.SINGLETON);
            }
            set {
                setFlag(Flag.SINGLETON, value);
            }
        }

        public bool isLoaded {
            get {
                return getFlag(Flag.LOADED);
            }
            set {
                setFlag(Flag.LOADED, value);
            }
        }

        public bool isUnk1 {
            get {
                return getFlag(Flag.UNK1);
            }
            set {
                setFlag(Flag.UNK1, value);
            }
        }

        public ushort gcHigh {
            get {
                return (ushort)((id & GC_HIGH_MASK) >> 10);
            }
            set {
                id = (id & ~GC_HIGH_MASK) | (((uint)value << 10) & GC_HIGH_MASK);
            }
        }

        public ReferenceType referenceType {
            get {
                return (ReferenceType)((id & REFERENCE_TYPE_MASK) >> 8);
            }
            set {
                id = (id & ~GC_HIGH_MASK) | (((uint)value << 8) & REFERENCE_TYPE_MASK);
            }
        }

        public byte gcLow {
            get {
                return (byte)(id & GC_LOW_MASK);
            }
            set {
                id = (id & ~GC_LOW_MASK) | (value & GC_LOW_MASK);
            }
        }

        private bool getFlag(Flag flag) {
            return (id & (uint)flag) != 0;
        }

        private void setFlag(Flag flag, bool set) {
            if (set) {
                id |= (uint)flag;
            } else {
                id &= ~(uint)flag;
            }
        }
    }
}
