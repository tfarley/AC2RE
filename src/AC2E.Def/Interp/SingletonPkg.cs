using System.IO;

namespace AC2E.Def {

    public class SingletonPkg : IPackage {

        public DataId did;

        public SingletonPkg() {

        }

        public SingletonPkg(BinaryReader data) {
            did = data.ReadDataId();
        }

        public void write(BinaryWriter data, PackageRegistry registry) {
            data.Write(did);
        }
    }
}
