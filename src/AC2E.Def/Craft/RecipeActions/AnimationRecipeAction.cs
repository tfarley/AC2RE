namespace AC2E.Def {

    public class AnimationRecipeAction : IPackage {

        public PackageType packageType => PackageType.AnimationRecipeAction;

        public uint anim; // m_anim
        public bool camera; // m_bCamera
        public uint cycles; // m_uiCycles

        public AnimationRecipeAction(AC2Reader data) {
            anim = data.ReadUInt32();
            camera = data.ReadBoolean();
            cycles = data.ReadUInt32();
        }
    }
}
