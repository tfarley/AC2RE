using System;
using System.Collections.Generic;

namespace AC2RE.Definitions;

public class RList : List<IHeapObject>, IHeapObject {

    public NativeType nativeType => NativeType.RList;

    public List<T> to<T>() {
        return to(v => (T)v);
    }

    public List<T> to<T>(Func<IHeapObject, T> elementConversion) {
        List<T> converted = new(Count);
        foreach (var element in this) {
            converted.Add(elementConversion.Invoke(element));
        }
        return converted;
    }

    public static RList from<T>(List<T> source) where T : IHeapObject {
        return from(source, v => v);
    }

    public static RList from<T>(List<T> source, Func<T, IHeapObject> elementConversion) {
        if (source == null) {
            return null;
        }

        RList converted = new(source.Count);
        foreach (var element in source) {
            converted.Add(elementConversion.Invoke(element));
        }
        return converted;
    }

    private RList(int capacity) : base(capacity) {

    }

    public RList(AC2Reader data) {
        foreach (var element in data.ReadList(data.ReadReferenceId)) {
            data.heapObjectRegistry.addResolver(() => this.Add(data.heapObjectRegistry.get<IHeapObject>(element)));
        }
    }

    public void write(AC2Writer data) {
        data.Write(this, data.WriteHO);
    }
}
