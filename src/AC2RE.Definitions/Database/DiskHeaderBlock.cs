namespace AC2RE.Definitions;

public class DiskHeaderBlock {

    // DiskHeaderBlock_t
    public byte[] acVersionString; // acVersionStr_
    public DiskTransactInfo transactInfo; // TransactInfo_
    public DiskFileInfo fileInfo; // FileInfo_

    public DiskHeaderBlock(AC2Reader data) {
        acVersionString = data.ReadBytes(256);
        transactInfo = new(data);
        fileInfo = new(data);
    }
}
