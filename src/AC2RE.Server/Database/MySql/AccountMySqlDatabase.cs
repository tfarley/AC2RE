using MySqlConnector;
using System;
using System.Data;

namespace AC2RE.Server.Database {

    internal class AccountMySqlDatabase : BaseMySqlDatabase, IAccountDatabase {

        protected override string? databaseName => "ac2re_account";

        public Account? getAccountWithUserNameAndPassword(string userName, string password) {
            using (MySqlCommand cmd = new($"SELECT id FROM account WHERE userName = '{userName}' AND password = '{password}';", connection)) {
                using (MySqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleResult)) {
                    if (reader.Read()) {
                        return new(new(reader.GetGuid("id")), userName, password);
                    }
                }
            }

            return null;
        }

        public bool accountExistsWithUserName(string userName) {
            using (MySqlCommand cmd = new($"SELECT 1 FROM account WHERE userName = '{userName}';", connection)) {
                using (MySqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleResult)) {
                    return reader.HasRows;
                }
            }
        }

        public void upsertAccount(Account account) {
            runInTransaction(connection, transaction => {
                using (MySqlCommand cmd = upsertCommand(connection, transaction, "account",
                    new("id", MySqlDbType.String),
                    new("userName", MySqlDbType.VarString),
                    new("password", MySqlDbType.VarString))) {
                    cmd.Parameters[0].Value = account.id.id;
                    cmd.Parameters[1].Value = escape(account.userName);
                    cmd.Parameters[2].Value = escape(account.password);
                    return cmd.ExecuteNonQuery();
                }
            });
        }
    }
}
