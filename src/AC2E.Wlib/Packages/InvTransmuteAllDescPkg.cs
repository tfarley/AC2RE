using AC2E.Def;
using System.IO;

namespace AC2E.WLib {

    public class InvTransmuteAllDescPkg : IPackage {

        public PackageType packageType => PackageType.InvTransmuteAllDesc;

        public uint m_lastError;
        public bool bIgnoreAttunement;
        public LList m_itemsTransmuted;
        public bool checkTakePermFlag;
        public uint m_moneyEarned;
        public bool m_bQuiet;
        public bool noAnimFlag;
        public uint m_status;
        public InstanceId m_fromContainerID;
        public LList m_itemsNotTransmuted;
        public bool playedAnim;
        public bool noMoveFlag;
        public InstanceId m_targetPlayerID;

        public InvTransmuteAllDescPkg() {

        }

        public InvTransmuteAllDescPkg(BinaryReader data, PackageRegistry registry) {
            m_lastError = data.ReadUInt32();
            bIgnoreAttunement = data.ReadUInt32() != 0;
            data.ReadPkgRef<LList>(v => m_itemsTransmuted = v, registry);
            checkTakePermFlag = data.ReadUInt32() != 0;
            m_moneyEarned = data.ReadUInt32();
            m_bQuiet = data.ReadUInt32() != 0;
            noAnimFlag = data.ReadUInt32() != 0;
            m_status = data.ReadUInt32();
            m_fromContainerID = data.ReadInstanceId();
            data.ReadPkgRef<LList>(v => m_itemsNotTransmuted = v, registry);
            playedAnim = data.ReadUInt32() != 0;
            noMoveFlag = data.ReadUInt32() != 0;
            m_targetPlayerID = data.ReadInstanceId();
        }

        public void write(BinaryWriter data, PackageRegistry registry) {
            data.Write(m_lastError);
            data.Write(bIgnoreAttunement ? (uint)1 : (uint)0);
            data.Write(m_itemsTransmuted, registry);
            data.Write(checkTakePermFlag ? (uint)1 : (uint)0);
            data.Write(m_moneyEarned);
            data.Write(m_bQuiet ? (uint)1 : (uint)0);
            data.Write(noAnimFlag ? (uint)1 : (uint)0);
            data.Write(m_status);
            data.Write(m_fromContainerID);
            data.Write(m_itemsNotTransmuted, registry);
            data.Write(playedAnim ? (uint)1 : (uint)0);
            data.Write(noMoveFlag ? (uint)1 : (uint)0);
            data.Write(m_targetPlayerID);
        }
    }
}
