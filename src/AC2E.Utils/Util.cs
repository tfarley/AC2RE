using System.Text.RegularExpressions;

namespace AC2E.Utils {

    public static class Util {

        public static byte[] hexStringToBytes(string hexStr) {
            hexStr = new Regex(@"[^0-9a-fA-F]|0x").Replace(hexStr, "");

            byte[] bytes = new byte[hexStr.Length / 2];

            for (int i = 0, byteIndex = 0; i < hexStr.Length; i += 2, byteIndex++) {
                byte highNibble = (byte)hexStr[i];
                byte lowNibble = (byte)hexStr[i + 1];
                bytes[byteIndex] = (byte)(((highNibble - (highNibble < 58 ? 48 : (highNibble < 97 ? 55 : 87))) << 4)
                    + (lowNibble - (lowNibble < 58 ? 48 : (lowNibble < 97 ? 55 : 87))));
            }

            return bytes;
        }

        public static ushort byteSwapped(ushort value) {
            return (ushort)(((value & 0x00FFU) << 8) | ((value & 0xFF00U) >> 8));
        }
    }
}
