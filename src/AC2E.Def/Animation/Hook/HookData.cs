namespace AC2E.Def {

    public class HookData : IPackage {

        public virtual PackageType packageType => PackageType.HookData;

        public uint hookType; // mHookType

        public HookData(AC2Reader data) {
            hookType = data.ReadUInt32();
        }
    }
}
