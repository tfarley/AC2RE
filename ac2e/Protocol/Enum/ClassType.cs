using System;

[Flags]
public enum ClassType : uint {
    UNDEF = 0,
    WARRIOR = 1 << 0,
    ARCHER = 1 << 1,
    SHAMAN = 1 << 2,
}
