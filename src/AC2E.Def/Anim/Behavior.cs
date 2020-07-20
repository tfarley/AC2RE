namespace AC2E.Def {

    public class Behavior {

        public ulong flags; // m_Flags

        public Behavior(AC2Reader data) {
            flags = data.ReadUInt64();
        }

        public void write(AC2Writer data) {
            data.Write(flags);
        }
    }
}
