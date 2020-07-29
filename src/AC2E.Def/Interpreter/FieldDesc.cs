namespace AC2E.Def {

    public struct FieldDesc {

        public readonly StackType fieldType;
        public readonly uint numWords;

        public FieldDesc(StackType fieldType, uint numWords) {
            this.fieldType = fieldType;
            this.numWords = numWords;
        }
    }
}
