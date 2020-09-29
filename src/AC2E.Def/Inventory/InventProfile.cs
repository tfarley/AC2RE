namespace AC2E.Def {

    public class InventProfile : IPackage {

        public PackageType packageType => PackageType.InventProfile;

        public VisualDescInfo visualDescInfo; // m_visualDescInfo
        public InvLoc slotsTaken; // m_slotsTaken
        public InvLoc location; // m_location
        public int it; // m_it
        public InstanceId id; // m_iid

        public InventProfile() {

        }

        public InventProfile(AC2Reader data) {
            data.ReadPkg<VisualDescInfo>(v => visualDescInfo = v);
            slotsTaken = (InvLoc)data.ReadUInt32();
            location = (InvLoc)data.ReadUInt32();
            it = data.ReadInt32();
            id = data.ReadInstanceId();
        }

        public void write(AC2Writer data) {
            data.WritePkg(visualDescInfo);
            data.Write((uint)slotsTaken);
            data.Write((uint)location);
            data.Write(it);
            data.Write(id);
        }
    }
}
