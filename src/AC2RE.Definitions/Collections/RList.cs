using System;
using System.Collections.Generic;

namespace AC2RE.Definitions;

public class RList : List<IPackage>, IPackage {

    public NativeType nativeType => NativeType.RList;

    public List<T> to<T>() {
        return to(v => (T)v);
    }

    public List<T> to<T>(Func<IPackage, T> elementConversion) {
        List<T> converted = new(Count);
        foreach (var element in this) {
            converted.Add(elementConversion.Invoke(element));
        }
        return converted;
    }

    public static RList from<T>(List<T> source) where T : IPackage {
        return from(source, v => v);
    }

    public static RList from<T>(List<T> source, Func<T, IPackage> elementConversion) {
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
        foreach (var element in data.ReadList(data.ReadPackageId)) {
            data.packageRegistry.addResolver(() => this.Add(data.packageRegistry.get<IPackage>(element)));
        }
    }

    public void write(AC2Writer data) {
        data.Write(this, data.WritePkg);
    }
}
