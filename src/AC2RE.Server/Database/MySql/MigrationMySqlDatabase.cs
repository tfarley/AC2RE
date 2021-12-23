using MySqlConnector;
using System;
using System.Data;

namespace AC2RE.Server.Database;

internal class MigrationMySqlDatabase : BaseMySqlDatabase, IMigrationDatabase {

    protected override string? databaseName => "ac2re_migration";

    public bool hasRunMigration(string migrationName) {
        using (MySqlCommand cmd = new($"SELECT 1 FROM migration WHERE name = '{migrationName}';", connection)) {
            using (MySqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleResult)) {
                return reader.HasRows;
            }
        }
    }

    public void setRunMigration(string migrationName) {
        using (MySqlCommand cmd = new($"INSERT INTO migration (name) VALUES ('{migrationName}');", connection)) {
            if (cmd.ExecuteNonQuery() != 1) {
                throw new Exception();
            }
        }
    }
}
