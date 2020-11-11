using AC2E.Server.Database;
using System;

namespace AC2E.Server {

    internal class AccountManager {

        private readonly AccountDatabase accountDb;

        public AccountManager(AccountDatabase accountDb) {
            this.accountDb = accountDb;
        }

        public Account? authenticate(string userName, string password) {
            return accountDb.getAccountWithUserNameAndPassword(userName, password);
        }

        public Account create(string userName, string password) {
            Account account = new(new(Guid.NewGuid()), userName, password);

            accountDb.upsertAccount(account);

            return account;
        }

        public bool accountExistsWithUserName(string userName) {
            return accountDb.accountExistsWithUserName(userName);
        }
    }
}
