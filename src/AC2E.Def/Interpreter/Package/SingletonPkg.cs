namespace AC2E.Def {

    public class SingletonPkg : IPackage {

        public DataId did;

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
