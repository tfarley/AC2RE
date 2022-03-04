using AC2RE.Server.Database;
using MySqlConnector;
using System;
using System.Collections.Generic;

namespace AC2RE.Server.Migration;

internal class MigrationManager {

    private static readonly string BOOTSTRAP_STATEMENT = @"CREATE DATABASE IF NOT EXISTS ac2re_migration;
USE ac2re_migration;

CREATE TABLE IF NOT EXISTS migration (
    name VARCHAR(128) NOT NULL PRIMARY KEY,
    timestamp TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
);";

    private readonly IMigrationDatabase migrationDb;

    public MigrationManager(IMigrationDatabase migrationDb) {
        this.migrationDb = migrationDb;
    }

    public static void bootstrap() {
        using (MySqlConnection connection = BaseMySqlDatabase.createConnection()) {
            using (MySqlCommand cmd = new(BOOTSTRAP_STATEMENT, connection)) {
                cmd.ExecuteNonQuery();
            }
        }
    }

    public void runMigrations() {
        List<IMigration> migrations = new() {
            new CreateInitialSchemaMigration(),
            new LoadMapMigration(),
        };

        foreach (IMigration migration in migrations) {
            string migrationName = migration.GetType().Name;
            if (!migrationDb.hasRunMigration(migrationName)) {
                Logs.STATUS.info("Running migration", "name", migrationName);
                try {
                    migration.execute();
                } catch (Exception e) {
                    if (migration.optional) {
                        Logs.STATUS.warn(e, "Optional migration failed - skipping", "name", migrationName);
                    } else {
                        throw;
                    }
                }
                migrationDb.setRunMigration(migrationName);
            }
        }
    }
}
