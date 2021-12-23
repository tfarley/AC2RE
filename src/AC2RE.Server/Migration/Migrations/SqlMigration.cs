using AC2RE.Server.Database;
using MySqlConnector;

namespace AC2RE.Server.Migration;

internal abstract class SqlMigration : IMigration {

    protected abstract string statement { get; }

    public void execute() {
        using (MySqlConnection connection = BaseMySqlDatabase.createConnection()) {
            using (MySqlCommand cmd = new(statement, connection)) {
                int numRowsAffected = cmd.ExecuteNonQuery();
                Logs.STATUS.info("Completed SQL migration", "numRowsAffected", numRowsAffected);
            }
        }
    }
}
