namespace AC2RE.Definitions;

public class GrantRecipeEffect : Effect {

    public override PackageType packageType => PackageType.GrantRecipeEffect;

    public DataId recipeDid; // m_didRecipe

    public GrantRecipeEffect(AC2Reader data) : base(data) {
        recipeDid = data.ReadDataId();
    }
}
