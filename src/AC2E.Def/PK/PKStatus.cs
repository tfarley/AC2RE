namespace AC2E.Def {

    public class PKStatus : IPackage {

        public PackageType packageType => PackageType.PKStatus;

        public FactionStatus factionStatus; // m_factionStatus
        public FactionType faction; // m_faction
        public uint permAlwaysTrue; // m_permAlwaysTrue
        public uint flags; // m_flags
        public InstanceId petMasterId; // m_petMaster
        public uint pkType; // m_pkType
        public uint permAlwaysFalse; // m_permAlwaysFalse
        public InstanceId id; // m_iid
        public ErrorType errorTypeInvulnerability; // m_etInvulnerability

        public PKStatus(AC2Reader data) {
            factionStatus = (FactionStatus)data.ReadUInt32();
            faction = (FactionType)data.ReadUInt32();
            permAlwaysTrue = data.ReadUInt32();
            flags = data.ReadUInt32();
            petMasterId = data.ReadInstanceId();
            pkType = data.ReadUInt32();
            permAlwaysFalse = data.ReadUInt32();
            id = data.ReadInstanceId();
            errorTypeInvulnerability = (ErrorType)data.ReadUInt32();
        }
    }
}
