namespace AC2E.Def {

    public class MasterListMember : IPackage {

        public virtual PackageType packageType => PackageType.MasterListMember;

        public uint enumVal; // mEnum

        public MasterListMember(AC2Reader data) {
            enumVal = data.ReadUInt32();
        }
    }
}
