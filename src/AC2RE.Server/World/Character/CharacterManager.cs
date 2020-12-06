using AC2RE.Definitions;
using AC2RE.Server.Database;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace AC2RE.Server {

    internal class CharacterManager {

        private readonly World world;
        private readonly HashSet<AccountId> loadedAccounts = new();
        private readonly Dictionary<CharacterId, Character> characters = new();

        public CharacterManager(World world) {
            this.world = world;
        }

        private void loadWithAccount(AccountId accountId) {
            if (!loadedAccounts.Contains(accountId)) {
                List<Character> dbCharacters = world.worldDb.getCharactersWithAccount(accountId);
                foreach (Character dbCharacter in dbCharacters) {
                    characters.TryAdd(dbCharacter.id, dbCharacter);
                }

                loadedAccounts.Add(accountId);
            }
        }

        public bool tryGet(CharacterId id, [MaybeNullWhen(false)] out Character character) {
            character = get(id);
            return character != null;
        }

        public Character? get(CharacterId id) {
            if (!characters.TryGetValue(id, out Character? character)) {
                character = world.worldDb.getCharacterWithId(id);
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
                if (!character.deleted && character.accountId == accountId) {
                    charactersWithAccount.Add(character);
                }
            }
            charactersWithAccount.Sort((c1, c2) => c1.order.CompareTo(c2.order));
            return charactersWithAccount;
        }

        public bool tryGetWithAccountAndObject(AccountId accountId, InstanceId objectId, [MaybeNullWhen(false)] out Character character) {
            character = getWithAccountAndObject(accountId, objectId);
            return character != null;
        }

        public Character? getWithAccountAndObject(AccountId accountId, InstanceId objectId) {
            loadWithAccount(accountId);
            foreach (Character character in characters.Values) {
                if (!character.deleted && character.accountId == accountId && character.objectId == objectId) {
                    return character;
                }
            }
            return null;
        }

        public bool existsWithAccountAndWorldObject(AccountId accountId, InstanceId objectId) {
            loadWithAccount(accountId);
            foreach (Character character in characters.Values) {
                if (!character.deleted && character.accountId == accountId && character.objectId == objectId) {
                    return true;
                }
            }
            return false;
        }

        public Character createWithAccountAndObject(AccountId accountId, InstanceId objectId) {
            uint highestOrder = 0;
            List<Character> existingCharacters = getWithAccount(accountId);
            if (existingCharacters.Count > 0) {
                highestOrder = existingCharacters[^1].order;
            }

            Character character = new(new(Guid.NewGuid()), highestOrder + 1, accountId, objectId);
            characters[character.id] = character;
            return character;
        }

        public void contributeToSave(WorldSave worldSave) {
            worldSave.characters.AddRange(characters.Values);
        }
    }
}
