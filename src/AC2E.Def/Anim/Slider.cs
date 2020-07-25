namespace AC2E.Def {

    public class Slider {

        // Enum Slider::Controller
        public enum Controller {

            CLIENT_CONTROLLED = 0,
            WEENIE_CONTROLLED = 1,
        }

        public Controller controller; // controller

        public Slider(AC2Reader data) {
            controller = (Controller)data.ReadUInt32();
        }

        public void write(AC2Writer data) {
            data.Write((uint)controller);
        }
    }
}
