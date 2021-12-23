using System.Collections.Generic;

namespace AC2RE.Definitions;

public class EmoteTable : IPackage {

    public PackageType packageType => PackageType.EmoteTable;

    public Dictionary<uint, EmoteInfo> emoteHash; // _emoteHash
    public bool setupOK; // _setupOK

    public EmoteTable(AC2Reader data) {
        data.ReadPkg<ARHash>(v => emoteHash = v.to<uint, EmoteInfo>());
        setupOK = data.ReadBoolean();
    }
}
