using AC2E.Dat;
using System.IO;

namespace AC2E {

    public static class BinaryWriterExtensions {

        public static void Write(this BinaryWriter writer, DataId value) {
            writer.Write(value.id);
        }

        public static void Write(this BinaryWriter writer, QualifiedDataId value) {
            writer.Write((uint)value.dbType);
            writer.Write(value.did);
        }
    }
}
