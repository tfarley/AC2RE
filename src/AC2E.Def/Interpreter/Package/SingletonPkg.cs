namespace AC2E.Def {

    public class SingletonPkg<T> : IPackage where T : IPackage {

        public DataId did;
        public T package;

        public SingletonPkg<U> to<U>() where U : class, IPackage {
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
    }
}
