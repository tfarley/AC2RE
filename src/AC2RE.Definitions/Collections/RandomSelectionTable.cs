using System.Collections.Generic;

namespace AC2RE.Definitions;

public class RandomSelectionTable : IHeapObject {

    public NativeType nativeType => NativeType.RandomSelectionTable_Int;

    public class ElementStruct {

        public uint obj; // m_object
        public uint weight; // m_weight
        public uint subTreeWeight; // m_subTreeWeight

        public ElementStruct() {

        }

        public ElementStruct(AC2Reader data) {
            obj = data.ReadUInt32();
            weight = data.ReadUInt32();
            subTreeWeight = data.ReadUInt32();
        }

        public void write(AC2Writer data) {
            data.Write(obj);
            data.Write(weight);
            data.Write(subTreeWeight);
        }
    }

    public List<ElementStruct> unusedElements; // m_unusedElements
    public List<ElementStruct> usedElements; // m_usedElements

    public RandomSelectionTable() {

    }

    public RandomSelectionTable(AC2Reader data) {
        unusedElements = data.ReadList(() => new ElementStruct(data));
        usedElements = data.ReadList(() => new ElementStruct(data));
    }

    public void write(AC2Writer data) {
        data.Write(unusedElements, v => v.write(data));
        data.Write(usedElements, v => v.write(data));
    }
}
