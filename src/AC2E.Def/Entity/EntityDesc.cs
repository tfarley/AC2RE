using System;
using System.Text;

namespace AC2E.Def {

    public class EntityDesc : IPackage {

        public NativeType nativeType => NativeType.ENTITYDESC;

        // Enum EntityDescPack::Flag
        [Flags]
        public enum PackFlag : uint {
            NONE = 0,
            DATABASE = 1 << 0, // 0x00000001
            TYPE = 1 << 1, // 0x00000002
            RUNTIMEID = 1 << 2, // 0x00000004
            DATAID = 1 << 3, // 0x00000008
            OFFSET = 1 << 4, // 0x00000010
            SCALE = 1 << 5, // 0x00000020
            VERSION = 1 << 6, // 0x00000040
            PROPERTIES = 1 << 7, // 0x00000080
            WBNAME = 1 << 8, // 0x00000100
        }

        public PackFlag packFlags;
        public DataId did; // m_DID
        public EntityType type; // m_type
        public InstanceId runtimeId; // m_runtimeID
        public DataId dataId; // m_dataID
        public PackageId packageId; // m_pkgID
        public Matrix4 offset; // m_offset
        public Vector scale; // m_scale
        public PropertyCollection properties; // m_properties
        public uint version; // m_version
        public InstanceId wbId; // m_wbID
        public string wbName; // m_strWbName
        public string comments; // m_strComments
        public bool inside; // m_inside

        public EntityDesc() {

        }

        public EntityDesc(AC2Reader data) {
            packFlags = (PackFlag)data.ReadUInt32();
            if (packFlags.HasFlag(PackFlag.DATABASE)) {
                did = data.ReadDataId();
            }
            if (packFlags.HasFlag(PackFlag.TYPE)) {
                type = (EntityType)data.ReadUInt32();
            }
            if (packFlags.HasFlag(PackFlag.RUNTIMEID)) {
                runtimeId = data.ReadInstanceId();
            }
            if (packFlags.HasFlag(PackFlag.DATAID)) {
                dataId = data.ReadDataId();
            }
            if (packFlags.HasFlag(PackFlag.OFFSET)) {
                offset = new Matrix4(data);
            }
            if (packFlags.HasFlag(PackFlag.SCALE)) {
                scale = data.ReadVector();
            }
            if (packFlags.HasFlag(PackFlag.PROPERTIES)) {
                properties = new PropertyCollection(data);
            }
            if (packFlags.HasFlag(PackFlag.VERSION)) {
                version = data.ReadUInt32();
            }
            if (packFlags.HasFlag(PackFlag.WBNAME)) {
                wbName = data.ReadString();
            }
        }

        public void write(AC2Writer data) {
            data.Write((uint)packFlags);
            if (packFlags.HasFlag(PackFlag.DATABASE)) {
                data.Write(did);
            }
            if (packFlags.HasFlag(PackFlag.TYPE)) {
                data.Write((uint)type);
            }
            if (packFlags.HasFlag(PackFlag.RUNTIMEID)) {
                data.Write(runtimeId);
            }
            if (packFlags.HasFlag(PackFlag.DATAID)) {
                data.Write(dataId);
            }
            if (packFlags.HasFlag(PackFlag.OFFSET)) {
                offset.write(data);
            }
            if (packFlags.HasFlag(PackFlag.SCALE)) {
                data.Write(scale);
            }
            if (packFlags.HasFlag(PackFlag.PROPERTIES)) {
                properties.write(data);
            }
            if (packFlags.HasFlag(PackFlag.VERSION)) {
                data.Write(version);
            }
            if (packFlags.HasFlag(PackFlag.WBNAME)) {
                data.Write(wbName);
            }
        }
    }
}
