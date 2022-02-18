using AC2RE.Definitions;
using System.Collections.Generic;

namespace AC2RE.DatTool;

internal class CellParse {

    public static byte[] generateHeightmap(DatReader cell1DatReader, int bitsPerPixel, out int imageWidth, out int imageHeight, out int stride) {
        int numBlocksX = 256;
        int numBlocksY = 256;
        int numHeightsX = 17;
        int numHeightsY = 17;
        imageWidth = numBlocksX * numHeightsX;
        imageHeight = numBlocksY * numHeightsY;
        stride = (imageWidth * bitsPerPixel + 7) / 8;
        byte[] pixels = new byte[stride * imageHeight];

        int bytesPerPixel = (bitsPerPixel + 7) / 8;

        for (int y = 0; y < numBlocksY; y++) {
            for (int x = 0; x < numBlocksX; x++) {
                DataId blockDid = new(new CellId((byte)x, (byte)y, 0xFF, 0xFF).id);
                if (cell1DatReader.contains(blockDid)) {
                    using (AC2Reader data = cell1DatReader.getFileReader(blockDid)) {
                        CLandBlockData landBlockData = new(data);
                        for (int j = 0; j < numHeightsY; j++) {
                            for (int i = 0; i < numHeightsX; i++) {
                                int dataIndex = (i * numHeightsX) + j;
                                byte height = landBlockData.heights[dataIndex];
                                for (int p = 0; p < bytesPerPixel; p++) {
                                    pixels[((((numBlocksY - 1 - y) * numHeightsY) + (numHeightsY - 1 - j)) * (numBlocksX * numHeightsX) + ((x * numHeightsX) + i)) * bytesPerPixel + p] = height;
                                }
                            }
                        }
                    }
                }
            }
        }

        return pixels;
    }

    public static void getMissingCells(DatReader datReader) {
        HashSet<ushort> seenLandblocks = new();

        foreach (DataId did in datReader.dids) {
            seenLandblocks.Add((ushort)((did.id >> 16) & 0xFFFF));
        }

        Logs.GENERAL.info("Parsed dat",
            "fileName", datReader.datFileName,
            "numLandblocks", seenLandblocks.Count);
    }
}
