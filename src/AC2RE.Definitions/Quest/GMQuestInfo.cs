namespace AC2RE.Definitions {

    public class GMQuestInfo : IPackage {

        public NativeType nativeType => NativeType.GMQUESTINFO;

        public QuestId questId; // _questID
        public StringInfo questName; // _strQuestName
        public StringInfo questDescription; // _strQuestDescription
        public DataId iconDid; // _didIcon
        public int challengeLevel; // _iChallengeLevel
        public QuestStatus questStatus; // _questStatus
        public uint curPhase; // _uiCurPhase
        public StringInfo curJournalEntry; // _strCurJournalEntry
        public double bestowalTime; // _ttBestowal
        public double doneTime; // _ttDone
        public bool expired; // _bExpires
        public bool maxedOut; // _bMaxedOut
        public double secondsRemaining; // _ttSecondsRemaining
        public double secondsUntilRetry; // _ttSecondsUntilRetry
        public bool playFxOnUpdate; // _playFXOnUpdate

        public GMQuestInfo() {

        }

        public GMQuestInfo(AC2Reader data) {
            // TODO: This format does not match known stuff, so the names/order here may be slightly incorrect
            questId = (QuestId)data.ReadUInt32();
            questName = new(data);
            questDescription = new(data);
            iconDid = data.ReadDataId();
            questStatus = (QuestStatus)data.ReadUInt32();
            curPhase = data.ReadUInt32();
            curJournalEntry = new(data);
            challengeLevel = data.ReadInt32();
            bestowalTime = data.ReadDouble();
            doneTime = data.ReadDouble();
            expired = data.ReadBoolean();
            secondsRemaining = data.ReadDouble();
            secondsUntilRetry = data.ReadDouble();
            maxedOut = data.ReadBoolean();
            playFxOnUpdate = data.ReadBoolean();
        }

        public void write(AC2Writer data) {
            data.Write((uint)questId);
            questName.write(data);
            questDescription.write(data);
            data.Write(iconDid);
            data.Write((uint)questStatus);
            data.Write(curPhase);
            curJournalEntry.write(data);
            data.Write(challengeLevel);
            data.Write(bestowalTime);
            data.Write(doneTime);
            data.Write(expired);
            data.Write(secondsRemaining);
            data.Write(secondsUntilRetry);
            data.Write(maxedOut);
            data.Write(playFxOnUpdate);
        }
    }
}
