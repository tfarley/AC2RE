namespace AC2E.Def {

    public class ActionMapping {

        public uint action; // act
        public uint semantic; // semantic
        public string name; // name

        public ActionMapping(AC2Reader data) {
            action = data.ReadUInt32();
            semantic = data.ReadUInt32();
            name = data.ReadString();
        }
    }
}
