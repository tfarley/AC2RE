using AC2E.Def;
using System;

namespace AC2E.Server {

    public class Character {

        public Guid id { get; private set; }

        public Guid ownerAccountId;
        public InstanceId worldObjectId;
    }
}
