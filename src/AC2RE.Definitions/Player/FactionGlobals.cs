namespace AC2RE.Definitions {

    public class FactionGlobals : IPackage {

        public PackageType packageType => PackageType.FactionGlobals;

        public uint unk1;

        public FactionGlobals(AC2Reader data) {
            unk1 = data.ReadUInt32();
        }
    }
}
