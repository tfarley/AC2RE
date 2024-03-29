﻿namespace AC2RE.Definitions;

public class ItemProfile : IHeapObject {

    public PackageType packageType => PackageType.ItemProfile;

    public InstanceId itemId; // itemID
    public InstanceId containerId; // containerID
    public uint slot; // slot

    public ItemProfile(AC2Reader data) {
        itemId = data.ReadInstanceId();
        containerId = data.ReadInstanceId();
        slot = data.ReadUInt32();
    }
}
