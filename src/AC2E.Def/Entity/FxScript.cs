using System.Collections.Generic;

namespace AC2E.Def {

    public class FxScript {

        public class Trigger {

            public uint name; // mName
            public bool stop; // mStop
            public float time; // mTime
            public float intensity; // mIntensity
            public float intensityVar; // mIntensityVar
            public float probability; // mProbability
            public bool play; // mPlay

            public Trigger(AC2Reader data) {
                name = data.ReadUInt32();
                stop = data.ReadBoolean();
                time = data.ReadSingle();
                intensity = data.ReadSingle();
                intensityVar = data.ReadSingle();
                probability = data.ReadSingle();
                play = data.ReadBoolean();
            }

            public void write(AC2Writer data) {
                data.Write(name);
                data.Write(stop);
                data.Write(time);
                data.Write(intensity);
                data.Write(intensityVar);
                data.Write(probability);
                data.Write(play);
            }
        }

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

        public DataId did; // m_DID
        public List<Trigger> effectsTriggers; // mEffectsTriggers
        public List<ImpulseTrigger> impulseTriggers; // mImpulseTriggers
        public float period; // mPeriod

        public FxScript() {

        }

        public FxScript(AC2Reader data) {
            did = data.ReadDataId();
            effectsTriggers = data.ReadList(() => new Trigger(data));
            impulseTriggers = data.ReadList(() => new ImpulseTrigger(data));
            period = data.ReadSingle();
        }

        public void write(AC2Writer data) {
            data.Write(did);
            data.Write(effectsTriggers, v => v.write(data));
            data.Write(impulseTriggers, v => v.write(data));
            data.Write(period);
        }
    }
}
