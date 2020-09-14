namespace AC2E.Def {

    public class EmoteTable : IPackage {

        public PackageType packageType => PackageType.EmoteTable;

        public ARHash<EmoteInfo> emoteHash; // _emoteHash
        public bool setupOK; // _setupOK

        public EmoteTable(AC2Reader data) {
            data.ReadPkg<ARHash<IPackage>>(v => emoteHash = v.to<EmoteInfo>());
            setupOK = data.ReadBoolean();
        }
    }
}
