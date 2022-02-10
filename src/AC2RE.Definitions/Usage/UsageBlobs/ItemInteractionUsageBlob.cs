namespace AC2RE.Definitions;

public class ItemInteractionUsageBlob : UsageBlob {

    public override PackageType packageType => PackageType.ItemInteractionUsageBlob;

    public StringInfo itemName; // m_siItemName
    public TargetInteraction interaction; // m_interaction

    public ItemInteractionUsageBlob() : base() {

    }

    public ItemInteractionUsageBlob(AC2Reader data) : base(data) {
        data.ReadHO<StringInfo>(v => itemName = v);
        data.ReadHO<TargetInteraction>(v => interaction = v);
    }

    public override void write(AC2Writer data) {
        base.write(data);
        data.WriteHO(itemName);
        data.WriteHO(interaction);
    }
}
