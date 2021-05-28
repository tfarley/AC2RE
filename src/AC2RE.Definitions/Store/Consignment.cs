using System;

namespace AC2RE.Definitions {

    public class Consignment : IPackage {

        public PackageType packageType => PackageType.Consignment;

        // WLib
        [Flags]
        public enum Flag : uint {
            NONE = 0,
            ALL = uint.MaxValue,

            EXPIRED = 1 << 0, // 0x00000001, Consignment::IsExpired
            REMOVED = 1 << 1, // 0x00000002, Consignment::IsRemoved
            DIRTY = 1 << 2, // 0x00000004, Consignment::IsDirty
        }

        public PlayerSaleProfile saleProfile; // m_profile
        public InstanceId ownerId; // m_iidOwner
        public uint saleId; // m_saleID
        public int quantityOffered; // m_quantityOffered
        public int quantitySold; // m_quantitySold
        public double enteredTime; // m_ttTimeEntered
        public Flag flags; // m_uiFlags

        public Consignment() {

        }

        public Consignment(AC2Reader data) {
            data.ReadPkg<PlayerSaleProfile>(v => saleProfile = v);
            ownerId = data.ReadInstanceId();
            saleId = data.ReadUInt32();
            quantityOffered = data.ReadInt32();
            quantitySold = data.ReadInt32();
            enteredTime = data.ReadDouble();
            flags = (Flag)data.ReadUInt32();
        }

        public void write(AC2Writer data) {
            data.WritePkg(saleProfile);
            data.Write(ownerId);
            data.Write(saleId);
            data.Write(quantityOffered);
            data.Write(quantitySold);
            data.Write(enteredTime);
            data.Write((uint)flags);
        }
    }
}
