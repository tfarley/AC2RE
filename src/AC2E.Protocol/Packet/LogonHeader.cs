using AC2E.Def.Extensions;
using System.IO;

namespace AC2E.Protocol.Packet {

    public class LogonHeader {

        // Const *_NetAuth
        public enum NetAuthType : uint {
            UNDEF = 0,
            ACCOUNT_ONLY = 0x00000001,
            PASSWORD = 0x00000002,
            GLS_USERNAME_PASSWORD = 0x40000001,
            GLS_USERNAME_TICKET = 0x40000002,
            GLS_SERVICE_PROVIDER = 0x40000004,
            GUN_TICKET = 0x41000001,
        }

        public class NetAuthenticator {

            public NetAuthType netAuthType; // m_dwAuthType
            public uint authFlags; // m_dwAuthFlags
            public uint connectionSeq; // m_dwConnectionSequenceNumber
            public string account; // m_Account
            public string accountToLogonAs; // m_AccountToLogonAs
            public byte[] cryptoData; // m_CryptoData
            public byte[] extraData; // m_ExtraData

            public NetAuthenticator(BinaryReader data) {
                netAuthType = (NetAuthType)data.ReadUInt32();
                authFlags = data.ReadUInt32();
                connectionSeq = data.ReadUInt32();
                // TODO: Might need to check the auth type before reading this
                account = data.ReadEncryptedString();
                // TODO: Read rest of fields
            }
        }

        // CLogonHeader::HandshakeWireData
        public string clientVersion; // ClientVersion
        public uint length; // cbAuthData
        public NetAuthenticator netAuth;

        public LogonHeader(BinaryReader data) {
            clientVersion = data.ReadEncryptedString();
            length = data.ReadUInt32();

            long authDataStart = data.BaseStream.Position;

            netAuth = new NetAuthenticator(data);

            // TODO: Skip the rest - unknown data
            netAuth.extraData = data.ReadBytes((int)length - (int)(data.BaseStream.Position - authDataStart));
        }
    }
}
