using System.Collections.Generic;

namespace AC2RE.Definitions;

public class FxScript {

    public DataId did; // m_DID
    public List<Trigger> fxTriggers; // mEffectsTriggers
    public List<ImpulseTrigger> impulseTriggers; // mImpulseTriggers
    public float period; // mPeriod

    public FxScript() {

    }

    public FxScript(AC2Reader data) {
        did = data.ReadDataId();
        fxTriggers = data.ReadList(() => new Trigger(data));
        impulseTriggers = data.ReadList(() => new ImpulseTrigger(data));
        period = data.ReadSingle();
    }

    public void write(AC2Writer data) {
        data.Write(did);
        data.Write(fxTriggers, v => v.write(data));
        data.Write(impulseTriggers, v => v.write(data));
        data.Write(period);
    }
}
