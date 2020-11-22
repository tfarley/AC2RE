using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace AC2RE.Server.Database {

    internal class AccountDatabase : MongoDatabase {

        protected override string databaseName => "account";

        private static bool inited;
        private readonly IMongoCollection<Account> accounts;

        public AccountDatabase(string endpoint) : base(endpoint) {
            accounts = setupAccounts();
            inited = true;
        }

        private IMongoCollection<Account> setupAccounts() {
            if (!inited) {
                BsonClassMap.RegisterClassMap<Account>(c => {
                    c.AutoMap();
                    c.MapCreator(r => new(r.id, r.userName, r.password));
                });
            }

            IMongoCollection<Account> accounts = database.GetCollection<Account>("account");

            if (!inited) {
                accounts.Indexes.CreateOne(new CreateIndexModel<Account>(
                    Builders<Account>.IndexKeys.Ascending(r => r.userName),
                    new CreateIndexOptions<Account>() { Unique = true }));

                accounts.Indexes.CreateOne(new CreateIndexModel<Account>(
                    Builders<Account>.IndexKeys
                        .Ascending(r => r.userName)
                        .Ascending(r => r.password)));
            }

            return accounts;
        }

        public Account? getAccountWithUserNameAndPassword(string userName, string password) {
            return accounts.Find(r => !r.deleted && r.userName == userName && r.password == password).FirstOrDefault();
        }

        public bool accountExistsWithUserName(string userName) {
            return accounts.Find(r => !r.deleted && r.userName == userName).Any();
        }

        public void upsertAccount(Account account) {
            accounts.ReplaceOne(r => r.id == account.id, account, new ReplaceOptions() { IsUpsert = true });
        }
    }
}
