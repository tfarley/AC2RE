using System;

// DB table 23000008
[Flags]
public enum ClassType : uint {
    UNDEF = 0,
    WARRIOR = 1 << 0,
    ARCHER = 1 << 1,
    SHAMAN = 1 << 2,
    EXARCH = 1 << 3,
    FANATIC = 1 << 4,
}
