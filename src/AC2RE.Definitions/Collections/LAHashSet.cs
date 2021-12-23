using System.Collections.Generic;

namespace AC2RE.Definitions;

public class LAHashSet : HashSet<ulong>, IPackage {

    public NativeType nativeType => NativeType.LAHashSet;

    public HashSet<T> to<T>() {
        HashSet<T> converted = new(Count);
        Converter<ulong> elementConverter = Converters.getULong(typeof(T));
        foreach (var element in this) {
            converted.Add(elementConverter.read<T>(element));
        }
        return converted;
    }

    public static LAHashSet from<T>(HashSet<T> source) {
        if (source == null) {
            return null;
        }

        LAHashSet converted = new(source.Count);
        Converter<ulong> elementConverter = Converters.getULong(typeof(T));
        foreach (var element in source) {
            converted.Add(elementConverter.write(element));
        }
        return converted;
    }

    private LAHashSet(int capacity) : base(capacity) {

    }

    public LAHashSet(AC2Reader data) {
        data.ReadSet(this, data.ReadUInt64);
    }

    public void write(AC2Writer data) {
        data.Write(this, data.Write);
    }
}
