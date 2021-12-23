using System;

namespace AC2RE.Server.Database;

internal interface IAccountDatabase : IDisposable {

    public Account? getAccountWithUserNameAndPassword(string userName, string password);

    public bool accountExistsWithUserName(string userName);

    public void upsertAccount(Account account);
}
