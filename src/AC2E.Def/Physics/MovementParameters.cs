using System;
using System.Collections.Generic;
using System.IO;

namespace AC2E.Def {

    public class MovementParameters : IPackage {

        public NativeType nativeType => NativeType.MOVEMENTPARAMETERS;

        // Class nested in MovementParameters
        [Flags]
        public enum PackFlag : uint {
            NONE = 0,
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
        public float desiredVelocity; // m_desired_velocity
        public float failDistance; // m_fail_distance
        public float desiredDistance; // m_desired_distance
        public float maxSuccessDistance; // m_max_success_distance
        public float minSuccessDistance; // m_min_success_distance
        public InstanceId targetId; // m_target_object_id
        public Position targetPosition; // m_target_position
        public List<Position> positionList; // m_position_list
        public Vector offsetVector; // m_offset_vector
        public uint offsetId; // m_offset_id
        public uint contextId; // m_context_id

        public MovementParameters() {

        }

        public MovementParameters(BinaryReader data) {
            packFlags = (PackFlag)data.ReadUInt32();
            if (packFlags.HasFlag(PackFlag.MOVE) || packFlags.HasFlag(PackFlag.AWAY)) {
                desiredVelocity = data.ReadSingle();
                failDistance = data.ReadSingle();
                desiredDistance = data.ReadSingle();
                if (packFlags.HasFlag(PackFlag.MOVE)) {
                    maxSuccessDistance = data.ReadSingle();
                }
                if (packFlags.HasFlag(PackFlag.AWAY)) {
                    minSuccessDistance = data.ReadSingle();
                }
            }
            if (packFlags.HasFlag(PackFlag.OBJECT)) {
                targetId = data.ReadInstanceId();
            } else {
                targetPosition = new Position(data);
            }
            if (packFlags.HasFlag(PackFlag.NONZERO_OFFSET_VECTOR)) {
                offsetVector = data.ReadVector();
            }
            if (packFlags.HasFlag(PackFlag.NONZERO_OFFSET_ID)) {
                offsetId = data.ReadUInt32();
            }
            positionList = data.ReadList(() => new Position(data));
            contextId = data.ReadUInt32();
        }

        public void write(BinaryWriter data) {
            data.Write((uint)packFlags);
            if (packFlags.HasFlag(PackFlag.MOVE) || packFlags.HasFlag(PackFlag.AWAY)) {
                data.Write(desiredVelocity);
                data.Write(failDistance);
                data.Write(desiredDistance);
                if (packFlags.HasFlag(PackFlag.MOVE)) {
                    data.Write(maxSuccessDistance);
                }
                if (packFlags.HasFlag(PackFlag.AWAY)) {
                    data.Write(minSuccessDistance);
                }
            }
            if (packFlags.HasFlag(PackFlag.OBJECT)) {
                data.Write(targetId);
            } else {
                targetPosition.write(data);
            }
            if (packFlags.HasFlag(PackFlag.NONZERO_OFFSET_VECTOR)) {
                data.Write(offsetVector);
            }
            if (packFlags.HasFlag(PackFlag.NONZERO_OFFSET_ID)) {
                data.Write(offsetId);
            }
            data.Write(positionList, v => v.write(data));
            data.Write(contextId);
        }

        public void write(BinaryWriter data, PackageRegistry registry) {
            write(data);
        }
    }
}
