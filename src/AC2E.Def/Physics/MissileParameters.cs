using System;

namespace AC2E.Def {

    public class MissileParameters : IPackage {

        public NativeType nativeType => NativeType.MISSILEPARAMETERS;

        // Enum MissileParameters::MissileParameterInfo
        [Flags]
        public enum PackFlag : uint {
            NONE = 0,
            SPEED_LIMITS = 1 << 0, // 0x00000001
            MAX_ACCEL = 1 << 1, // 0x00000002
            FUEL = 1 << 2, // 0x00000004
            LIFESPAN = 1 << 3, // 0x00000008
            DRAG = 1 << 4, // 0x00000010
            AFFECTED_GRAVITY = 1 << 5, // 0x00000020
            AFFECTED_WIND = 1 << 6, // 0x00000040
            FOLLOW_GROUND = 1 << 7, // 0x00000080

            MAINT_DIST = 1 << 9, // 0x00000200
            MISSILE_REACTION = 1 << 10, // 0x00000400
            TURN_METHOD = 1 << 11, // 0x00000800
            // NOTE: Duplicate - not a typo; probably a mistake in client code
            CONSERVE_FUEL = 1 << 12, // 0x00001000
            ENV_MAX_VELOCITY = 1 << 12, // 0x00001000
            ALIGN_TYPE = 1 << 13, // 0x00002000
            STICKY = 1 << 14, // 0x00004000
            CAN_LAND = 1 << 15, // 0x00008000
            DETONATES_ON_MISS = 1 << 16, // 0x00010000
            DETONATES_ON_PROXIMITY = 1 << 17, // 0x00020000
        }

        public PackFlag bitfield; // m_bitfield
        public float maxVelocity; // m_max_velocity
        public float minVelocity; // m_max_velocity
        public float maxForwardAccel; // m_max_forward_accel
        public float maxDecel; // m_max_decel
        public float maxLeftAccel; // m_max_left_accel
        public float maxRightAccel; // m_max_right_accel
        public float maxUpwardAccel; // m_max_upward_accel
        public float maxDownwardAccel; // m_max_downward_accel
        public float totalFuel; // m_total_fuel
        public bool usesFuel; // m_uses_fuel
        public bool reportIfNoFuel; // m_report_if_no_fuel
        public float missileLifespan; // m_missile_lifespan
        public bool missileHasLifespan; // m_missile_has_lifespan
        public float drag; // m_drag
        public bool affectedByGravity; // m_affected_by_gravity
        public bool affectedByWind; // m_affected_by_wind
        public uint groundFollowType; // m_ground_follow_type
        public float heightAboveGround; // m_height_above_ground
        public uint tightTurnMethod; // m_tight_turn_method
        public bool conserveFuel; // m_conserve_fuel
        public bool considerRemainingTime; // m_consider_remaining_time
        public float minCatchupSpeed; // m_min_catchup_speed
        public float maintainDistance; // m_maintain_distance
        public bool useMaintDist; // m_use_maint_dist
        public float reactionTimeCheck; // m_reaction_time_check
        public float minReactionTime; // m_min_reaction_time
        public bool correctForTargetVelocity; // m_correct_for_target_velocity
        public bool useEnvMaxVelocities; // m_use_env_max_velocities
        public bool alignType; // m_align_type
        public float spinRate; // m_spin_rate
        public bool sticksUponCollide; // m_sticks_upon_collide
        public bool canLand; // m_can_land
        public bool detonatesOnMiss; // m_detonates_on_miss
        public float detonationProximity; // m_detonation_proximity
        public float detonationChance; // m_detonation_chance

        public MissileParameters(AC2Reader data) {
            bitfield = (PackFlag)data.ReadUInt32();
            if (bitfield.HasFlag(PackFlag.SPEED_LIMITS)) {
                maxVelocity = data.ReadSingle();
                minVelocity = data.ReadSingle();
            }
            if (bitfield.HasFlag(PackFlag.MAX_ACCEL)) {
                maxForwardAccel = data.ReadSingle();
                maxDecel = data.ReadSingle();
                maxLeftAccel = data.ReadSingle();
                maxRightAccel = data.ReadSingle();
                maxUpwardAccel = data.ReadSingle();
                maxDownwardAccel = data.ReadSingle();
            }
            if (bitfield.HasFlag(PackFlag.FUEL)) {
                totalFuel = data.ReadSingle();
                usesFuel = data.ReadBoolean();
                reportIfNoFuel = data.ReadBoolean();
            }
            if (bitfield.HasFlag(PackFlag.LIFESPAN)) {
                missileLifespan = data.ReadSingle();
                missileHasLifespan = data.ReadBoolean();
            }
            if (bitfield.HasFlag(PackFlag.DRAG)) {
                drag = data.ReadSingle();
            }
            if (bitfield.HasFlag(PackFlag.AFFECTED_GRAVITY)) {
                affectedByGravity = data.ReadBoolean();
            }
            if (bitfield.HasFlag(PackFlag.AFFECTED_WIND)) {
                affectedByWind = data.ReadBoolean();
            }
            if (bitfield.HasFlag(PackFlag.FOLLOW_GROUND)) {
                groundFollowType = data.ReadUInt32();
                heightAboveGround = data.ReadSingle();
            }
            if (bitfield.HasFlag(PackFlag.CONSERVE_FUEL)) {
                conserveFuel = data.ReadBoolean();
                considerRemainingTime = data.ReadBoolean();
                minCatchupSpeed = data.ReadSingle();
            }
            if (bitfield.HasFlag(PackFlag.MAINT_DIST)) {
                maintainDistance = data.ReadSingle();
                useMaintDist = data.ReadBoolean();
            }
            if (bitfield.HasFlag(PackFlag.MISSILE_REACTION)) {
                reactionTimeCheck = data.ReadSingle();
                minReactionTime = data.ReadSingle();
                correctForTargetVelocity = data.ReadBoolean();
            }
            if (bitfield.HasFlag(PackFlag.TURN_METHOD)) {
                tightTurnMethod = data.ReadUInt32();
            }
            if (bitfield.HasFlag(PackFlag.ENV_MAX_VELOCITY)) {
                useEnvMaxVelocities = data.ReadBoolean();
            }
            if (bitfield.HasFlag(PackFlag.ALIGN_TYPE)) {
                alignType = data.ReadBoolean();
                spinRate = data.ReadSingle();
            }
            if (bitfield.HasFlag(PackFlag.STICKY)) {
                sticksUponCollide = data.ReadBoolean();
            }
            if (bitfield.HasFlag(PackFlag.CAN_LAND)) {
                canLand = data.ReadBoolean();
            }
            if (bitfield.HasFlag(PackFlag.DETONATES_ON_MISS)) {
                detonatesOnMiss = data.ReadBoolean();
            }
            if (bitfield.HasFlag(PackFlag.DETONATES_ON_PROXIMITY)) {
                detonationProximity = data.ReadSingle();
                detonationChance = data.ReadSingle();
            }
        }
    }
}
