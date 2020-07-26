namespace AC2E.Def {

    public class GMQuestInfo : IPackage {

        public NativeType nativeType => NativeType.GMQUESTINFO;

        public uint questId; // _questID
        public StringInfo questName; // _strQuestName
        public StringInfo questDescription; // _strQuestDescription
        public DataId iconDid; // _didIcon
        public int challengeLevel; // _iChallengeLevel
        public uint questStatus; // _questStatus
        public uint curPhase; // _uiCurPhase
        public StringInfo curJournalEntry; // _strCurJournalEntry
        public double bestowalTime; // _ttBestowal
        public double doneTime; // _ttDone
        public bool expired; // _bExpires
        public bool maxedOut; // _bMaxedOut
        public double secondsRemaining; // _ttSecondsRemaining
        public double secondsUntilRetry; // _ttSecondsUntilRetry
        public bool playFXOnUpdate; // _playFXOnUpdate

        public GMQuestInfo() {

        }

        public GMQuestInfo(AC2Reader data) {
            // TODO: This format does not match known stuff, so the names/order here may be slightly incorrect
            questId = data.ReadUInt32();
            questName = new StringInfo(data);
            questDescription = new StringInfo(data);
            iconDid = data.ReadDataId();
            questStatus = data.ReadUInt32();
            curPhase = data.ReadUInt32();
            curJournalEntry = new StringInfo(data);
            challengeLevel = data.ReadInt32();
            bestowalTime = data.ReadDouble();
            doneTime = data.ReadDouble();
            expired = data.ReadBoolean();
            secondsRemaining = data.ReadDouble();
            secondsUntilRetry = data.ReadDouble();
            maxedOut = data.ReadBoolean();
            playFXOnUpdate = data.ReadBoolean();
        }

        public void write(AC2Writer data) {
            data.Write(questId);
            questName.write(data);
            questDescription.write(data);
            data.Write(iconDid);
            data.Write(questStatus);
            data.Write(curPhase);
            curJournalEntry.write(data);
            data.Write(challengeLevel);
            data.Write(bestowalTime);
            data.Write(doneTime);
            data.Write(expired);
            data.Write(secondsRemaining);
            data.Write(secondsUntilRetry);
            data.Write(maxedOut);
            data.Write(playFXOnUpdate);
        }
    }
}
