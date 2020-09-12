namespace AC2E.Def {

    public class QuestTriggerEffect : InstantEffect {

        public override PackageType packageType => PackageType.QuestTriggerEffect;

        public uint questUpdateType; // m_questUpdateType
        public uint questId; // m_questID

        public QuestTriggerEffect(AC2Reader data) : base(data) {
            questUpdateType = data.ReadUInt32();
            questId = data.ReadUInt32();
        }
    }
}
