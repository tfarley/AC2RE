﻿using AC2E.Utils;
using Serilog;
using System;
using System.Text;

namespace AC2E.Server {

    internal class StringParse {

        private static void printDecryptedString(byte[] bytes, Encoding encoding) {
            AC2Crypto.decrypt(bytes, 0, bytes.Length);
            Log.Information($"Str: {encoding.GetString(bytes)}");
        }

        private static void printEncryptedString(string str, Encoding encoding) {
            byte[] bytes = encoding.GetBytes(str);
            AC2Crypto.encrypt(bytes, 0, bytes.Length);
            Log.Information($"Str: {str}");
            Log.Information($"Enc: {BitConverter.ToString(bytes).Replace("-", "")}");
        }
    }
}
