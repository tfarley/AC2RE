using System;

public static class RandomExtensions {

    public static uint NextUInt(this Random random) {
        var buffer = new byte[4];
        random.NextBytes(buffer);
        return BitConverter.ToUInt32(buffer);
    }

    public static ulong NextULong(this Random random) {
        var buffer = new byte[8];
        random.NextBytes(buffer);
        return BitConverter.ToUInt64(buffer);
    }
}
