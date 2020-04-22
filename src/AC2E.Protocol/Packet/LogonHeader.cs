using AC2E.Def.Extensions;
using System.IO;

namespace AC2E.Protocol.Packet {

    public class LogonHeader {

        public enum NetAuthType : uint {
            UNDEF = 0x00000000,
            ACCOUNT_ONLY = 0x00000001,
            PASSWORD = 0x00000002,
            GLS_USERNAME_PASSWORD = 0x40000001,
            GLS_USERNAME_TICKET = 0x40000002,
            GLS_SERVICE_PROVIDER = 0x40000004,
            GUN_TICKET = 0x41000001,
        }

        public string clientVersion;
        public uint length;
        public NetAuthType netAuthType;
        public uint authFlags;
        public uint timestamp;
        public string account;

        public LogonHeader(BinaryReader data) {
            clientVersion = data.ReadEncryptedString();
            length = data.ReadUInt32();

            long authDataStart = data.BaseStream.Position;

            netAuthType = (NetAuthType)data.ReadUInt32();
            authFlags = data.ReadUInt32();
            timestamp = data.ReadUInt32();
            // TODO: Might need to check the auth type before reading this
            account = data.ReadEncryptedString();

            // TODO: Skip the rest - unknown data
            data.ReadBytes((int)length - (int)(data.BaseStream.Position - authDataStart));
        }
    }
}
