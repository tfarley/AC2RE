namespace AC2E.Dat {

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
