namespace AC2E.Def {

    public class WeenieHook {

        // Const *_AnimHookType
        public enum AnimHookType : uint {
            UNDEF = 0,
            ATTACK = 0x40000002,
        }

        public AnimHookType hookType; // mHookType
        public uint hookNum; // mHookNum
        public uint hookData; // mData
        public bool triggered; // mTriggered
        public float time; // mTime

        public WeenieHook(AC2Reader data) {
            hookType = (AnimHookType)data.ReadUInt32();
            hookNum = data.ReadUInt32();
            hookData = data.ReadUInt32();
            triggered = data.ReadBoolean();
            time = data.ReadSingle();
        }

        public void write(AC2Writer data) {
            data.Write((uint)hookType);
            data.Write(hookNum);
            data.Write(hookData);
            data.Write(triggered);
            data.Write(time);
        }
    }
}
