using System.IO;

namespace AC2RE.Definitions {

    public class TransitionDesc {

        // Const TRANS*
        public enum TransitionType : uint {
            UNDEF,
            MOVE,
            RESIZE,
            FADE,
            ROTATE,
        }

        public class MoveResizeTransitionData : IWritable {

            // TD_Data_Move, TD_Data_Resize
            public int endOffsetX; // endxOffset
            public int endOffsetY; // endyOffset
            public int startOffsetX; // startxOffset
            public int startOffsetY; // startyOffset
            public double duration; // duration
            public uint flags; // flags

            public MoveResizeTransitionData() {

            }

            public MoveResizeTransitionData(AC2Reader data) {
                endOffsetX = data.ReadInt32();
                endOffsetY = data.ReadInt32();
                startOffsetX = data.ReadInt32();
                startOffsetY = data.ReadInt32();
                duration = data.ReadDouble();
                flags = data.ReadUInt32();
            }

            public void write(AC2Writer data) {
                data.Write(endOffsetX);
                data.Write(endOffsetY);
                data.Write(startOffsetX);
                data.Write(startOffsetY);
                data.Write(duration);
                data.Write(flags);
            }
        }

        public class FadeTransitionData : IWritable {

            // TD_Data_Fade
            public float endAlpha; // endAlpha
            public float startAlpha; // startAlpha
            public double duration; // duration
            public uint flags; // flags

            public FadeTransitionData() {

            }

            public FadeTransitionData(AC2Reader data) {
                endAlpha = data.ReadSingle();
                startAlpha = data.ReadSingle();
                duration = data.ReadDouble();
                flags = data.ReadUInt32();
            }

            public void write(AC2Writer data) {
                data.Write(endAlpha);
                data.Write(startAlpha);
                data.Write(duration);
                data.Write(flags);
            }
        }

        public class RotateTransitionData : IWritable {

            // TD_Data_Rotate
            public int endX; // endX
            public int endY; // endY
            public int endZ; // endZ
            public double duration; // duration
            public uint flags; // flags

            public RotateTransitionData() {

            }

            public RotateTransitionData(AC2Reader data) {
                endX = data.ReadInt32();
                endY = data.ReadInt32();
                endZ = data.ReadInt32();
                duration = data.ReadDouble();
                flags = data.ReadUInt32();
            }

            public void write(AC2Writer data) {
                data.Write(endX);
                data.Write(endY);
                data.Write(endZ);
                data.Write(duration);
                data.Write(flags);
            }
        }

        public TransitionType type; // type
        public uint when; // when
        public uint state; // state
        public IWritable transitionData; // data

        public TransitionDesc() {

        }

        public TransitionDesc(AC2Reader data) {
            type = (TransitionType)data.ReadUInt32();
            when = data.ReadUInt32();
            state = data.ReadUInt32();
            transitionData = type switch {
                TransitionType.MOVE or TransitionType.RESIZE => new MoveResizeTransitionData(data),
                TransitionType.FADE => new FadeTransitionData(data),
                TransitionType.ROTATE => new RotateTransitionData(data),
                _ => throw new InvalidDataException(type.ToString()),
            };
        }

        public void write(AC2Writer data) {
            data.Write((uint)type);
            data.Write(when);
            data.Write(state);
            transitionData.write(data);
        }
    }
}
