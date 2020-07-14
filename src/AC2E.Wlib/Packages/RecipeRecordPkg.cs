using AC2E.Dat;
using AC2E.Interp;
using System.Collections.Generic;
using System.IO;

namespace AC2E.WLib {

    public class RecipeRecordPkg : IPackage {

        public PackageType packageType => PackageType.RecipeRecord;

        public uint m_recipe;
        public double m_ttLastUsed;
        public int m_iSuccesses;
        public double m_ttLastClientUpdate;
        public int m_iCharges;
        public DataId m_didRecipe;
        public double m_nextRefreshGame;
        public int m_iLastSpinnerValue;
        public int m_nextRefreshReal;

        public RecipeRecordPkg() {

        }

        public RecipeRecordPkg(BinaryReader data) {
            m_recipe = data.ReadUInt32();
            m_ttLastUsed = data.ReadDouble();
            m_iSuccesses = data.ReadInt32();
            m_ttLastClientUpdate = data.ReadDouble();
            m_iCharges = data.ReadInt32();
            m_didRecipe = data.ReadDataId();
            m_nextRefreshGame = data.ReadDouble();
            m_iLastSpinnerValue = data.ReadInt32();
            m_nextRefreshReal = data.ReadInt32();
        }

        public void write(BinaryWriter data, List<IPackage> references) {
            data.Write(m_recipe);
            data.Write(m_ttLastUsed);
            data.Write(m_iSuccesses);
            data.Write(m_ttLastClientUpdate);
            data.Write(m_iCharges);
            data.Write(m_didRecipe);
            data.Write(m_nextRefreshGame);
            data.Write(m_iLastSpinnerValue);
            data.Write(m_nextRefreshReal);
        }
    }
}
