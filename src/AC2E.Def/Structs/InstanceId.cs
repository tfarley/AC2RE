namespace AC2E.Def.Structs {

    public struct InstanceId {

        // Enum IIDUtils::IDType
        public enum IDType : uint {
            UNKNOWN = 0,
            UI = 1,
            CAMERA = 2,
            TEMPORARY = 3,
            PLAYER = 4,
            STATIC = 5,
            DYNAMIC = 6,
        }

        public ulong id;

        public InstanceId(ulong id) {
            this.id = id;
        }

        public override string ToString() {
            return $"0x{id:X16}";
        }
    }
}
