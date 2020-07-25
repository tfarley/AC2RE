namespace AC2E.Def {

    public class Mode {

        public bool isCombat; // isCombat

        public Mode(AC2Reader data) {
            isCombat = data.ReadBoolean();
        }

        public void write(AC2Writer data) {
            data.Write(isCombat);
        }
    }
}
