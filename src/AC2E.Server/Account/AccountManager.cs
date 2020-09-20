using AC2E.Def;
using AC2E.Server.Database;
using System;
using System.Collections.Generic;

namespace AC2E.Server {

    internal class AccountManager {

        private readonly AccountDatabase accountDb;

        public AccountManager(AccountDatabase accountDb) {
            this.accountDb = accountDb;
        }

        public Account authenticate(string userName, string password) {
            return accountDb.getAccountWithUserNameAndPassword(userName, password);
        }

        public Account register(string userName, string password) {
            Account account = new Account {
                userName = userName,
                password = password,
            };

            accountDb.insertAccount(account);

            return account;
        }

        public bool accountExistsWithUserName(string userName) {
            return accountDb.accountExistsWithUserName(userName);
        }

        public List<Character> getCharactersWithAccount(Guid accountId) {
            return accountDb.getCharactersWithAccount(accountId);
        }

        public Character getCharacterWithAccountAndWorldObject(Guid accountId, InstanceId worldObjectId) {
            return accountDb.getCharacterWithAccountAndWorldObject(accountId, worldObjectId);
        }

        public bool characterExistsWithAccountAndWorldObject(Guid accountId, InstanceId worldObjectId) {
            return accountDb.characterExistsWithAccountAndWorldObject(accountId, worldObjectId);
        }

        public Character createCharacter(Character character) {
            return accountDb.insertCharacter(character);
        }

        public bool deleteCharacter(Guid id) {
            return accountDb.deleteCharacter(id);
        }
    }
}
