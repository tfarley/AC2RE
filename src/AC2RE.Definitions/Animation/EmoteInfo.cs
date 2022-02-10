namespace AC2RE.Definitions;

public class EmoteInfo : IHeapObject {

    public PackageType packageType => PackageType.EmoteInfo;

    public StringInfo description; // _desc
    public StringInfo selfText; // _self
    public StringInfo otherText; // _other
    public BehaviorParams emoteParams; // _emoteParams
    public StringInfo commandText; // _cmd

    public EmoteInfo(AC2Reader data) {
        data.ReadHO<StringInfo>(v => description = v);
        data.ReadHO<StringInfo>(v => selfText = v);
        data.ReadHO<StringInfo>(v => otherText = v);
        data.ReadHO<BehaviorParams>(v => emoteParams = v);
        data.ReadHO<StringInfo>(v => commandText = v);
    }
}
