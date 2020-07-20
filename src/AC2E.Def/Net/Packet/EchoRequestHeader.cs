namespace AC2E.Def {

    public class EchoRequestHeader {

        // CEchoRequestHeader
        public float localTime; // m_LocalTime

        public EchoRequestHeader(AC2Reader data) {
            localTime = data.ReadSingle();
        }
    }
}
