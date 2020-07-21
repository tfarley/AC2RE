namespace AC2E.Def {

    public class DiskHeaderBlock {

        public byte[] acVersionString; // acVersionStr_
        public DiskTransactInfo transactInfo; // TransactInfo_
        public DiskFileInfo fileInfo; // FileInfo_

        public DiskHeaderBlock(AC2Reader data) {
            acVersionString = data.ReadBytes(256);
            transactInfo = new DiskTransactInfo(data);
            fileInfo = new DiskFileInfo(data);
        }
    }
}
