using System;

namespace AC2RE.Definitions;

public readonly struct EffectId : IEquatable<EffectId> {

    public static readonly EffectId NULL = new(0);

    public readonly uint id;

    public EffectId(uint id) {
        this.id = id;
    }

    public static bool operator ==(EffectId lhs, EffectId rhs) => lhs.id == rhs.id;
    public static bool operator !=(EffectId lhs, EffectId rhs) => lhs.id != rhs.id;
    public bool Equals(EffectId other) => id == other.id;
    public override bool Equals(object obj) => obj is EffectId castObj && id == castObj.id;
    public override int GetHashCode() => id.GetHashCode();

    public override string ToString() => id.ToString();
}
