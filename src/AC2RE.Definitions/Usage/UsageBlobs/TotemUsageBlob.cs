using System.Collections.Generic;

namespace AC2RE.Definitions;

public class TotemUsageBlob : UsageBlob {

    public override PackageType packageType => PackageType.TotemUsageBlob;

    public WeenieType targetWeenieType; // m_targetWeenieType
    public StringInfo playerName; // m_siPlayerName
    public int minTargetLore; // m_minTargetLore
    public Dictionary<uint, IPackage> effects; // m_effects
    public int minTargetLevel; // m_minTargetLevel

    public TotemUsageBlob() : base() {

    }

    public TotemUsageBlob(AC2Reader data) : base(data) {
        targetWeenieType = (WeenieType)data.ReadUInt32();
        data.ReadPkg<StringInfo>(v => playerName = v);
        minTargetLore = data.ReadInt32();
        data.ReadPkg<ARHash>(v => effects = v);
        minTargetLevel = data.ReadInt32();
    }

    public override void write(AC2Writer data) {
        base.write(data);
        data.Write((uint)targetWeenieType);
        data.WritePkg(playerName);
        data.Write(minTargetLore);
        data.WritePkg(ARHash.from(effects));
        data.Write(minTargetLevel);
    }
}
