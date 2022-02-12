namespace AC2RE.Definitions;

public class InventProfile : IHeapObject {

    public PackageType packageType => PackageType.InventProfile;

    public VisualDescInfo visualDescInfo; // m_visualDescInfo
    public InvLoc slotsTaken; // m_slotsTaken
    public InvLoc location; // m_location
    public int it; // m_it
    public InstanceId id; // m_iid

    public InventProfile() {

    }

    public InventProfile(AC2Reader data) {
        data.ReadHO<VisualDescInfo>(v => visualDescInfo = v);
        slotsTaken = data.ReadEnum<InvLoc>();
        location = data.ReadEnum<InvLoc>();
        it = data.ReadInt32();
        id = data.ReadInstanceId();
    }

    public void write(AC2Writer data) {
        data.WriteHO(visualDescInfo);
        data.WriteEnum(slotsTaken);
        data.WriteEnum(location);
        data.Write(it);
        data.Write(id);
    }
}
