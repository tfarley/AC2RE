namespace AC2RE.Definitions;

public class BookUsageBlob : UsageBlob {

    public override PackageType packageType => PackageType.BookUsageBlob;

    public StringInfo bookSource; // m_siBookSource
    public bool controls; // m_bControls
    public DataId imageDid; // m_didImage

    public BookUsageBlob() : base() {

    }

    public BookUsageBlob(AC2Reader data) : base(data) {
        data.ReadHO<StringInfo>(v => bookSource = v);
        controls = data.ReadBoolean();
        imageDid = data.ReadDataId();
    }

    public override void write(AC2Writer data) {
        base.write(data);
        data.WriteHO(bookSource);
        data.Write(controls);
        data.Write(imageDid);
    }
}
