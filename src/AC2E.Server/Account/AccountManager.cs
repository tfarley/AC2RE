using AC2E.Def;
using System.Collections.Generic;

namespace AC2E.Server {

    internal class AccountManager {

        public Account authenticate(string accountName) {
            return new Account(accountName, new List<Character> { new Character(new InstanceId(0x213000000000dd9d), "TestChar") });
        }
    }
}
