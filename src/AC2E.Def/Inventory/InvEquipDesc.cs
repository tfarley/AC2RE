using System.IO;

namespace AC2E.Def {

    public class InvEquipDesc : IPackage {

        public PackageType packageType => PackageType.InvEquipDesc;

        public uint m_blockingItemLocation;
        public EquipItemProfile m_eip;
        public uint m_quantity;
        public InstanceId m_itemID;
        public uint m_precludedSlots;
        public uint m_targetContainerSlot;
        public uint m_location;
        public InstanceId m_equipperID;
        public InstanceId m_containerID;
        public uint m_controlFlags;
        public uint status;
        public uint m_takenSlots;
        public uint m_actualLocation;
        public InstanceId m_blockingItemID;
        public uint m_containerSlot;

        public InvEquipDesc() {

        }

        public InvEquipDesc(BinaryReader data, PackageRegistry registry) {
            m_blockingItemLocation = data.ReadUInt32();
            data.ReadPkgRef<EquipItemProfile>(v => m_eip = v, registry);
            m_quantity = data.ReadUInt32();
            m_itemID = data.ReadInstanceId();
            m_precludedSlots = data.ReadUInt32();
            m_targetContainerSlot = data.ReadUInt32();
            m_location = data.ReadUInt32();
            m_equipperID = data.ReadInstanceId();
            m_containerID = data.ReadInstanceId();
            m_controlFlags = data.ReadUInt32();
            status = data.ReadUInt32();
            m_takenSlots = data.ReadUInt32();
            m_actualLocation = data.ReadUInt32();
            m_blockingItemID = data.ReadInstanceId();
            m_containerSlot = data.ReadUInt32();
        }

        public void write(BinaryWriter data, PackageRegistry registry) {
            data.Write(m_blockingItemLocation);
            data.Write(m_eip, registry);
            data.Write(m_quantity);
            data.Write(m_itemID);
            data.Write(m_precludedSlots);
            data.Write(m_targetContainerSlot);
            data.Write(m_location);
            data.Write(m_equipperID);
            data.Write(m_containerID);
            data.Write(m_controlFlags);
            data.Write(status);
            data.Write(m_takenSlots);
            data.Write(m_actualLocation);
            data.Write(m_blockingItemID);
            data.Write(m_containerSlot);
        }
    }
}
