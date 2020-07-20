namespace AC2E.Def {

    public class EchoResponseHeader {

        // CEchoResponseHeader
        public float localTime; // m_LocalTimeConstructed
        public float localToServerTimeDelta; // m_LocalTimeConstructed

        public void write(AC2Writer data) {
            data.Write(localTime);
            data.Write(localToServerTimeDelta);
        }
    }
}
