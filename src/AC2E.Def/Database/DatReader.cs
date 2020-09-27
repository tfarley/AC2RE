using System;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Def {

    public class DatReader : IDisposable {

        public static readonly uint BLOCK_FREE_FLAG = 0x80000000;

        public readonly string datFileName;
        public readonly AC2Reader data;
        public readonly DiskHeaderBlock header;
        public IEnumerable<DataId> dids => didToEntry.Keys;
        private readonly BTree filesystemTree;
        private readonly Dictionary<DataId, BTEntry> didToEntry = new Dictionary<DataId, BTEntry>();

        public void Dispose() {
            data.Dispose();
        }

        public DatReader(string datFileName) {
            if (!File.Exists(datFileName)) {
                throw new FileNotFoundException("Dat file not found.", datFileName);
            }

            this.datFileName = datFileName;
            data = new AC2Reader(File.OpenRead(datFileName));
            init(ref header, ref filesystemTree);
        }

        public DatReader(AC2Reader data, string datFileName = null) {
            this.datFileName = datFileName;
            this.data = data;
            init(ref header, ref filesystemTree);
        }

        private void init(ref DiskHeaderBlock header, ref BTree filesystemTree) {
            header = new DiskHeaderBlock(data);
            filesystemTree = new BTree(this);
            foreach (BTNode node in filesystemTree.offsetToNode.Values) {
                foreach (BTEntry entry in node.entries) {
                    didToEntry.Add(entry.did, entry);
                }
            }
        }

        public bool contains(DataId did) {
            return didToEntry.ContainsKey(did);
        }

        public BTEntry getEntry(DataId did) {
            return didToEntry.GetValueOrDefault(did, null);
        }

        public AC2Reader getFileReader(DataId did) {
            return new AC2Reader(new MemoryStream(readFileBytes(did)));
        }

        public AC2Reader getFileReaderRaw(uint offset, int size) {
            return new AC2Reader(new MemoryStream(readFileBytesRaw(offset, size)));
        }

        public byte[] readFileBytes(DataId did) {
            BTEntry entry = didToEntry[did];
            return readFileBytesRaw(entry.offset, entry.size);
        }

        public byte[] readFileBytesRaw(uint offset, int size) {
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

        public void readFile(DataId did, Stream output, int prefixSkipSize = 0) {
            BTEntry entry = didToEntry[did];
            readFileRaw(entry.offset, entry.size, output, prefixSkipSize);
        }

        public void readFileRaw(uint offset, int size, Stream output, int prefixSkipSize = 0) {
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

        public List<Tuple<uint, int>> getFileBlocks(DataId did) {
            BTEntry entry = didToEntry[did];
            uint offset = entry.offset;
            int size = entry.size;

            List<Tuple<uint, int>> blocks = new List<Tuple<uint, int>>();

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
                blocks.Add(new Tuple<uint, int>(offset, sizeToRead));
                remainingSize -= sizeToRead;

                offset = nextBlockOffset;
            }

            return blocks;
        }

        public uint backtraceOffsetToOriginBlock(uint offset) {
            offset -= offset % header.fileInfo.blockSize;
            data.BaseStream.Seek(offset, SeekOrigin.Begin);
            while (offset > 0) {
                data.BaseStream.Seek(-header.fileInfo.blockSize, SeekOrigin.Current);
                uint prevBlockPointer = data.ReadUInt32();
                data.BaseStream.Seek(-4, SeekOrigin.Current);
                if (prevBlockPointer != offset && prevBlockPointer != (offset | BLOCK_FREE_FLAG)) {
                    break;
                }
                offset -= header.fileInfo.blockSize;
            }
            return offset;
        }
    }
}
