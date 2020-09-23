namespace AC2E.Def {

    public class InvEquipDesc : IPackage {

        public PackageType packageType => PackageType.InvEquipDesc;

        public InvLoc blockingItemLocation; // m_blockingItemLocation
        public EquipItemProfile equipProfile; // m_eip
        public uint quantity; // m_quantity
        public InstanceId itemId; // m_itemID
        public uint precludedSlots; // m_precludedSlots
        public uint targetContainerSlot; // m_targetContainerSlot
        public InvLoc location; // m_location
        public InstanceId equipperId; // m_equipperID
        public InstanceId containerId; // m_containerID
        public uint controlFlags; // m_controlFlags
        public uint status; // status
        public uint takenSlots; // m_takenSlots
        public InvLoc actualLocation; // m_actualLocation
        public InstanceId blockingItemId; // m_blockingItemID
        public uint containerSlot; // m_containerSlot

        public InvEquipDesc() {

        }

        public InvEquipDesc(AC2Reader data) {
            blockingItemLocation = (InvLoc)data.ReadUInt32();
            data.ReadPkg<EquipItemProfile>(v => equipProfile = v);
            quantity = data.ReadUInt32();
            itemId = data.ReadInstanceId();
            precludedSlots = data.ReadUInt32();
            targetContainerSlot = data.ReadUInt32();
            location = (InvLoc)data.ReadUInt32();
            equipperId = data.ReadInstanceId();
            containerId = data.ReadInstanceId();
            controlFlags = data.ReadUInt32();
            status = data.ReadUInt32();
            takenSlots = data.ReadUInt32();
            actualLocation = (InvLoc)data.ReadUInt32();
            blockingItemId = data.ReadInstanceId();
            containerSlot = data.ReadUInt32();
        }

        public void write(AC2Writer data) {
            data.Write((uint)blockingItemLocation);
            data.WritePkg(equipProfile);
            data.Write(quantity);
            data.Write(itemId);
            data.Write(precludedSlots);
            data.Write(targetContainerSlot);
            data.Write((uint)location);
            data.Write(equipperId);
            data.Write(containerId);
            data.Write(controlFlags);
            data.Write(status);
            data.Write(takenSlots);
            data.Write((uint)actualLocation);
            data.Write(blockingItemId);
            data.Write(containerSlot);
        }
    }
}
