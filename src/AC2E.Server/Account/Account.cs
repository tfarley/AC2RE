using System;

namespace AC2E.Server {

    public class Account {

        public Guid id { get; private set; }

        public string userName;
        public string password;
    }
}
