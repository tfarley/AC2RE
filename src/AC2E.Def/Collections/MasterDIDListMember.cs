namespace AC2E.Def {

    public class MasterDIDListMember : IPackage {

        public virtual PackageType packageType => PackageType.MasterDIDListMember;

        public uint enumVal; // mEnum

        public MasterDIDListMember(AC2Reader data) {
            enumVal = data.ReadUInt32();
        }
    }
}
