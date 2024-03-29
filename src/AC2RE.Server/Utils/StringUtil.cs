﻿using AC2RE.Definitions;
using System;
using System.Text;

namespace AC2RE.Server;

internal class StringUtil {

    public static string removeMetaTags(string str) {
        int metaStartIndex = str.IndexOf(" [");
        return metaStartIndex >= 0 ? str[..metaStartIndex] : str;
    }

    public static void printDecryptedString(byte[] bytes, Encoding encoding) {
        AC2Crypto.decrypt(bytes, 0, bytes.Length);
        Logs.GENERAL.info($"Str: {encoding.GetString(bytes)}");
    }

    public static void printEncryptedString(string str, Encoding encoding) {
        byte[] bytes = encoding.GetBytes(str);
        AC2Crypto.encrypt(bytes, 0, bytes.Length);
        Logs.GENERAL.info($"Str: {str}");
        Logs.GENERAL.info($"Enc: {BitConverter.ToString(bytes).Replace("-", "")}");
    }
}
