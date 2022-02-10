using System;

namespace AC2RE.Definitions;

public struct ReferenceEntry {

    // ReferenceEntry::ReferenceEntryField
    [Flags]
    public enum Field : uint {
        NONE = 0,
        RECURSE = 0x04000000, // recurse
        PROCESS = 0x08000000, // process
        ROOT = 0x10000000, // root
        SINGLETON = 0x20000000, // singleton
        LOADED = 0x40000000, // loaded
        FILLER = 0x80000000, // filler
    }

    // ReferenceEntry::ReferenceEntryField
    private static readonly uint OFFSET_MASK = 0x03FFFC00; // offset
    private static readonly uint REFERENCE_TYPE_MASK = 0x00000300; // type
    private static readonly uint GC_RUN_MASK = 0x000000FF; // gcRun

    // ReferenceEntry
    public uint blob; // m_blob

    public ReferenceEntry(uint blob) {
        this.blob = blob;
    }

    public ReferenceEntry(Field fields, ReferenceType referenceType) {
        blob = (uint)fields | (((uint)referenceType << 8) & REFERENCE_TYPE_MASK);
    }

    public bool isRecurse {
        get => getField(Field.RECURSE);
        set => setField(Field.RECURSE, value);
    }

    public bool isProcess {
        get => getField(Field.PROCESS);
        set => setField(Field.PROCESS, value);
    }

    public bool isRoot {
        get => getField(Field.ROOT);
        set => setField(Field.ROOT, value);
    }

    public bool isSingleton {
        get => getField(Field.SINGLETON);
        set => setField(Field.SINGLETON, value);
    }

    public bool isLoaded {
        get => getField(Field.LOADED);
        set => setField(Field.LOADED, value);
    }

    public bool isFiller {
        get => getField(Field.FILLER);
        set => setField(Field.FILLER, value);
    }

    public ushort offset {
        get => (ushort)((blob & OFFSET_MASK) >> 10);
        set => blob = (blob & ~OFFSET_MASK) | (((uint)value << 10) & OFFSET_MASK);
    }

    public ReferenceType referenceType {
        get => (ReferenceType)((blob & REFERENCE_TYPE_MASK) >> 8);
        set => blob = (blob & ~REFERENCE_TYPE_MASK) | (((uint)value << 8) & REFERENCE_TYPE_MASK);
    }

    public byte gcLow {
        get => (byte)(blob & GC_RUN_MASK);
        set => blob = (blob & ~GC_RUN_MASK) | (value & GC_RUN_MASK);
    }

    private bool getField(Field field) {
        return (blob & (uint)field) != 0;
    }

    private void setField(Field field, bool set) {
        if (set) {
            blob |= (uint)field;
        } else {
            blob &= ~(uint)field;
        }
    }
}
