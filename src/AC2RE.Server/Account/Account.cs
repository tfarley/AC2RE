namespace AC2RE.Server;

internal class Account {

    public readonly AccountId id;
    public bool deleted;
    public string userName;
    public string password;
    public bool banned;

    public Account(AccountId id, bool deleted, string userName, string password, bool banned) {
        this.id = id;
        this.deleted = deleted;
        this.userName = userName;
        this.password = password;
        this.banned = banned;
    }
}
