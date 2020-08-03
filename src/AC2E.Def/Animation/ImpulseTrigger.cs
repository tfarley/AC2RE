namespace AC2E.Def {

    public class ImpulseTrigger {

        public Vector impulse; // mImpulse
        public bool jump; // mJump
        public uint impulseNum; // mImpulseNum
        public float time; // mTime

        public ImpulseTrigger(AC2Reader data) {
            jump = data.ReadBoolean();
            if (!jump) {
                impulse = data.ReadVector();
            }
            impulseNum = data.ReadUInt32();
            time = data.ReadSingle();
        }

        public void write(AC2Writer data) {
            data.Write(jump);
            if (!jump) {
                data.Write(impulse);
            }
            data.Write(impulseNum);
            data.Write(time);
        }
    }
}
