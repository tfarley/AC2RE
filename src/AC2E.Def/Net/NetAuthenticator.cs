using System;

namespace AC2E.Def {

    public class NetAuthenticator {

        // Enum DEFAULT_AUTHFLAGS
        [Flags]
        public enum AuthFlag : uint {
            NONE = 0,
            ENABLECRYPTO = 1 << 0, // 0x00000001
            ADMINACCTOVERRIDE = 1 << 1, // 0x00000002
            EXTRADATA = 1 << 2, // 0x00000004
        }

        public AuthType authType; // m_dwAuthType
        public AuthFlag authFlags; // m_dwAuthFlags
        public uint connectionSeq; // m_dwConnectionSequenceNumber
        public string accountName; // m_Account
        public string accountToLogonAs; // m_AccountToLogonAs
        public byte[] cryptoData; // m_CryptoData
        public byte[] extraData; // m_ExtraData

        public NetAuthenticator(AC2Reader data) {
            authType = (AuthType)data.ReadUInt32();
            authFlags = (AuthFlag)data.ReadUInt32();
            connectionSeq = data.ReadUInt32();
            // TODO: Might need to check the auth type before reading this
            accountName = data.ReadString();
            // TODO: Read rest of fields
        }
    }
}
