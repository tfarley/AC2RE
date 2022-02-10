namespace AC2RE.Definitions;

public class KeyUsageBlob : UsageBlob {

    public override PackageType packageType => PackageType.KeyUsageBlob;

    public StringInfo itemName; // m_siItemName
    public uint keyId; // m_uiKeyID

    public KeyUsageBlob() : base() {

    }

    public KeyUsageBlob(AC2Reader data) : base(data) {
        data.ReadHO<StringInfo>(v => itemName = v);
        keyId = data.ReadUInt32();
    }

    public override void write(AC2Writer data) {
        base.write(data);
        data.WriteHO(itemName);
        data.Write(keyId);
    }
}
