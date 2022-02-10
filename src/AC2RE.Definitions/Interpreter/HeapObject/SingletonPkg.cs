using System;

namespace AC2RE.Definitions;

public class SingletonPkg<T> : IHeapObject where T : class, IHeapObject {

    public DataId wstateDid;
    public T package;

    private SingletonPkg<U> to<U>() where U : class, IHeapObject {
        return new() {
            wstateDid = wstateDid,
            package = package as U,
        };
    }

    public SingletonPkg() {

    }

    public SingletonPkg(AC2Reader data) {
        wstateDid = data.ReadDataId();
    }

    public void write(AC2Writer data) {
        data.Write(wstateDid);
    }

    public static SingletonPkg<T> cast(IHeapObject package) {
        Type packageType = package.GetType();
        if (packageType.IsGenericType && packageType.GetGenericTypeDefinition() == typeof(SingletonPkg<>)) {
            return ((SingletonPkg<IHeapObject>)package).to<T>();
        } else {
            return new() {
                package = (T)package,
            };
        }
    }
}
