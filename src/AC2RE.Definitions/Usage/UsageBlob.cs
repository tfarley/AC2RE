namespace AC2RE.Definitions;

public class UsageBlob : IHeapObject {

    public virtual PackageType packageType => PackageType.UsageBlob;

    public StringInfo criticalSuccessText; // m_criticalSuccessMessage
    public StringInfo successText; // m_successMessage
    public uint userBehaviorRepeatCount; // m_userBehaviorRepeatCount
    public float userBehaviorTimeScale; // m_userBehaviorTimeScale
    public BehaviorId userBehavior; // m_userBehavior
    public bool userBehaviorFadeChildren; // m_userBehaviorFadeChildren

    public UsageBlob() {

    }

    public UsageBlob(AC2Reader data) {
        data.ReadHO<StringInfo>(v => criticalSuccessText = v);
        data.ReadHO<StringInfo>(v => successText = v);
        userBehaviorRepeatCount = data.ReadUInt32();
        userBehaviorTimeScale = data.ReadSingle();
        userBehavior = (BehaviorId)data.ReadUInt32();
        userBehaviorFadeChildren = data.ReadBoolean();
    }

    public virtual void write(AC2Writer data) {
        data.WriteHO(criticalSuccessText);
        data.WriteHO(successText);
        data.Write(userBehaviorRepeatCount);
        data.Write(userBehaviorTimeScale);
        data.Write((uint)userBehavior);
        data.Write(userBehaviorFadeChildren);
    }
}
