using System.Collections.Generic;

namespace AC2RE.Definitions;

public class PetGenesisInfo : IHeapObject {

    public PackageType packageType => PackageType.PetGenesisInfo;

    public List<ulong> pets; // m_pets
    public Dictionary<uint, ulong> relevantPerks; // m_relevantPerks
    public InstanceId leaderId; // m_iidLeader
    public uint flags; // m_flags
    public PKStatus status; // m_status
    public InstanceId petId; // m_iidPet

    public PetGenesisInfo(AC2Reader data) {
        data.ReadHO<LList>(v => pets = v);
        data.ReadHO<ALHash>(v => relevantPerks = v);
        leaderId = data.ReadInstanceId();
        flags = data.ReadUInt32();
        data.ReadHO<PKStatus>(v => status = v);
        petId = data.ReadInstanceId();
    }
}
