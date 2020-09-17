using AC2E.Def;

namespace AC2E.Server {

    internal class Character {

        public readonly InstanceId id;
        public readonly string name;

        public Character(InstanceId id, string name) {
            this.id = id;
            this.name = name;
        }

        public CharacterIdentity toIdentity() {
            return new CharacterIdentity {
                id = id,
                name = name,
                secondsGreyedOut = 0,
                visualDesc = new VisualDesc {
                    packFlags = VisualDesc.PackFlag.PARENT,
                    parentDid = new DataId(0x1F001110),
                },
            };
        }
    }
}
