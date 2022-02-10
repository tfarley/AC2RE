using System;
using System.Collections.Generic;

namespace AC2RE.Definitions;

public class ARHash : Dictionary<uint, IHeapObject>, IHeapObject {

    public NativeType nativeType => NativeType.ARHash;

    public Dictionary<K, V> to<K, V>() {
        return to<K, V>(v => (V)v);
    }

    public Dictionary<K, V> to<K, V>(Func<IHeapObject, V> valueConversion) {
        Dictionary<K, V> converted = new(Count);
        Converter<uint> keyConverter = Converters.getUInt(typeof(K));
        foreach ((var key, var value) in this) {
            converted[keyConverter.read<K>(key)] = valueConversion.Invoke(value);
        }
        return converted;
    }

    public static ARHash from<K, V>(Dictionary<K, V> source) where V : IHeapObject {
        return from(source, v => v);
    }

    public static ARHash from<K, V>(Dictionary<K, V> source, Func<V, IHeapObject> valueConversion) {
        if (source == null) {
            return null;
        }

        ARHash converted = new(source.Count);
        Converter<uint> keyConverter = Converters.getUInt(typeof(K));
        foreach ((var key, var value) in source) {
            converted[keyConverter.write(key)] = valueConversion.Invoke(value);
        }
        return converted;
    }

    private ARHash(int capacity) : base(capacity) {

    }

    public ARHash(AC2Reader data) {
        foreach ((var key, var value) in data.ReadDictionary(data.ReadUInt32, data.ReadReferenceId)) {
            data.heapObjectRegistry.addResolver(() => this[key] = data.heapObjectRegistry.get<IHeapObject>(value));
        }
    }

    public void write(AC2Writer data) {
        data.Write(this, data.Write, data.WriteHO);
    }
}
