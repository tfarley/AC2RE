﻿using System;

namespace AC2RE.Definitions;

public class InvEquipDesc : IHeapObject {

    public PackageType packageType => PackageType.InvEquipDesc;

    // WLib InvEquipDesc
    [Flags]
    public enum ControlFlag : uint {
        None = 0,
        IsQuiet = 1 << 0, // IsQuiet 0x00000001
        ShouldDoSwitchAnimMode = 1 << 1, // ShouldDoSwitchAnimMode 0x00000002
        ShouldLock = 1 << 2, // ShouldLock 0x00000004
        ShouldPutInContents = 1 << 3, // ShouldPutInContents 0x00000008
        ShouldCheckUsagePerm = 1 << 4, // ShouldCheckUsagePerm 0x00000010
        IsForcedUnequip = 1 << 5, // IsForcedUnequip 0x00000020
        EquipItemOnlyAtThisLocation = 1 << 6, // EquipItemOnlyAtThisLocation 0x00000040
        ShouldUnequipBlockingItems = 1 << 7, // ShouldUnequipBlockingItems 0x00000080
    }

    public InvLoc blockingItemLocation; // m_blockingItemLocation
    public EquipItemProfile equipProfile; // m_eip
    public uint quantity; // m_quantity
    public InstanceId itemId; // m_itemID
    public InvLoc precludedSlots; // m_precludedSlots
    public int targetContainerSlot; // m_targetContainerSlot
    public InvLoc location; // m_location
    public InstanceId equipperId; // m_equipperID
    public InstanceId containerId; // m_containerID
    public ControlFlag controlFlags; // m_controlFlags
    public ErrorType error; // status
    public InvLoc takenSlots; // m_takenSlots
    public InvLoc actualLocation; // m_actualLocation
    public InstanceId blockingItemId; // m_blockingItemID
    public int containerSlot; // m_containerSlot

    public InvEquipDesc() {

    }

    public InvEquipDesc(AC2Reader data) {
        blockingItemLocation = data.ReadEnum<InvLoc>();
        data.ReadHO<EquipItemProfile>(v => equipProfile = v);
        quantity = data.ReadUInt32();
        itemId = data.ReadInstanceId();
        precludedSlots = data.ReadEnum<InvLoc>();
        targetContainerSlot = data.ReadInt32();
        location = data.ReadEnum<InvLoc>();
        equipperId = data.ReadInstanceId();
        containerId = data.ReadInstanceId();
        controlFlags = data.ReadEnum<ControlFlag>();
        error = data.ReadEnum<ErrorType>();
        takenSlots = data.ReadEnum<InvLoc>();
        actualLocation = data.ReadEnum<InvLoc>();
        blockingItemId = data.ReadInstanceId();
        containerSlot = data.ReadInt32();
    }

    public void write(AC2Writer data) {
        data.WriteEnum(blockingItemLocation);
        data.WriteHO(equipProfile);
        data.Write(quantity);
        data.Write(itemId);
        data.WriteEnum(precludedSlots);
        data.Write(targetContainerSlot);
        data.WriteEnum(location);
        data.Write(equipperId);
        data.Write(containerId);
        data.WriteEnum(controlFlags);
        data.WriteEnum(error);
        data.WriteEnum(takenSlots);
        data.WriteEnum(actualLocation);
        data.Write(blockingItemId);
        data.Write(containerSlot);
    }
}
