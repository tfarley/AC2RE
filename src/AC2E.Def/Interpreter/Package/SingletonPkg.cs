using System;

namespace AC2E.Def {

    public class SingletonPkg<T> : IPackage where T : class, IPackage {

        public DataId did;
        public T package;

        private SingletonPkg<U> to<U>() where U : class, IPackage {
            return new SingletonPkg<U> {
                did = did,
                package = package as U,
            };
        }

        public SingletonPkg() {

        }

        public SingletonPkg(AC2Reader data) {
            did = data.ReadDataId();
        }

        public void write(AC2Writer data) {
            data.Write(did);
        }

        public static SingletonPkg<T> cast(IPackage package) {
            Type packageType = package.GetType();
            if (packageType.IsGenericType && packageType.GetGenericTypeDefinition() == typeof(SingletonPkg<>)) {
                return ((SingletonPkg<IPackage>)package).to<T>();
            } else {
                return new SingletonPkg<T> {
                    package = (T)package,
                };
            }
        }
    }
}
