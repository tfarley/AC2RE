﻿namespace AC2E.Def {

    public class QuestTriggerEffect : Effect {

        public override PackageType packageType => PackageType.QuestTriggerEffect;

        public QuestUpdateType questUpdateType; // m_questUpdateType
        public QuestId questId; // m_questID

        public QuestTriggerEffect(AC2Reader data) : base(data) {
            questUpdateType = new QuestUpdateType(data.ReadUInt32());
            questId = (QuestId)data.ReadUInt32();
        }
    }
}
