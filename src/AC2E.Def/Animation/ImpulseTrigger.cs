namespace AC2E.Def {

    public class ImpulseTrigger {

        public Vector impulse; // mImpulse
        public bool jump; // mJump
        public uint impulseNum; // mImpulseNum
        public bool triggered; // mTriggered
        public float time; // mTime

        public ImpulseTrigger(AC2Reader data) {
            impulse = data.ReadVector();
            jump = data.ReadBoolean();
            impulseNum = data.ReadUInt32();
            triggered = data.ReadBoolean();
            time = data.ReadSingle();
        }

        public void write(AC2Writer data) {
            data.Write(impulse);
            data.Write(jump);
            data.Write(impulseNum);
            data.Write(triggered);
            data.Write(time);
        }
    }
}
