using AC2E.Def;
using System;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Interp {

    public interface IPackage {

        NativeType nativeType { get; }
        PackageType packageType { get; }
        InterpReferenceMeta referenceMeta { get; }

        PackageId id { get; set; }

        // This allows packages to convert/cast packages from their plain type to their easier-to-work-with generic type in the registry, if applicable
        void resolveGenericRefs() {

        }

        void write(BinaryWriter data, List<PkgRef<IPackage>> references) {
            throw new NotImplementedException("IPackage implementor must override write().");
        }
    }
}
