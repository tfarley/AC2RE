using AC2E.Dat;
using System.IO;

namespace AC2E {

    public static class BinaryReaderExtensions {

        public static DataId ReadDataId(this BinaryReader reader) {
            return new DataId(reader.ReadUInt32());
        }

        public static QualifiedDataId ReadQualifiedDataId(this BinaryReader reader) {
            return new QualifiedDataId((DbType)reader.ReadUInt32(), reader.ReadDataId());
        }
    }
}
