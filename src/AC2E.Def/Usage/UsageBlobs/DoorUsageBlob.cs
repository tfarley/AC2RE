namespace AC2E.Def {

    public class DoorUsageBlob : UsageBlob {

        public override PackageType packageType => PackageType.DoorUsageBlob;

        public bool isAI; // m_bIsAI
        public bool canUseDoors; // m_bCanUseDoors
        public Position userPos; // m_userPos

        public DoorUsageBlob() : base() {

        }

        public DoorUsageBlob(AC2Reader data) : base(data) {
            isAI = data.ReadBoolean();
            canUseDoors = data.ReadBoolean();
            data.ReadPkg<Position>(v => userPos = v);
        }

        public override void write(AC2Writer data) {
            base.write(data);
            data.Write(isAI);
            data.Write(canUseDoors);
            data.WritePkg(userPos);
        }
    }
}
