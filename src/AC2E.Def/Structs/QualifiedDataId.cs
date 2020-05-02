using AC2E.Def.Enums;

namespace AC2E.Def.Structs {

    public struct QualifiedDataId {

        public DbType dbType; // Type
        public DataId did; // ID

        public QualifiedDataId(DbType dbType, DataId did) {
            this.dbType = dbType;
            this.did = did;
        }

        public override string ToString() {
            return $"{dbType}.{did}";
        }
    }
}
