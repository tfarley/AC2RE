namespace AC2RE.Definitions {

    public class Consignment : IPackage {

        public PackageType packageType => PackageType.Consignment;

        public PlayerSaleProfile saleProfile; // m_profile
        public InstanceId ownerId; // m_iidOwner
        public uint saleId; // m_saleID
        public int quantityOffered; // m_quantityOffered
        public int quantitySold; // m_quantitySold
        public double enteredTime; // m_ttTimeEntered
        public uint flags; // m_uiFlags

        public Consignment() {

        }

        public Consignment(AC2Reader data) {
            data.ReadPkg<PlayerSaleProfile>(v => saleProfile = v);
            ownerId = data.ReadInstanceId();
            saleId = data.ReadUInt32();
            quantityOffered = data.ReadInt32();
            quantitySold = data.ReadInt32();
            enteredTime = data.ReadDouble();
            flags = data.ReadUInt32();
        }

        public void write(AC2Writer data) {
            data.WritePkg(saleProfile);
            data.Write(ownerId);
            data.Write(saleId);
            data.Write(quantityOffered);
            data.Write(quantitySold);
            data.Write(enteredTime);
            data.Write(flags);
        }
    }
}
