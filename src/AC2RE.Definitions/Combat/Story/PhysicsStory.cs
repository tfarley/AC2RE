﻿using System;
using System.Collections.Generic;

namespace AC2RE.Definitions {

    public class PhysicsStory {

        // Const - globals
        [Flags]
        public enum PackFlag : uint {
            NONE = 0,

            ATTACKER_ID = 1 << 1, // ATTACKER_ID 0x00000002
            ATTACKER_BVR = 1 << 2, // ATTACKER_BVR 0x00000004
            CLIENT_CONTEXT = 1 << 3, // CLIENT_CONTEXT 0x00000008
            SKILL_ID = 1 << 4, // SKILL_ID 0x00000010
            BASIC_ATTACK = 1 << 5, // BASIC_ATTACK 0x00000020
        }

        public PackFlag packFlags;
        public InstanceId attackerId; // m_attacker_id
        public BehaviorParams attackerBehavior; // m_attacker_behavior
        public uint clientAttackContextId; // m_client_attack_context_id
        public SkillId skillId; // m_skillID
        public bool basicAttack; // m_basicAttack
        public List<StoryHookData> hooks; // m_hooks

        public PhysicsStory(AC2Reader data) {
            packFlags = (PackFlag)data.ReadUInt32();
            if (packFlags.HasFlag(PackFlag.ATTACKER_ID)) {
                attackerId = data.ReadInstanceId();
            }
            if (packFlags.HasFlag(PackFlag.ATTACKER_BVR)) {
                attackerBehavior = new(data);
            }
            if (packFlags.HasFlag(PackFlag.CLIENT_CONTEXT)) {
                clientAttackContextId = data.ReadUInt32();
            }
            if (packFlags.HasFlag(PackFlag.SKILL_ID)) {
                skillId = (SkillId)data.ReadUInt32();
            }
            basicAttack = packFlags.HasFlag(PackFlag.BASIC_ATTACK);
            hooks = data.ReadList(() => new StoryHookData(data));
        }
    }
}
