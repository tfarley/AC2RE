﻿namespace AC2E.Server {

    internal class Player {

        public readonly ClientId clientId;
        public readonly Account account;

        public Player(ClientId clientId, Account account) {
            this.clientId = clientId;
            this.account = account;
        }
    }
}
