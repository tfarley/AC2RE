using AC2E.Dat;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Interp {

    public class SingletonPkg : IPackage {

        public DataId did;

        public SingletonPkg() {

        }

        public SingletonPkg(BinaryReader data) {
            did = data.ReadDataId();
        }

        public void write(BinaryWriter data, List<PkgRef<IPackage>> references) {
            data.Write(did);
        }
    }
}
