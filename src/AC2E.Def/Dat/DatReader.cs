using System;
using System.IO;

namespace AC2E.Def {

    public class DatReader : IDisposable {

        private readonly AC2Reader data;
        public readonly DiskHeaderBlock header;

        public DatReader(AC2Reader data) {
            this.data = data;

            header = new DiskHeaderBlock(data);
        }

        public AC2Reader getFileReader(uint offset, int size) {
            return new AC2Reader(new MemoryStream(readFileBytes(offset, size)));
        }

        public byte[] readFileBytes(uint offset, int size) {
            byte[] fileData = new byte[size];

            int remainingSize = size;
            while (remainingSize > 0) {
                data.BaseStream.Seek(offset, SeekOrigin.Begin);

                uint nextBlock = data.ReadUInt32();

                int sizeToRead = (int)Math.Min(remainingSize, header.fileInfo.blockSize - 4);
                data.Read(fileData, size - remainingSize, sizeToRead);
                remainingSize -= sizeToRead;

                offset = nextBlock;
            }

            return fileData;
        }

        public void readFile(Stream output, uint offset, int size) {
            byte[] blockBuffer = new byte[header.fileInfo.blockSize];
            bool skippedDid = false;
            int remainingSize = size;
            while (remainingSize > 0) {
                data.BaseStream.Seek(offset, SeekOrigin.Begin);

                uint nextBlock = data.ReadUInt32();

                int sizeToRead = (int)Math.Min(remainingSize, header.fileInfo.blockSize - 4);
                if (!skippedDid) {
                    data.BaseStream.Seek(4, SeekOrigin.Current);
                    sizeToRead -= 4;
                    remainingSize -= 4;
                }
                data.Read(blockBuffer, 0, sizeToRead);
                output.Write(blockBuffer, 0, sizeToRead);
                remainingSize -= sizeToRead;

                offset = nextBlock;
            }
        }

        public void Dispose() {
            data.Dispose();
        }
    }
}
