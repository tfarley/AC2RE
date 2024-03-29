﻿namespace AC2RE.Definitions;

public struct QualifiedDataId {

    // QualifiedDataID
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
