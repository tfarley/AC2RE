using System;

namespace AC2RE.Definitions;

public readonly struct InstanceId : IEquatable<InstanceId> {

    public static readonly InstanceId NULL = new(0);

    // Enum IIDUtils::IDType
    public enum IdType : uint {
        Unknown, // Unknown_IDType
        UI, // UI_IDType
        Camera, // Camera_IDType
        Temporary, // Temporary_IDType
        Player, // Player_IDType
        Static, // Static_IDType
        Dynamic, // Dynamic_IDType
    }

    private static readonly ulong TYPE_MASK = 0xFF00000000000000;

    // InstanceID
    public readonly ulong id; // id

    public InstanceId(ulong id) {
        this.id = id;
    }

    public InstanceId(IdType type, ulong index) {
        ulong typeOr = type switch {
            IdType.Dynamic => 0x8000000000000000,
            IdType.Static => 0x4000000000000000,
            IdType.Player => 0x2000000000000000,
            IdType.UI => 0x1100000000000000,
            IdType.Camera => 0x480000,
            IdType.Temporary => 0x1300000000000000,
            _ => 0,
        };
        id = (typeOr | index);
    }

    public IdType type {
        get {
            int type = (int)((id & TYPE_MASK) >> 56);
            if ((type & 0x80) != 0) {
                return IdType.Dynamic;
            } else if ((type & 0x40) != 0) {
                return IdType.Static;
            } else if ((type & 0x20) != 0) {
                return IdType.Player;
            } else if (type == 0x11) {
                return IdType.UI;
            } else if ((id & 0x3FC0000) == 0x480000) {
                return IdType.Camera;
            } else if (type == 0x13) {
                return IdType.Temporary;
            }
            return IdType.Unknown;
        }
    }

    public static bool operator ==(InstanceId lhs, InstanceId rhs) => lhs.id == rhs.id;
    public static bool operator !=(InstanceId lhs, InstanceId rhs) => lhs.id != rhs.id;
    public bool Equals(InstanceId other) => id == other.id;
    public override bool Equals(object obj) => obj is InstanceId castObj && id == castObj.id;
    public override int GetHashCode() => id.GetHashCode();

    public override string ToString() => $"0x{id:X16} ({type})";
}
