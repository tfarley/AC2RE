using System;

[Flags]
public enum SexType : uint {
    UNDEF = 0,
    MALE = 1 << 12,
    FEMALE = 1 << 13,
}
