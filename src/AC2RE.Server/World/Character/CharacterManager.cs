using AC2RE.Definitions;
using AC2RE.Server.Database;
using System;
using System.Collections.Generic;

namespace AC2RE.Server {

    internal class CharacterManager {

        private readonly WorldDatabase worldDb;
        private readonly HashSet<AccountId> loadedAccounts = new();
        private readonly Dictionary<CharacterId, Character> characters = new();

        public CharacterManager(WorldDatabase worldDb) {
            this.worldDb = worldDb;
        }

        private void loadWithAccount(AccountId accountId) {
            if (!loadedAccounts.Contains(accountId)) {
                List<Character> dbCharacters = worldDb.getCharactersWithAccount(accountId);
                foreach (Character dbCharacter in dbCharacters) {
                    characters.TryAdd(dbCharacter.id, dbCharacter);
                }

                loadedAccounts.Add(accountId);
            }
        }

        public Character? get(CharacterId id) {
            if (!characters.TryGetValue(id, out Character? character)) {
                character = worldDb.getCharacterWithId(id);
                if (character != null) {
                    characters[id] = character;
                }
            }
            return (character == null || character.deleted) ? null : character;
        }

        public List<Character> getWithAccount(AccountId accountId) {
            loadWithAccount(accountId);
            List<Character> charactersWithAccount = new();
            foreach (Character character in characters.Values) {
                if (!character.deleted && character.ownerAccountId == accountId) {
                    charactersWithAccount.Add(character);
                }
            }
            charactersWithAccount.Sort((c1, c2) => c1.order.CompareTo(c2.order));
            return charactersWithAccount;
        }

        public Character? getWithAccountAndWorldObject(AccountId accountId, InstanceId worldObjectId) {
            loadWithAccount(accountId);
            foreach (Character character in characters.Values) {
                if (!character.deleted && character.ownerAccountId == accountId && character.worldObjectId == worldObjectId) {
                    return character;
                }
            }
            return null;
        }

        public bool existsWithAccountAndWorldObject(AccountId accountId, InstanceId worldObjectId) {
            loadWithAccount(accountId);
            foreach (Character character in characters.Values) {
                if (!character.deleted && character.ownerAccountId == accountId && character.worldObjectId == worldObjectId) {
                    return true;
                }
            }
            return false;
        }

        public Character createWithAccountAndWorldObject(AccountId accountId, InstanceId worldObjectId) {
            uint highestOrder = 0;
            List<Character> existingCharacters = getWithAccount(accountId);
            if (existingCharacters.Count > 0) {
                highestOrder = existingCharacters[^1].order;
            }

            Character character = new(new(Guid.NewGuid())) {
                order = highestOrder + 1,
                ownerAccountId = accountId,
                worldObjectId = worldObjectId,
            };
            characters[character.id] = character;
            return character;
        }

        public void contributeToSave(WorldSave worldSave) {
            worldSave.characters.AddRange(characters.Values);
        }
    }
}
