namespace AC2RE.Definitions {

    public class ICMDCommandHeader {

        // CICMDCommandStruct
        public ICMDCommand cmd; // Cmd
        public uint param; // Param

        public ICMDCommandHeader() {

        }

        public ICMDCommandHeader(AC2Reader data) {
            cmd = (ICMDCommand)data.ReadUInt32();
            param = data.ReadUInt32();
        }

        public void write(AC2Writer data) {
            data.Write((uint)cmd);
            data.Write(param);
        }
    }
}
