namespace AC2E.Def {

    public class DefaultTakePermissionBlob : IPackage {

        public PackageType packageType => PackageType.DefaultTakePermissionBlob;

        public uint requiredQuestStatus; // m_requiredQuestStatus
        public uint requiredQuest; // m_requiredQuest

        public DefaultTakePermissionBlob(AC2Reader data) {
            requiredQuestStatus = data.ReadUInt32();
            requiredQuest = data.ReadUInt32();
        }
    }
}
