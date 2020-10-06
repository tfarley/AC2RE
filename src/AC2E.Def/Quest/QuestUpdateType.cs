namespace AC2E.Def {

    // Dat file 23000091
    public struct QuestUpdateType {

        private static readonly uint STATUS_MASK = 0xF0000000; // RawStatusMask
        private static readonly uint PHASE_NUMBER_MASK = 0x0FFFFFFF; // PhaseNumberMask

        public QuestStatus status;
        public uint phase;
        public uint value => ((uint)status | phase);

        public QuestUpdateType(uint value) {
            status = (QuestStatus)(value & STATUS_MASK);
            phase = (value & PHASE_NUMBER_MASK);
        }
    }
}
