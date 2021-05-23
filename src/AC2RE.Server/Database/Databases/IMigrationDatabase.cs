using System;

namespace AC2RE.Server.Database {

    internal interface IMigrationDatabase : IDisposable {

        public bool hasRunMigration(string migrationName);
        public void setRunMigration(string migrationName);
    }
}
