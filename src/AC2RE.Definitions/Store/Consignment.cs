using System;

namespace AC2RE.Definitions {

    public class Consignment : IPackage {

        public PackageType packageType => PackageType.Consignment;

        // WLib Consignment
        [Flags]
        public enum Flag : uint {
            None = 0,
            IsExpired = 1 << 0, // IsExpired 0x00000001
            IsRemoved = 1 << 1, // IsRemoved 0x00000002
            IsDirty = 1 << 2, // IsDirty 0x00000004
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
