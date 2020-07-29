using System;

namespace AC2E.Def {

    public class StoryHookData {

        // Const - globals
        [Flags]
        public enum PackFlag : uint {
            NONE = 0,

            WEAPON_ID = 1 << 5, // 0x00000020

            ATTACK_RESULT = 1 << 7, // 0x00000080
            TARGET_ID = 1 << 8, // 0x00000100
            ANIM_HOOK_NUMBER = 1 << 9, // 0x00000200
            ATTACK_HOOK_NUMBER = 1 << 10, // 0x00000400
            TARGET_HEALTH = 1 << 11, // 0x00000800
            ATTACKER_HEALTH = 1 << 12, // 0x00001000
            TARGET_PKDAMAGE = 1 << 13, // 0x00002000
            ATTACKER_PKDAMAGE = 1 << 14, // 0x00004000
        }

        public PackFlag packFlags;
        public uint type; // m_type
        public InstanceId targetId; // m_target_id
        public InstanceId weaponId; // m_weaponID
        public uint attackResult; // m_attack_result
        public uint animHookNumber; // m_animHookNumber
        public uint attackHookNumber; // m_attackHookNumber
        public int targetHealthChange; // m_targetHealthChange
        public int attackerHealthChange; // m_attackerHealthChange
        public int targetPkDamageChange; // m_targetPKDamageChange
        public int attackerPkDamageChange; // m_attackerPKDamageChange

        public StoryHookData(AC2Reader data) {
            packFlags = (PackFlag)data.ReadUInt32();
            type = (uint)packFlags & 0xF;
            packFlags = (PackFlag)((uint)packFlags & ~0xF);
            if (packFlags.HasFlag(PackFlag.TARGET_ID)) {
                targetId = data.ReadInstanceId();
            }
            if (packFlags.HasFlag(PackFlag.WEAPON_ID)) {
                weaponId = data.ReadInstanceId();
            }
            if (packFlags.HasFlag(PackFlag.ATTACK_RESULT)) {
                attackResult = data.ReadUInt32();
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
    }
}
