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

    // InstanceID
    public readonly ulong id; // id

    public InstanceId(ulong id) {
        this.id = id;
    }

    public static bool operator ==(InstanceId lhs, InstanceId rhs) => lhs.id == rhs.id;
    public static bool operator !=(InstanceId lhs, InstanceId rhs) => lhs.id != rhs.id;
    public bool Equals(InstanceId other) => id == other.id;
    public override bool Equals(object obj) => obj is InstanceId castObj && id == castObj.id;
    public override int GetHashCode() => id.GetHashCode();

    public override string ToString() => $"0x{id:X16}";
}
