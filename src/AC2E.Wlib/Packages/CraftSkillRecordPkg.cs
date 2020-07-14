using AC2E.Dat;
using AC2E.Interp;
using System.Collections.Generic;
using System.IO;

namespace AC2E.WLib {

    public class CraftSkillRecordPkg : IPackage {

        public PackageType packageType => PackageType.CraftSkillRecord;

        public ulong m_uliXPEarnedToday;
        public DataId m_didCraftSkill;
        public ulong m_uliAvailableCraftXP;
        public int m_iLevel;

        public CraftSkillRecordPkg() {

        }

        public CraftSkillRecordPkg(BinaryReader data) {
            m_uliXPEarnedToday = data.ReadUInt64();
            m_didCraftSkill = data.ReadDataId();
            m_uliAvailableCraftXP = data.ReadUInt64();
            m_iLevel = data.ReadInt32();
        }

        public void write(BinaryWriter data, List<IPackage> references) {
            data.Write(m_uliXPEarnedToday);
            data.Write(m_didCraftSkill);
            data.Write(m_uliAvailableCraftXP);
            data.Write(m_iLevel);
        }
    }
}
