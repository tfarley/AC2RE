﻿namespace AC2E.Def {

    // Enum PixelFormat
    public enum PixelFormat : uint {
        UNKNOWN = 0,
        R8G8B8 = 20,
        A8R8G8B8 = 21,
        X8R8G8B8 = 22,
        R5G6B5 = 23,
        X1R5G5B5 = 24,
        A1R5G5B5 = 25,
        A4R4G4B4 = 26,
        R3G3B2 = 27,
        A8 = 28,
        A8R3G3B2 = 29,
        X4R4G4B4 = 30,
        A8P8 = 40,
        P8 = 41,
        L8 = 50,
        A8L8 = 51,
        A4L4 = 52,
        V8U8 = 60,
        L6V5U5 = 61,
        X8L8V8U8 = 62,
        Q8W8V8U8 = 63,
        V16U16 = 64,
        W11V11U10 = 65,
        UYVY = 0x59565955,
        YUY2 = 0x32595559,
        DXT1 = 0x31545844,
        DXT2 = 0x32545844,
        DXT3 = 0x33545844,
        DXT4 = 0x34545844,
        DXT5 = 0x35545844,
        D16_LOCKABLE = 70,
        D32 = 71,
        D15S1 = 73,
        D24S8 = 75,
        D16 = 80,
        D24X8 = 77,
        D24X4S4 = 79,
        VERTEXDATA = 100,
        INDEX16 = 101,
        INDEX32 = 102,
        CUSTOM_R8G8B8A8 = 240,
        CUSTOM_A8B8G8R8 = 241,
        CUSTOM_B8G8R8 = 242,
        CUSTOM_RAW_JPEG = 500,
        INVALID = 2147483647,
    }
}
