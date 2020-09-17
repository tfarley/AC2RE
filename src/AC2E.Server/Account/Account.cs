using System.Collections.Generic;

namespace AC2E.Server {

    internal class Account {

        public readonly string accountName;
        public readonly List<Character> characters;

        public Account(string accountName, List<Character> characters) {
            this.accountName = accountName;
            this.characters = characters;
        }
    }
}
