namespace AC2RE.Definitions {

    public class MasterDIDListMember : IPackage {

        public virtual PackageType packageType => PackageType.MasterDIDListMember;

        public uint enumVal; // mEnum

        public MasterDIDListMember(AC2Reader data) {
            enumVal = data.ReadUInt32();
        }
    }
}
