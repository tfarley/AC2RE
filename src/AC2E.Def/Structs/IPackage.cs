using AC2E.Def.Enums;
using System.IO;

namespace AC2E.Def.Structs {

    public interface IPackage {

        uint id { get; }

        NativeType nativeType { get; }

        void write(BinaryWriter data);
    }
}
