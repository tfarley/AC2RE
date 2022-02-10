namespace AC2RE.Definitions;

public class AnimationRecipeAction : IHeapObject {

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
