public static class CryptoUtil {

    private static readonly byte[] CRYPTO_KEY = new byte[] {
        0x38, 0x92, 0x84, 0xE5,
        0x7D, 0x76, 0x2D, 0xEB,
        0x03, 0xDD, 0x0F, 0xFC,
        0xA6, 0xA6, 0x45, 0xCB,
        0xBB, 0xE6, 0x21, 0x7F,
        0xC8, 0xCD, 0xA6, 0xF5,
        0x12, 0xAD, 0xD9, 0x22,
        0xDE, 0xD4, 0xA3, 0xB2,
        0xB7, 0x4D, 0x83, 0x3B,
        0x1C, 0xC9, 0x06, 0xC4,
        0x5E, 0x30, 0x92, 0x83,
        0x99, 0x00, 0x9F, 0x0A,
        0x0C, 0xB9, 0x43, 0x09,
        0x3C, 0x95, 0x6D, 0xA6,
        0x1F, 0x9E, 0x92, 0x0D,
        0xD4, 0x9A, 0x2E, 0x0E,
    };

    public static uint calcChecksum(byte[] buffer, long offset, long size, bool includeSize) {
        return calcChecksum(buffer, (int)offset, (int)size, includeSize);
    }

    public static uint calcChecksum(byte[] buffer, int offset, int size, bool includeSize) {
        uint checksum = includeSize ? ((uint)size << 16) : 0;

        int shift = 0;
        for (int i = offset; i < offset + size; i++) {
            checksum += ((uint)buffer[i] << shift);
            shift += 8;
            if (shift > 24) {
                shift = 0;
            }
        }

        return checksum;
    }

    public static void encrypt(byte[] buffer, int offset, int size) {
        swapNibbles(buffer, offset, size);
        xorWithKey(buffer, offset, size, CRYPTO_KEY);
    }

    public static void decrypt(byte[] buffer, int offset, int size) {
        xorWithKey(buffer, offset, size, CRYPTO_KEY);
        swapNibbles(buffer, offset, size);
    }

    private static void swapNibbles(byte[] buffer, int offset, int size) {
        for (int i = offset; i < offset + size; i++) {
            buffer[i] = (byte)((buffer[i] >> 4) | (buffer[i] << 4));
        }
    }

    private static void xorWithKey(byte[] buffer, int offset, int size, byte[] key) {
        for (int i = offset; i < offset + size - (size % 4); i++) {
            buffer[i] = (byte)(buffer[i] ^ key[i % key.Length]);
        }
    }
}
