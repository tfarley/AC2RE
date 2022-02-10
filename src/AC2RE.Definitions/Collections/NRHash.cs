using System;
using System.Collections.Generic;

namespace AC2RE.Definitions;

public class NRHash : Dictionary<IHeapObject, IHeapObject>, IHeapObject {

    public NativeType nativeType => NativeType.NRHash;

    public Dictionary<K, V> to<K, V>() {
        return to(k => (K)k, v => (V)v);
    }

    public Dictionary<K, V> to<K, V>(Func<IHeapObject, K> keyConversion, Func<IHeapObject, V> valueConversion) {
        Dictionary<K, V> converted = new(Count);
        foreach ((var key, var value) in this) {
            converted[keyConversion.Invoke(key)] = valueConversion.Invoke(value);
        }
        return converted;
    }

    public static NRHash from<K, V>(Dictionary<K, V> source) where K : IHeapObject where V : IHeapObject {
        return from(source, k => k, v => v);
    }

    public static NRHash from<K, V>(Dictionary<K, V> source, Func<K, IHeapObject> keyConversion, Func<V, IHeapObject> valueConversion) {
        if (source == null) {
            return null;
        }

        NRHash converted = new(source.Count);
        foreach ((var key, var value) in source) {
            converted[keyConversion.Invoke(key)] = valueConversion.Invoke(value);
        }
        return converted;
    }

    private NRHash(int capacity) : base(capacity) {

    }

    public NRHash(AC2Reader data) {
        foreach ((var key, var value) in data.ReadDictionary(data.ReadHOFullRef, data.ReadReferenceId)) {
            data.heapObjectRegistry.addResolver(() => this[data.heapObjectRegistry.get<IHeapObject>(key)] = data.heapObjectRegistry.get<IHeapObject>(value));
        }
    }

    public void write(AC2Writer data) {
        data.Write(this, data.WriteHOFullRef, data.WriteHO);
    }
}
