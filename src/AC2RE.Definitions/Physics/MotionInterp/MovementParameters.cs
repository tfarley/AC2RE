using System;
using System.Collections.Generic;
using System.Numerics;

namespace AC2RE.Definitions {

    public class MovementParameters : IPackage {

        public NativeType nativeType => NativeType.MOVEMENTPARAMETERS;

        // Class nested in MovementParameters
        [Flags]
        public enum PackFlag : uint {
            NONE = 0,
            ALL = uint.MaxValue,

            MOVE = 1 << 0, // m_move 0x00000001
            TURN = 1 << 1, // m_turn 0x00000002
            AWAY = 1 << 2, // m_away 0x00000004
            PERSISTENT = 1 << 3, // m_persistent 0x00000008
            PERSISTENT_FAIL_EXITS = 1 << 4, // m_persistent_fail_exits 0x00000010
            OBJECT = 1 << 5, // m_object 0x00000020
            USE_CYL = 1 << 6, // m_use_cyl 0x00000040
            USE_PATH_PLAN_MANAGER = 1 << 7, // m_use_path_plan_manager 0x00000080
            NONZERO_OFFSET_VECTOR = 1 << 8, // m_nonzero_offset_vector 0x00000100
            NONZERO_OFFSET_ID = 1 << 9, // m_nonzero_offset_id 0x00000200
            CALLBACK_WHEN_IN_RANGE = 1 << 10, // m_callback_when_in_range 0x00000400
        }

        public PackFlag packFlags; // bitfield
        public float desiredVel; // m_desired_velocity
        public float failDist; // m_fail_distance
        public float desiredDist; // m_desired_distance
        public float maxSuccessDist; // m_max_success_distance
        public float minSuccessDist; // m_min_success_distance
        public InstanceId targetId; // m_target_object_id
        public Position targetPos; // m_target_position
        public List<Position> posList; // m_position_list
        public Vector3 offset; // m_offset_vector
        public uint offsetId; // m_offset_id
        public uint contextId; // m_context_id

        public MovementParameters() {

        }

        public MovementParameters(AC2Reader data) {
            packFlags = (PackFlag)data.ReadUInt32();
            if (packFlags.HasFlag(PackFlag.MOVE) || packFlags.HasFlag(PackFlag.AWAY)) {
                desiredVel = data.ReadSingle();
                failDist = data.ReadSingle();
                desiredDist = data.ReadSingle();
                if (packFlags.HasFlag(PackFlag.MOVE)) {
                    maxSuccessDist = data.ReadSingle();
                }
                if (packFlags.HasFlag(PackFlag.AWAY)) {
                    minSuccessDist = data.ReadSingle();
                }
            }
            if (packFlags.HasFlag(PackFlag.OBJECT)) {
                targetId = data.ReadInstanceId();
            } else {
                targetPos = new(data);
            }
            if (packFlags.HasFlag(PackFlag.NONZERO_OFFSET_VECTOR)) {
                offset = data.ReadVector();
            }
            if (packFlags.HasFlag(PackFlag.NONZERO_OFFSET_ID)) {
                offsetId = data.ReadUInt32();
            }
            posList = data.ReadList(() => new Position(data));
            contextId = data.ReadUInt32();
        }

        public void write(AC2Writer data) {
            data.Write((uint)packFlags);
            if (packFlags.HasFlag(PackFlag.MOVE) || packFlags.HasFlag(PackFlag.AWAY)) {
                data.Write(desiredVel);
                data.Write(failDist);
                data.Write(desiredDist);
                if (packFlags.HasFlag(PackFlag.MOVE)) {
                    data.Write(maxSuccessDist);
                }
                if (packFlags.HasFlag(PackFlag.AWAY)) {
                    data.Write(minSuccessDist);
                }
            }
            if (packFlags.HasFlag(PackFlag.OBJECT)) {
                data.Write(targetId);
            } else {
                targetPos.write(data);
            }
            if (packFlags.HasFlag(PackFlag.NONZERO_OFFSET_VECTOR)) {
                data.Write(offset);
            }
            if (packFlags.HasFlag(PackFlag.NONZERO_OFFSET_ID)) {
                data.Write(offsetId);
            }
            data.Write(posList, v => v.write(data));
            data.Write(contextId);
        }
    }
}
