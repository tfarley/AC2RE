namespace AC2E.Def {

    public class PetGenesisInfo : IPackage {

        public PackageType packageType => PackageType.PetGenesisInfo;

        public LList pets; // m_pets
        public ALHash relevantPerks; // m_relevantPerks
        public InstanceId leaderId; // m_iidLeader
        public uint flags; // m_flags
        public IPackage status; // m_status
        public InstanceId petId; // m_iidPet

        public PetGenesisInfo(AC2Reader data) {
            data.ReadPkg<LList>(v => pets = v);
            data.ReadPkg<ALHash>(v => relevantPerks = v);
            leaderId = data.ReadInstanceId();
            flags = data.ReadUInt32();
            data.ReadPkg<IPackage>(v => status = v); // TODO: PKStatus
            petId = data.ReadInstanceId();
        }
    }
}
