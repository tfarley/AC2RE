namespace AC2E.Def {

    public struct InstanceId {

        // Enum IIDUtils::IDType
        public enum IdType : uint {
            UNKNOWN,
            UI,
            CAMERA,
            TEMPORARY,
            PLAYER,
            STATIC,
            DYNAMIC,
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
