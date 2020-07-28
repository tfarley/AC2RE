using System;
using System.IO;

namespace AC2E.Def {

    public class DatReader : IDisposable {

        public static readonly uint BLOCK_FREE_FLAG = 0x80000000;

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

                uint nextBlockOffset = data.ReadUInt32();

                if ((nextBlockOffset & BLOCK_FREE_FLAG) != 0) {
                    throw new InvalidDataException($"Encountered free block in file at offset {offset:X8}.");
                }

                int sizeToRead = (int)Math.Min(remainingSize, header.fileInfo.blockSize - 4);
                data.Read(fileData, size - remainingSize, sizeToRead);
                remainingSize -= sizeToRead;

                offset = nextBlockOffset;
            }

            return fileData;
        }

        public void readFile(Stream output, uint offset, int size) {
            byte[] blockBuffer = new byte[header.fileInfo.blockSize];
            bool skippedDid = false;
            int remainingSize = size;
            while (remainingSize > 0) {
                data.BaseStream.Seek(offset, SeekOrigin.Begin);

                uint nextBlockOffset = data.ReadUInt32();

                if ((nextBlockOffset & BLOCK_FREE_FLAG) != 0) {
                    throw new InvalidDataException($"Encountered free block in file at offset {offset:X8}.");
                }

                int sizeToRead = (int)Math.Min(remainingSize, header.fileInfo.blockSize - 4);
                if (!skippedDid) {
                    data.BaseStream.Seek(4, SeekOrigin.Current);
                    sizeToRead -= 4;
                    remainingSize -= 4;
                }
                data.Read(blockBuffer, 0, sizeToRead);
                output.Write(blockBuffer, 0, sizeToRead);
                remainingSize -= sizeToRead;

                offset = nextBlockOffset;
            }
        }

        public void Dispose() {
            data.Dispose();
        }
    }
}
