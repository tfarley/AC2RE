using System.IO;

namespace AC2E.Def {

    public class FXScalarAndTarget {

        public float scalar; // m_scalar
        public InstanceId targetId; // m_target_id

        public FXScalarAndTarget(BinaryReader data) {
            scalar = data.ReadSingle();
            targetId = data.ReadInstanceId();
        }

        public void write(BinaryWriter data) {
            data.Write(scalar);
            data.Write(targetId);
        }
    }
}
