using System.IO;

namespace AC2E.Def {

    public class InstanceIdWithStamp {

        public InstanceId id; // m_id
        public ushort instanceStamp; // m_instance_stamp
        public ushort otherStamp; // m_other_stamp

        public InstanceIdWithStamp(BinaryReader data) {
            id = data.ReadInstanceId();
            instanceStamp = data.ReadUInt16();
            otherStamp = data.ReadUInt16();
        }
    }
}
