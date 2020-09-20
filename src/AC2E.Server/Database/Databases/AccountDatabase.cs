using AC2E.Def;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace AC2E.Server.Database {

    internal class AccountDatabase : MongoDatabase {

        protected override string databaseName => "account";

        private static bool inited;
        private readonly IMongoCollection<Account> accounts;
        private readonly IMongoCollection<Character> characters;

        public AccountDatabase(string endpoint) : base(endpoint) {
            accounts = setupAccounts();
            characters = setupCharacters();
            inited = true;
        }

        private IMongoCollection<Account> setupAccounts() {
            if (!inited) {
                BsonClassMap.RegisterClassMap<Account>(c => {
                    c.AutoMap();
                    c.MapIdProperty("id");
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

        private IMongoCollection<Character> setupCharacters() {
            if (!inited) {
                BsonClassMap.RegisterClassMap<Character>(c => {
                    c.AutoMap();
                    c.MapIdProperty("id");
                });
            }

            IMongoCollection<Character> characters = database.GetCollection<Character>("character");

            if (!inited) {
                characters.Indexes.CreateOne(new CreateIndexModel<Character>(
                Builders<Character>.IndexKeys.Ascending(r => r.worldObjectId),
                new CreateIndexOptions<Character>() { Unique = true }));
            }

            return characters;
        }

        public Account getAccountWithUserNameAndPassword(string userName, string password) {
            return accounts.Find(r => r.userName == userName && r.password == password).FirstOrDefault();
        }

        public bool accountExistsWithUserName(string userName) {
            return accounts.Find(r => r.userName == userName).Any();
        }

        public void insertAccount(Account account) {
            accounts.InsertOne(account);
        }

        public bool deleteAccount(Guid id) {
            return accounts.DeleteOne(r => r.id == id).DeletedCount == 1;
        }

        public List<Character> getCharactersWithAccount(Guid accountId) {
            return characters.Find(r => r.ownerAccountId == accountId).ToList();
        }

        public Character getCharacterWithAccountAndWorldObject(Guid accountId, InstanceId worldObjectId) {
            return characters.Find(r => r.ownerAccountId == accountId && r.worldObjectId == worldObjectId).FirstOrDefault();
        }

        public bool characterExistsWithAccountAndWorldObject(Guid accountId, InstanceId worldObjectId) {
            return characters.Find(r => r.ownerAccountId == accountId && r.worldObjectId == worldObjectId).Any();
        }

        public Character insertCharacter(Character character) {
            characters.InsertOne(character);
            return character;
        }

        public bool deleteCharacter(Guid id) {
            return characters.DeleteOne(r => r.id == id).DeletedCount == 1;
        }
    }
}
