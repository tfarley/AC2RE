namespace AC2E.Def {

    public class TotemUsageBlob : UsageBlob {

        public override PackageType packageType => PackageType.TotemUsageBlob;

        public uint targetWeenieType; // m_targetWeenieType
        public StringInfo playerName; // m_siPlayerName
        public int minTargetLore; // m_minTargetLore
        public ARHash<IPackage> effects; // m_effects
        public int minTargetLevel; // m_minTargetLevel

        public TotemUsageBlob() : base() {

        }

        public TotemUsageBlob(AC2Reader data) : base(data) {
            targetWeenieType = data.ReadUInt32();
            data.ReadPkg<StringInfo>(v => playerName = v);
            minTargetLore = data.ReadInt32();
            data.ReadPkg<ARHash<IPackage>>(v => effects = v);
            minTargetLevel = data.ReadInt32();
        }

        public override void write(AC2Writer data) {
            base.write(data);
            data.Write(targetWeenieType);
            data.WritePkg(playerName);
            data.Write(minTargetLore);
            data.WritePkg(effects);
            data.Write(minTargetLevel);
        }
    }
}
