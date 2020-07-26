namespace AC2E.Def {

    public struct ModeStateKey {

        public uint elementId; // elementID
        public uint modeId; // modeID

        public ModeStateKey(AC2Reader data) {
            elementId = data.ReadUInt32();
            modeId = data.ReadUInt32();
        }
    }
}
