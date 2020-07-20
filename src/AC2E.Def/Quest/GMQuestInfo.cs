namespace AC2E.Def {

    public class GMQuestInfo : IPackage {

        public NativeType nativeType => NativeType.GMQUESTINFO;

        public uint _questID;
        public StringInfo _strQuestName;
        public StringInfo _strQuestDescription;
        public DataId _didIcon;
        public int _iChallengeLevel;
        public uint _questStatus;
        public uint _uiCurPhase;
        public StringInfo _strCurJournalEntry;
        public double _ttBestowal;
        public double _ttDone;
        public bool _bExpires;
        public bool _bMaxedOut;
        public double _ttSecondsRemaining;
        public double _ttSecondsUntilRetry;
        public bool _playFXOnUpdate;

        public GMQuestInfo() {

        }

        public GMQuestInfo(AC2Reader data) {
            // TODO: This format does not match known stuff, so the names/order here may be slightly incorrect
            _questID = data.ReadUInt32();
            _strQuestName = new StringInfo(data);
            _strQuestDescription = new StringInfo(data);
            _didIcon = data.ReadDataId();
            _questStatus = data.ReadUInt32();
            _uiCurPhase = data.ReadUInt32();
            _strCurJournalEntry = new StringInfo(data);
            _iChallengeLevel = data.ReadInt32();
            _ttBestowal = data.ReadDouble();
            _ttDone = data.ReadDouble();
            _bExpires = data.ReadBoolean();
            _ttSecondsRemaining = data.ReadDouble();
            _ttSecondsUntilRetry = data.ReadDouble();
            _bMaxedOut = data.ReadBoolean();
            _playFXOnUpdate = data.ReadBoolean();
        }

        public void write(AC2Writer data) {
            data.Write(_questID);
            _strQuestName.write(data);
            _strQuestDescription.write(data);
            data.Write(_didIcon);
            data.Write(_questStatus);
            data.Write(_uiCurPhase);
            _strCurJournalEntry.write(data);
            data.Write(_iChallengeLevel);
            data.Write(_ttBestowal);
            data.Write(_ttDone);
            data.Write(_bExpires);
            data.Write(_ttSecondsRemaining);
            data.Write(_ttSecondsUntilRetry);
            data.Write(_bMaxedOut);
            data.Write(_playFXOnUpdate);
        }
    }
}
