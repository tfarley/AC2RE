using System.Collections.Generic;
using System.IO;

namespace AC2E.Interp {

    public class GMQuestInfoListPkg : IPackage {

        public NativeType nativeType => NativeType.GMQUESTINFOLIST;
        public PackageType packageType => PackageType.UNDEF;
        public InterpReferenceMeta referenceMeta => new InterpReferenceMeta(InterpReferenceMeta.Flag.LOADED | InterpReferenceMeta.Flag.RECURSE, ReferenceType.HEAPOBJECT);

        public uint id { get; set; }

        public List<GMQuestInfoPkg> contents;

        public void write(BinaryWriter data, List<IPackage> references) {
            data.Write(contents, v => v.write(data, references));
        }
    }
}
