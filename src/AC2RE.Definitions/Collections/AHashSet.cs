﻿using System.Collections.Generic;

namespace AC2RE.Definitions;

public class AHashSet : HashSet<uint>, IHeapObject {

    public NativeType nativeType => NativeType.AHashSet;

    public HashSet<T> to<T>() {
        HashSet<T> converted = new(Count);
        Converter<uint> elementConverter = Converters.getUInt(typeof(T));
        foreach (var element in this) {
            converted.Add(elementConverter.read<T>(element));
        }
        return converted;
    }

    public static AHashSet from<T>(HashSet<T> source) {
        if (source == null) {
            return null;
        }

        AHashSet converted = new(source.Count);
        Converter<uint> elementConverter = Converters.getUInt(typeof(T));
        foreach (var element in source) {
            converted.Add(elementConverter.write(element));
        }
        return converted;
    }

    private AHashSet(int capacity) : base(capacity) {

    }

    public AHashSet(AC2Reader data) {
        data.ReadSet(this, data.ReadUInt32);
    }

    public void write(AC2Writer data) {
        data.Write(this, data.Write);
    }
}
