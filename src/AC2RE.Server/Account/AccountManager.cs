using AC2RE.Server.Database;
using System;

namespace AC2RE.Server;

internal class AccountManager {

    private readonly IAccountDatabase accountDb;

    public AccountManager(IAccountDatabase accountDb) {
        this.accountDb = accountDb;
    }

    public Account? authenticate(string userName, string password) {
        return accountDb.getAccountWithUserNameAndPassword(userName, password);
    }

    public Account create(string userName, string password) {
        Account account = new(new(Guid.NewGuid()), false, userName, password, false);

        accountDb.upsertAccount(account);

        return account;
    }

    public bool accountExistsWithUserName(string userName) {
        return accountDb.accountExistsWithUserName(userName);
    }
}
