using System.Collections.Generic;

namespace AC2E.Def {

    public class VertexFormatInfo {

        private static readonly Dictionary<uint, uint> ENCODED_NUM_WEIGHTS = new Dictionary<uint, uint> {
            { 6, 1 },
            { 8, 2 },
            { 10, 3 },
            { 12, 4 },
            { 14, 5 },
        };

        private static readonly Dictionary<uint, uint> ENCODED_NUM_TEXCOORD_PAIRS = new Dictionary<uint, uint> {
            { 256, 1 },
            { 512, 2 },
            { 768, 3 },
            { 1024, 4 },
            { 1280, 5 },
            { 1536, 5 },
            { 1792, 5 },
            { 2048, 5 },
        };

        public uint format; // format
        public bool hasOrigin;
        public uint numWeights; // numWeights
        public uint numTexCoordPairs; // numTCPairs
        public uint numMatrices; // numMatrices
        public uint offsetOrigin; // offsetOrigin
        public uint[] offsetWeights; // offsetWeight0 + offsetWeight1 + offsetWeight2 + offsetWeight3 + offsetWeight4
        public uint offsetNormal; // offsetNormal
        public uint offsetPointSize; // offsetPointSize
        public uint offsetDiffuseColor; // offsetDiffuse
        public uint offsetSpecularColor; // offsetSpecular
        public uint offsetVectorS; // offsetVectorS
        public uint offsetVectorT; // offsetVectorT
        public uint offsetVectorPtToLight; // offsetVectorPtToLight
        public uint[] offsetTexCoordPairs; // offsetTCPair
        public uint offsetMatrices; // offsetMatrices
        public uint offsetMatrixWeights; // offsetMWeights
        public uint vertexSize;

        public VertexFormatInfo(uint format) {
            this.format = format;

            uint curOffset = 0;

            hasOrigin = (format & 2) != 0;
            if (hasOrigin) {
                offsetOrigin = 0;
                curOffset += 3 * 4;
            }

            numWeights = ENCODED_NUM_WEIGHTS.GetValueOrDefault((format & 0xE), (uint)0);
            offsetWeights = new uint[numWeights];
            for (int i = 0; i < numWeights; i++) {
                offsetWeights[i] = curOffset;
                curOffset += 1 * 4;
            }

            if ((format & 0x10) != 0) {
                offsetNormal = curOffset;
                curOffset += 3 * 4;
            }

            if ((format & 0x20) != 0) {
                offsetPointSize = curOffset;
                curOffset += 1 * 4;
            }

            if ((format & 0x40) != 0) {
                offsetDiffuseColor = curOffset;
                curOffset += 4 * 1;
            }

            if ((format & 0x80) != 0) {
                offsetSpecularColor = curOffset;
                curOffset += 4 * 1;
            }

            if ((format & 0x10000000) != 0) {
                offsetVectorS = curOffset;
                curOffset += 3 * 4;
            }

            if ((format & 0x20000000) != 0) {
                offsetVectorT = curOffset;
                curOffset += 3 * 4;
            }

            if ((format & 0x40000000) != 0) {
                offsetVectorPtToLight = curOffset;
                curOffset += 3 * 4;
            }

            numTexCoordPairs = ENCODED_NUM_TEXCOORD_PAIRS.GetValueOrDefault((format & 0xF00), (uint)0);
            offsetTexCoordPairs = new uint[numTexCoordPairs];
            for (int i = 0; i < numTexCoordPairs; i++) {
                offsetTexCoordPairs[i] = curOffset;
                curOffset += 2 * 4;
            }

            numMatrices = ((format >> 16) & 0xFF);
            offsetMatrices = curOffset;
            curOffset += numMatrices * 1;

            offsetMatrixWeights = curOffset;
            curOffset += numMatrices * 4;

            vertexSize = curOffset;
        }
    }
}
