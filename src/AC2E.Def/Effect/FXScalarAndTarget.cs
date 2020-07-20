namespace AC2E.Def {

    public class FXScalarAndTarget {

        public float scalar; // m_scalar
        public InstanceId targetId; // m_target_id

        public FXScalarAndTarget(AC2Reader data) {
            scalar = data.ReadSingle();
            targetId = data.ReadInstanceId();
        }

        public void write(AC2Writer data) {
            data.Write(scalar);
            data.Write(targetId);
        }
    }
}
