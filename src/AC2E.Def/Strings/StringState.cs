using System.Text;

namespace AC2E.Def {

    public class StringState {

        public uint version; // version
        public ushort numberBase; // base
        public ushort numDecimalDigits; // numDecimalDigits
        public bool leadingZero; // leadingZero
        public ushort groupingSize; // groupingSize
        public string numerals; // numerals
        public string decimalSeperator; // decimalSeperator
        public string groupingSeperator; // groupingSeperator
        public string negativeNumberFormat; // negativeNumberFormat
        public bool isZeroSingular; // isZeroSingular
        public bool isOneSingular; // isOneSingular
        public bool isNegativeOneSingular; // isNegativeOneSingular
        public bool isTwoOrMoreSingular; // isTwoOrMoreSingular
        public bool isNegativeTwoOrLessSingular; // isNegativeTwoOrLessSingular
        public string treasurePrefixLetters; // treasurePrefixLetters
        public string treasureMiddleLetters; // treasureMiddleLetters
        public string treasureSuffixLetters; // treasureSuffixLetters
        public string malePlayerLetters; // malePlayerLetters
        public string femalePlayerLetters; // femalePlayerLetters
        public uint topGoreLevel; // m_topGoreLevel
        public uint topBloodColor; // m_topBloodColor
        public uint imeEnabledSetting; // m_ImeEnabledSetting
        public uint symbolColor; // m_symbolColor
        public uint symbolColorText; // m_symbolColorText
        public uint symbolHeight; // m_symbolHeight
        public uint symbolTranslucence; // m_symbolTranslucence
        public uint symbolPlacement; // m_symbolPlacement
        public uint candColorBase; // m_candColorBase
        public uint candColorBorder; // m_candColorBorder
        public uint candColorText; // m_candColorText
        public uint compColorInput; // m_compColorInput
        public uint compColorTargetConv; // m_compColorTargetConv
        public uint compColorConverted; // m_compColorConverted
        public uint compColorTargetNotConv; // m_compColorTargetNotConv
        public uint compColorInputErr; // m_compColorInputErr
        public uint compTranslucence; // m_compTranslucence
        public uint compColorText; // m_compColorText
        public uint otherIME; // m_otherIME
        public bool wordWrapOnSpace; // m_wordWrapOnSpace
        public string additionalSettings; // m_additionalSettings
        public uint additionalFlags; // m_additionalFlags

        public StringState(AC2Reader data) {
            version = data.ReadUInt32();
            numberBase = data.ReadUInt16();
            numDecimalDigits = data.ReadUInt16();
            leadingZero = data.ReadBoolean();
            groupingSize = data.ReadUInt16();
            data.Align(4);
            numerals = data.ReadString(Encoding.Unicode);
            decimalSeperator = data.ReadString(Encoding.Unicode);
            groupingSeperator = data.ReadString(Encoding.Unicode);
            negativeNumberFormat = data.ReadString(Encoding.Unicode);
            isZeroSingular = data.ReadBoolean();
            isOneSingular = data.ReadBoolean();
            isNegativeOneSingular = data.ReadBoolean();
            isTwoOrMoreSingular = data.ReadBoolean();
            isNegativeTwoOrLessSingular = data.ReadBoolean();
            treasurePrefixLetters = data.ReadString(Encoding.Unicode);
            treasureMiddleLetters = data.ReadString(Encoding.Unicode);
            treasureSuffixLetters = data.ReadString(Encoding.Unicode);
            malePlayerLetters = data.ReadString(Encoding.Unicode);
            femalePlayerLetters = data.ReadString(Encoding.Unicode);
            topGoreLevel = data.ReadUInt32();
            topBloodColor = data.ReadUInt32();
            imeEnabledSetting = data.ReadUInt32();
            symbolColor = data.ReadUInt32();
            symbolColorText = data.ReadUInt32();
            symbolHeight = data.ReadUInt32();
            symbolTranslucence = data.ReadUInt32();
            symbolPlacement = data.ReadUInt32();
            candColorBase = data.ReadUInt32();
            candColorBorder = data.ReadUInt32();
            candColorText = data.ReadUInt32();
            compColorInput = data.ReadUInt32();
            compColorTargetConv = data.ReadUInt32();
            compColorConverted = data.ReadUInt32();
            compColorTargetNotConv = data.ReadUInt32();
            compColorInputErr = data.ReadUInt32();
            compTranslucence = data.ReadUInt32();
            compColorText = data.ReadUInt32();
            otherIME = data.ReadUInt32();
            wordWrapOnSpace = data.ReadBoolean();
            additionalSettings = data.ReadString(Encoding.Unicode);
            additionalFlags = data.ReadUInt32();
        }
    }
}
