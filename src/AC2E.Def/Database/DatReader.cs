using System;
using System.IO;

namespace AC2E.Def {

    public class DatReader : IDisposable {

        public static readonly uint BLOCK_FREE_FLAG = 0x80000000;

        public readonly AC2Reader data;
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

                int remainingBlockSize = (int)header.fileInfo.blockSize;

                uint nextBlockOffset = data.ReadUInt32();
                remainingBlockSize -= sizeof(uint);

                if ((nextBlockOffset & BLOCK_FREE_FLAG) != 0) {
                    throw new InvalidDataException($"Encountered free block in file at offset {offset:X8}.");
                }

                int sizeToRead = Math.Min(remainingSize, remainingBlockSize);
                data.Read(fileData, size - remainingSize, sizeToRead);
                remainingSize -= sizeToRead;

                offset = nextBlockOffset;
            }

            return fileData;
        }

        public void readFile(Stream output, uint offset, int size, int prefixSkipSize = 0) {
            byte[] blockBuffer = new byte[header.fileInfo.blockSize];
            int remainingPrefixSkipSize = prefixSkipSize;
            int remainingSize = size;
            while (remainingSize > 0) {
                data.BaseStream.Seek(offset, SeekOrigin.Begin);

                int remainingBlockSize = (int)header.fileInfo.blockSize;

                uint nextBlockOffset = data.ReadUInt32();
                remainingBlockSize -= sizeof(uint);

                if ((nextBlockOffset & BLOCK_FREE_FLAG) != 0) {
                    throw new InvalidDataException($"Encountered free block in file at offset {offset:X8}.");
                }

                // Skip things like DID, file length, etc.
                if (remainingPrefixSkipSize > 0) {
                    int sizeToSkip = Math.Min(remainingPrefixSkipSize, remainingBlockSize);
                    data.BaseStream.Seek(sizeToSkip, SeekOrigin.Current);
                    remainingBlockSize -= sizeToSkip;
                    remainingSize -= sizeToSkip;
                    remainingPrefixSkipSize -= sizeToSkip;
                }

                int sizeToRead = Math.Min(remainingSize, remainingBlockSize);
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
