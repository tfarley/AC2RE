using AC2RE.Definitions;
using MySqlConnector;
using System;
using System.Text;

namespace AC2RE.Server.Database {

    internal abstract class BaseMySqlDatabase : IDisposable {

        protected abstract string? databaseName { get; }

        protected readonly MySqlConnection connection;

        public BaseMySqlDatabase() {
            connection = createConnection(databaseName);
        }

        public void Dispose() {
            connection.Dispose();
        }

        public static MySqlConnection createConnection(string? databaseName = null) {
            MySqlConnectionStringBuilder connectionStringBuilder = new();
            connectionStringBuilder.Server = "localHost";
            connectionStringBuilder.UserID = "root";
            connectionStringBuilder.AllowUserVariables = true;
            if (databaseName != null) {
                connectionStringBuilder.Database = databaseName;
            }

            MySqlConnection connection = new(connectionStringBuilder.ToString());
            connection.Open();

            return connection;
        }

        protected string escape(string value) {
            return MySqlHelper.EscapeString(value);
        }

        protected MySqlCommand upsertCommand(MySqlConnection connection, MySqlTransaction? transaction, string tableName, params MySqlParameter[] parameters) {
            StringBuilder statementStringBuilder = new StringBuilder("INSERT INTO ")
                .Append(tableName)
                .Append(" (");

            for (int i = 0; i < parameters.Length; i++) {
                MySqlParameter parameter = parameters[i];
                if (i != 0) {
                    statementStringBuilder.Append(", ");
                }
                statementStringBuilder.Append(parameter.ParameterName);
            }

            statementStringBuilder.Append(") VALUES(");

            for (int i = 0; i < parameters.Length; i++) {
                if (i != 0) {
                    statementStringBuilder.Append(", ");
                }
                statementStringBuilder.Append('?');
            }

            statementStringBuilder.Append(") as upsertVals ON DUPLICATE KEY UPDATE ");

            for (int i = 0; i < parameters.Length; i++) {
                MySqlParameter parameter = parameters[i];
                if (i != 0) {
                    statementStringBuilder.Append(", ");
                }
                statementStringBuilder.Append(parameter.ParameterName)
                    .Append("=upsertVals.")
                    .Append(parameter.ParameterName);
            }

            statementStringBuilder.Append(';');

            MySqlCommand cmd = new(statementStringBuilder.ToString(), connection, transaction);
            cmd.Parameters.AddRange(parameters);

            return cmd;
        }

        protected int delete(MySqlConnection connection, MySqlTransaction? transaction, string tableName, string whereClause) {
            if (transaction == null) {
                return runInTransaction(connection, transaction => deleteInternal(connection, transaction, tableName, whereClause));
            } else {
                return deleteInternal(connection, transaction, tableName, whereClause);
            }
        }

        private int deleteInternal(MySqlConnection connection, MySqlTransaction transaction, string tableName, string whereClause) {
            using (MySqlCommand cmd = new($"INSERT INTO {tableName}_del SELECT * FROM {tableName} WHERE {whereClause}", connection, transaction)) {
                return cmd.ExecuteNonQuery();
            }
        }

        protected T runInTransaction<T>(MySqlConnection connection, Func<MySqlTransaction, T> action) {
            using (MySqlTransaction transaction = connection.BeginTransaction()) {
                try {
                    T result = action.Invoke(transaction);
                    transaction.Commit();
                    return result;
                } catch (Exception) {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        protected Position mapPosition(MySqlDataReader reader) {
            return new() {
                cell = new(new(reader.GetUInt16("landblockId")), new(reader.GetUInt16("cellId"))),
                frame = new(new(reader.GetFloat("posX"), reader.GetFloat("posY"), reader.GetFloat("posZ")), new(reader.GetFloat("rotX"), reader.GetFloat("rotY"), reader.GetFloat("rotZ"), reader.GetFloat("rotW"))),
            };
        }
    }
}
