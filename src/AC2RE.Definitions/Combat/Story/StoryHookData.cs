using System;

namespace AC2RE.Definitions;

public class StoryHookData {

    // Const - globals
    public enum HookType : uint {
        Undef,
        Damage, // Damage
        CreateMissiles, // CreateMissiles
        FireMissile, // FireMissile
    }

    // Const - globals
    [Flags]
    public enum PackFlag : uint {
        NONE = 0,

        WEAPON_ID = 1 << 5, // WEAPON_ID 0x00000020

        ATTACK_RESULT = 1 << 7, // ATTACK_RESULT 0x00000080
        TARGET_ID = 1 << 8, // TARGET_ID 0x00000100
        ANIM_HOOK_NUMBER = 1 << 9, // ANIM_HOOK_NUMBER 0x00000200
        ATTACK_HOOK_NUMBER = 1 << 10, // ATTACK_HOOK_NUMBER 0x00000400
        TARGET_HEALTH = 1 << 11, // TARGET_HEALTH 0x00000800
        ATTACKER_HEALTH = 1 << 12, // ATTACKER_HEALTH 0x00001000
        TARGET_PKDAMAGE = 1 << 13, // TARGET_PKDAMAGE 0x00002000
        ATTACKER_PKDAMAGE = 1 << 14, // ATTACKER_PKDAMAGE 0x00004000
    }

    // StoryHookData
    public PackFlag packFlags;
    public HookType type; // m_type
    public InstanceId targetId; // m_target_id
    public InstanceId weaponId; // m_weaponID
    public CombatResultType attackResult; // m_attack_result
    public uint animHookNumber; // m_animHookNumber
    public uint attackHookNumber; // m_attackHookNumber
    public int targetHealthChange; // m_targetHealthChange
    public int attackerHealthChange; // m_attackerHealthChange
    public int targetPkDamageChange; // m_targetPKDamageChange
    public int attackerPkDamageChange; // m_attackerPKDamageChange

    public StoryHookData() {

    }

    public StoryHookData(AC2Reader data) {
        packFlags = data.ReadEnum<PackFlag>();
        type = (HookType)((uint)packFlags & 0xF);
        packFlags = (PackFlag)((uint)packFlags & ~0xF);
        if (packFlags.HasFlag(PackFlag.TARGET_ID)) {
            targetId = data.ReadInstanceId();
        }
        if (packFlags.HasFlag(PackFlag.WEAPON_ID)) {
            weaponId = data.ReadInstanceId();
        }
        if (packFlags.HasFlag(PackFlag.ATTACK_RESULT)) {
            attackResult = data.ReadEnum<CombatResultType>();
        }
        if (packFlags.HasFlag(PackFlag.ANIM_HOOK_NUMBER)) {
            animHookNumber = data.ReadUInt32();
        }
        if (packFlags.HasFlag(PackFlag.ATTACK_HOOK_NUMBER)) {
            attackHookNumber = data.ReadUInt32();
        }
        if (packFlags.HasFlag(PackFlag.TARGET_HEALTH)) {
            targetHealthChange = data.ReadInt32();
        }
        if (packFlags.HasFlag(PackFlag.ATTACKER_HEALTH)) {
            attackerHealthChange = data.ReadInt32();
        }
        if (packFlags.HasFlag(PackFlag.TARGET_PKDAMAGE)) {
            targetPkDamageChange = data.ReadInt32();
        }
        if (packFlags.HasFlag(PackFlag.ATTACKER_PKDAMAGE)) {
            attackerPkDamageChange = data.ReadInt32();
        }
    }

    public void write(AC2Writer data) {
        packFlags = 0;
        if (targetId != default) packFlags |= PackFlag.TARGET_ID;
        if (weaponId != default) packFlags |= PackFlag.WEAPON_ID;
        if (attackResult != default) packFlags |= PackFlag.ATTACK_RESULT;
        if (animHookNumber != default) packFlags |= PackFlag.ANIM_HOOK_NUMBER;
        if (attackHookNumber != default) packFlags |= PackFlag.ATTACK_HOOK_NUMBER;
        if (targetHealthChange != default) packFlags |= PackFlag.TARGET_HEALTH;
        if (attackerHealthChange != default) packFlags |= PackFlag.ATTACKER_HEALTH;
        if (targetPkDamageChange != default) packFlags |= PackFlag.TARGET_PKDAMAGE;
        if (attackerPkDamageChange != default) packFlags |= PackFlag.ATTACKER_PKDAMAGE;

        data.WriteEnum((PackFlag)((uint)packFlags | (uint)type));
        if (packFlags.HasFlag(PackFlag.TARGET_ID)) {
            data.Write(targetId);
        }
        if (packFlags.HasFlag(PackFlag.WEAPON_ID)) {
            data.Write(weaponId);
        }
        if (packFlags.HasFlag(PackFlag.ATTACK_RESULT)) {
            data.WriteEnum(attackResult);
        }
        if (packFlags.HasFlag(PackFlag.ANIM_HOOK_NUMBER)) {
            data.Write(animHookNumber);
        }
        if (packFlags.HasFlag(PackFlag.ATTACK_HOOK_NUMBER)) {
            data.Write(attackHookNumber);
        }
        if (packFlags.HasFlag(PackFlag.TARGET_HEALTH)) {
            data.Write(targetHealthChange);
        }
        if (packFlags.HasFlag(PackFlag.ATTACKER_HEALTH)) {
            data.Write(attackerHealthChange);
        }
        if (packFlags.HasFlag(PackFlag.TARGET_PKDAMAGE)) {
            data.Write(targetPkDamageChange);
        }
        if (packFlags.HasFlag(PackFlag.ATTACKER_PKDAMAGE)) {
            data.Write(attackerPkDamageChange);
        }
    }
}
