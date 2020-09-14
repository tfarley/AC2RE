namespace AC2E.Def {

    public class EmoteInfo : IPackage {

        public PackageType packageType => PackageType.EmoteInfo;

        public StringInfo description; // _desc
        public StringInfo selfText; // _self
        public StringInfo otherText; // _other
        public BehaviorParams emoteParams; // _emoteParams
        public StringInfo commandText; // _cmd

        public EmoteInfo(AC2Reader data) {
            data.ReadPkg<StringInfo>(v => description = v);
            data.ReadPkg<StringInfo>(v => selfText = v);
            data.ReadPkg<StringInfo>(v => otherText = v);
            data.ReadPkg<BehaviorParams>(v => emoteParams = v);
            data.ReadPkg<StringInfo>(v => commandText = v);
        }
    }
}
