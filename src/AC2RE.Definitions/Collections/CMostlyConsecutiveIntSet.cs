namespace AC2RE.Definitions;

public class CMostlyConsecutiveIntSet {

    public int[] ints; // m_Ints

    public CMostlyConsecutiveIntSet(AC2Reader data) {
        uint numInts = data.ReadUInt32();
        ints = new int[numInts];
        uint curInt = 0;
        if (numInts > 0) {
            while (curInt < numInts) {
                uint consecutiveFlagAndVal = data.ReadUInt32();
                bool isConsecutive = (consecutiveFlagAndVal & 0x80000000) != 0;
                if (isConsecutive) {
                    int numConsecutive = -(int)consecutiveFlagAndVal;
                    int startInt = data.ReadInt32();
                    for (int i = 0; i < numConsecutive; i++) {
                        ints[curInt] = startInt + i;
                        curInt++;
                    }
                } else {
                    bool isNegative = (consecutiveFlagAndVal & 0x40000000) != 0;
                    if (isNegative) {
                        consecutiveFlagAndVal |= 0x80000000;
                    }
                    ints[curInt] = (int)consecutiveFlagAndVal;
                    curInt++;
                }
            }
        }
    }
}
